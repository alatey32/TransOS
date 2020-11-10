using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Core.Bookmarks.View
{
    public partial class Form_AddBookmark : Form
    {
        readonly Context Os;

        private string[] CashedTabCommands;

        public BookmarkItem bookmarkItem
        {
            get => new BookmarkItem
            {
                Id = this.Os.Helper.GetRandomGuidString(),
                Text = this.comboBox_Text.Text,
                Command = this.textBox_Command.Text,
                Description = this.textBox_Description.Text
            };
            set
            {
                this.comboBox_Text.Text = value.Text;
                this.textBox_Command.Text = value.Command;
                this.textBox_Description.Text = value.Description;
            }
        }

        public Form_AddBookmark(Context Os)
        {
            InitializeComponent();

            this.Os = Os;
        }

        private void comboBox_Text_DropDown(object sender, EventArgs e)
        {
            this.comboBox_Text.Items.Clear();
            var AllTabs = this.Os.Gi.Gui.Windows.Tabs.All.ToArray();
            this.CashedTabCommands = AllTabs.Select(x => x.RestoreCommand).ToArray();
            this.comboBox_Text.Items.AddRange(AllTabs.Select(x => x.Header.Text).ToArray());
        }

        private void comboBox_Text_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBox_Text.SelectedIndex >=0
                && this.comboBox_Text.SelectedIndex < this.CashedTabCommands.Length)
            {
                this.textBox_Command.Text = this.CashedTabCommands[this.comboBox_Text.SelectedIndex];
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
