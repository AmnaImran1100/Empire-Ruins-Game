using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Enums;

namespace Framework.Core
{
    public interface ICollisionAction
    {
        void performAction(IGame game, ObjectTypes type, GameObject source1, GameObject source2);
    }
}
