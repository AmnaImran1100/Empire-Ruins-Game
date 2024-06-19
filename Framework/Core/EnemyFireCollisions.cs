using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core;
using Framework.Enums;

namespace Framework.Core
{
    public class EnemyFireCollisions : ICollisionAction
    {
        public void performAction(IGame game, ObjectTypes type, GameObject source1, GameObject source2)
        {
            GameObject enemyFire;
            GameObject player;

            if (ObjectTypes.enemyFire == type)
            {
                if (source1.Type == ObjectTypes.enemyFire)
                {
                    enemyFire = source1;
                }
                else
                {
                    enemyFire = source2;
                }
                game.raiseEnemyFireTouchesPlayer(enemyFire.Pb);
            }
        }
    }
}
