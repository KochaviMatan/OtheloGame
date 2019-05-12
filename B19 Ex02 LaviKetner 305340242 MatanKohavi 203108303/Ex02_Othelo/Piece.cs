﻿
namespace Ex02_Othelo
{
    public class Piece
    {
        public Player.eTeam r_Team;
        private Coordinates m_CoordinatesOnBoard;


        // Piece Constructor
        public Piece(Player.eTeam i_Team, Coordinates i_Coordinates)
        {
            r_Team = i_Team;
            m_CoordinatesOnBoard = i_Coordinates;
        }

        ////-------------------------------------------------------------------------------
        //                                 Properties
        ////-------------------------------------------------------------------------------

        public Player.eTeam Team
        {
            get
            {
                return r_Team;
            }

            set
            {
                r_Team = value;
            }
        }

        public Coordinates CoordinatesOnBoard
        {
            get
            {
                return m_CoordinatesOnBoard;
            }

            set
            {
                m_CoordinatesOnBoard = value;
            }
        }
        ////-------------------------------------------------------------------------------
        //                                 Other functions
        ////-------------------------------------------------------------------------------

        public Player.eTeam Symbol
        {
            get
            {
                return r_Team;
            }
        }

        public void changePieceTeam()
        {
           r_Team = r_Team == Player.eTeam.Black ? Player.eTeam.White : Player.eTeam.Black;
        }

    }
}
