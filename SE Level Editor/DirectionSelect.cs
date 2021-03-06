using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Converters;


namespace Level_Editor
{
    class DirectionSelect : Form
    {
        private ComboBox comboBox1;
        private Label label1;

        public DirectionSelect() : base()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Null",
            "North",
            "South",
            "East",
            "West",
            "North-East",
            "North-East - Wrapped",
            "North-West",
            "North-West - Wrapped",
            "South-East",
            "South-East - Wrapped",
            "South-West",
            "South-West - Wrapped"});
            this.comboBox1.Location = new System.Drawing.Point(13, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Direction";
            // 
            // DirectionSelect
            // 
            this.ClientSize = new System.Drawing.Size(198, 46);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "DirectionSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            string direction = box.GetItemText(box.SelectedItem);
            EditorData.brushDirection = Entity.GetDirection(direction);
            this.Close();
        }
    }
}
