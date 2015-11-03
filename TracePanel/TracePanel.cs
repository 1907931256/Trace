using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraceCtrlLib
{
    [Editor("System.Windows.Forms.Design.AnchorEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [Flags]
    public enum OpenAtStyles
    {
        Top = 1,
        Bottom = 2,
        Left = 4,
        Right = 8,
    }

    public class TracePanel : Panel
    {
        private OpenAtStyles OpenAt { get; set; }

        public TracePanel()
        {
            if(true)
            {
                            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawBorder();
            DrawText();
        }

        #region "Drawing"

        private void DrawBorder()
        {
            
        }

        private void DrawText()
        {
            
        }
        #endregion
    }
}
