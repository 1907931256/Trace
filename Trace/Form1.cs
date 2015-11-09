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
    public partial class Form1 : Form
    {
        private TracePanelManage _tpm;
        public Form1()
        {
            InitializeComponent();
            _tpm = new TracePanelManage(panel1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //collect the anchor controls
            _tpm.ArrangeAnchorCtrls();
        }

        
    }
}
