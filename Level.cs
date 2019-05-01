using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace worldgenerator2
{
    class Level
    {
        public void Load( string [] lines, ref int currIndex )
        {
            name = lines[currIndex];
            ++currIndex;
            bitFieldTest = Convert.ToInt32(lines[currIndex]);
            ++currIndex;
        }

        public void Save(StreamWriter writer)
        {
            writer.WriteLine(name);
            writer.WriteLine(bitFieldTest);
        }
        public string name;
        public int bitFieldTest;
        //ignore bitfield for now
    }
}
