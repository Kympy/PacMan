using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    internal class MainFunction
    {
        static void Main()
        {
            GameLoop.Instance.Awake();
            GameLoop.Instance.Start();
            GameLoop.Instance.Update();
        }
    }
}
