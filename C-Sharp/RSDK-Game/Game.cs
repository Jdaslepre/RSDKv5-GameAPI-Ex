using CS.GameLogic;
using RSDK;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static RSDK.EngineAPI;

namespace GameLogic
{
    public unsafe class Game
    {
        GlobalVariables.Constructor c = new();
        GlobalVariables* globals = null;

        public static void** registerGlobals = null;
        public static int registerGlobalsSize = 0;

        private static delegate* unmanaged<void*, void> registerGlobalsInitCB = null;

        public static void RegisterGlobals(void** globals, int size, delegate* unmanaged<void*, void> initCB)
        {
            registerGlobals = globals;
            registerGlobalsSize = size;
            registerGlobalsInitCB = initCB;
        }


        [UnmanagedCallersOnly(EntryPoint = "LinkGameLogicDLL")]
        private static void LinkGameLogicDLL(EngineInfo* info)
        {
            InitEngineInfo(info);
            TestInfo(info);

            /*
            if (registerGlobalsInitCB != null)
            {
                IntPtr cb = Marshal.GetFunctionPointerForDelegate(registerGlobalsInitCB);
                RSDKTable.RegisterGlobalVariables(registerGlobals, registerGlobalsSize, cb);
            }
            else
            {
                Dev.Print(0, "registerGlobalsInitCB = null");
            }
            */

            LinkEmbeddedLogic();
        }

        public static void LinkEmbeddedLogic()
        {
            Dev.PrintInt32(Dev.PrintModes.PRINT_NORMAL, "Count", ObjectRegInfo.registerListCount);

            GameObject.Register<BSS_Collectable, BSS_Collectable.Static>(ref BSS_Collectable.sVars);
            GameObject.Register<TitleSeq, TitleSeq.Static>(ref TitleSeq.sVars);

            GameObject.Register<ContinueSetup, ContinueSetup.Static>(ref ContinueSetup.sVars);

            Dev.PrintInt32(Dev.PrintModes.PRINT_NORMAL, "Count", ObjectRegInfo.registerListCount);
        }

        static void TestInfo(EngineInfo* info)
        {
            RSDK.Vector2 vec = new();
            vec.x = (128) << 16;
            vec.y = (64) << 16;


            float f = 2.25f;

            Hitbox hitbox = new();
            hitbox.left = -32;
            hitbox.top = 20;
            hitbox.right = 32;
            hitbox.bottom = -20;

            Dev.Print(0, "PrintLog");
            Dev.PrintText(0, "PrintText");

            Dev.PrintVector2(0, "PrintVector2", vec);
            Dev.PrintVector2(0, "PrintVector2 inl", 20, 40);
            Dev.PrintVector2(0, "PrintVector2 ptr", &vec);

            Dev.PrintInt32(0, "PrintInt32", vec.x);
            Dev.PrintUInt32(0, "PrintUInt32", (UInt32)vec.x);

            Dev.PrintFloat(0, "PrintFloat", f);

            Dev.PrintHitbox(0, "PrintHitbox", hitbox);
            Dev.PrintHitbox(0, "PrintHitbox ptr", &hitbox);

            // StateMachine

            //StateMachine state = new StateMachine(TitleSeq.Example);
            //state.Run();

            //Dev.Print(0, "Copying:");
            //StateMachine stateCpy = new StateMachine(state);
            //stateCpy.Run();
        }

    }
}
