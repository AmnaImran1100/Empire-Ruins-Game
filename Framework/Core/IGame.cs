using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Framework.Core
{
    public interface IGame
    {
        void raisePlayerHealthDecAction(ProgressBar pb);
        void raisePlayerTouchesWall(PictureBox player);
        void raisePlayerTouchesEnergizer(PictureBox energizer);
        void raisePlayerFireTouchesShootingEnemy(PictureBox playerFire);
        void raisePlayerTouchesEnemy(PictureBox playerFire, ProgressBar enemy);
        void raiseEnemyFireTouchesPlayer(PictureBox enemyFire);
    }
}
