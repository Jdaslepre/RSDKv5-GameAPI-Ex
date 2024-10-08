using RSDK;
using static RSDK.EngineAPI;
using System.Runtime.InteropServices;
using System;
using Windows.Media.MediaProperties;
namespace CS.GameLogic
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class BSS_Collectable : GameObject.Entity
    {
        // ----------------
        // Static Variables
        // ----------------

        public struct Static
        {
            public GameObject.Static vars;

            public Animator[] sphereAnimator = new Animator[24];
            public byte initializedTables;
            public fixed int ringScaleTableX[32];
            public fixed int ringScaleTableY[32];
            public fixed int medalScaleTable[32];
            public fixed int screenYValues[32];
            public fixed int medalScreenYVals[32];
            public SpriteAnimation aniFrames;
            public SpriteAnimation ringFrames;

            public Static() { }
        }

        // -----------------
        // Enums / Constants
        // -----------------

        public enum Types : int
        {
            BSS_NONE = 0,
            BSS_SPHERE_BLUE = 1,
            BSS_SPHERE_RED = 2,
            BSS_SPHERE_BUMPER = 3,
            BSS_SPHERE_YELLOW = 4,
            BSS_SPHERE_GREEN = 5,
            BSS_SPHERE_PINK = 6,
            BSS_RING = 7,
            BSS_SPAWN_UP = 8,
            BSS_SPAWN_RIGHT = 9,
            BSS_SPAWN_DOWN = 10,
            BSS_SPAWN_LEFT = 11,
            BSS_UNUSED_1 = 12,
            BSS_UNUSED_2 = 13,
            BSS_UNUSED_3 = 14,
            BSS_RING_SPARKLE = 15,
            BSS_EMERALD_CHAOS = 16,
            BSS_EMERALD_SUPER = 17,
            BSS_MEDAL_SILVER = 18,
            BSS_MEDAL_GOLD = 19,
            BSS_UNUSED_4 = 20,
            BSS_UNUSED_5 = 21,
            BSS_UNUSED_6 = 22,
            BSS_UNUSED_7 = 23,

            BSS_SPHERE_GREEN_STOOD = 0x80 | 1,
            BSS_BLUE_STOOD = 0x80 | 2,
            BSS_SPHERE_PINK_STOOD = 0x80 | 6,
        }

        Types type;
        Animator animator;

        // ----------------
        // Entity Variables
        // ----------------

        // -------------
        // Entity Events
        // -------------

        public new void Update() { }
        public new void LateUpdate() { }

        public new static void StaticUpdate()
        {
            sVars->sphereAnimator[(int)Types.BSS_RING].Process();
            sVars->sphereAnimator[(int)Types.BSS_RING_SPARKLE].Process();
            sVars->sphereAnimator[(int)Types.BSS_MEDAL_SILVER].Process();
            sVars->sphereAnimator[(int)Types.BSS_MEDAL_GOLD].Process();
        }

        public new void Draw()
        {
            /*
            Vector2 drawPos;

            switch (type)
            {
                case Types.BSS_RING:
                    drawFX = FX_FLIP | FX_SCALE;
                    scale.x = sVars->ringScaleTableX[animator.frameID];
                    scale.y = sVars->ringScaleTableY[animator.frameID];
                    direction = (sVars->sphereAnimator[(int)type].frameID > 8) ? (byte)1 : (byte)0;
                    drawPos.x = position.x;
                    drawPos.y = position.y;
                    drawPos.y -= sVars->screenYValues[animator.frameID];
                    sVars->sphereAnimator[(int)type].DrawSprite(&drawPos, 1);
                    
                    drawFX = FX_NONE;
                    return;

                case Types.BSS_RING_SPARKLE: RSDK.DrawSprite(&BSS_Collectable->sphereAnimator[self->type], NULL, true); break;

                case Types.BSS_EMERALD_CHAOS:
                case Types.BSS_EMERALD_SUPER:
                    BSS_Collectable->sphereAnimator[self->type].frameID = self->animator.frameID >> 1;
                    RSDK.DrawSprite(&BSS_Collectable->sphereAnimator[self->type], NULL, true);
                    break;

                case Types.BSS_MEDAL_SILVER:
                case Types.BSS_MEDAL_GOLD:
                    self->drawFX = FX_SCALE;
                    self->scale.x = BSS_Collectable->medalScaleTable[self->animator.frameID];
                    self->scale.y = BSS_Collectable->medalScaleTable[self->animator.frameID];
                    drawPos.x = self->position.x;
                    drawPos.y = self->position.y;
                    drawPos.y -= BSS_Collectable->screenYValues[self->animator.frameID];
                    RSDK.DrawSprite(&BSS_Collectable->sphereAnimator[self->type], &drawPos, true);

                    self->drawFX = FX_NONE;
                    break;

                case Types.BSS_SPHERE_GREEN_STOOD:
                    BSS_Collectable->sphereAnimator[BSS_SPHERE_GREEN].frameID = self->animator.frameID;
                    self->alpha = 0x80;
                    self->inkEffect = INK_ALPHA;
                    RSDK.DrawSprite(&BSS_Collectable->sphereAnimator[BSS_SPHERE_GREEN], NULL, true);

                    self->inkEffect = INK_NONE;
                    break;

                case Types.BSS_BLUE_STOOD:
                    BSS_Collectable->sphereAnimator[BSS_SPHERE_BLUE].frameID = self->animator.frameID;
                    self->alpha = 0x80;
                    self->inkEffect = INK_ALPHA;
                    RSDK.DrawSprite(&BSS_Collectable->sphereAnimator[BSS_SPHERE_BLUE], NULL, true);

                    self->inkEffect = INK_NONE;
                    break;

                case Types.BSS_SPHERE_PINK_STOOD:
                    BSS_Collectable->sphereAnimator[BSS_SPHERE_PINK].frameID = self->animator.frameID;
                    self->alpha = 0x80;
                    self->inkEffect = INK_ALPHA;
                    RSDK.DrawSprite(&BSS_Collectable->sphereAnimator[BSS_SPHERE_PINK], NULL, true);

                    self->inkEffect = INK_NONE;
                    break;

                default:
                    BSS_Collectable->sphereAnimator[self->type].frameID = self->animator.frameID;
                    RSDK.DrawSprite(&BSS_Collectable->sphereAnimator[self->type], NULL, true);
                    break;
            }
            */
        }

        public new void Create(void* data)
        {
            if (!sceneInfo->inEditor)
            {
                active = ActiveFlags.ACTIVE_NORMAL;
                visible = 1;
                drawGroup = 3;
                updateRange.x = MathRSDK.TO_FIXED(128);
                updateRange.y = MathRSDK.TO_FIXED(128);

                for (ushort i = 0; i < 8; ++i) sVars->sphereAnimator[i + 1].SetAnimation(sVars->aniFrames, i, 1, 0);

                sVars->sphereAnimator[(int)Types.BSS_RING].SetAnimation(sVars->ringFrames, 0, 1, 0);
                sVars->sphereAnimator[(int)Types.BSS_RING_SPARKLE].SetAnimation(sVars->ringFrames, 1, 1, 0);
                sVars->sphereAnimator[(int)Types.BSS_EMERALD_CHAOS].SetAnimation(sVars->aniFrames, 6, 1, 0);
                sVars->sphereAnimator[(int)Types.BSS_EMERALD_SUPER].SetAnimation(sVars->aniFrames, 7, 1, 0);
                sVars->sphereAnimator[(int)Types.BSS_MEDAL_SILVER].SetAnimation(sVars->aniFrames, 8, 1, 0);
                sVars->sphereAnimator[(int)Types.BSS_MEDAL_GOLD].SetAnimation(sVars->aniFrames, 9, 1, 0);
            }
        }

        public new static void StageLoad()
        {
            sVars->aniFrames.Load("SpecialBS/StageObjects.bin", Scopes.SCOPE_STAGE);
            sVars->ringFrames.Load("SpecialBS/Ring.bin", Scopes.SCOPE_STAGE);

            if (sVars->initializedTables == 0)
            {
                sVars->initializedTables = 1;

                int id = 0x20;
                for (int i = 0; i < 0x20; ++i)
                {
                    sVars->ringScaleTableX[i] *= 14;
                    sVars->ringScaleTableY[i] *= 14;
                    sVars->medalScaleTable[i] *= 16;
                    sVars->screenYValues[i] = id * (sVars->ringScaleTableY[i] << 6);
                    sVars->medalScreenYVals[i] = id * (sVars->medalScaleTable[i] << 6);

                    int scale = i * (sVars->ringScaleTableY[i] - sVars->ringScaleTableX[i]);
                    int scaleX = sVars->ringScaleTableX[i];
                    sVars->ringScaleTableY[i] = scaleX + (scale >> 5);

                    --id;
                }
            }
        }

        //
        // Declare Attribute
        //

        public static Static* sVars = null;
        public static void _Create(void* data) => ((BSS_Collectable*)&sceneInfo->entity)->Create(data);
        public static void _Draw() => ((BSS_Collectable*)&sceneInfo->entity)->Draw();
        public static void _Update() => ((BSS_Collectable*)&sceneInfo->entity)->Update();
        public static void _LateUpdate() => ((BSS_Collectable*)&sceneInfo->entity)->LateUpdate();
    }
}
