using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Level_Editor
{
    public class CreateLevelForm : Form
    {
        private Label label1;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private Label label3;
        private NumericUpDown numericUpDown3;
        private Label label4;
        private NumericUpDown numericUpDown4;
        private Label label5;
        private NumericUpDown numericUpDown5;
        private Label label6;
        private Button button1;
        private Label label2;
        private NumericUpDown numericUpDown6;
        private Label label7;
        private string levelsPath = "";

        public CreateLevelForm(string path) : base()
        {
            this.InitializeComponent();
            this.levelsPath = path;
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create level:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(13, 30);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Level ID";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(13, 57);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Level Width";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(13, 84);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Level Height";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown4.Location = new System.Drawing.Point(13, 111);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            -294967296,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            -294967296,
            0,
            0,
            -2147483648});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown4.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(139, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Level Origin X";
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown5.Location = new System.Drawing.Point(13, 138);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            -294967296,
            0,
            0,
            0});
            this.numericUpDown5.Minimum = new decimal(new int[] {
            -294967296,
            0,
            0,
            -2147483648});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown5.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(139, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Level Origin Y";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(13, 165);
            this.numericUpDown6.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown6.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(140, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Terrain Height";
            // 
            // CreateLevelForm
            // 
            this.ClientSize = new System.Drawing.Size(220, 219);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDown6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericUpDown5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Name = "CreateLevelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.levelValidityCheck())
            {
                this.Close();
                return;
            }

            //If valid
            LevelEditorCommands.CreateLevel((uint)numericUpDown2.Value, (uint)numericUpDown3.Value, (uint)numericUpDown1.Value,
                (int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value, this.levelsPath);
            this.Close();
        }

        private bool levelValidityCheck()
        {
            //Check dimensions
            if (this.numericUpDown2.Value < 3 || this.numericUpDown3.Value < 3)
            {
                LevelEditorCommands.ErrorMessage("Level Dimensions too small!");
                return false;
            }
            //Check level ID does not exist
            string[] fileNames = Directory.GetFiles(this.levelsPath);
            const string levelStart = "level";
            foreach (string file in fileNames)
            {
                if (file.Contains(levelStart+this.numericUpDown1.Value))
                {
                    LevelEditorCommands.ErrorMessage("Level already exists!");
                    return false;
                }
            }
            return true;
        }
    }
    public class SelectFileTypeForm : Form
    {
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Button button1;
        private RadioButton radioButton1;
        private string currentPath = "";

        public SelectFileTypeForm(string path) : base()
        {
            this.Width = 100;
            this.Height = 100;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.currentPath = path;
            InitializeComponent();
        }

        public void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            this.button1.Tag = button.Tag;
        }

        private void InitializeComponent()
        {
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 13);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Create Level";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Tag = FileType.LEVEL;
            this.radioButton1.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(13, 37);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(86, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Create Script";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Tag = FileType.SCRIPT;
            this.radioButton2.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(13, 61);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(81, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Placeholder";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Tag = FileType.NULL;
            this.radioButton3.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(13, 85);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(81, 17);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Delete folder";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Tag = FileType.DELETE;
            this.radioButton4.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Ok";
            this.button1.Tag = FileType.LEVEL;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SelectFileTypeForm
            // 
            this.ClientSize = new System.Drawing.Size(120, 135);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Name = "SelectFileTypeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            //check what selected
            FileType type = (FileType)button.Tag;
            switch (type)
            {
                case FileType.LEVEL:
                    CreateLevelForm form = new CreateLevelForm(this.currentPath);
                    form.ShowDialog();
                    break;
                case FileType.SCRIPT:
                    break;
                case FileType.DELETE:
                    bool delete = LevelEditorCommands.ConfirmMessage("Delete selected folder?");
                    if (delete)
                    {
                        //TODO - get deleting working
                        //Directory.Delete(currentPath, true);
                    }
                    break;
                default:
                    break;
            }
            this.Close();
        }
    }
}
