using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureGame.core
{
    public class Maze
    {
        public int[,] maze { get; set; }
        public Maze()
        {
            maze = new int[10, 10];
            //use a nested for loop
            //if if i=0 wall / if j = 0 wall
            //program walls on all edges, insides ranomized
        }



        public void MazeGenerator()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || i == 9 || j == 0 || j == 9)
                    {
                        maze[i, j] = 1; //wall
                    }
                    else
                    {
                        maze[i, j] = new Random().Next(0, 2); //randomly place walls inside maze
                    }

                }
            }

        }

        public void placeThings()
        {
            //place player, monsters, and items in maze
            //use a while loop to place each thing in a random location until it finds an empty spot (0)
            //place player first, then monsters, then items

        }
        public override string ToString()
        {
            string output = string.Empty;
            for (int i=0; i<10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    output += maze[i, j] + " ";
                }
                output += "\n";
            }

             return output;
        }
    }
}