using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    struct Tile
    {
        private int m_Value;
        private bool m_Expose;

        internal Tile(int Value)
        {
            m_Value = Value;
            m_Expose = false;
        }

        public int Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        internal bool Expose
        {
            get { return m_Expose; }
            set { m_Expose = value; }
        }

    }
}
