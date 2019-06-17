using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_enumList : Form
    {
        private Test selectedIndex;

        public ex_enumList()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.enumList1.SelectedIndexChanged += EnumList1_SelectedIndexChanged;
            this.enumList1.Initialize(new Test());
        }

        private void EnumList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = enumList1.GetEnumIndex<Test>();
            label1.Text = selectedIndex.ToString();
        }

        public enum Test
        {
            NONE = 0,

            t1,
            t2,
            t3,
            t4,
            t5,
            t6,
        }
    }
}
