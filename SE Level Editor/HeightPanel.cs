using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Level_Editor
{
    class HeightPanel : Panel
    {
        private Workspace workspace = null;
        public HeightPanel() : base()
        {
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Fill;
            this.Controls.Add(panel);
        }
        
        public void LinkWorkspace(Workspace workspace)
        {
            this.workspace = workspace;
        }
        public void AddHeight(int[] height)
        {
            //Create all buttons in unsorted order
            Button[] buttonArray = new Button[height.Length];
            int[] heightArray = new int[height.Length];
            int index = 0;
            foreach(int h in height)
            {
                Button button = new Button();
                button.Tag = h;
                button.Text = "" + h;
                button.Click += new EventHandler(buttons_Click);
                buttonArray[index] = button;
                heightArray[index] = (int)button.Tag;
                index++;
            }

            //Sort in height order and add to screen
            Array.Sort(heightArray);
            foreach(int i in heightArray)
            {
                foreach (Button button in buttonArray)
                {
                    if ((int)button.Tag == i)
                    {
                        this.Controls[0].Controls.Add(button);
                        break;
                    }
                }
            }

            //Adds button to add new height
            Button addButton = new Button();
            addButton.Text = "Add height layer";
            addButton.Click += new EventHandler(addButton_Click);
            this.Controls[0].Controls.Add(addButton);
        }

        public void ClearButtons()
        {
            this.Controls[0].Controls.Clear();
        }

        private void buttons_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            EditorData.brushWorldHeight = (int)button.Tag;
            this.workspace.ClearTileOverlays();
            this.workspace.OverlayPermissions();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            HeightSelect select = new HeightSelect();
            select.ShowDialog();
            EditorData.AddHeightLayer(select.GetValue());
            this.ClearButtons();
            this.AddHeight(EditorData.currentLevelWorldHeights);
        }
    }
}
