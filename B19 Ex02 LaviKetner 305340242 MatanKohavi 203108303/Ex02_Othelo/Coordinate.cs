
namespace Ex02_Othelo
{
    public struct Coordinates
    {
        private byte m_Y;
        private byte m_X;

        // Coordinates constructor
        public Coordinates(byte i_X, byte i_Y)
        {
            m_Y = i_Y;
            m_X = i_X;
        }

        //-------------------------------------------------------------------------------//
        //                                 Get & Set                                     //
        //-------------------------------------------------------------------------------//

        public byte Y
        {
            get
            {
                return m_Y;
            }

            set
            {
                m_Y = value;
            }
        }

        public byte X
        {
            get
            {
                return m_X;
            }

            set
            {
                m_X = value;
            }
        }
    }
}
