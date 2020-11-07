namespace TransOS.Plugin.WebBrowser
{
    partial class Form_Browser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Browser));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_ContentView = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox_Url = new System.Windows.Forms.ToolStripComboBox();
            this.MainMenuToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.pagePropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_ContentView);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 475);
            this.panel1.TabIndex = 0;
            // 
            // panel_ContentView
            // 
            this.panel_ContentView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_ContentView.AutoScroll = true;
            this.panel_ContentView.Location = new System.Drawing.Point(3, 30);
            this.panel_ContentView.Name = "panel_ContentView";
            this.panel_ContentView.Size = new System.Drawing.Size(710, 442);
            this.panel_ContentView.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripMenuItem,
            this.arrowToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.toolStripComboBox_Url,
            this.MainMenuToolStrip});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(716, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemAdded);
            this.menuStrip1.ItemRemoved += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemRemoved);
            this.menuStrip1.SizeChanged += new System.EventHandler(this.menuStrip1_SizeChanged);
            // 
            // backToolStripMenuItem
            // 
            this.backToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backToolStripMenuItem.Enabled = false;
            this.backToolStripMenuItem.Image = global::TransOS.Plugin.WebBrowser.Properties.Resources.back;
            this.backToolStripMenuItem.Name = "backToolStripMenuItem";
            this.backToolStripMenuItem.Size = new System.Drawing.Size(28, 23);
            this.backToolStripMenuItem.Text = "Back";
            // 
            // arrowToolStripMenuItem
            // 
            this.arrowToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.arrowToolStripMenuItem.Enabled = false;
            this.arrowToolStripMenuItem.Image = global::TransOS.Plugin.WebBrowser.Properties.Resources.arrow;
            this.arrowToolStripMenuItem.Name = "arrowToolStripMenuItem";
            this.arrowToolStripMenuItem.Size = new System.Drawing.Size(28, 23);
            this.arrowToolStripMenuItem.Text = "Arrow";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripMenuItem.Image")));
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(28, 23);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripComboBox_Url
            // 
            this.toolStripComboBox_Url.AutoSize = false;
            this.toolStripComboBox_Url.Name = "toolStripComboBox_Url";
            this.toolStripComboBox_Url.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox_Url.DropDown += new System.EventHandler(this.toolStripComboBox_Url_DropDown);
            this.toolStripComboBox_Url.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_Url_SelectedIndexChanged);
            this.toolStripComboBox_Url.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripComboBox_Url_KeyDown);
            // 
            // MainMenuToolStrip
            // 
            this.MainMenuToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MainMenuToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pagePropertiesToolStripMenuItem});
            this.MainMenuToolStrip.Image = global::TransOS.Plugin.WebBrowser.Properties.Resources.Menu;
            this.MainMenuToolStrip.Name = "MainMenuToolStrip";
            this.MainMenuToolStrip.Size = new System.Drawing.Size(28, 23);
            this.MainMenuToolStrip.Text = "MainMenu";
            // 
            // pagePropertiesToolStripMenuItem
            // 
            this.pagePropertiesToolStripMenuItem.Name = "pagePropertiesToolStripMenuItem";
            this.pagePropertiesToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.pagePropertiesToolStripMenuItem.Text = "Page properties";
            this.pagePropertiesToolStripMenuItem.Click += new System.EventHandler(this.pagePropertiesToolStripMenuItem_Click);
            // 
            // Form_Browser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 475);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_Browser";
            this.Text = "Form_Browser2";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arrowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_Url;
        private System.Windows.Forms.ToolStripMenuItem MainMenuToolStrip;
        private System.Windows.Forms.Panel panel_ContentView;
        private System.Windows.Forms.ToolStripMenuItem pagePropertiesToolStripMenuItem;
    }
}