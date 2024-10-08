using System;
using System.Runtime.InteropServices;
using RSDK;
using static RSDK.EngineAPI;

namespace CS.GameLogic
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Title3DSprite : GameObject.Entity
    {
        // -----------------
        // Enums / Constants
        // -----------------

        public enum Title3DSpriteFrames
        {
            TITLE3DSPRITE_MOUNTAIN_L,
            TITLE3DSPRITE_MOUNTAIN_M,
            TITLE3DSPRITE_MOUNTAIN_S,
            TITLE3DSPRITE_TREE,
            TITLE3DSPRITE_BUSH,
        }

        // ----------------
        // Static Variables
        // ----------------

        public struct Static
        {
            public GameObject.Static vars;
            public int islandSize;
            public int height;
            public int baseDepth;
            public RSDK.SpriteAnimation aniFrames;
        }

        // ----------------
        // Entity Variables
        // ----------------

        public int frame;
        public RSDK.Vector2 relativePos;
        public RSDK.Animator animator;

        // -------------
        // Entity Events
        // -------------

        public new void Update() 
        {
            relativePos.x = (-((position.y >> 8) * MathRSDK.Sin1024(TitleBG.sVars->angle)) - (position.x >> 8) * MathRSDK.Cos1024(TitleBG.sVars->angle)) >> 10;
            relativePos.y = (+((position.y >> 8) * MathRSDK.Cos1024(TitleBG.sVars->angle)) - (position.x >> 8) * MathRSDK.Sin1024(TitleBG.sVars->angle)) >> 10;

            zdepth = relativePos.y;
        }

        public new void Draw() 
        {
            int depth = zdepth + sVars->baseDepth;
            if (depth != 0 && depth >= 0x100)
            {
                scale.x = Math.Min(0x18000 * sVars->islandSize / depth, 0x200);
                scale.y = scale.x;

                Vector2 drawPos;
                drawPos.x = (sVars->islandSize * relativePos.x / depth + screenInfo.center.x) << 16;
                drawPos.y = (sVars->islandSize * sVars->height / depth + 152) << 16;
                animator.DrawSprite(&drawPos, true);
            }
        }

        public new void Create(void* data)
        {
            if (!sceneInfo->inEditor)
            {
                animator.SetAnimation(sVars->aniFrames, 5, 1, frame);
                position.x -= 0x2000000;
                position.y -= 0x2000000;
                active = ActiveFlags.ACTIVE_NORMAL;
                visible = 0;
                drawGroup = 2;
                drawFX = DrawFX.FX_NONE;
            }
        }

        public new static void StageLoad()
        {
            sVars->aniFrames.Load("Title/Background.bin", Scopes.SCOPE_STAGE);

            sVars->islandSize = 0x90;
            sVars->height = 0x2800;
            sVars->baseDepth = 0xA000;
        }

        public new void Serialize() 
        {
            sVars->vars.EditableVar(VarTypes.VAR_ENUM, "frame", (int)Marshal.OffsetOf<Title3DSprite>("frame"));
            // RSDK_EDITABLE_VAR(Title3DSprite, VAR_ENUM, frame);
        }

        //
        // Declare Attribute
        //

        public static Static* sVars = null;
        public static void _Create(void* data) => ((Title3DSprite*)&sceneInfo->entity)->Create(data);
        public static void _Draw() => ((Title3DSprite*)&sceneInfo->entity)->Draw();
        public static void _Update() => ((Title3DSprite*)&sceneInfo->entity)->Update();
        public static void _LateUpdate() => ((Title3DSprite*)&sceneInfo->entity)->LateUpdate();
    }
}
