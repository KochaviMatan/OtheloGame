using System.Collections.Generic;
namespace Ex02_Othelo
{
    public class Player
    {
        public enum eTeam
        {
            White = 'O',
            Black = 'X',
        }

        public readonly eTeam r_Team;
        private string m_Name = null;
        private int m_Score = 0;
        private bool m_PlayerIsComputer = false;
        private bool m_IsHaveValidMove = false;
        private List<Piece> m_Pieces = new List<Piece>();

        // Player Constructor
        public Player(string i_Name, bool i_PlayerIsComputer, eTeam i_Team)
        {
            m_Name = i_Name;
            m_PlayerIsComputer = i_PlayerIsComputer;
            r_Team = i_Team;
            List<Piece> m_Pieces = new List<Piece>();
        }

        //-------------------------------------------------------------------------------//
        //                                Get & Set                                      //
        //-------------------------------------------------------------------------------//

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public bool IsHaveValidMove
        {
            get
            {
                return m_IsHaveValidMove;
            }

            set
            {
                m_IsHaveValidMove = value;
            }
        }

        public bool IsPlayerIsComputer
        {
            get
            {
                return m_PlayerIsComputer;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public List<Piece> Pieces
        {
            get
            {
                return m_Pieces;
            }
        }

        // -----------------------------------------------------------------------------//
        //                           Player Public functions                            //
        // -----------------------------------------------------------------------------//

        public void AddPiece(Piece i_Piece)
        {
            m_Pieces.Add(i_Piece);
        }

        public void RemovePiece(Piece i_Piece)
        {
            if (m_Pieces.Count != 0)
            {
                m_Pieces.Remove(i_Piece);
            }
        }
    }
}