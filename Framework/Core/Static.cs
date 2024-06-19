using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Framework.Core
{
    public class Static : IMovement
    {
        public Point move(Point location)
        {
            return location;
        }
    }
}
