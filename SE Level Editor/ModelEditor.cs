using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Level_Editor
{
    class ModelEditor : Form
    {
        Thread thread;
        public ModelEditor()
        {
            this.InitializeComponent();
            thread = new Thread(ModelWindow.RunWindow);
            thread.Name = "3DWindow";
            thread.Priority = ThreadPriority.Normal;
            thread.Start();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ModelEditor
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ModelEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
