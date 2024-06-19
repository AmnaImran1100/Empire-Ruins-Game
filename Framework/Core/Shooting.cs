using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Framework.Core
{
    public class Shooting : IMovement
    {
        private int speed;
        private Point boundary;
        private string direction;

        public Shooting(int speed, Point boundary, string direction)
        {
            this.speed = speed;
            this.boundary = boundary;
            this.direction = direction;
        }

        public Point move(Point location)
        {
            if(direction == "Right")
            {
                location.X += speed;
            }
            else if (direction == "Left")
            {
                location.X -= speed;
            }
            else if (direction == "Up")
            {
                location.Y -= speed;
            }
            else if (direction == "Down")
            {
                location.Y += speed;
            }
            return location;
        }
    }
}
