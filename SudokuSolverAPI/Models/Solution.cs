using System.Globalization;
using System.Text;
using System.Xml.Schema;

namespace SudokuSolverAPI.Models
{
    public class Solution
    {
        private char[,] ReceivedBoard { get; set; }

        public string _return;

        private bool Solvable = true;

        private bool CheckBoard()
        {
            foreach (var _char in ReceivedBoard)
            {
                int _out;
                string s =  _char.ToString();
                if (!Int32.TryParse(s, out _out))
                {
                    return false;
                }
            }
            return true;
        }
        
        private void GetSolution()
        {
            if (Solvable)
            {
                RunCalculator(ReceivedBoard);
                int i = 0;
                StringBuilder _builder = new StringBuilder();
                foreach (var VARIABLE in ReceivedBoard)
                {
                    if (i < 9)
                    {
                        _builder.Append(VARIABLE);
                        i++;
                    }
                    else
                    {
                        _builder.Append($"/ {VARIABLE}");
                        i = 1;
                    }
                }
                _return = _builder.ToString();
            }
            else
            {
                _return = "Not solvable!";
            }
        }

        private bool RunCalculator(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int r = 0; r < board.GetLength(1); r++)
                {
                    if (board[i, r] == '0')
                    {
                        for (char m = '1'; m <= '9'; m++)
                        {
                            if (Check(board, i, r, m))
                            {
                                ReceivedBoard[i, r] = m;

                                if (RunCalculator(board))
                                    return true;
                                
                                board[i, r] = '0';
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        private bool Check(char[,] board, int row, int column, char m)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i, column] != '0' && board[i, column] == m)
                    return false;
                if (board[row, i] != '0' && board[row, i] == m)
                    return false;
                if (board[3 * (row / 3) + i / 3, 3 * (column / 3) + i % 3] != '0' && board[3 * (row / 3) + i / 3, 3 * (column / 3) + i % 3] == m)
                    return false;
            }
            return true;
        }

        private char[,] ConvertString(string board)
        {
            char[] CharConverter = board.ToCharArray(0, board.Length);
            var _return = new char[,]
            {
                { CharConverter[0], CharConverter[1], CharConverter[2], CharConverter[3], CharConverter[4], CharConverter[5], CharConverter[6], CharConverter[7], CharConverter[8]},
                { CharConverter[9], CharConverter[10], CharConverter[11], CharConverter[12], CharConverter[13], CharConverter[14], CharConverter[15], CharConverter[16], CharConverter[17]},
                { CharConverter[18], CharConverter[19], CharConverter[20], CharConverter[21], CharConverter[22], CharConverter[23], CharConverter[24], CharConverter[25], CharConverter[26]},
                { CharConverter[27], CharConverter[28], CharConverter[29], CharConverter[30], CharConverter[31], CharConverter[32], CharConverter[33], CharConverter[34], CharConverter[35]},
                { CharConverter[36], CharConverter[37], CharConverter[38], CharConverter[39], CharConverter[40], CharConverter[41], CharConverter[42], CharConverter[43], CharConverter[44]},
                { CharConverter[45], CharConverter[46], CharConverter[47], CharConverter[48], CharConverter[49], CharConverter[50], CharConverter[51], CharConverter[52], CharConverter[53]},
                { CharConverter[54], CharConverter[55], CharConverter[56], CharConverter[57], CharConverter[58], CharConverter[59], CharConverter[60], CharConverter[61], CharConverter[62]},
                { CharConverter[63], CharConverter[64], CharConverter[65], CharConverter[66], CharConverter[67], CharConverter[68], CharConverter[69], CharConverter[70], CharConverter[71]},
                { CharConverter[72], CharConverter[73], CharConverter[74], CharConverter[75], CharConverter[76], CharConverter[77], CharConverter[78], CharConverter[79], CharConverter[80]},
            };

            return _return;
        }

        public async Task<string> GetReturn(string board)
        {
            if (board.Length < 81)
                Solvable = false;

            else
            {
                ReceivedBoard = ConvertString(board);
                if (!CheckBoard())
                    Solvable = false;
            }
            GetSolution();
            return _return;
        }

    }


}
