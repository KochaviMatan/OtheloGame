using System;
using System.Text;
using Ex02.ConsoleUtils;
namespace Ex02_Othelo
{
    class GameUIUX
    {

        //--------------------------------------------------------------------------------------//
        //                                  Enum                                                //
        //--------------------------------------------------------------------------------------//

        public enum eBoardSize : byte
        {
            Small = 6,
            Large = 8
        }

        public enum eGameMode : byte
        {
            AgainstComputer = 1,
            AgainstPlayer = 2
        }

        //--------------------------------------------------------------------------------------//
        //                                   conts                                              //
        //--------------------------------------------------------------------------------------//

        private const byte        k_Player1            = 1;
        private const byte        k_Player2            = 2;
        private const string      k_ComputerName       = "Computer";
        private const char        k_Quit               = 'Q';
        private const char        k_Yes                = 'Y';
        private const char        k_No                 = 'N';
        private const byte        k_NameMaxLength      = 20;
        private const byte        k_NameMinLength      = 2;
        private const byte        k_MoveStrLength      = 2;

        //--------------------------------------------------------------------------------------//
        //                                   Data Members                                       //
        //--------------------------------------------------------------------------------------//

        private OtheloGameManager m_OtheloGameManager  = null;
        private bool              m_GameOver           = false;
        private string            m_CurrentPlayerInput = null;

        //--------------------------------------------------------------------------------------//
        //                                  Run Game                                            //
        //--------------------------------------------------------------------------------------//

        //
        public void Run()
        {
            initializeOtheloGame();
            startPlaying();
        }

        //
        private void initializeOtheloGame()
        {
            string player1Name = getValidNameFromUser(k_Player1);
            string player2Name = null;
            bool isAgainstComputer = false;

            byte boardSize = getValidBoardSizeFromUser();
            eGameMode gameMode = getValidGameMode();

            // Check if Player1 play agains the computer 
            if (gameMode == eGameMode.AgainstComputer)
            {
                player2Name = k_ComputerName;
                isAgainstComputer = true;
            }
            else
            {
                player2Name = getValidNameFromUser(k_Player2);
            }

            m_OtheloGameManager = new OtheloGameManager(boardSize, player1Name, player2Name, isAgainstComputer);
            m_OtheloGameManager.InitializeGame();
        }

        //
        private void startPlaying()
        {
            clearScreenAndPrintGamePanel();

            while (!m_GameOver)
            {
                if (m_OtheloGameManager.GetCurrentPlayer().IsPlayerIsComputer)
                {
                    doComputerMove();
                }
                else
                {                 
                    doAMoveOrQuit();
                }

                // Check if the game is over
                if (m_OtheloGameManager.IsGameOver())
                {
                    currentGameRoundIsOver();
                }
            }

            Screen.Clear();
            printGameOverMsg();
        }
                
        //NEED TO FILL
        private void doComputerMove()
        {
            
        }


        //NEED TO FILL
        private void doAMoveOrQuit()
        {
            Coordinates Coordinate;
            m_CurrentPlayerInput = getValidActionFromThePlayer();
            convertStrToMove(m_CurrentPlayerInput, out Coordinate);

            if (isQuitSyntex(m_CurrentPlayerInput))
            {
                m_OtheloGameManager.Winner = m_OtheloGameManager.GetOpposingPlayer();
                currentGameRoundIsOver();
            }
            else
            {
                doPlayerMove(Coordinate);
            }
        }

        //NEED TO FILL
        private void doPlayerMove(Coordinates i_Coordinate)
        {
            if(!m_OtheloGameManager.setPiece(i_Coordinate))
            {
                doAMoveOrQuit();
            }                               
    
            m_OtheloGameManager.ChangeTurn();
            clearScreenAndPrintTheActionThatRivalDone();
        }

        //NEED TO FILL
        private void currentGameRoundIsOver()
        {
            Screen.Clear();
            m_OtheloGameManager.UpdatePlayerScore();
            printWinnerOrDrawMsg();
            printGameScoreOfThePlayers();
            askToContinueAndActInAccordance();
        }

        //
        private void askToContinueAndActInAccordance()
        {
            string toContinueAnswer;
            toContinueAnswer = getValidAnswerFromThePlayer();

            if (toContinueAnswer.Equals(k_No.ToString()))
            {
                m_GameOver = true;
            }
            else
            {
                initializeGameRound();
            }
        }
                
        //
        private void initializeGameRound()
        {
            m_GameOver = false;
            m_CurrentPlayerInput = null;
            m_OtheloGameManager.InitializeGame();
            clearScreenAndPrintGamePanel();
        }

        //
        private void convertStrToMove(string i_ValidStringInput, out Coordinates i_Coordinate)
        {
            byte xCoordinate = (byte)(i_ValidStringInput[1] - '1'); 
            byte yCoordinate = (byte)(i_ValidStringInput[0] - 'A');

            i_Coordinate = new Coordinates(xCoordinate, yCoordinate);
        }


        //--------------------------------------------------------------------------------------//
        //                               update board                                           //
        //--------------------------------------------------------------------------------------//

        //
        private void clearScreenAndPrintGamePanel()
        {
            Screen.Clear();
            Console.WriteLine(m_OtheloGameManager.GamePanel.GetBoardPanelAsString());
        }

        //
        private void clearScreenAndPrintTheActionThatRivalDone()
        {
            clearScreenAndPrintGamePanel();
            printPreviousTurnMoveMsg(m_CurrentPlayerInput);
        }

        // 
        private void clearScreenAndPrintTheActionThatCurrentPlayerDone()
        {
            clearScreenAndPrintGamePanel();
            printCurrentPreviousTuenMoveOfCurrentPlayerMsg(m_CurrentPlayerInput);
        }

        //
        private void printPreviousTurnMoveMsg(string i_TheMove)
        {
            Player PreviousPlayer = m_OtheloGameManager.GetOpposingPlayer();
            Console.WriteLine("{0}'s move was ({1}) : {2}", PreviousPlayer.Name, (char)PreviousPlayer.r_Team, i_TheMove);
        }

        //
        private void printCurrentPreviousTuenMoveOfCurrentPlayerMsg(string i_TheMove)
        {
            Player CurrentPlayer = m_OtheloGameManager.GetCurrentPlayer();
            Console.WriteLine("{0}'s move was ({1}) : {2}", CurrentPlayer.Name, (char)CurrentPlayer.r_Team, i_TheMove);
        }

        //--------------------------------------------------------------------------------------//
        //                           printiong messeges to user                                 //
        //--------------------------------------------------------------------------------------//

        //
        private void printPlayersTurnMsg()
        {
            Player currentPlayer = m_OtheloGameManager.GetCurrentPlayer();
            Console.Write("{0}'s turn ({1}): ", currentPlayer.Name, (char)currentPlayer.r_Team);
        }

        //
        private void printWinnerOrDrawMsg()
        {
            // If The winner is null it means that we have a draw:
            if (m_OtheloGameManager.Winner == null)
            {
                Console.WriteLine("Draw");
            }
            else
            {
                Console.WriteLine("The winner is {0}!", m_OtheloGameManager.Winner.Name);
            }
        }

        //
        private void printGameScoreOfThePlayers()
        {
            printPlayerScore(m_OtheloGameManager.Player1);
            printPlayerScore(m_OtheloGameManager.Player2);
        }

        //
        private void printGameOverMsg()
        {
            Console.WriteLine("Game Over");
        }

        //
        private void printPlayerScore(Player i_ThePlayer)
        {
            Console.WriteLine("{0}'s score: {1}", i_ThePlayer.Name, i_ThePlayer.Score);
        }

        //--------------------------------------------------------------------------------------//
        //                                 getting info from user                               //
        //--------------------------------------------------------------------------------------//

        //
        private string getValidNameFromUser(byte i_Player)
        {
            Console.WriteLine("Player{0} Please Enter your name: ", i_Player);
            string userName = Console.ReadLine();

            while (!isValidName(userName))

            {
                printErrorMsgForGettingInvalidName();
                userName = Console.ReadLine();
            }

            return userName;
        }

        //
        private byte getValidBoardSizeFromUser()
        {
            Console.WriteLine("Please Enter the Size of the board (6/8):");
            string boardSize = Console.ReadLine();

            while (!isValidBoardSize(boardSize))
            {
                printErrorMsgForGettingInvalidBoardSize();
                boardSize = Console.ReadLine();
            }

            return byte.Parse(boardSize);
        }

        //
        private eGameMode getValidGameMode()
        {
            Console.WriteLine
       (@"Please select game mode:
 {0} - Against the computer
 {1} - Against other player", (byte)eGameMode.AgainstComputer, (byte)eGameMode.AgainstPlayer);

            string gameMode = Console.ReadLine();

            while (!isValidGameMode(gameMode))
            {
                printErrorMsgForGettingInvalidGameMode();
                gameMode = Console.ReadLine();
            }

            return (eGameMode)(int.Parse(gameMode));
        }

        //
        private void askToContinue()
        {
            Console.WriteLine("Do You Want to play again? ( {0} = yes, {1} = no) ", k_Yes, k_No);
        }

        //
        private string getValidAnswerFromThePlayer()
        {
            askToContinue();
            string answer = Console.ReadLine();

            while (!isValidAnswer(answer))
            {
                printErrorMsgForGettingInvalidAsnwer();
                askToContinue();
                answer = Console.ReadLine();
            }

            return answer;
        }

        //
        private string getValidActionFromThePlayer()
        {
            Coordinates Coordinate;
            printPlayersTurnMsg();
            string playerAction = Console.ReadLine();

            convertStrToMove(playerAction, out Coordinate);

            while (!isValidSyntexAndAction(playerAction))
            {
                printErrorMsgForGettingInvalidAction();
                printPlayersTurnMsg();
                playerAction = Console.ReadLine();
            }


            return playerAction;
        }

        //--------------------------------------------------------------------------------------//
        //                                  Valididation                                        //
        //--------------------------------------------------------------------------------------//

        //
        private bool isValidAnswer(string i_Answer)
        {
            return i_Answer.Equals(k_No.ToString()) || i_Answer.Equals(k_Yes.ToString());
        }

        //
        private bool isValidName(string i_UserName)
        {
            return i_UserName.Length > 1 && !i_UserName.Contains(" ");
        }

        //
        private bool isValidBoardSize(string i_BoardSize)
        {
            byte boardSize;
            bool isValidSize = false;

            if (byte.TryParse(i_BoardSize, out boardSize))
            {
                isValidSize = boardSize == (byte)eBoardSize.Small ||
                              boardSize == (byte)eBoardSize.Large;
            }

            return isValidSize;
        }

        //
        private bool isValidGameMode(string i_GameMode)
        {
            byte gameMode;
            bool isValidGameMode = false;

            if (byte.TryParse(i_GameMode, out gameMode))
            {
                isValidGameMode = gameMode == (byte)eGameMode.AgainstComputer ||
                                  gameMode == (byte)eGameMode.AgainstPlayer;
            }

            return isValidGameMode;
        }

        // EMPTY
        private bool isValidSyntexAndAction(string i_StringInput)
        {
            return true;
        }

        //
        private bool isQuitSyntex(string i_StringInput)
        {
            return i_StringInput.Equals(k_Quit.ToString());
        }

        //--------------------------------------------------------------------------------------//
        //                                  Error Messages                                      //
        //--------------------------------------------------------------------------------------//

        // This function print error message due to receiving invalid name.
        private void printErrorMsgForGettingInvalidName()
        {
            Console.WriteLine("Error! Invalid input:");
            Console.WriteLine("the name need contain no more than {0} and no less then {1} letters and without spaces...", k_NameMaxLength, k_NameMinLength);
        }

        // This function print error message due to receiving invalid board size.
        private void printErrorMsgForGettingInvalidBoardSize()
        {
            Console.WriteLine("Error! Invalid input: the board size invalid...");
        }

        // This function print error message due to receiving invalid game mode.
        private void printErrorMsgForGettingInvalidGameMode()
        {
            Console.WriteLine("Error! Invalid input: the mode you enter does not exist...");
        }

        // This function print error message due to invalid answer.
        private void printErrorMsgForGettingInvalidAsnwer()
        {
            Console.WriteLine("Error! Invalid input: this answer not valid...");
        }

        // This function print error message due to invalid action.
        private void printErrorMsgForGettingInvalidAction()
        {
            Console.WriteLine("Error! Invalid input: the action you try to do is not valid...");
        }


    }
}
