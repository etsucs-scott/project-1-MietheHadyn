namespace AdventureGame.core
{

    
    public interface ICharacter
    {
        int Health { get; set;  }
        int Atk { get; set; }
        void Attack(ICharacter target);
        
    }

    public class Player : ICharacter
    {

        public int Health { get; set; } = 100;
        public int Atk { get; set; } = 10;
        new public List<string> Inventory = new List<string>(); //keep track of picked up items
        public Player(int atk) : base()
        {
            this.Health = 100;
            this.Atk = atk;

        }

        public (int, int) PlayerLocation { get; set; }
        public (int, int) NewLocation { get; set; }

        public void Attack(ICharacter target)
        {
            //math for atk damage
            target.Health -= this.Atk;
        }

        public void Heal()
        {
            if (Health < 150)
            {
                Health += 20;
                Inventory.Add("Potion");
                Console.WriteLine($"Player healed 20 health! Current health: {Health}");
                Inventory.Remove("Potion");

            }
            else if (Health == 150)
            {
                Console.WriteLine("Player health at max!, item stored"); 
                Inventory.Add("Potion");
            }
        }
        

        public override string ToString()
        {
            return "!";
        }
    }
    public class Monster : ICharacter
    {
        public int Health { get; set; } = 80;
        public int Atk { get; set; } = 10;
        public static object MonsterLocation { get; internal set; }
        new public static List<object> MonsterLocations = new List<object>(); //track monster locations, make static list; add as monsters spawn

        public Monster(int atk) : base()
        {
            this.Health = 40;
            this.Atk = 10;
        }

        public void Attack(ICharacter target) 
        {
            //math for atk damage
             target.Health -= this.Atk;
        }

        public override string ToString()
        {
            return $"M";
        }
    }


}
