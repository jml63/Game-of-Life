using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Game
    {
        //properties
        private const int ROWS = 16;
        private const int COLS = 20;
        private int GEN = 0;
        private int[,] board_current = new int[ROWS, COLS];
        private int[,] board_next = new int[ROWS, COLS];

        //main loop
        public Game()
        {
            Setup_Game();

            for (int c = 0; c < 150; c++)
            {
                Draw();
                System.Threading.Thread.Sleep(50);
                Console.Clear();
            }

            Console.ReadKey();
        }

        //count neighboring cells
        public int Count_Neighbors(int x, int y)
        {
            int sum = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    sum += board_current[(x + i + ROWS) % ROWS, (y + j + COLS) % COLS];
                }
            }
            sum -= board_current[x, y];
            return sum;
        }

        //get cell states and set next board
        public void Check_Board()
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int state = board_current[i,j];
                    int neighbors = Count_Neighbors(i, j);

                    if (state == 0 && neighbors == 3)
                    {
                        board_next[i,j] = 1;
                    }
                    else if (state == 1 && (neighbors < 2 || neighbors > 3))
                    {
                        board_next[i,j] = 0;
                    }
                    else
                    {
                        board_next[i,j] = state;
                    }
                }
            }

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    board_current[i,j] = board_next[i,j];
                }
            }
        }

        //initialise board
        public void Setup_Game()
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    board_current[i,j] = 0;
                    board_next[i,j] = 0;
                }
            }

            //glider
            board_current[0,1] = 1;
            board_current[1, 2] = 1;
            board_current[2,0] = 1;
            board_current[2,1] = 1;
            board_current[2,2] = 1;
        }

        //draw the board
        public void Draw()
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    Console.Write(board_current[i,j] + " ");
                }
                Console.WriteLine();
            }

            Console.Write(GEN++);
            Check_Board();
        }
    }
}
