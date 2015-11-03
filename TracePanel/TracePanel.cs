using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraceCtrlLib
{
    public class TracePanel : Panel
    {
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
