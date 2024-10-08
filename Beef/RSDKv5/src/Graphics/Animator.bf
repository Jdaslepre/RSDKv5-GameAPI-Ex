namespace RSDK;

public struct Animator
{
    public enum RotationSyles
    {
        ROTSTYLE_NONE,
        ROTSTYLE_FULL,
        ROTSTYLE_45DEG,
        ROTSTYLE_90DEG,
        ROTSTYLE_180DEG,
        ROTSTYLE_STATICFRAMES,
    };

    public RSDK.SpriteFrame* frames;
    public int32 frameID;
    public int16 animationID;
    public int16 prevAnimationID;
    public int16 speed;
    public int16 timer;
    public int16 frameDuration;
    public int16 frameCount;
    public uint8 loopIndex;
    public uint8 rotationStyle;

#if RETRO_MOD_LOADER_VER_2
    public void SetAnimation(RSDK.SpriteAnimation spriteAni, uint16 listID, bool32 forceApply, int32 frameID) mut
    {
#else
    public void SetAnimation(RSDK.SpriteAnimation spriteAni, uint16 listID, bool32 forceApply, int16 frameID) mut
    {
#endif
        RSDKTable.SetSpriteAnimation(spriteAni.aniFrames, listID, &this, forceApply, frameID);
    }

#if RETRO_MOD_LOADER_VER_2
    public void SetAnimation(RSDK.SpriteAnimation *spriteAni, uint16 listID, uint32 forceApply, int32 frameID) mut
    {
#else
    public void SetAnimation(RSDK.SpriteAnimation* spriteAni, uint16 listID, uint32 forceApply, int16 frameID) mut
    {
#endif
        RSDKTable.SetSpriteAnimation(spriteAni != null ? spriteAni.aniFrames : (.)(-1), listID, &this, forceApply, frameID);
    }

    public void SetAnimation(RSDK.Mesh mesh, int16 speed, uint8 loopIndex, bool32 forceApply, int16 frameID) mut => RSDKTable.SetModelAnimation(mesh.id, &this, speed, loopIndex, forceApply, frameID);
    public void SetAnimation(RSDK.Mesh* mesh, int16 speed, uint8 loopIndex, bool32 forceApply, int16 frameID) mut => RSDKTable.SetModelAnimation(mesh != null ? mesh.id : (.)(-1), &this, speed, loopIndex, forceApply, frameID);

    public void Process() mut => RSDKTable.ProcessAnimation(&this);

    public int32 GetFrameID() mut { return RSDKTable.GetFrameID(&this); }
    public RSDK.Hitbox* GetHitbox(uint8 id) mut { return RSDKTable.GetHitbox(&this, id); }
    public RSDK.SpriteFrame* GetFrame(RSDK.SpriteAnimation aniFrames) { return aniFrames.GetFrame(animationID, frameID); }

    public void DrawSprite(RSDK.Vector2* position, bool32 screenRelative) mut => RSDKTable.DrawSprite(&this, position, screenRelative);
    public void DrawString(RSDK.Vector2* position, RSDK.String* string, int32 endFrame, int32 textLength, int32 align, int32 spacing, Vector2* charOffsets, bool screenRelative) mut => RSDKTable.DrawText(&this, position, string, endFrame, textLength, align, spacing, null, charOffsets, screenRelative);

#if RETRO_REV0U
    public void DrawAniTiles(uint16 tileID) mut => RSDKTable.DrawDynamicAniTiles(&this, tileID);
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
    public void DrawAniTiles(uint16 tileID) mut => modTable.DrawDynamicAniTiles(&this, tileID);
#endif
}