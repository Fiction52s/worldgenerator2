using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace worldgenerator2
{
    class Sector
    {
        public Sector()
        {
            topBonuses = new List<int>();
            botBonuses = new List<int>();
            conditions = new List<Tuple<int, int>>();
            levels = new List<Level>();
        }
        public void Load( string[] lines, ref int currIndex)
        {
            name = lines[currIndex];
            ++currIndex;
            sectorType = Convert.ToInt32(lines[currIndex]);
            ++currIndex;
            int numUnlockConditions = Convert.ToInt32(lines[currIndex]);
            ++currIndex;
            for ( int i = 0; i < numUnlockConditions; ++i )
            {
                int condType = Convert.ToInt32(lines[currIndex]);
                ++currIndex;
                int numCond = Convert.ToInt32(lines[currIndex]);
                ++currIndex;
                conditions.Add(new Tuple<int, int>(condType, numCond));
            }

            int numLevels = Convert.ToInt32(lines[currIndex]);
            ++currIndex;
            for (int i = 0; i < numLevels; ++i)
            {
                Level lev = new Level();
                levels.Add(lev);
                lev.Load(lines, ref currIndex);
            }

            int numTopBonuses = Convert.ToInt32(lines[currIndex]);
            ++currIndex;

            for( int i = 0; i < numTopBonuses; ++i )
            {
                int c = Convert.ToInt32(lines[currIndex]);
                ++currIndex;
                topBonuses.Add(c);
            }

            int numBotBonuses = Convert.ToInt32(lines[currIndex]);
            ++currIndex;

            for (int i = 0; i < numBotBonuses; ++i)
            {
                int c = Convert.ToInt32(lines[currIndex]);
                ++currIndex;
                botBonuses.Add(c);
            }
        }

        public void Save(StreamWriter writer)
        {
            writer.WriteLine(name);
            writer.WriteLine(sectorType);
            writer.WriteLine(conditions.Count);
            foreach( Tuple<int,int> tup in conditions )
            {
                writer.WriteLine(tup.Item1);
                writer.WriteLine(tup.Item2);
            }
            writer.WriteLine(levels.Count);
            foreach (Level lev in levels)
            {
                lev.Save(writer);
            }

            writer.WriteLine(topBonuses.Count);
            foreach (int bon in topBonuses)
            {
                writer.Write(bon);
            }

            writer.WriteLine(botBonuses.Count);
            foreach (int bon in botBonuses)
            {
                writer.Write(bon);
            }
        }

        public List<int> topBonuses;
        public List<int> botBonuses;
        public List<Tuple<int, int>> conditions;
        public int sectorType;
        //int numLevels;
        public List<Level> levels;
        public string name;
    }
}
