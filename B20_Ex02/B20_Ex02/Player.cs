namespace B20_Ex02
{
    internal class Player           
    {
        private string m_Name;
        private bool m_Pc;
        private int m_Pairs;
        internal AI m_PlayerVsComputer;

        internal Player(string i_Name, bool i_Pc)
        {
            //Send i_Pc = True if player is AI

            m_Name = i_Name;
            m_Pc = i_Pc;

            if (m_Pc)
            {
                m_PlayerVsComputer = new AI();
            }
        }

        internal string Name
        {
            get { return m_Name; }
        }

        internal int Pairs
        {
            get { return m_Pairs; }
            set { m_Pairs++; }
        }

        internal bool Pc
        {
            get { return m_Pc; }
        }
    }
}