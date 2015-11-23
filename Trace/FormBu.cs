using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TraceCtrlLib.PanelExtend;

namespace Trace
{
    public partial class FormBu : Form
    {
        private TracePanelManage _tpm;
        public FormBu()
        {
            InitializeComponent();
            _tpm = new TracePanelManage(this);
        }

        
    }
}
