using System.Text;

namespace AdventureGame.core
{


    public class Maze
    {
        public object[,] maze { get; set; }
        public string wall = "#";
        public Char exit = '@';

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
                            
                        }

                    }
                    else
                    {
                        maze[i, j] = null; 
                        //limit # of walls to lessen likelihood of blockages
                    }
                }
            }
        }

        public Player Player { get; set; }
        public void placeThings(int monsterCnt = 2, int itemCnt = 3, int wallCnt = 8)
        {
            //place player first, then monsters, then items

            var (px, py) = FindEmptyCell();
            var playerObj = new Player(10);        //create instance player
            maze[px, py] = playerObj;              //place player in maze
            Player = playerObj;                    //assign Maze.Player property to instance
            Player.PlayerLocation = (px, py);      //set player location 



            for (int m = 0; m < monsterCnt; m++)
            {
                var (mx, my) = FindEmptyCell();
                var MonstObj = new Monster(10);  //create instance monster
                maze[mx, my] = MonstObj;  //place monster
                Monster.MonsterLocations.Add((mx, my)); //add monster location to list

            }


            for (int k = 0; k < itemCnt; k++)
            {
                var (ix, iy) = FindEmptyCell();
                var PotionObj = new Items.Potion();  //create instance potion
                maze[ix, iy] = new Items.Potion();
                Items.Potion.PotionLocations.Add((ix, iy)); //add potion to location list
            }

            for (int w = 0; w < itemCnt; w++)
            {
                var (ix, iy) = FindEmptyCell();
                var WeaponObj = new Items.Weapon();  //create instance weapon
                maze[ix, iy] = new Items.Weapon();
                Items.Weapon.WeaponLocations.Add((ix, iy)); //add weapon to location list
            }

            for (int g = 0; g < wallCnt; g++)
            {
                var (ix, iy) = FindEmptyCell();
                maze[ix, iy] = wall;
            }

            for (int e = 0; e < 1; e++)
            {
                var (ex, ey) = FindEmptyCell();
                maze[ex, ey] = exit;
            }

        }

        public void ReloadMaze()
        {
            //load the maze again with each movement with updated player position and interactions, but same monster/item locations
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (maze[i, j] is string) //keep walls and exit
                        {
                            continue;
                        }
                        else if (maze[i, j] is Player) //update player position
                        {
                            maze[i, j] = null; //clear old position
                            var (px, py) = Player.PlayerLocation;
                            maze[px, py] = Player; //place player in new position
                        }
                        else if (maze[i, j] is Monster) //keep monsters in same place
                        {
                            continue;
                        }
                        else if (maze[i, j] is Items.Potion) //keep potions in same place
                        {
                            continue;
                        }
                        else if (maze[i, j] is Items.Weapon) //keep weapons in same place
                        {
                            continue;
                        }
                        else
                        {
                            maze[i, j] = null; //clear empty cells
                        }
                    }
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

    }
}