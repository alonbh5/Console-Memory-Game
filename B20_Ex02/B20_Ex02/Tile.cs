namespace B20_Ex02
{
    internal struct Tile            //// add internal
    {
        private int m_Value;
        private bool m_Expose;

        internal Tile(int Value)
        {
            m_Value = Value;
            m_Expose = false;
        }

        internal int Value
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