namespace TransOS.Plugin.Helper.InformationTable.View
{
    partial class Form_ЗадатьЗначениеСвойства
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_ОК = new System.Windows.Forms.Button();
            this.button_Отмена = new System.Windows.Forms.Button();
            this.textBox_Значение = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_ОК
            // 
            this.button_ОК.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ОК.Location = new System.Drawing.Point(193, 42);
            this.button_ОК.Name = "button_ОК";
            this.button_ОК.Size = new System.Drawing.Size(75, 23);
            this.button_ОК.TabIndex = 0;
            this.button_ОК.Text = "ОК";
            this.button_ОК.UseVisualStyleBackColor = true;
            this.button_ОК.Click += new System.EventHandler(this.button_ОК_Click);
            // 
            // button_Отмена
            // 
            this.button_Отмена.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Отмена.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Отмена.Location = new System.Drawing.Point(274, 42);
            this.button_Отмена.Name = "button_Отмена";
            this.button_Отмена.Size = new System.Drawing.Size(75, 23);
            this.button_Отмена.TabIndex = 1;
            this.button_Отмена.Text = "Отмена";
            this.button_Отмена.UseVisualStyleBackColor = true;
            // 
            // textBox_Значение
            // 
            this.textBox_Значение.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Значение.Location = new System.Drawing.Point(12, 12);
            this.textBox_Значение.Name = "textBox_Значение";
            this.textBox_Значение.Size = new System.Drawing.Size(360, 20);
            this.textBox_Значение.TabIndex = 2;
            this.textBox_Значение.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Значение_KeyPress);
            // 
            // Form_ЗадатьЗначениеСвойства
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 67);
            this.Controls.Add(this.textBox_Значение);
            this.Controls.Add(this.button_Отмена);
            this.Controls.Add(this.button_ОК);
            this.MinimizeBox = false;
            this.Name = "Form_ЗадатьЗначениеСвойства";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_ЗадатьЗначениеСвойства";
            this.Shown += new System.EventHandler(this.Form_ЗадатьЗначениеСвойства_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ОК;
        private System.Windows.Forms.Button button_Отмена;
        private System.Windows.Forms.TextBox textBox_Значение;
    }
}