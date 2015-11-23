using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceCtrlLib.PanelExtend
{
    public class TraceChain
    {
        public TracePanel PreviousCtrl { get; set; }
        public TracePanel NextCtrl { get; set; }
        public String Entity { get; set; }
    }
}
