using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WzComparerR2.WzLib;

namespace WzComparerR2.Db2
{
    public partial class DB2Form : Form
    {
        public DB2Form()
        {
            InitializeComponent();
        }

        public Wz_Node baseWzNode { get; set; }
    }
}
