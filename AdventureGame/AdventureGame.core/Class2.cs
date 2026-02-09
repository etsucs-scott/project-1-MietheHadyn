using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureGame.core
{
    public abstract class Items
    {
        public abstract void Use(Player player);
        public Items()
        {

        }
        public class Potion : Items
        {
            public override void Use(Player player) //use upon contact
            {
                player.Heal();
            }

            public override string ToString()
            {
                return $"p";
            }
        }
        public class Weapon : Items
        {
            public override void Use(Player player) //use upon contact
            {
                player.Atk *=5;
                Console.WriteLine("Player picked up a new weapon. Your attack has increased!");
            }

            public override string ToString()
            {
                return $"t";
            }
        }
    }
}
