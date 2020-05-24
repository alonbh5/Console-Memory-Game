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
        internal AI m_pvc;
        

        internal Player(string name, bool pc)
        {
            m_Name = name;
            m_Pc = pc;
            if (pc)
            {
                m_pvc = new AI();
            }
        }

        public string Name
        {
            get { return m_Name; }
        }

        public int Pairs
        {
            get { return m_Pairs; }
            set { m_Pairs++; }
        }

        public bool Pc
        {
            get { return m_Pc; }
        }
    }
}
