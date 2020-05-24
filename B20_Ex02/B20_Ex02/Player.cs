using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    class Player
    {
        string m_Name;
        bool m_Pc;
        int m_Pairs;

        internal Player(string name, bool pc)
        {
            m_Name = name;
            m_Pc = pc;
        }

        public string Name
        {
            get { return m_Name; }
        }
    }
}
