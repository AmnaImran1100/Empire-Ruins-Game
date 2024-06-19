using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmpireRuins
{
    public partial class NewLevelForm : Form
    {
        public NewLevelForm()
        {
            InitializeComponent();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            this.Close();
            Level2 form = new Level2();
            form.Show();
        }
    }
}
