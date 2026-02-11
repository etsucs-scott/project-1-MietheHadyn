using System.Text;

namespace AdventureGame.core
{
    public enum CellType  //come back to change the enum into something like a string or char, but for now this'll do
    {
        Empty = 0,
        Wall = 1,
        Player = 2,
        Monster = 3,
        Item = 4,
        Exit = 8
    }

    public class Maze
    {
        public int[,] maze { get; set; }
        private Random rand = new Random();
        public Maze()
        {
            maze = new int[10, 10];

        }



        public void MazeGenerator(int Wallcnt = 6)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || i == 9 || j == 0 || j == 9)
                    {
                        for (int w = 0; w < Wallcnt; w++)
                        {
                            maze[i, j] = (int)CellType.Wall; //wall limit # of walls to lessen likelihood of blockages
                            //doesn't seem to be working, ask teacher
                        }

                    }
                    else
                    {
                        maze[i, j] = rand.Next(0, 2); //randomly places walls inside maze
                    }
                }
            }
        }

        public void placeThings(int monsterCnt = 2, int itemCnt = 3)
        {
            //place player first, then monsters, then items

            var (px, py) = FindEmptyCell();
            maze[px, py] = (int)CellType.Player;


            for (int m = 0; m < monsterCnt; m++)
            {
                var (mx, my) = FindEmptyCell();
                maze[mx, my] = (int)CellType.Monster;
            }


            for (int k = 0; k < itemCnt; k++)
            {
                var (ix, iy) = FindEmptyCell();
                maze[ix, iy] = (int)CellType.Item;
            }

            for (int e = 0; e < 1; e++)
            {
                var (ex, ey) = FindEmptyCell();
                maze[ex, ey] = (int)CellType.Exit;
            }

        }

        private (int, int) FindEmptyCell()
        {
            int x, y;
            do
            {
                x = rand.Next(1, 9); //avoid edges
                y = rand.Next(1, 9);
            } while (maze[x, y] != (int)CellType.Empty); //keep looking until empty cell
            return (x, y);
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    sb.Append(maze[i, j]);
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}