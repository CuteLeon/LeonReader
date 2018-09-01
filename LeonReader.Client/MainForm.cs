using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LeonReader.Model;

namespace LeonReader.Client
{
    public partial class MainForm : Form
    {
        UnityDBContext DBContext;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DBContext = new UnityDBContext();
            MessageBox.Show(string.Join("\n", DBContext.Articles.Select(a=>a.ArticleID)));
        }
    }
}
