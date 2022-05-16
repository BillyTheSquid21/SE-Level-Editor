using System;
using System.Windows.Forms;

namespace Level_Editor
{
    class PermissionSelect : Form
    {
        private Label label1;
        private ComboBox comboBox1;

        public PermissionSelect() : base()
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
            "CLEAR",
            "WALL",
            "STAIRS_NORTH",
            "STAIRS_SOUTH",
            "STAIRS_EAST",
            "STAIRS_WEST",
            "WATER",
            "LEDGE_NORTH",
            "LEDGE_SOUTH",
            "LEDGE_EAST",
            "LEDGE_WEST",
            "LEVEL_BRIDGE"});
            this.comboBox1.Location = new System.Drawing.Point(13, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(163, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Permission";
            // 
            // PermissionSelect
            // 
            this.ClientSize = new System.Drawing.Size(244, 45);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "PermissionSelect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            string permission = box.GetItemText(box.SelectedItem);
            if (permission == "CLEAR")
            {
                EditorData.brushPermission = 0;
            }
            else if (permission == "WALL")
            {
                EditorData.brushPermission = 1;
            }
            else if (permission == "STAIRS_NORTH")
            {
                EditorData.brushPermission = 2;
            }
            else if (permission == "STAIRS_SOUTH")
            {
                EditorData.brushPermission = 3;
            }
            else if (permission == "STAIRS_EAST")
            {
                EditorData.brushPermission = 4;
            }
            else if (permission == "STAIRS_WEST")
            {
                EditorData.brushPermission = 5;
            }
            else if (permission == "WATER")
            {
                EditorData.brushPermission = 6;
            }
            else if (permission == "LEDGE_NORTH")
            {
                EditorData.brushPermission = 7;
            }
            else if (permission == "LEDGE_SOUTH")
            {
                EditorData.brushPermission = 8;
            }
            else if (permission == "LEDGE_EAST")
            {
                EditorData.brushPermission = 9;
            }
            else if (permission == "LEDGE_WEST")
            {
                EditorData.brushPermission = 10;
            }
            else if (permission == "LEVEL_BRIDGE")
            {
                EditorData.brushPermission = 11;
            }

            this.Close();
        }
    }
}
