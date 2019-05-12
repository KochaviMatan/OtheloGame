using System.Collections.Generic;

namespace Ex02_Othelo
{
    class OtheloGameManager
    {
        
        //--------------------------------------------------------------------------------------//
        //                                   Conts                                              //
        //--------------------------------------------------------------------------------------//

        const byte k_LeftUpWhite    = 0,
                   k_RightUpBlack   = 1, 
                   k_LeftDownBlack  = 2,
                   k_RightDownWhite = 3;

        //--------------------------------------------------------------------------------------//
        //                                   Data Members                                       //
        //--------------------------------------------------------------------------------------//

        private static GamePanel    s_GamePanel;
        private Player              m_Player1;
        private Player              m_Player2;
        private static Player.eTeam s_Turn              = Player.eTeam.Black;
        private Player              m_Winner            = null;
        private Movement     m_CurrentValidMoves;

        class Movement
        {
            private enum eMoveDirection
            {
                TopRightDirection  = 0,
                TopDirection       = 1,
                TopLeftDirection   = 2,
                LeftDirection      = 3,
                LeftDownDirection  = 4,
                DownDirection      = 5,
                RightDownDirection = 6,
                RightDirection     = 7
            }
                
           const byte k_NumOfDirections = 8;

            private List<Piece>[,] m_ChangeTeamPieces;    

            public List<Piece> this[Coordinates i_Cell]
            {
                get
                {
                    return m_ChangeTeamPieces[i_Cell.X, i_Cell.Y];
                }
            }

            private void makeAListOfCurrectMoves(Player i_CurrentPlayrTurn)
            {
               List<Piece> allThePiecesFormCurrentPlayer = i_CurrentPlayrTurn.Pieces;

               foreach(Piece currentPiece in allThePiecesFormCurrentPlayer)
                {
                    Coordinates currentPieceCoordinate = currentPiece.CoordinatesOnBoard;

                    for (eMoveDirection currentDirection = eMoveDirection.TopRightDirection; (byte)currentDirection < k_NumOfDirections; currentDirection++)
                    {
                        List<Piece> currentListOfsequencePieces = new List<Piece>();
                        Coordinates currentCoordinate = currentPieceCoordinate;
                        currentCoordinate = getCellCoordinateToProcced(currentCoordinate, currentDirection);

                        while (isAValidMove(currentCoordinate))
                        {
                            Piece currentRivalPiece = s_GamePanel[currentCoordinate];
                            currentListOfsequencePieces.Add(currentRivalPiece);

                            currentCoordinate = getCellCoordinateToProcced(currentCoordinate, currentDirection);

                            if (checkIfArriveToEmptyCellOnBoard(currentCoordinate))
                            {
                                saveTheSequenceList(ref currentListOfsequencePieces, currentCoordinate);
                                currentListOfsequencePieces.Clear();
                            }

                            else if (isCurrentCoordinateContainRival(currentCoordinate)) 
                            {
                                currentListOfsequencePieces.Clear();
                                break;
                            }
                        }

                        currentListOfsequencePieces.Clear();
                    }
                } 
            }

            private bool isCurrentCoordinateContainRival(Coordinates i_CurrentCoordinate)
            {
                return s_GamePanel[i_CurrentCoordinate].r_Team == s_Turn;
            }

            private bool checkIfArriveToEmptyCellOnBoard(Coordinates i_CurrentCoordinate)
            {
                return s_GamePanel[i_CurrentCoordinate] == null;
            }

            private void saveTheSequenceList(ref List<Piece> i_CurrentListOfsequencePieces, Coordinates i_CurrentCoordinate)
            {
                foreach (Piece RivalPiece in i_CurrentListOfsequencePieces)
                {
                    m_ChangeTeamPieces[i_CurrentCoordinate.X, i_CurrentCoordinate.Y].Add(RivalPiece);
                }
            }

            private Coordinates getCellCoordinateToProcced(Coordinates i_CurrentCoordinate, eMoveDirection i_CurrentDirection)
            {
                Coordinates nextCoordinateInDirection = new Coordinates();

                switch (i_CurrentDirection)
                {
                    case eMoveDirection.TopRightDirection:
                        {
                            moveCoordinateTopRightDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                        }
                        break;

                    case eMoveDirection.TopDirection:
                        {
                            moveCoordinateTopDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                        }
                        break;

                    case eMoveDirection.TopLeftDirection:
                        {
                            moveCoordinateTopLeftDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                        }
                        break;

                    case eMoveDirection.LeftDirection:
                        {
                            moveCoordinateLeftDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                        }
                        break;

                    case eMoveDirection.LeftDownDirection:
                        {
                            moveCoordinateLeftDownDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                        }
                        break;

                    case eMoveDirection.DownDirection:
                        {
                            moveCoordinateDownDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                        }
                        break;

                    case eMoveDirection.RightDownDirection:
                        {
                            moveCoordinateRightDownDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                        }
                        break;

                     case eMoveDirection.RightDirection:
                        {
                            moveCoordinateRightDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                        }
                        break;
                }

                return nextCoordinateInDirection;
            }

            //----------------------------------- Move Coordinate to each Direction function --------------------------------------------

            private void moveCoordinateTopRightDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
            {
                io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X + 1);
                io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y - 1);
            }

            private void moveCoordinateTopDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
            {
                io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X);
                io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y - 1);
            }

            private void moveCoordinateTopLeftDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
            {
                io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X - 1);
                io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y - 1);
            }

            private void moveCoordinateLeftDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
            {
                io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X - 1);
                io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y);
            }

            private void moveCoordinateLeftDownDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
            {
                io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X - 1);
                io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y + 1);
            }

            private void moveCoordinateDownDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
            {
                io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X);
                io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y + 1);
            }

            private void moveCoordinateRightDownDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
            {
                io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X + 1);
                io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y + 1);
            }

            private void moveCoordinateRightDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
            {
                io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X + 1);
                io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y);
            }
            
            //------------------------------------------------------------------------------------------------------------------------

            private bool isAValidMove(Coordinates i_CurrentMove)
            {
                bool isValidMove = false;

                if (s_GamePanel.DoesCellExist(i_CurrentMove) && s_GamePanel.DoesCellOccupied(i_CurrentMove))
                {                  
                    if (doesCellOccupiedByEnemey(i_CurrentMove))
                    {
                        isValidMove = true;
                    }
                }

                return isValidMove;
            }

            private bool doesCellOccupiedByEnemey(Coordinates i_Cell)
            {
                Piece PieceOnCoordinate = s_GamePanel[i_Cell];
                return PieceOnCoordinate.r_Team != s_Turn;
            }

        }

        //--------------------------------------------------------------------------------------//
        //                                  Run Game                                            //
        //--------------------------------------------------------------------------------------//

        // OtheloGameManager Constructor
        public OtheloGameManager(byte i_BoardSize, string i_Player1Name, string i_Player2Name, bool i_IsPlayer2IsComputer)
        {
            const bool v_Player1IsAlwaysNotComputer = false;

            s_GamePanel = new GamePanel(i_BoardSize);
            m_Player1 = new Player(i_Player1Name, v_Player1IsAlwaysNotComputer, Player.eTeam.Black);
            m_Player2 = new Player(i_Player2Name, i_IsPlayer2IsComputer, Player.eTeam.White);
        }
        
        //
        public void InitializeGame()
        {
            m_Player1.Pieces.Clear();
            m_Player2.Pieces.Clear();
            initializeStartPositionOfPiecesOnBoard();
            m_Winner = null;
            s_Turn = Player.eTeam.Black;
           // buildCurrentValidMovesList();
        }

        //Place the first 4 pieces.
        private void initializeStartPositionOfPiecesOnBoard()
        {
            //Create the 4 coordinate for the starting position pieces.
            Coordinates[] fourInitializeCoordinates = new Coordinates[4];
            InitializeFourCoordinats(fourInitializeCoordinates);

            //Create the 4 start position Pieces. 
            Piece[] fourInitializePieces = new Piece[4];
            InitializeFourPieces(fourInitializePieces, fourInitializeCoordinates);

            //Assign the 4 Start Pieces To Players
            AssignTheFourInitializePiecesToPlayers(fourInitializePieces);

            //Place the 4 stat Pieces on board.
            placeTheFourInitializePiecesOnBoard(fourInitializePieces, fourInitializeCoordinates);
        }

        //
        private void InitializeFourPieces(Piece[] io_fourInitializePieces,Coordinates[] io_fourInitializeCoordinates)
        {
            io_fourInitializePieces[k_RightUpBlack]   = new Piece(Player.eTeam.Black, io_fourInitializeCoordinates[k_RightUpBlack]);
            io_fourInitializePieces[k_LeftUpWhite]    = new Piece(Player.eTeam.White, io_fourInitializeCoordinates[k_LeftUpWhite]);
            io_fourInitializePieces[k_LeftDownBlack]  = new Piece(Player.eTeam.Black, io_fourInitializeCoordinates[k_LeftDownBlack]);
            io_fourInitializePieces[k_RightDownWhite] = new Piece(Player.eTeam.White, io_fourInitializeCoordinates[k_RightDownWhite]);
        }

        //
        private void InitializeFourCoordinats(Coordinates[] io_fourInitializeCoordinate)
        {
            //Find the middle of the board, to calculate the location for the first 4 pieces.
            byte middleRow = (byte)((s_GamePanel.r_Size / 2) - 1);
            byte middleColumn = middleRow;

            io_fourInitializeCoordinate[k_LeftUpWhite]    = new Coordinates(middleRow, middleColumn);
            io_fourInitializeCoordinate[k_RightUpBlack]   = new Coordinates(middleRow, (byte)(middleColumn + 1));
            io_fourInitializeCoordinate[k_LeftDownBlack]  = new Coordinates((byte)(middleRow + 1), (byte)(middleColumn));
            io_fourInitializeCoordinate[k_RightDownWhite] = new Coordinates((byte)(middleRow + 1), (byte)(middleColumn + 1)); 
        }

        //
        private void AssignTheFourInitializePiecesToPlayers(Piece[] i_fourInitializePiece)
        {
            m_Player1.AddPiece(i_fourInitializePiece[k_RightUpBlack]);
            m_Player1.AddPiece(i_fourInitializePiece[k_LeftDownBlack]);
            m_Player2.AddPiece(i_fourInitializePiece[k_LeftUpWhite]);
            m_Player2.AddPiece(i_fourInitializePiece[k_RightDownWhite]);
        }

        //
        private void placeTheFourInitializePiecesOnBoard(Piece[] i_fourInitializePieces, Coordinates[] i_fourInitializeCoordinates)
        {
            s_GamePanel[i_fourInitializeCoordinates[k_RightUpBlack]] = i_fourInitializePieces[k_RightUpBlack];
            s_GamePanel[i_fourInitializeCoordinates[k_LeftUpWhite]] = i_fourInitializePieces[k_LeftUpWhite];
            s_GamePanel[i_fourInitializeCoordinates[k_RightDownWhite]] = i_fourInitializePieces[k_RightDownWhite];
            s_GamePanel[i_fourInitializeCoordinates[k_LeftDownBlack]] = i_fourInitializePieces[k_LeftDownBlack];
        }

        // Builds a list of current players available moves:
        private void buildCurrentValidMovesList()
        {

        }

        //
        public Player GetCurrentPlayer()
        {
            return s_Turn == m_Player1.r_Team ? m_Player1 : m_Player2;
        }

        //
        public GamePanel GamePanel
        {
            get
            {
                return s_GamePanel;
            }
        }

        //
        public Player Winner
        {
            get
            {
                return m_Winner;
            }

            set
            {
                m_Winner = value;
            }
        }

        //
        public Player Player1
        {
            get
            {
                return m_Player1;
            }
        }

        //
        public Player Player2
        {
            get
            {
                return m_Player2;
            }
        }

        //NEED TO FILL
        public bool IsGameOver()
        {
            return true;
        }

        //NEED TO FILL
        public void UpdatePlayerScore()
        {

        }

        //NEED TO FILL
        public void setPiece(Coordinates i_Coordinate)
        {
            Piece newPiece = new Piece(s_Turn, i_Coordinate);
            s_GamePanel[i_Coordinate] = newPiece;

        }

        //
        private Movement getMovementByCoordinates(Coordinates i_Coordinate)
        {
            Movement returnValue = null;

            foreach (Movement validMove in m_CurrentValidMoves)
            {
                if (doesMovementContainCoordinates(validMove, i_Coordinate))
                {
                    returnValue = validMove;
                    break;
                }
            }

            return returnValue;
        }

        //
        private bool doesMovementContainCoordinates(Movement i_Move, Coordinates i_Coordinate)
        {
            return i_Move.Equals(i_Coordinate);
        }

        //
        public void ChangeTurn()
        {
            s_Turn = s_Turn == Player.eTeam.Black ? Player.eTeam.White : Player.eTeam.Black;
            buildCurrentValidMovesList();
        }

        //
        public Player GetOpposingPlayer()
        {
            return s_Turn == m_Player1.r_Team ? m_Player2 : m_Player1;
        }
    }
}
