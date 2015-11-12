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
            foreach (Control child in container.Controls.Cast<Control>().Where(child => child.GetType() == typeof(TracePanel)))
                _tracePanelColl.Add((TracePanel)child);
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
                        int anchorLeft = (int) (anchor.Left + anchor.Width*anchor.OpenStart);
                        int anchorRight = (int) (anchor.Left + anchor.Width * (anchor.OpenStart+anchor.OpenRatio));
                        int childLeft = tpChild.Left + (int) (tpChild.Width*tpChild.OpenStart);
                        int childRight = tpChild.Left + (int) (tpChild.Width*(tpChild.OpenStart + tpChild.OpenRatio));
                        return anchor.Bottom == tpChild.Top
                                && (anchor.OpenAt & AnchorStyles.Bottom) != AnchorStyles.None
                                &&(anchorLeft.IsBetween(childLeft, childRight, true, true) || anchorRight.IsBetween(childLeft, childRight, true, true));
                    }))
                        tpChild.AnchorTop.Add(anchor);
                }
                //anchor bottom, to see those top is equal to 
                if ((tpChild.OpenAt & AnchorStyles.Bottom) != AnchorStyles.None)
                {
                    foreach (TracePanel anchor in _tracePanelColl.Where(anchor =>
                    {
                        int anchorLeft = (int)(anchor.Left + anchor.Width * anchor.OpenStart);
                        int anchorRight = (int)(anchor.Left + anchor.Width * (anchor.OpenStart + anchor.OpenRatio));
                        int childLeft = tpChild.Left + (int)(tpChild.Width * tpChild.OpenStart);
                        int childRight = tpChild.Left + (int)(tpChild.Width * (tpChild.OpenStart + tpChild.OpenRatio));
                        return anchor.Top == tpChild.Bottom
                               && (anchor.OpenAt & AnchorStyles.Top) != AnchorStyles.None
                               && (anchorLeft.IsBetween(childLeft, childRight, true, true) || anchorRight.IsBetween(childLeft, childRight, true, true));
                    }))
                        tpChild.AnchorBottom.Add(anchor);
                }
                //anchor left, to see those right is equal to 
                if ((tpChild.OpenAt & AnchorStyles.Left) != AnchorStyles.None)
                {
                    foreach (TracePanel anchor in _tracePanelColl.Where(anchor =>
                    {
                        int anchorTop = (int)(anchor.Top + anchor.Height * anchor.OpenStart);
                        int anchorBottom = (int)(anchor.Top + anchor.Height * (anchor.OpenStart + anchor.OpenRatio));
                        int childTop = tpChild.Top + (int)(tpChild.Height * tpChild.OpenStart);
                        int childBottom = tpChild.Top + (int)(tpChild.Height * (tpChild.OpenStart + tpChild.OpenRatio));
                        return anchor.Right == tpChild.Left
                               && (anchor.OpenAt & AnchorStyles.Right) != AnchorStyles.None
                               && (anchorTop.IsBetween(childTop, childBottom, true, true) || anchorBottom.IsBetween(childTop, childBottom, true, true));
                    }))
                        tpChild.AnchorLeft.Add(anchor);
                }
                //anchor right, to see those left is equal to 
                if ((tpChild.OpenAt & AnchorStyles.Right) != AnchorStyles.None)
                {
                    foreach (TracePanel anchor in _tracePanelColl.Where(anchor =>
                    {
                        int anchorTop = (int)(anchor.Top + anchor.Height * anchor.OpenStart);
                        int anchorBottom = (int)(anchor.Top + anchor.Height * (anchor.OpenStart + anchor.OpenRatio));
                        int childTop = tpChild.Top + (int)(tpChild.Height * tpChild.OpenStart);
                        int childBottom = tpChild.Top + (int)(tpChild.Height * (tpChild.OpenStart + tpChild.OpenRatio));
                        return anchor.Left == tpChild.Right
                               && (anchor.OpenAt & AnchorStyles.Left) != AnchorStyles.None
                               && (anchorTop.IsBetween(childTop, childBottom, true, true) || anchorBottom.IsBetween(childTop, childBottom, true, true));
                    }))
                        tpChild.AnchorRight.Add(anchor);
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
