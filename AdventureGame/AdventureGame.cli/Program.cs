using AdventureGame.core;

// See https://aka.ms/new-console-template for more information



ActivePlay();


void Start()
{
    //intro text/lore??

    Maze mazeInstance = new Maze();
    mazeInstance.MazeGenerator();
    mazeInstance.placeThings();
    Console.WriteLine(mazeInstance); //this and prev 3 lines create maze first


}

void ActivePlay()
{
    //main game loop, call start and then handle input until player wins/loses
    Maze mazeInstance = new Maze();
    mazeInstance.MazeGenerator();
    mazeInstance.placeThings();
    Console.WriteLine(mazeInstance);
    while (mazeInstance.Player.Health > 0)
    {
        MovePlayer(mazeInstance.Player, Console.ReadLine(), mazeInstance);
        Interact(mazeInstance.Player, mazeInstance);
        Console.WriteLine(mazeInstance);

    }

}
//write a method for the turn based combat system with a while loop that continues until either the player or monster's health reaches 0
static void Combat(Player player, Monster monster)
{
    Console.WriteLine("COMBAT START!!");

    //monster turn
    Console.WriteLine("---Monster turn: ");
    monster.Attack(player);
    Console.WriteLine($"Monster attacks for {monster.Atk}!");
    Console.WriteLine($"Player health is now at {player.Health}");

    //player turn
    while (player.Health > 0 && monster.Health > 0)
    {
        Console.WriteLine("---Player Turn: (1)Attack or (2)Heal?");
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

       
    }



}

void MovePlayer(Player player, string input, Maze maze)
{
    switch (input.ToLower())
    {
        case "w":
            player.NewLocation = (player.PlayerLocation.Item1 - 1, player.PlayerLocation.Item2); //up
            break;
        case "s":
            player.NewLocation = (player.PlayerLocation.Item1 + 1, player.PlayerLocation.Item2); //down
            break;
        case "a":
            player.NewLocation = (player.PlayerLocation.Item1, player.PlayerLocation.Item2 - 1); //left
            break;
        case "d":
            player.NewLocation = (player.PlayerLocation.Item1, player.PlayerLocation.Item2 + 1); //right
            break;
        default:
            Console.WriteLine("Invalid input! Use W/A/S/D to move.");
            return;
    }

    // Check bounds and walls
    int newX = player.NewLocation.Item1;
    int newY = player.NewLocation.Item2;
    int px = player.PlayerLocation.Item1;
    int py = player.PlayerLocation.Item2;

    if (newX < 0 || newX >= 10 || newY < 0 || newY >= 10 || maze.maze[newX, newY] is string)
    {
        Console.WriteLine("Cannot move there!");
        return;
    }

    
   
    maze.maze[px, py] = null; // clear old position ; don't override monster/potion/weapon/exit
    player.PlayerLocation = (newX, newY);
}

void Interact(Player player, Maze maze)
{
    var (px, py) = player.PlayerLocation;
    var tile = maze.maze[px, py];

    //Monster
    if (tile is Monster monster)
    {
        Combat(player, monster);

        //If player win
        if (monster.Health <= 0 && player.Health > 0)
        {
            maze.maze[px, py] = player;
        }
        
        return;
    }

    //Potion
    if (tile is Items.Potion)
    {
        player.Heal();
        maze.maze[px, py] = player; 
        return;
    }

    // Weapon
    if (tile is Items.Weapon weapon)
    {
        weapon.Use(player);
        maze.maze[px, py] = player; 
        return;
    }

    // Exit (string tile)
    if (tile is char exit)
    {
        Console.WriteLine("Congratulations! You win!");
        Environment.Exit(0);
        return;
    }

    //Empty/other tile: place player
    if (tile == null)
    {
        maze.maze[px, py] = player;
    }
}