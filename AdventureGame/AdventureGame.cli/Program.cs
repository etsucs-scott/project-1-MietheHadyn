using AdventureGame.core;

// See https://aka.ms/new-console-template for more information



Maze mazeInstance = new Maze();
mazeInstance.MazeGenerator();
mazeInstance.placeThings();
Console.WriteLine(mazeInstance);

//mazeInstance.MovePlayer();
void Main(string[] args)
{
    //intro text/lore??

    
}
//write a method for the turn based combat system with a while loop that continues until either the player or monster's health reaches 0
void Combat(Player player, Monster monster)
{
    Console.WriteLine("COMBAT START!!");
    //player turn
        while (player.Health > 0 && monster.Health > 0)
    { Console.WriteLine("---Player Turn: (1)Attack or (2)Heal?");
        string InputStr = Console.ReadLine();
        if (!int.TryParse(InputStr, out int Input))
        {
            Console.WriteLine("Invalid input. Please enter 1 or 2.");


        }
        if (Input == 1)
        {
            Console.WriteLine(value: $"Player attacks for {player.Atk}!");
            player.Attack(monster);
            Console.WriteLine($"Monster health is now at {monster.Health}");
            //break;
        }
        else if (Input == 2)
        {
            Console.WriteLine("Player heals for 20 health!");
            player.Heal();
            //break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter 1 or 2.");
            //continue;
        }

        if (monster.Health <= 0)
        {
            Console.WriteLine("Monster defeated! You win!");
            //break;
        }
        
        //monster turn
        Console.WriteLine("---Monster turn: ");
        monster.Attack(player);
        Console.WriteLine($"Monster attacks for {monster.Atk}!");
        Console.WriteLine($"Player health is now at {player.Health}");
        }


    
}

 void HandleInput(Player player, string input, Maze maze)
{
    switch (input.ToLower())
    {
        case "w":
            maze.MovePlayer(player, -1, 0); //up
            break;
        case "s":
            maze.MovePlayer(player, 1, 0); //down
            break;
        case "a":
            maze.MovePlayer(player, 0, -1); //left
            break;
        case "d":
            maze.MovePlayer(player, 0, 1); //right
            break;
        default:
            Console.WriteLine("Invalid input! Use W/A/S/D to move.");
            break;
    }
}