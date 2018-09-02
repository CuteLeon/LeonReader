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
            foreach (var art in DBContext.Articles)
            {
                Console.WriteLine("——————————————");
                Console.WriteLine($"文章：{art.Title} ({art.ArticleID})");
                foreach (var cnt in art.Contents)
                {
                    Console.WriteLine($"\t{cnt.ImageDescription}");
                }
            }
        }
    }
}
