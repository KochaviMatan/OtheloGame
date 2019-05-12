using System.Text;
namespace Ex02_Othelo
{
    class GamePanel
    {

        //--------------------------------------------------------------------------------------//
        //                                   conts                                              //
        //--------------------------------------------------------------------------------------//

        private const char k_FirstLetterOfHeader      = 'A';
        private const byte k_NumOfSpacesBetwenColumns = 3;
        private const char k_RowsBufferSymbol         = '=';
        private const char k_ColumnsBufferSymbol      = '|';
        private const byte k_BufferLength             = 4;

        //--------------------------------------------------------------------------------------//
        //                                   Data Members                                       //
        //--------------------------------------------------------------------------------------//

        public readonly byte r_Size;
        private Piece[,] m_Board;
        public GamePanel(byte i_Size)
        {
            r_Size = i_Size;
            m_Board = new Piece[r_Size, r_Size];
        }

        //--------------------------------------------------------------------------------------//
        //                                     Set & Get                                        //
        //--------------------------------------------------------------------------------------//

        //
        public Piece this[Coordinates i_Cell]
        {
            // This indexer returns the piece in the index of the given coordinate.
            get
            {
                return m_Board[i_Cell.Y, i_Cell.X];
            }

            // This indexer enter the given piece at the place of the board that the coordinate represent.
            set
            {
                m_Board[i_Cell.Y, i_Cell.X] = value;
            }
        }

        //--------------------------------------------------------------------------------------//
        //                                   Build Board                                        //
        //--------------------------------------------------------------------------------------//

        //
        public bool DoesCellExist (Coordinates i_Cell)
        {
            return i_Cell.X >= 1 && i_Cell.X <= r_Size && i_Cell.Y >= 0 && i_Cell.Y <= r_Size;
        }

        public bool DoesCellOccupied(Coordinates i_Cell)
        {
            return m_Board[i_Cell.Y, i_Cell.X] != null;
        }

        //
        public string GetBoardPanelAsString()
        {
            //
            char currentRowHeader = '1';   
            string emptyCellString = buildEmptyCellString();             
            string endOfRowString = buildEndOfRowString();               
            StringBuilder boardString = new StringBuilder(buildBoardHeaderString());

            // Bulid the board that will be drawn:
            for (byte row = 0; row < r_Size; row++)
            {
                // Open row with row header:
                boardString.Append(currentRowHeader.ToString());

                // Build the rest of the board:
                for (byte column = 0; column < r_Size; column++)
                {
                    if (m_Board[row, column] == null)
                    {
                        boardString.Append(emptyCellString);
                    }
                    else
                    {
                        string occupiedCellString = string.Format("{0}{1}{2}{1}", k_ColumnsBufferSymbol, ' ', (char)m_Board[row, column].Symbol);
                        boardString.Append(occupiedCellString);
                    }
                }

                boardString.AppendLine(endOfRowString);
                currentRowHeader++;
            }

            // Remove the last surplus line in string:
            boardString.Remove(boardString.Length - 1, "\n".Length);
            return boardString.ToString();
        }

        //
        private string buildEmptyCellString()
        {
            string sequenceOfSpaces = new string(' ', k_NumOfSpacesBetwenColumns);
            string emptyCellString = string.Format("{0}{1}", k_ColumnsBufferSymbol, sequenceOfSpaces);
            return emptyCellString;
        }

        //
        private string buildEndOfRowString()
        {
            string endOfRowString = string.Format("{0}{1}{2}", k_ColumnsBufferSymbol, System.Environment.NewLine, buildSequenceOfBufferSymbol());
            return endOfRowString;
        }

        //
        private string buildSequenceOfBufferSymbol()
        {
            string sequenceOfSymbol = new string(k_RowsBufferSymbol, (k_BufferLength * r_Size) + 1);
            string bufferRowStringSymbol = string.Format("{0}{1}", " ", sequenceOfSymbol);
            return bufferRowStringSymbol;
        }

        //
        private string buildBoardHeaderString()
        {
            char currentLetter = k_FirstLetterOfHeader;
            string sequenceOfSpaces = new string(' ', k_NumOfSpacesBetwenColumns);

            StringBuilder boardHeaderString = new StringBuilder(sequenceOfSpaces);

            for (int i = 0; i < r_Size; i++)
            {
                boardHeaderString.Append(currentLetter);
                boardHeaderString.Append(sequenceOfSpaces);
                currentLetter++;
            }

            boardHeaderString.AppendLine();

            return string.Format("{0}{1}{2}", boardHeaderString, buildSequenceOfBufferSymbol(), System.Environment.NewLine);
        }

    }
}
