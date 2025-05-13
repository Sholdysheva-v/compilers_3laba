using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторная_1_компиляторы
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            richTextBox1.Text = content;
        }
        string content = @"Грамматика G[‹Def›] является автоматной.
Правила(1) – (18) для G[‹Def›] реализованы на графе(см.рисунок 1).
Сплошные стрелки на графе характеризуют синтаксически верный разбор; пунктирные символизируют состояние ошибки(ERROR);
        Состояние 11 символизирует успешное завершение разбора.";

    }
}
