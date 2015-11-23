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
        private readonly TracePanelManage _tpm;
        public Form1()
        {
            InitializeComponent();
            _tpm = new TracePanelManage(panel2);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            _tpm.TraceFromTo("Carey1", tracePanel9, tracePanel10);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            _tpm.TraceFromTo("Carey1", tracePanel10, tracePanel9);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            _tpm.TraceFromTo("Carey1", tracePanel10, tracePanel11);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            _tpm.TraceFromTo("Carey1", tracePanel11, tracePanel10);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _tpm.TraceFromTo("Carey1", tracePanel10, tracePanel12);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            _tpm.TraceFromTo("Carey1", tracePanel12, tracePanel10);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _tpm.TraceFromTo("Carey1", tracePanel12, tracePanel13);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            _tpm.TraceFromTo("Carey1", tracePanel13, tracePanel12);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
