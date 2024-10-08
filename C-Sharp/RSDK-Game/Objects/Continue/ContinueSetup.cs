using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using RSDK;
using static RSDK.EngineAPI;

namespace CS.GameLogic
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class ContinueSetup : GameObject.Entity
    {
        // -----------------
        // Enums / Constants
        // -----------------


        // ----------------
        // Static Variables
        // ----------------

        public struct Static
        {
            public GameObject.Static vars;
            public Animator animator;
            public SoundFX sfxAccept;
            public Mesh[] countIndex = new Mesh[10];
            public Scene3D sceneIndex;

            public Static() { }
        }

        // ----------------
        // Entity Variables
        // ----------------

        public StateMachine state;
        public int timer;
        public int secondTimer;
        public int countTimer;
        public int rotationX;
        public int unused1;
        public int numberColor;
        public bool32 showContinues;
        public Matrix matTemp;
        public Matrix matTranslate;
        public Matrix matRotateX;
        public Matrix matRotateY;
        public Matrix matFinal;

        // -------------
        // Entity Events
        // -------------

        public new void Update()
        {
            state.Run();

            angle = (angle - 2) & 0x3FF;
        }

        public new void Draw()
        {
            sVars->sceneIndex.Prepare();

            matTranslate.TranslateXYZ(0, -0xF0000, 0x500000);
            matRotateX.RotateX(rotationX);
            matRotateY.RotateZ(angle);

            // Number 1 (tens)
            matTemp.TranslateXYZ(-0x120000, 0, 0);
            Matrix.Multiply(ref matFinal, ref matRotateY, ref matRotateX);
            Matrix.Multiply(ref matFinal, ref matTemp, ref matFinal);
            Matrix.Multiply(ref matFinal, ref matFinal, ref matTranslate);
            sVars->sceneIndex.AddModel(sVars->countIndex[countTimer / 10 % 10], Scene3D.DrawTypes.DRAWTYPE_SOLIDCOLOR_SHADED_BLENDED_SCREEN, ref matFinal, ref matFinal, (uint)numberColor);

            // Number 2 (single digits)
            matTemp.TranslateXYZ(0x120000, 0, 0);
            Matrix.Multiply(ref matFinal, ref matRotateY, ref matRotateX);
            Matrix.Multiply(ref matFinal, ref matTemp, ref matFinal);
            Matrix.Multiply(ref matFinal, ref matFinal, ref matTranslate);
            sVars->sceneIndex.AddModel(sVars->countIndex[countTimer % 10], Scene3D.DrawTypes.DRAWTYPE_SOLIDCOLOR_SHADED_BLENDED_SCREEN, ref matFinal, ref matFinal, (uint)numberColor);
            sVars->sceneIndex.Draw();
        }

        public new void Create(void* data)
        {
            if (!sceneInfo->inEditor)
            {
                active = ActiveFlags.ACTIVE_NORMAL;
                visible = 1;
                drawGroup = 1;
                rotationX = 240;
                angle = 256;
                countTimer = 10;
                numberColor = 0xFF00FF;
                showContinues = true;
                // state = ContinueSetup_State_FadeIn;
                updateRange.x = 0x4000000;
                updateRange.y = 0x4000000;

                Graphics.paletteBank[1].SetActivePalette(0, screenInfo.size.y);
            }
        }

        public new static void StageLoad()
        {
            string[] paths = {
                "Continue/Count0.bin", "Continue/Count1.bin", "Continue/Count2.bin", "Continue/Count3.bin", "Continue/Count4.bin",
                "Continue/Count5.bin", "Continue/Count6.bin", "Continue/Count7.bin", "Continue/Count8.bin", "Continue/Count9.bin",
            };

            for (int i = 0; i < 10; ++i) sVars->countIndex[i].Load(paths[i], Scopes.SCOPE_STAGE);

            sVars->sceneIndex.Create("View:Continue", 4096, Scopes.SCOPE_STAGE);

            sVars->sceneIndex.SetDiffuseColor(0xA0, 0xA0, 0xA0);
            sVars->sceneIndex.SetDiffuseIntensity(8, 8, 8);
            sVars->sceneIndex.SetSpecularIntensity(15, 15, 15);

            sVars->sfxAccept.Get("Global/MenuAccept.wav");
        }

        public void State_FadeIn()
        {
            if (++timer >= 8)
            {
                timer = 0;
                state.Set(State_HandleCountdown);
            }
        }

        public void State_HandleCountdown()
        {
            if (++secondTimer == 60)
            {
                secondTimer = 0;

                if (countTimer > 0)
                {
                    countTimer--;

                    if (alpha < 0xFF)
                        alpha += 0x18;

                    numberColor = (int)RSDKTable.GetPaletteEntry(2, (byte)alpha);
                }
            }

            if (controllerInfo.keyA.press == 1 || controllerInfo.keyStart.press == 1 || touchInfo.count == 1)
            {

            }
        }

        public new void Serialize() => sVars->vars.EditableVar(VariableTypes.VAR_ENUM, "type", (int)Marshal.OffsetOf<TitleBG>("type"));


        //
        // Declare Attribute
        //

        public static Static* sVars = null;
        public static void _Create(void* data) => ((ContinueSetup*)&sceneInfo->entity)->Create(data);
        public static void _Draw() => ((ContinueSetup*)&sceneInfo->entity)->Draw();
        public static void _Update() => ((ContinueSetup*)&sceneInfo->entity)->Update();
        public static void _LateUpdate() => ((ContinueSetup*)&sceneInfo->entity)->LateUpdate();
    }
}
