namespace RSDK;

public enum RotationStyles
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
    IntPtr frames;
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
        RSDKTable.SetSpriteAnimation(spriteAni.id, listID, ref this, forceApply, frameID);
    }

#if RETRO_MOD_LOADER_VER_2
    public void SetAnimation(SpriteAnimation* spriteAni, ushort listID, bool32 forceApply, int frameID)
    {
#else
    public void SetAnimation(SpriteAnimation* spriteAni, ushort listID, bool32 forceApply, int16 frameID)
    {
#endif
        RSDKTable.SetSpriteAnimation(spriteAni != null ? spriteAni->id : unchecked((ushort)-1), listID, ref this, forceApply, frameID);
    }

    public void SetAnimation(Mesh mesh, short speed, byte loopIndex, bool32 forceApply, short frameID)
    {
        RSDKTable.SetModelAnimation(mesh.id, ref this, speed, loopIndex, forceApply, frameID);
    }
    public void SetAnimation(Mesh* mesh, short speed, byte loopIndex, bool32 forceApply, short frameID)
    {
        RSDKTable.SetModelAnimation(mesh != null ? mesh->id : unchecked((ushort)-1), ref this, speed, loopIndex, forceApply, frameID);
    }

    public void Process() => RSDKTable.ProcessAnimation(ref this);
    public int GetFrameID() => RSDKTable.GetFrameID(ref this);
    public Hitbox* GetHitbox(byte id) => RSDKTable.GetHitbox(ref this, id);
    public SpriteFrame* GetFrame(SpriteAnimation spriteAni) => spriteAni.GetFrame(animationID, frameID);
    public void DrawSprite(ref Vector2 drawPos, bool32 screenRelative) => RSDKTable.DrawSprite(ref this, ref drawPos, screenRelative);
    public void DrawString(ref Vector2 position, ref String @string, int endFrame, int textLength, int align, int spacing, ref Vector2 charOffsets, bool32 screenRelative)
    {
        RSDKTable.DrawText(ref this, ref position, ref @string, endFrame, textLength, align, spacing, null, ref charOffsets, screenRelative);
    }

#if RETRO_REV0U
    public void DrawTiles(ushort tileID) => RSDKTable.DrawDynamicAniTiles(ref this, tileID);
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
    public void DrawTiles(ushort tileID) => modTable->DrawDynamicAniTiles(ref this, tileID);
#endif
}
