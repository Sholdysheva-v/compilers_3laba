using System;
using System.Windows.Forms;

namespace Лабораторная_1_компиляторы
{
    public partial class Form4 : Form
    {
        public Form4(string title, string content)
        {
            InitializeComponent();
            this.Text = title;
            richTextBox1.Text = content;
        }
    }
}