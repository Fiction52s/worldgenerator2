using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace worldgenerator2
{
    public partial class WorldPreview : Form
    {
        int factor;
        int xSize;
        int ySize;
        int xSpacing;
        int ySpacing;

        public WorldPreview()
        {
            InitializeComponent();

            allPreviews = new List<Tuple<Label, List<Tuple<Label, PictureBox>>>>();

            factor = 4;
            xSize = 912 / factor;
            ySize = 492 / factor;
            xSpacing = 20;
            ySpacing = 80;//40;
        }

        public void ClearPreview()
        {
            foreach( Tuple<Label, List<Tuple<Label, PictureBox>>> lp in allPreviews )
            {
                Controls.Remove(lp.Item1);
                foreach( Tuple<Label, PictureBox> pb in lp.Item2 )
                {
                    Controls.Remove(pb.Item1);
                    Controls.Remove(pb.Item2);
                }
            }
            allPreviews.Clear();
        }

        public void AddSector( string name )
        {
            List<Tuple<Label, PictureBox>> pbList = new List<Tuple<Label, PictureBox>>();
            Label label = new Label();

            //label.Size = new System.Drawing.Size(100, 100);
            label.AutoSize = true;
            label.Left = 10;// + allPreviews.Count * ;
            label.Top = 10 + ( ySpacing + ySize )* allPreviews.Count;
            label.Font = new System.Drawing.Font(label.Font.Name, 16F);
            label.BackColor = Color.Transparent;
            label.Text = name;
            Controls.Add(label);

            allPreviews.Add(new Tuple<Label, List<Tuple<Label, PictureBox>>>( label, pbList ) );
        }
        public void AddLevel( string name, Image im )
        {
            PictureBox pb = new PictureBox();
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Image = im;

            Label label = new Label();
            label.Text = name;
            label.TextAlign = ContentAlignment.TopCenter;
            //label.Left = 100;//(100 + j * (xSize + xSpacing));
            //label.Top = 10;//(40 + i * (ySize + ySpacing));
            //label.Size = new System.Drawing.Size(100, 100);
            allPreviews[allPreviews.Count - 1].Item2.Add(new Tuple<Label, PictureBox>(label, pb));
        }


        public void SetWorldName( string text )
        {
            this.Text = text;
        }

        public void GeneratePreview()
        {
            

            for ( int i = 0; i < allPreviews.Count; ++i )
            {
                Label sectorLabel = allPreviews[i].Item1;

                for( int j = 0; j < allPreviews[i].Item2.Count; ++j )
                {
                    PictureBox pb = allPreviews[i].Item2[j].Item2;
                    Label levelLabel = allPreviews[i].Item2[j].Item1;

                    levelLabel.Left = 100 + j * (xSize + xSpacing) + xSize / 2 - levelLabel.Size.Width / 2;
                    levelLabel.Top = 50 + i * (ySize + ySpacing);
                    //levelLabel.Left = 10;//(100 + j * (xSize + xSpacing));
                    //levelLabel.Top = 10;//(40 + i * (ySize + ySpacing));

                    pb.SetBounds(100 + j * (xSize + xSpacing), 70 + i * (ySize + ySpacing), xSize, ySize);
                   
                    Controls.Add(pb);
                    Controls.Add(levelLabel);
                }
            }
        }

        List<Tuple<Label, List<Tuple<Label,PictureBox>>>> allPreviews;
    }
}
