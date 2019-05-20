using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace worldgenerator2
{
    class World
    {
        public World()
        {
            sectors = new List<Sector>();
        }
        public void Load(string[] lines, ref int currIndex)
        {
            name = lines[currIndex];
            ++currIndex;
            int numSectors = Convert.ToInt32(lines[currIndex]);
            ++currIndex;

            for (int i = 0; i < numSectors; ++i)
            {
                Sector sec = new Sector();
                sectors.Add(sec);
                sec.Load(lines, ref currIndex);
            }
        }

        public void Save( StreamWriter writer )
        {
            writer.WriteLine(name);
            writer.WriteLine(sectors.Count);
            foreach ( Sector s in sectors )
            {
                s.Save(writer);
            }
        }

        public string name;
        public List<Sector> sectors;
    }
}
