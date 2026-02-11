using System.Text;

namespace AdventureGame.core
{


    public class Maze
    {
        public object[,] maze { get; set; }
        public string wall = "#";
        public string exit = "@";

        private Random rand = new Random();
        public Maze()
        {
            maze = new object[10, 10];

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
                            maze[i, j] = wall; //walls
                            //doesn't seem to be working, ask teacher
                        }

                    }
                    else
                    {
                        maze[i, j] = null; //randomly places walls inside maze
                        //limit # of walls to lessen likelihood of blockages
                    }
                }
            }
        }

        public Player Player { get; set; }
        public void placeThings(int monsterCnt = 2, int itemCnt = 3)
        {
            //place player first, then monsters, then items

            var (px, py) = FindEmptyCell();
            maze[px, py] = new Player(10);  //place player 
            Player.PlayerLocation = (px, py); //set player location


            for (int m = 0; m < monsterCnt; m++)
            {
                var (mx, my) = FindEmptyCell();
                maze[mx, my] = new Monster(10);  //place monster
            }


            for (int k = 0; k < itemCnt; k++)
            {
                var (ix, iy) = FindEmptyCell();
                maze[ix, iy] = new Items.Potion();
            }

            for (int w = 0; w < itemCnt; w++)
            {
                var (ix, iy) = FindEmptyCell();
                maze[ix, iy] = new Items.Weapon();
            }

            for (int e = 0; e < 1; e++)
            {
                var (ex, ey) = FindEmptyCell();
                maze[ex, ey] = exit;
            }

        }

        private (int, int) FindEmptyCell()
        {
            int x, y;
            do
            {
                x = rand.Next(1, 9); //avoid edges
                y = rand.Next(1, 9);
            } while (maze[x, y] != null); //keep looking until empty cell
            return (x, y);
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (maze[i, j] is null)
                    {
                        sb.Append("  ");
                        continue;

                    }
                    Type type = maze[i, j].GetType();
                    if (type.Equals(typeof(Player)))
                    {
                        sb.Append((Player)maze[i, j]); //display character
                    }
                    else if (type.Equals(typeof(Monster)))
                    {
                        sb.Append((Monster)maze[i, j]);
                    }
                    else if (type.Equals(typeof(Items.Potion)))
                    {
                        sb.Append((Items.Potion)maze[i, j]);
                    }
                    else if (type.Equals(typeof(Items.Weapon)))
                    {
                        sb.Append((Items.Weapon)maze[i, j]);
                    }
                    else
                    {
                        sb.Append(maze[i, j]); //display character
                    }
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }



        public void MovePlayer(Player player, int dx, int dy)
        {
            //find player position
            int px = -1, py = -1;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (maze[i, j] is Player)
                    {
                        px = i;
                        py = j;
                        break;
                    }
                }
                if (px != -1) break;
            }
        
         //get movement input from player and move them accordingly in console

        

            int newX = px + dx;
            int newY = py + dy;

            //check bounds and walls
            if (newX < 0 || newX >= 10 || newY < 0 || newY >= 10 || maze[newX, newY] is string)
            {
                Console.WriteLine("Cannot move there!");
                return;
            }
            //move player
            maze[px, py] = null; //clear old position
            maze[newX, newY] = player; //move to new position

            //check for interactions
        }
    }
}