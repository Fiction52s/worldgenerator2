using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace worldgenerator2
{
    class SaveFile
    {
        public SaveFile()
        {
            worlds = new List<World>();
        }

        public void Load( string[] lines)
        {
            int currLine = 0;
            int numWorlds = Convert.ToInt32(lines[currLine]);
            ++currLine;
            for ( int i = 0; i < numWorlds; ++i )
            {
                World newWorld = new World();
                worlds.Add(newWorld);
                newWorld.Load(lines, ref currLine);
            }
        }

        public void Save( SaveFileDialog save )
        {
           // SaveFileDialog save = new SaveFileDialog();

            //save.FileName = fileName

            //save.Filter = "Breakneck Data File | *.kin";
            StreamWriter writer = new StreamWriter(save.OpenFile());
                    
            writer.WriteLine(worlds.Count);
            foreach( World w in worlds )
            {
                w.Save(writer);
            }

            for( int i = 0; i < 5; ++i )
                writer.WriteLine("0");
            for (int i = 0; i < 5; ++i)
                writer.WriteLine("0");

            writer.Dispose();

            writer.Close();
        }

        public List<World> worlds;
    }
}
