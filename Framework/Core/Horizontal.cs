using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Framework.Core
{
    public class Horizontal : IMovement
    {
        private int speed;
        private Point boundary;
        private string direction;
        private int offset = 90;

        public Horizontal(int speed, Point boundary, string direction)
        {
            this.speed = speed;
            this.boundary = boundary;
            this.direction = direction;
        }

        public Point move(Point location)
        {
            if((location.X + offset) >= boundary.X)
            {
                direction = "Left";
            }
            else if (location.X + speed <= 0)
            {
                direction = "Right";
            }
            if (direction == "Left")
            {
                location.X -= speed;
            }
            else
            {
                location.X += speed;
            }
            return location;
        }
    }
}
