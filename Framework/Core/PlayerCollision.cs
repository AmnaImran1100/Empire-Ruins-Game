using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core;
using Framework.Enums;

namespace Framework.Core
{
    public class PlayerCollision : ICollisionAction
    {
        public void performAction(IGame game, ObjectTypes type, GameObject source1, GameObject source2)
        {
            GameObject player;
            GameObject energizer;
            GameObject wall;

            if (ObjectTypes.shootingEnemy == type || ObjectTypes.enemy == type)
            {
                if (source1.Type == ObjectTypes.player)
                {
                    player = source1;
                }
                else
                {
                    player = source2;
                }
                game.raisePlayerHealthDecAction(player.H);
            }

            if (ObjectTypes.wall == type)
            {
                if (source1.Type == ObjectTypes.player)
                {
                    wall = source2;
                }
                else
                {
                    wall = source1;
                }
                game.raisePlayerTouchesWall(wall.Pb);
            }

            if (ObjectTypes.energizer == type)
            {
                if (source1.Type == ObjectTypes.player)
                { 
                    energizer = source2;
                }
                else
                {
                    energizer = source1;
                }
                game.raisePlayerTouchesEnergizer(energizer.Pb);
            }
           
        }
    }
}
