using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Framework.Core
{
    public interface IMovement
    {
        Point move(Point location);
    }
}
