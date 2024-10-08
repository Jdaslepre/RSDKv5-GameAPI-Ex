using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public unsafe class GameVariables
    {
        public static GlobalVariables* globals;
    }

    public unsafe struct GlobalVariables
    {
        public struct Constructor
        {
            // public Constructor() { fixed (GlobalVariables** g = &GameVariables.globals) { Game.RegisterGlobals((void**)g, sizeof(GlobalVariables), &GlobalVariables.Init); }  }
        }

        
        public static void Init(void* g) 
        {
            //
        }
    }
}
