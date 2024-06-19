using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Framework.Core
{
    public class Keyboard : IMovement
    {
        private int speed;
        private Point boundary;
        private string arrowAction;

        public Keyboard(int speed, Point boundary)
        {
            this.speed = speed;
            this.boundary = boundary;
            arrowAction = null;
        }

        public void keyPressedByUser(Keys keyCode)
        {
            if (keyCode == Keys.Up)
            {
                arrowAction = "Up";
            }
            else if (keyCode == Keys.Down)
            {
                arrowAction = "Down";
            }
            else if (keyCode == Keys.Left)
            {
                arrowAction = "Left";
            }
            else if (keyCode == Keys.Right)
            {
                arrowAction = "Right";
            }
        }

        public Point move(Point location)
        {
            if (arrowAction != null)
            {
                if (arrowAction == "Up")
                {
                    location.Y -= speed;
                }
                else if (arrowAction == "Down")
                {
                    location.Y += speed;
                }
                else if (arrowAction == "Left")
                {
                    location.X -= speed;
                }
                else if (arrowAction == "Right")
                {
                    location.X += speed;
                }
                arrowAction = null;
            }
            return location;
        }
    }
}
