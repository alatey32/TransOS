namespace TransOS.Plugin.WebBrowser.WebPageProperties
{
    partial class Form_Properties
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_Request = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_HttpResponse = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView_Certs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(479, 438);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(471, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "HTTP";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(465, 406);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_Request);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 201);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Request";
            // 
            // textBox_Request
            // 
            this.textBox_Request.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Request.Location = new System.Drawing.Point(3, 16);
            this.textBox_Request.Multiline = true;
            this.textBox_Request.Name = "textBox_Request";
            this.textBox_Request.ReadOnly = true;
            this.textBox_Request.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Request.Size = new System.Drawing.Size(459, 182);
            this.textBox_Request.TabIndex = 0;
            this.textBox_Request.WordWrap = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_HttpResponse);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(465, 201);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Response";
            // 
            // textBox_HttpResponse
            // 
            this.textBox_HttpResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_HttpResponse.Location = new System.Drawing.Point(3, 16);
            this.textBox_HttpResponse.Multiline = true;
            this.textBox_HttpResponse.Name = "textBox_HttpResponse";
            this.textBox_HttpResponse.ReadOnly = true;
            this.textBox_HttpResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_HttpResponse.Size = new System.Drawing.Size(459, 182);
            this.textBox_HttpResponse.TabIndex = 1;
            this.textBox_HttpResponse.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView_Certs);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(471, 412);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SSL/TLS cert";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView_Certs
            // 
            this.listView_Certs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_Certs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Certs.HideSelection = false;
            this.listView_Certs.Location = new System.Drawing.Point(3, 3);
            this.listView_Certs.Name = "listView_Certs";
            this.listView_Certs.Size = new System.Drawing.Size(465, 406);
            this.listView_Certs.TabIndex = 0;
            this.listView_Certs.UseCompatibleStateImageBehavior = false;
            this.listView_Certs.View = System.Windows.Forms.View.Details;
            this.listView_Certs.DoubleClick += new System.EventHandler(this.listView_Certs_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Subject";
            // 
            // Form_Properties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 438);
            this.Controls.Add(this.tabControl1);
            this.MinimizeBox = false;
            this.Name = "Form_Properties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Page properties";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_Request;
        private System.Windows.Forms.TextBox textBox_HttpResponse;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView_Certs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}