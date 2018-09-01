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
        UnityDBContext DBContext = new UnityDBContext();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var art in DBContext.Articles)
            {
                Console.WriteLine($"{art.Title} {art.Description}");
            }
        }
    }
}
