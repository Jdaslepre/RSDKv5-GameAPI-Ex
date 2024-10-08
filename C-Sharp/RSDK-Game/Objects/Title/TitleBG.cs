using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using RSDK;
using static RSDK.EngineAPI;
using static RSDK.GameObject;

namespace CS.GameLogic
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class TitleBG : GameObject.Entity
    {
        // -----------------
        // Enums / Constants
        // -----------------

        public enum Types : ushort
        {
            Mountain1,
            Mountain2,
            Reflection,
            WaterSparkle,
            WingShine,
        }

        // ----------------
        // Static Variables
        // ----------------

        public struct Static
        {
            public GameObject.Static vars;
            public int palTimer;
            public int timer;
            public int angle;
            public SpriteAnimation aniFrames;
        }

        // ----------------
        // Entity Variables
        // ----------------

        public Types type;
        public int timer;
        public RSDK.Animator animator;

        // -------------
        // Entity Events
        // -------------

        public new void Update()
        {
            if (type == Types.WingShine)
            {
                position.y += 0x10000;

                if (++timer == 32)
                {
                    timer = 0;
                    position.y -= 0x200000;
                }
            }
            else
            {
                position.x -= 0x10000;
                if (position.x < -0x800000)
                    position.x += 0x3000000;
            }
        }

        public new static void StaticUpdate()
        {
            sVars->timer += 0x8000;
            sVars->timer &= 0x7FFFFFFF;

            ++sVars->angle;
            sVars->angle &= 0x3FF;

            if (++sVars->palTimer == 6)
            {
                sVars->palTimer = 0;
                Graphics.paletteBank[0].Rotate(140, 143, false);
            }
        }

        public new void Draw()
        {
            Graphics.SetClipBounds(0, 0, 0, screenInfo.size.x, screenInfo.size.y);
            animator.DrawSprite(null, false);
        }

        public new void Create(void* data)
        {
            if (!sceneInfo->inEditor)
            {
                animator.SetAnimation(sVars->aniFrames, (ushort)type, true, 0);

                active = ActiveFlags.ACTIVE_NORMAL;
                visible = 0;
                drawGroup = 1;
                alpha = 0xFF;
                drawFX = DrawFX.FX_FLIP;

                switch (type)
                {
                    case Types.Mountain2: inkEffect = InkEffects.INK_BLEND; break;

                    case Types.Reflection:
                    case Types.WaterSparkle:
                        inkEffect = InkEffects.INK_ADD;
                        alpha = 0x80;
                        break;

                    case Types.WingShine:
                        drawGroup = 4;
                        inkEffect = InkEffects.INK_MASKED;
                        break;
                    default: break;
                }
            }
        }

        public new static void StageLoad()
        {
            sVars->aniFrames.Load("Title/Background.bin", Scopes.SCOPE_STAGE);

            Graphics.paletteBank[0].SetEntry(55, 0x202030);
        }

        public void SetupFX()
        {
            SceneLayer.GetTileLayer(0)->drawGroup[0] = Const.DRAWGROUP_COUNT;
            SceneLayer.GetTileLayer(1)->drawGroup[0] = 0;

            TileLayer* cloudLayer = SceneLayer.GetTileLayer(2);
            cloudLayer->drawGroup[0] = 0;
            cloudLayer->scanlineCallback = &Scanline_Clouds_Unmanaged;

            TileLayer* islandLayer = SceneLayer.GetTileLayer(3);
            islandLayer->drawGroup[0] = 1;
            islandLayer->scanlineCallback = &Scanline_Island_Unmanaged;

            TitleBG* titleBG;
            while (RSDKTable.GetActiveEntities(sVars->vars.classID, (void**)&titleBG))
            {
                titleBG->visible = 1;
            }

            Title3DSprite* title3DSprite;
            while (RSDKTable.GetActiveEntities(sVars->vars.classID, (void**)&title3DSprite))
            {
                title3DSprite->visible = 1;
            }

            /*
            foreach (var titleBG in GameObject.GetEntities<TitleBG>(ForeachTypes.FOR_ALL_ENTITIES))
            {
                titleBG->visible = 1;
            }
            */

            // foreach_all(TitleBG, titleBG) { titleBG->visible = true; }
            // foreach_all(Title3DSprite, title3DSprite) { title3DSprite->visible = true; }

            Graphics.paletteBank[0].SetEntry(55, 0x00FF00);
            Palette.SetPaletteMask(0x00FF00);
            Graphics.SetDrawGroupProperties(2, true, null);
        }

        [UnmanagedCallersOnly]
        public static void Scanline_Clouds_Unmanaged(ScanlineInfo* scanlines)
        {
            Scanline_Clouds(scanlines);
        }

        [UnmanagedCallersOnly]
        public static void Scanline_Island_Unmanaged(ScanlineInfo* scanlines)
        {
            Scanline_Island(scanlines);
        }

        public static void Scanline_Clouds(ScanlineInfo* scanlines)
        {
            Graphics.SetClipBounds(0, 0, 0, screenInfo.size.x, Const.SCREEN_YSIZE / 2);

            int sine = MathRSDK.Sin256(0);
            int cosine = MathRSDK.Cos256(0);

            int off = 0x1000000;
            for (int i = 0xA0; i > 0x20; --i)
            {
                int id = off / (8 * i);
                int sin = sine * id;
                int cos = cosine * id;

                scanlines->deform.x = (-cos >> 7);
                scanlines->deform.y = sin >> 7;
                scanlines->position.x = sin - screenInfo.center.x * (-cos >> 7);
                scanlines->position.y = sVars->timer + 2 * cos - screenInfo.center.x * (sin >> 7);

                off -= 0x4000;
                scanlines++;
            }
        }

        public static void Scanline_Island(ScanlineInfo* scanlines)
        {
            Graphics.SetClipBounds(0, 0, 168, screenInfo.size.x, Const.SCREEN_YSIZE);

            int sine = MathRSDK.Sin1024(-sVars->angle) >> 2;
            int cosine = MathRSDK.Cos1024(-sVars->angle) >> 2;

            ScanlineInfo* scanlinePtr = &scanlines[168];
            for (int i = 16; i < 88; ++i)
            {
                int id = 0xA00000 / (8 * i);
                int sin = sine * id;
                int cos = cosine * id;

                scanlinePtr->deform.y = sin >> 7;
                scanlinePtr->deform.x = -cos >> 7;
                scanlinePtr->position.y = cos - screenInfo.center.x * scanlinePtr->deform.y - 0xA000 * cosine + 0x2000000;
                scanlinePtr->position.x = sin - screenInfo.center.x * scanlinePtr->deform.x - 0xA000 * sine + 0x2000000;
                ++scanlinePtr;
            }
        }

        public new void Serialize() => sVars->vars.EditableVar(VarTypes.VAR_ENUM, "type", (int)Marshal.OffsetOf<TitleBG>("type"));


        //
        // Declare Attribute
        //

        public static Static* sVars = null;
        public static void _Create(void* data) => ((TitleBG*)&sceneInfo->entity)->Create(data);
        public static void _Draw() => ((TitleBG*)&sceneInfo->entity)->Draw();
        public static void _Update() => ((TitleBG*)&sceneInfo->entity)->Update();
        public static void _LateUpdate() => ((TitleBG*)&sceneInfo->entity)->LateUpdate();
    }
}
