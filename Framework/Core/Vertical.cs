using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Framework.Core
{
    public class Vertical : IMovement
    {
        private int speed;
        private Point boundary;
        private string direction;
        private int offset = 90;

        public Vertical(int speed, Point boundary, string direction)
        {
            this.speed = speed;
            this.boundary = boundary;
            this.direction = direction;
        }

        public Point move(Point location)
        {
            if ((location.Y + offset) >= boundary.Y)
            {
                direction = "Up";
            }
            else if (location.Y + speed <= 0)
            {
                direction = "Down";
            }
            if (direction == "Up")
            {
                location.Y -= speed;
            }
            else
            {
                location.Y += speed;
            }
            return location;
        }
    }
}
