using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Level_Editor
{
    static class LevelEditorCommands
    {
        public static void GenerateProject(string name, string rootDir)
        {
            //Create Project dir
            string projDir = rootDir + "/" + name;
            Directory.CreateDirectory(projDir);
            projDir += "/";

            //Create resource directories
            Directory.CreateDirectory(projDir + "fonts");
            Directory.CreateDirectory(projDir + "level");
            Directory.CreateDirectory(projDir + "model");
            Directory.CreateDirectory(projDir + "scripts");
            Directory.CreateDirectory(projDir + "shaders");
            Directory.CreateDirectory(projDir + "text");
            Directory.CreateDirectory(projDir + "textures");

            //Copy files
            new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(Directory.GetCurrentDirectory()+"/resources", projDir);
        }

        public static string OpenDirectoryDialog()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Directory.GetCurrentDirectory();
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
            }
            return null;
        }

        public static void ErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ConfirmMessage(string message)
        {
            DialogResult result = MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        public static void OpenCreateFileDialog(string path)
        {
            SelectFileTypeForm prompt = new SelectFileTypeForm(path);
            prompt.ShowDialog();
        }

        public static void CreateLevel(uint width, uint height, uint id, int originX, int originZ, string path)
        {
            LevelSerialize.CreateLevel(width, height, id, originX, originZ, path);
            LevelSerialize.CreateObjectFile(id, path);
        }
    }
}
