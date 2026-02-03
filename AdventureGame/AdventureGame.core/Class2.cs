using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureGame.core
{
    public class Items
    {
    public Items()
        {

        }
        public class Potion : Items
        {
            public void Use(ICharacter.Player player)
            {
                player.IncreaseHealth(20);
            }


            public class Weapon : Items
        {
            int atkMulti = 10;
        }
    }
}
