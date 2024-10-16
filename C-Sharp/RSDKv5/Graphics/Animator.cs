﻿namespace RSDK;

public enum RotationSyles
{
    ROTSTYLE_NONE,
    ROTSTYLE_FULL,
    ROTSTYLE_45DEG,
    ROTSTYLE_90DEG,
    ROTSTYLE_180DEG,
    ROTSTYLE_STATICFRAMES,
};

public unsafe struct Animator
{
    SpriteFrame* frames;
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
    public void SetAnimation(SpriteAnimation spriteAni, ushort listID, bool32 forceApply, int frameID)
    {
#else
    public void SetAnimation(SpriteAnimation spriteAni, ushort listID, bool32 forceApply, int16 frameID)
    {
#endif
        fixed (Animator* ani = &this) RSDKTable->SetSpriteAnimation(spriteAni.id, listID, ani, forceApply, frameID);
    }

#if RETRO_MOD_LOADER_VER_2
    public void SetAnimation(SpriteAnimation* spriteAni, ushort listID, bool32 forceApply, int frameID)
    {
#else
    public void SetAnimation(SpriteAnimation* spriteAni, ushort listID, bool32 forceApply, int16 frameID)
    {
#endif
        fixed (Animator* ani = &this) RSDKTable->SetSpriteAnimation(spriteAni != null ? spriteAni->id : unchecked((ushort)-1), listID, ani, forceApply, frameID);
    }

    public void SetAnimation(Mesh mesh, short speed, byte loopIndex, bool32 forceApply, short frameID)
    {
        fixed (Animator* ani = &this) RSDKTable->SetModelAnimation(mesh.id, ani, speed, loopIndex, forceApply, frameID);
    }
    public void SetAnimation(Mesh* mesh, short speed, byte loopIndex, bool32 forceApply, short frameID)
    {
        fixed (Animator* ani = &this) RSDKTable->SetModelAnimation(mesh != null ? mesh->id : unchecked((ushort)-1), ani, speed, loopIndex, forceApply, frameID);
    }

    public void Process() { fixed (Animator* ani = &this) RSDKTable->ProcessAnimation(ani); }
    public int GetFrameID() { fixed (Animator* ani = &this) return RSDKTable->GetFrameID(ani); }
    public Hitbox* GetHitbox(byte id) { fixed (Animator* ani = &this) return RSDKTable->GetHitbox(ani, id); }
    public SpriteFrame* GetFrame(SpriteAnimation spriteAni) => spriteAni.GetFrame(animationID, frameID);
    public void DrawSprite(Vector2* drawPos, bool32 screenRelative) { fixed (Animator* ani = &this) RSDKTable->DrawSprite(ani, drawPos, screenRelative); }
    public void DrawString(Vector2* position, String* @string, int endFrame, int textLength, int align, int spacing, Vector2* charOffsets, bool32 screenRelative)
    {
        fixed (Animator* ani = &this) RSDKTable->DrawText(ani, position, @string, endFrame, textLength, align, spacing, null, charOffsets, screenRelative);
    }

#if RETRO_REV0U
    public void DrawTiles(ushort tileID) { fixed (RSDK.Animator* ani = &this) RSDKTable->DrawDynamicAniTiles(ani, tileID); }
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
    public void DrawTiles(ushort tileID) { fixed (RSDK.Animator* ani = &this) modTable->DrawDynamicAniTiles(ani, tileID); }
#endif
}