namespace AdventureGame.core
{
    public class ICharacter
    {
        int health = 0;
        int atk = 20;

        public ICharacter(int atk)
        {
            this.atk = atk;
        }

        public class Player : ICharacter
        {
            public Player(int atk) : base(atk)
            {
                this.health = 100;
                this.atk = atk;
                
            }
            
        }
        public class Monster : ICharacter
        {
            public Monster(int atk) : base(atk) //BUT IT'S NOT A METHOD
            {
                this.health = 40;
                this.atk = 10;
            }
        }

        
    }
}
