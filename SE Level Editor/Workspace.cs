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
    //Workspace for building levels
    class Workspace : Panel
    {
        public Workspace() : base()
        {
            this.AutoScroll = true;
        }

        //Changes brush mode within workspace panel
        public void SetBrushMode(BrushMode mode)
        {
            this.ClearTileOverlays();
            EditorData.brushMode = mode;
            switch (EditorData.brushMode)
            {
                case BrushMode.DIRECTION:
                    OverlayDirections();
                    break;
                case BrushMode.HEIGHT:
                    OverlayHeights();
                    break;
                case BrushMode.PERMISSION:
                    OverlayPermissions();
                    break;
                default:
                    break;
            }
        }

        public void ClearTileOverlays()
        {
            foreach(Control control in this.Controls)
            {
                control.Controls.Clear();
            }
        }

        public void OverlayDirections()
        {
            int index = 0;
            for (int y = 0; y < EditorData.currentLevelHeight; y++)
            {
                for (int x = 0; x < EditorData.currentLevelWidth; x++)
                {
                    int direction = EditorData.currentLevelDirections[x, y];
                    Label label = new Label();
                    label.AutoSize = true;
                    label.Text = ""+direction;
                    label.Parent = this.Controls[index];
                    label.Location = this.Controls[index].PointToClient(label.PointToScreen(Point.Empty));
                    this.Controls[index].Controls.Add(label);
                    index++;
                }
            }
        }
        public void OverlayPermissions()
        {
            int index = 0;
            for (int y = 0; y < EditorData.currentLevelHeight; y++)
            {
                for (int x = 0; x < EditorData.currentLevelWidth; x++)
                {
                    int permission = EditorData.currentLevelPermissions[EditorData.brushWorldHeight][x, y];
                    Label label = new Label();
                    label.AutoSize = true;
                    label.Text = "" + permission;
                    label.Parent = this.Controls[index];
                    label.Location = this.Controls[index].PointToClient(label.PointToScreen(Point.Empty));
                    this.Controls[index].Controls.Add(label);
                    index++;
                }
            }
        }
        public void OverlayHeights()
        {
            int index = 0;
            for (int y = 0; y < EditorData.currentLevelHeight; y++)
            {
                for (int x = 0; x < EditorData.currentLevelWidth; x++)
                {
                    int direction = EditorData.currentLevelHeights[x, y];
                    Label label = new Label();
                    label.AutoSize = true;
                    label.Text = "" + direction;
                    label.Parent = this.Controls[index];
                    label.Location = this.Controls[index].PointToClient(label.PointToScreen(Point.Empty));
                    this.Controls[index].Controls.Add(label);
                    index++;
                }
            }
        }
        public Image RotateImageWithDirection(Image image, int direction)
        {
            if (direction == 2 || direction == 7)
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if (direction == 5 || direction == 4)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            else if (direction == 6 || direction == 8)
            {
                image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            return image;
        }
        public void LoadLevel(string path)
        {
            this.Controls.Clear();
            //Get data and store
            LevelSerialize.LoadLevelFromFile(path);

            //Get max x and y texture size in loaded level to ensure that enough dimensions exist
            uint maxTexX = 0; uint maxTexY = 0;
            for (int y = 0; y < EditorData.currentLevelHeight; y++)
            {
                for (int x = 0; x < EditorData.currentLevelWidth; x++)
                {
                    Tile texTile = EditorData.currentLevelTextures[x, y];
                    if (texTile.x > maxTexX)
                    {
                        maxTexX = texTile.x;
                    }
                    if (texTile.y > maxTexY)
                    {
                        maxTexY = texTile.y;
                    }
                }
            }

            if (maxTexX > EditorData.currentTilesetImages.GetLength(0) 
                || maxTexY > EditorData.currentTilesetImages.GetLength(1))
            {
                LevelEditorCommands.ErrorMessage("Loaded tileset does not have sufficient dimensions to represent level!");
                return;
            }

            //Create controls
            for (int y = 0; y < EditorData.currentLevelHeight; y++)
            {
                for (int x = 0; x < EditorData.currentLevelWidth; x++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.ClientSize = new Size((int)Constants.TILE_SIZE, (int)Constants.TILE_SIZE);                    

                    if (EditorData.currentTilesetImages != null)
                    {
                        Tile texTile = EditorData.currentLevelTextures[x, y];
                        int direction = EditorData.currentLevelDirections[x, y];
                        if (direction != 0 && direction != 1 && direction != 3)
                        {
                            Image image = (Image)EditorData.currentTilesetImages[texTile.x, texTile.y].Clone();
                            pictureBox.Image = RotateImageWithDirection(image, direction);
                        }
                        else
                        {
                            pictureBox.Image = (Image)EditorData.currentTilesetImages[texTile.x, texTile.y].Clone();
                        }
                    }

                    Tile currentTile = new Tile();
                    currentTile.x = (uint)x; currentTile.y = (uint)y;
                    pictureBox.Location = new Point(x*(int)Constants.TILE_SIZE, y*(int)Constants.TILE_SIZE);
                    pictureBox.MouseClick += new MouseEventHandler(tile_Click);
                    pictureBox.Tag = currentTile;
                    this.Controls.Add(pictureBox);
                }
            }
        }

        private void tile_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox boxInstance = (PictureBox)sender;
                Tile currentTile;
                switch (EditorData.brushMode)
                {
                    case BrushMode.TEXTURE:
                        //Set image on editor
                        boxInstance.Image = (Image)EditorData.brushTextureImage.Clone();

                        //Saves texture change in editor data
                        currentTile = (Tile)boxInstance.Tag;
                        EditorData.currentLevelTextures[currentTile.x, currentTile.y] = EditorData.brushTextureTile;
                        break;
                    case BrushMode.HEIGHT:
                        //Sets height change in editor data
                        currentTile = (Tile)boxInstance.Tag;
                        EditorData.currentLevelHeights[currentTile.x, currentTile.y] = EditorData.brushHeight;
                        //Shows change
                        this.ClearTileOverlays();
                        this.OverlayHeights(); //TODO - do more efficiently
                        break;
                    case BrushMode.DIRECTION:
                        //Sets direction change in editor data
                        currentTile = (Tile)boxInstance.Tag;
                        EditorData.currentLevelDirections[currentTile.x, currentTile.y] = EditorData.brushDirection;
                        Tile texTile = EditorData.currentLevelTextures[currentTile.x, currentTile.y];
                        boxInstance.Image = (Image)EditorData.currentTilesetImages[texTile.x, texTile.y].Clone();
                        boxInstance.Image = RotateImageWithDirection(boxInstance.Image, EditorData.brushDirection);
                        //Shows change
                        this.ClearTileOverlays();
                        this.OverlayDirections(); //TODO - do more efficiently
                        break;
                    case BrushMode.PERMISSION:
                        //Sets height change in editor data
                        currentTile = (Tile)boxInstance.Tag;
                        EditorData.currentLevelPermissions[EditorData.brushWorldHeight][currentTile.x, currentTile.y] = EditorData.brushPermission;
                        //Shows change
                        this.ClearTileOverlays();
                        this.OverlayPermissions(); //TODO - do more efficiently
                        break;
                    default:
                        break;
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                switch (EditorData.brushMode)
                {
                    case BrushMode.HEIGHT:
                        HeightSelect numericSelect = new HeightSelect();
                        numericSelect.ShowDialog();
                        EditorData.brushHeight = numericSelect.GetValue();
                        break;
                    case BrushMode.DIRECTION:
                        DirectionSelect directionSelect = new DirectionSelect();
                        directionSelect.ShowDialog();
                        break;
                    case BrushMode.PERMISSION:
                        PermissionSelect permSelect = new PermissionSelect();
                        permSelect.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
