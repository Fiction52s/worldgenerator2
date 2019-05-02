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

namespace worldgenerator2
{
    public partial class Form1 : Form
    {
        Dictionary<string, Image> previews;
        WorldPreview worldPreview;

        public Form1()
        {
            InitializeComponent();
            previews = new Dictionary<string, Image>();
            worldPreview = new WorldPreview();
            //treeView1.ContextMenuStrip = contextMenuStrip1;
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("Delete");
            //cm.MenuItems.Add("blah");
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            var item = treeView1.Nodes.Add("World " + (treeView1.Nodes.Count + 1));//String.Empty);
            treeView1.SelectedNode = item;
            item.BeginEdit();
            //item.ContextMenuStrip = contextMenuStrip1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if( treeView1.SelectedNode == null )
            {
                return;
            }

            string defaultStr = "";
            var node = treeView1.SelectedNode;
            if( node.Parent == null )//&& node.Parent.Parent == null )
            {
                defaultStr = "Sector " + (node.Nodes.Count + 1);
            }
            var item = treeView1.SelectedNode.Nodes.Add(defaultStr);
            treeView1.SelectedNode.Expand();
            treeView1.SelectedNode = item;
            item.BeginEdit();
            //item.ContextMenuStrip = contextMenuStrip1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "breakneck data files (*.kin)|*.kin";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                string[] lines = File.ReadAllLines(fileName);
                SaveFile sf = new SaveFile();
                sf.Load(lines);
                LoadFromSaveFile(sf);

               // try
               // {
               //     if ((myStream = openFileDialog1.OpenFile()) != null)
               //     {
               //         using (myStream)
               //         {

               //         }   
               //     }
               // }
               //catch( Exception ex )
               // {
               //     MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
               // }
            }
        }

        void LoadFromSaveFile( SaveFile sf )
        {
            treeView1.Nodes.Clear();
            for( int i = 0; i < sf.worlds.Count; ++i )
            {
                World w = sf.worlds[i];
                TreeNode worldNode = treeView1.Nodes.Add(w.name);//"World " + (i+1));
                for( int s = 0; s < w.sectors.Count; ++s )
                {
                    Sector sec = w.sectors[s];
                    TreeNode sectorNode = worldNode.Nodes.Add(sec.name);//"Sector " + (s + 1));
                    for( int le = 0; le < sec.levels.Count; ++le )
                    {
                        Level lev = sec.levels[le];
                        sectorNode.Nodes.Add(lev.name);
                    }
                }
            }
            treeView1.ExpandAll();
            
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if( e.Node.Parent != null && e.Node.Parent.Parent != null )
            //e.Node.BeginEdit();
            //if( !e.Node.IsExpanded )
            //{
            //    e.Node.Expand();
            //}
            //else
            //{
            //    e.Node.Collapse();
            //}
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if( e.Button == MouseButtons.Right )
            {
                treeView1.SelectedNode = e.Node;
                contextMenuStrip1.Show(this, new Point(e.X, e.Y + 20));
            }
        }

        void MoveNodeUp()
        {
            TreeNode node = treeView1.SelectedNode;
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index - 1, node);
                    treeView1.SelectedNode = node;
                }
            }
            else if (node.TreeView.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index > 0)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index - 1, node);
                    treeView1.SelectedNode = node;
                }
            }
        }

        void MoveNodeDown()
        {
            TreeNode node = treeView1.SelectedNode;
            TreeNode parent = node.Parent;
            int maxIndex = parent.Nodes.Count - 1;



            TreeView view = node.TreeView;

            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);

                if (index < maxIndex)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index + 1, node);
                    treeView1.SelectedNode = node;
                }
            }
            else if (node.TreeView.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index < maxIndex)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index + 1, node);
                    treeView1.SelectedNode = node;
                }
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            if( item.Text == "Edit" )
            {
                treeView1.SelectedNode.BeginEdit();
            }
            if (item.Text == "Delete")
            {
                treeView1.SelectedNode.Remove();
            }
            else if (item.Text == "Move Up")
            {
                MoveNodeUp();
            }
            else if (item.Text == "Move Down")
            {
                MoveNodeDown();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "breakneck data files (*.kin)|*.kin";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //string fileName = saveFileDialog1.FileName;

                SaveFile sf = new SaveFile();
                foreach (TreeNode tn in treeView1.Nodes)
                {
                    //worlds
                    World newWorld = new World();
                    sf.worlds.Add(newWorld);
                    newWorld.name = tn.Text;
                    foreach (TreeNode tnSector in tn.Nodes)
                    {
                        Sector newSector = new Sector();
                        newWorld.sectors.Add(newSector);
                        newSector.name = tnSector.Text;
                        foreach (TreeNode tnLevel in tnSector.Nodes)
                        {
                            Level newLevel = new Level();
                            newSector.levels.Add(newLevel);
                            newLevel.name = tnLevel.Text;
                        }
                    }
                }

                sf.Save(saveFileDialog1);
                //string[] lines = File.ReadAllLines(fileName);
                //SaveFile sf = new SaveFile();
                //sf.Load(lines);
                //LoadFromSaveFile(sf);
            }


            

            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TryDisplayPreview();
        }


        private void TryDisplayPreview()
        {
            var node = treeView1.SelectedNode;
            if (node.Parent != null && node.Parent.Parent != null)
            {
                string text = node.Text;
                pictureBox1.Image = GetPreviewImage(text);
            }
        }

        public Image GetPreviewImage( string text )
        {
            if (!previews.ContainsKey(text))
            {
                try
                {
                    previews.Add(text, Image.FromFile("C:/Users/ficti/Documents/Programming/BreakneckEmergence/Resources/Maps/Previews/" + text + "_preview_912x492.png"));
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return previews[text];
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            var node = e.Node;
            if (node.Parent != null && node.Parent.Parent != null)
            {
                e.Node.Text = e.Label;
            }
            
            //e.Node.Text = e.Label;
            //treeView1.SelectedNode = e.Node;
            TryDisplayPreview();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            var node = treeView1.SelectedNode;
            if( node != null )
            {
                while (node.Parent != null)
                    node = node.Parent;
            }
            if( node != null )
            {
                worldPreview.SetWorldName(node.Text);
                worldPreview.ClearPreview();
                var sectors = node.Nodes;
                foreach( TreeNode tn in sectors )
                {
                    worldPreview.AddSector(tn.Text);
                    var levels = tn.Nodes;
                    foreach( TreeNode lev in levels )
                    {
                        worldPreview.AddLevel(lev.Text, GetPreviewImage(lev.Text));
                    }
                }
                worldPreview.GeneratePreview();
                worldPreview.WindowState = FormWindowState.Maximized;
                worldPreview.ShowDialog();
            }
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //if( tn.Parent != null && tn.Parent.Parent != null )
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = treeView1.GetNodeAt(targetPoint);

            if (targetNode.Parent != null && targetNode.Parent.Parent == null) //sector
            {
                //it should only work in this case so you can drag levels around but not sectors or worlds


                // Retrieve the node that was dragged.
                TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

                // Confirm that the node at the drop location is not 
                // the dragged node and that target node isn't null
                // (for example if you drag outside the control)
                if (!draggedNode.Equals(targetNode) && targetNode != null)
                {
                    // Remove the node from its current 
                    // location and add it to the node at the drop location.
                    draggedNode.Remove();

                    targetNode.Nodes.Add(draggedNode);

                    // Expand the node at the location 
                    // to show the dropped node.
                    targetNode.Expand();
                }
            }
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if( e.Control && e.KeyCode == Keys.Up )
            {
                MoveNodeUp();
            }
            else if( e.Control && e.KeyCode == Keys.Down )
            {
                MoveNodeDown();
            }
        }
    }
}
