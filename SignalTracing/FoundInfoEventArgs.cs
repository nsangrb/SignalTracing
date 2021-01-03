namespace SignalTracing
{
    class FoundInfoEventArgs
    {
        // ----- Variables -----

        private int m_info;


        // ----- Constructor -----

        public FoundInfoEventArgs(int info)
        {
            m_info = info;
        }
        public FoundInfoEventArgs()
        {
        }

        // ----- Public Properties -----

        public int Info
        {
            get { return m_info; }
        }
    }
}
