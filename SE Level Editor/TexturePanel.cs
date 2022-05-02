using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Level_Editor
{
    //Panel that displays a grid of availible tiles
    class TexturePanel : Panel
    {
        public const string FuncDLL = @"..\..\LevelEdBackend.dll";
        [DllImport(FuncDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int test();
        public TexturePanel() : base()
        {
            //Get tile
            

            var panelLoc = this.Location;
            this.AutoScroll = true;
            this.Size = new System.Drawing.Size(192, 768);
            for (int x = 0; x < this.Width; x+=64)
            {
                for (int y = 0; y < this.Height; y+=64)
                {
                    TextureSelect select = new TextureSelect();
                    select.Location = new System.Drawing.Point(panelLoc.X + x, panelLoc.Y + y);
                    select.Name = "textureButton"+x+y;
                    select.Size = new System.Drawing.Size(64, 64);
                    select.TabIndex = 0;
                    select.Text = ""+test();
                    select.Click += new System.EventHandler(this.Select_Click);
                    this.Controls.Add(select);
                }
            }
        }

        private void Select_Click(object sender, EventArgs e)
        {
            
        }
    }

    class TextureSelect : Button
    { 
        
    }

}
