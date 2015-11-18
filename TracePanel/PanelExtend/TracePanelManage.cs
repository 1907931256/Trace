using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraceCtrlLib.PanelExtend
{
    public class TracePanelManage
    {
        private readonly List<TracePanel> _tracePanelColl;
        private Control _container;
        public TracePanelManage(Control container)
        {
            _container = container;
            //_container.Paint += ContainerOnPaint;
            _tracePanelColl = new List<TracePanel>();
            Point offset = new Point();
            CollectTracePanel(container, offset,  ref _tracePanelColl);
        }

        private void CollectTracePanel(Control host, Point offset, ref List<TracePanel> tracePanels)
        {
            if (host == null)
                return;
            if (tracePanels == null)
                tracePanels = new List<TracePanel>();
            foreach (Control child in host.Controls.Cast<Control>().Where(child => child.GetType() == typeof (TracePanel)))
                AddTracePanel((TracePanel)child, offset);
            foreach (Control child in host.Controls.Cast<Control>().Where(child => child.GetType() != typeof (TracePanel)))
            {
                Point newOffset = new Point(offset.X,offset.Y);
                newOffset.Offset(child.Location);
                CollectTracePanel(child, newOffset, ref tracePanels);
            }
        }

        private void AddTracePanel(TracePanel traceAdd, Point offset)
        {
            if (null == _tracePanelColl)
                return;
            if (_tracePanelColl.Contains(traceAdd))
                return;
            traceAdd.Offset = offset;
            _tracePanelColl.Add(traceAdd);
        }

        private void ContainerOnPaint(object sender, PaintEventArgs paintEventArgs)
        {
            
        }

        public void ArrangeAnchorCtrls()
        {
            foreach (TracePanel tpChild in _tracePanelColl)
            {
                //anchor top, to see those bottom is equal to 
                if ((tpChild.OpenAt & AnchorStyles.Top) != AnchorStyles.None)
                {
                    foreach (TracePanel anchor in _tracePanelColl.Where(anchor =>
                    {
                        int anchorLeft = (int) (anchor.Left + anchor.Width*anchor.OpenStart)+anchor.Offset.X;
                        int anchorRight = (int) (anchor.Left + anchor.Width * (anchor.OpenStart+anchor.OpenRatio)) + anchor.Offset.X;
                        int childLeft = tpChild.Left + (int) (tpChild.Width*tpChild.OpenStart)+tpChild.Offset.X;
                        int childRight = tpChild.Left + (int) (tpChild.Width*(tpChild.OpenStart + tpChild.OpenRatio)) + tpChild.Offset.X;
                        return anchor.Bottom + anchor.Offset.Y == tpChild.Top + tpChild.Offset.Y
                                && (anchor.OpenAt & AnchorStyles.Bottom) != AnchorStyles.None
                                && GlobalMethod.HasIntersection(anchorLeft, anchorRight, childLeft, childRight);
                    }))
                        tpChild.AddAnchor(anchor,AnchorStyles.Top);
                }
                //anchor bottom, to see those top is equal to 
                if ((tpChild.OpenAt & AnchorStyles.Bottom) != AnchorStyles.None)
                {
                    foreach (TracePanel anchor in _tracePanelColl.Where(anchor =>
                    {
                        int anchorLeft = (int)(anchor.Left + anchor.Width * anchor.OpenStart) + anchor.Offset.X;
                        int anchorRight = (int)(anchor.Left + anchor.Width * (anchor.OpenStart + anchor.OpenRatio)) + anchor.Offset.X;
                        int childLeft = tpChild.Left + (int)(tpChild.Width * tpChild.OpenStart) + tpChild.Offset.X;
                        int childRight = tpChild.Left + (int)(tpChild.Width * (tpChild.OpenStart + tpChild.OpenRatio)) + tpChild.Offset.X;
                        return anchor.Top + anchor.Offset.Y == tpChild.Bottom + tpChild.Offset.Y
                               && (anchor.OpenAt & AnchorStyles.Top) != AnchorStyles.None
                               && GlobalMethod.HasIntersection(anchorLeft, anchorRight, childLeft, childRight);
                    }))
                        tpChild.AddAnchor(anchor, AnchorStyles.Bottom);
                }
                //anchor left, to see those right is equal to 
                if ((tpChild.OpenAt & AnchorStyles.Left) != AnchorStyles.None)
                {
                    foreach (TracePanel anchor in _tracePanelColl.Where(anchor =>
                    {
                        int anchorTop = (int)(anchor.Top + anchor.Height * anchor.OpenStart) + anchor.Offset.Y;
                        int anchorBottom = (int)(anchor.Top + anchor.Height * (anchor.OpenStart + anchor.OpenRatio)) + anchor.Offset.Y;
                        int childTop = tpChild.Top + (int)(tpChild.Height * tpChild.OpenStart) + tpChild.Offset.Y;
                        int childBottom = tpChild.Top + (int)(tpChild.Height * (tpChild.OpenStart + tpChild.OpenRatio)) + tpChild.Offset.Y;
                        return anchor.Right + anchor.Offset.X == tpChild.Left + tpChild.Offset.X
                               && (anchor.OpenAt & AnchorStyles.Right) != AnchorStyles.None
                               && GlobalMethod.HasIntersection(anchorTop, anchorBottom, childTop, childBottom);
                    }))
                        tpChild.AddAnchor(anchor, AnchorStyles.Left);
                }
                //anchor right, to see those left is equal to 
                if ((tpChild.OpenAt & AnchorStyles.Right) != AnchorStyles.None)
                {
                    foreach (TracePanel anchor in _tracePanelColl.Where(anchor =>
                    {
                        int anchorTop = (int)(anchor.Top + anchor.Height * anchor.OpenStart) + anchor.Offset.Y;
                        int anchorBottom = (int)(anchor.Top + anchor.Height * (anchor.OpenStart + anchor.OpenRatio)) + anchor.Offset.Y;
                        int childTop = tpChild.Top + (int)(tpChild.Height * tpChild.OpenStart) + tpChild.Offset.Y;
                        int childBottom = tpChild.Top + (int)(tpChild.Height * (tpChild.OpenStart + tpChild.OpenRatio)) + tpChild.Offset.Y;
                        return anchor.Left + anchor.Offset.X == tpChild.Right + tpChild.Offset.X
                               && (anchor.OpenAt & AnchorStyles.Left) != AnchorStyles.None
                               && GlobalMethod.HasIntersection(anchorTop, anchorBottom, childTop, childBottom);
                    }))
                        tpChild.AddAnchor(anchor, AnchorStyles.Right);
                }
            }
        }

        public bool TraceFromTo(TracePanel start, TracePanel dest)
        {
            if (null == start || dest == null)
                return false;

            List<TracePanel> traceNodes = new List<TracePanel>();
            TraceNodes(start, dest, ref traceNodes, new List<TracePanel>());
            if (traceNodes.Count <= 0 || !traceNodes.Contains(dest))
                return false;
            else
                DrawTrace(traceNodes);
            return true;
        }

        private void TraceNodes(TracePanel start, TracePanel dest, ref List<TracePanel> traceNodes, List<TracePanel> curTraceNodes)
        {
            if (curTraceNodes.Contains(start))//如果当前路径中已经包含该节点，则返回，防止类似A-B，陷入无限循环中
                return;
            curTraceNodes.Add(start);
            if (!traceNodes.IsNullOrEmpty() && traceNodes.Count <= curTraceNodes.Count)//如果当前路径长度大于记录路径，则直接返回
                return;
            if (start == dest) //如果已经到达最终节点
            {
                if (traceNodes.Count==0 || traceNodes.Count > curTraceNodes.Count)
                {
                    traceNodes.Clear();
                    traceNodes = new List<TracePanel>(curTraceNodes);
                }
                return;
            }
            else//遍历子节点
            {
                List<TracePanel> anchorTotal = new List<TracePanel>();
                anchorTotal.AddRange(start.AnchorTop);
                anchorTotal.AddRange(start.AnchorBottom);
                anchorTotal.AddRange(start.AnchorLeft);
                anchorTotal.AddRange(start.AnchorRight);

                foreach (TracePanel tpChild in anchorTotal)
                {
                    List<TracePanel> nexTracePanels = new List<TracePanel>(curTraceNodes);
                    TraceNodes(tpChild, dest, ref traceNodes, nexTracePanels);
                }
            }
        }

        private void DrawTrace(List<TracePanel> traceNodes)
        {
            for (int i = 0; i < traceNodes.Count; i++)
            {
                TracePanel tpPre = null;
                TracePanel tpNext = null;
                if (i > 0)
                    tpPre = traceNodes[i-1];
                if (i < traceNodes.Count - 1)
                    tpNext = traceNodes[i + 1];
                traceNodes[i].AddTraceChain(tpPre, tpNext);
                traceNodes[i].Refresh();
            }
        }
    }
}
