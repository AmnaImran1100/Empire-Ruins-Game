using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core;
using Framework.Enums;

namespace Framework.Core
{
    public class PlayerFireCollisions : ICollisionAction
    {
        public void performAction(IGame game, ObjectTypes type, GameObject source1, GameObject source2)
        {
            GameObject playerFire;
            GameObject shootingEnemy;
            GameObject enemy;

            if (ObjectTypes.shootingEnemy == type )
            {
                if (source1.Type == ObjectTypes.playerFire)
                {
                    playerFire = source1;
                    shootingEnemy = source2;
                }
                else
                {
                    playerFire = source2;
                    shootingEnemy = source1;
                }
                game.raisePlayerFireTouchesShootingEnemy(playerFire.Pb);
            }

            if (ObjectTypes.enemy == type)
            {
                if (source1.Type == ObjectTypes.player)
                {
                    playerFire = source1; 
                    enemy = source2;
                }
                else
                {
                    playerFire = source2;
                    enemy = source1;
                }
                game.raisePlayerTouchesEnemy(playerFire.Pb, enemy.H);
            }

        }
    }
}

