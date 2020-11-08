using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Plugin.Helper.InformationTable.View
{
    public partial class Form_ЗадатьЗначениеСвойства : Form
    {
        readonly Таблица ИнфТаблица;
        readonly PropertyInfo ИнформацияОСвойстве;

        public Form_ЗадатьЗначениеСвойства(PropertyInfo ИнформацияОСвойстве, Таблица ИнфТаблица)
        {
            InitializeComponent();
            this.ИнфТаблица = ИнфТаблица;
            this.ИнформацияОСвойстве = ИнформацияОСвойстве;


            this.textBox_Значение.Text = Помощник.СвойствоКакСтрока(this.ИнформацияОСвойстве, this.ИнфТаблица);
        }

        private void button_ОК_Click(object sender, EventArgs e)
        {
            if (this.ИнформацияОСвойстве.PropertyType == typeof(float))
            {
                this.ИнформацияОСвойстве.SetValue(this.ИнфТаблица, Convert.ToSingle(this.textBox_Значение.Text), null);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                MessageBox.Show("Конвертирование данного типа не предусмотрено");
        }

        private void textBox_Значение_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.button_ОК_Click(sender, e);
        }

        private void Form_ЗадатьЗначениеСвойства_Shown(object sender, EventArgs e)
        {
            this.textBox_Значение.SelectAll();
            this.textBox_Значение.Focus();
        }
    }
}
