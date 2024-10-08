using static RSDK.EngineAPI;

namespace RSDK
{
    public unsafe struct Animator
    {
        public enum RotationSyles { RotateNone, RotateFull, Rotate45Deg, Rotate90Deg, Rotate180Deg, RotateStaticFrames };

        RSDK.SpriteFrame* frames;
        public int frameID;
        public ushort animationID;
        public ushort prevAnimationID;
        public ushort speed;
        public ushort timer;
        public ushort frameDuration;
        public ushort frameCount;
        public sbyte loopIndex;
        public byte rotationStyle;

#if RETRO_MOD_LOADER_VER_2
        public void SetAnimation(RSDK.SpriteAnimation spriteAni, ushort listID, bool32 forceApply, int frameID)
        {
            fixed (RSDK.Animator* ani = &this) RSDKTable.SetSpriteAnimation(spriteAni.aniFrames, listID, ani, forceApply, frameID);
        }
        public void SetAnimation(RSDK.SpriteAnimation* spriteAni, ushort listID, bool32 forceApply, int frameID)
        {
            fixed (RSDK.Animator* ani = &this)
            {
                RSDKTable.SetSpriteAnimation(spriteAni != null ? spriteAni->aniFrames : unchecked((ushort)-1), listID, ani, forceApply, frameID);
            }
        }
#else
        public void SetAnimation(RSDK.SpriteAnimation spriteAni, ushort listID, bool32 forceApply, ushort frameID)
        {
            fixed (RSDK.Animator* ani = &this) RSDKTable.SetSpriteAnimation(spriteAni.aniFrames, listID, a, forceApply, frameID);
        }
        public void SetAnimation(SpriteAnimation* spriteAni, ushort listID, bool32 forceApply, ushort frameID)
        {
            fixed (RSDK.Animator* ani = &this)
            {
                RSDKTable.SetSpriteAnimation(spriteAni != null ? spriteAni->aniFrames : unchecked((ushort)-1), listID, ani, forceApply, frameID);
            }
        }
#endif

        public void SetAnimation(RSDK.Mesh mesh, short speed, byte loopIndex, bool32 forceApply, short frameID)
        {
            fixed (RSDK.Animator* ani = &this) RSDKTable.SetModelAnimation(mesh.id, ani, speed, loopIndex, forceApply, frameID);
        }
        public void SetAnimation(RSDK.Mesh* mesh, short speed, byte loopIndex, bool32 forceApply, short frameID)
        {
            fixed (RSDK.Animator* ani = &this)
            {
                RSDKTable.SetModelAnimation(mesh != null ? mesh->id : unchecked((ushort)-1), ani, speed, loopIndex, forceApply, frameID);
            }
        }

        public void Process() { fixed (RSDK.Animator* ani = &this) RSDKTable.ProcessAnimation(ani); }
        public int GetFrameID() { fixed (RSDK.Animator* ani = &this) return RSDKTable.GetFrameID(ani); }
        public RSDK.Hitbox* GetHitbox(byte id) { fixed (RSDK.Animator* ani = &this) return RSDKTable.GetHitbox(ani, id); }
        public RSDK.SpriteFrame* GetFrame(RSDK.SpriteAnimation spriteAni) { return spriteAni.GetFrame(animationID, frameID); }
        public void DrawSprite(RSDK.Vector2* drawPos, bool32 screenRelative) { fixed (RSDK.Animator* ani = &this) RSDKTable.DrawSprite(ani, drawPos, screenRelative); }
        public void DrawString(RSDK.Vector2* position, RSDK.String* @string, int endFrame, int textLength, int align, int spacing, RSDK.Vector2* charOffsets, bool32 screenRelative)
        {
            fixed (RSDK.Animator* ani = &this) RSDKTable.DrawText(ani, position, @string, endFrame, textLength, align, spacing, null, charOffsets, screenRelative);
        }

#if RETRO_REV0U
        public void DrawTiles(ushort tileID) { fixed (RSDK.Animator* ani = &this) RSDKTable.DrawDynamicAniTiles(ani, tileID); }
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
        public void DrawTiles(ushort tileID) { fixed (RSDK.Animator* ani = &this) modTable.DrawDynamicAniTiles(ani, tileID); }
#endif
    }
}
