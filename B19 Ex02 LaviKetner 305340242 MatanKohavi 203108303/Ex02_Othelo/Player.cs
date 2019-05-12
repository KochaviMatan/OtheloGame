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
        private int k_NameLength = 2;
        private string m_Name = null;
        private int m_Score = 0;
        private bool m_PlayerIsComputer = false;
        private List<Piece> m_Pieces = new List<Piece>();

        // Player Constructor
        public Player(string i_Name, bool i_PlayerIsComputer, eTeam i_Team)
        {
            m_Name = i_Name;
            m_PlayerIsComputer = i_PlayerIsComputer;
            r_Team = i_Team;
            List<Piece> m_Pieces = new List<Piece>();
        }

        ////-------------------------------------------------------------------------------
        //                                 Properties
        ////-------------------------------------------------------------------------------

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

        ////-----------------------------Can only be return----------------------------------

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

        // -------------------------------------------------------------------------------
        //                           Player's tool functions
        // -------------------------------------------------------------------------------

        // This function return the number of pieces that the player have.
        public int GetNumberOfPieces()
        {
            return m_Pieces.Count;
        }

        // This function receive piece and add him to the player pieces
        public void AddPiece(Piece i_Piece)
        {
            m_Pieces.Add(i_Piece);
        }

        // This function receive piece that need to remove from the player pieces and remove it
        public void RemovePiece(Piece i_Piece)
        {
            // Verfiy that the array of pieces not empty from pieces
            if (m_Pieces.Count != 0)
            {
                m_Pieces.Remove(i_Piece);
            }
        }
    }
}