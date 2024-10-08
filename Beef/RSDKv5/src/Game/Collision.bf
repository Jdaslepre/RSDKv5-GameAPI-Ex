using System;

namespace RSDK;

public enum CollisionModes : uint8 { CMODE_FLOOR, CMODE_LWALL, CMODE_ROOF, CMODE_RWALL }

public enum CollisionSides : uint8 { C_NONE, C_TOP, C_LEFT, C_RIGHT, C_BOTTOM }

public struct Hitbox
{
    public int16 left;
    public int16 top;
    public int16 right;
    public int16 bottom;
}

#if RETRO_REV0U || RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
public struct CollisionSensor {
    public RSDK.Vector2 position;
    public bool32 collided;
    public uint8 angle;

#if RETRO_REV0U
    // expects an array of 5 sensors
    public static void SetPathGripSensors(RSDK.CollisionSensor *sensors) => RSDKTable.SetPathGripSensors(sensors);

    public void FindFloorPosition() mut => RSDKTable.FindFloorPosition(&this);
    public void FindLWallPosition() mut => RSDKTable.FindLWallPosition(&this); 
    public void FindRoofPosition() mut => RSDKTable.FindRoofPosition(&this);
    public void FindRWallPosition() mut => RSDKTable.FindRWallPosition(&this);
    public void FloorCollision() mut => RSDKTable.FloorCollision(&this); 
    public void LWallCollision() mut => RSDKTable.LWallCollision(&this);
    public void RoofCollision() mut => RSDKTable.RoofCollision(&this);
    public void RWallCollision() mut => RSDKTable.RWallCollision(&this);
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
    // expects an array of 5 sensors
    public static void SetPathGripSensors(RSDK.CollisionSensor *sensors) => modTable.SetPathGripSensors(sensors);

    public void FindFloorPosition() mut => modTable.FindFloorPosition(&this);
    public void FindLWallPosition() mut => modTable.FindLWallPosition(&this); 
    public void FindRoofPosition() mut => modTable.FindRoofPosition(&this);
    public void FindRWallPosition() mut => modTable.FindRWallPosition(&this);
    public void FloorCollision() mut => modTable.FloorCollision(&this); 
    public void LWallCollision() mut => modTable.LWallCollision(&this);
    public void RoofCollision() mut => modTable.RoofCollision(&this);
    public void RWallCollision() mut => modTable.RWallCollision(&this);
#endif
}
#endif

[CRepr] public struct CollisionMask
{
    public uint8[Const.TILE_SIZE] floorMasks;
    public uint8[Const.TILE_SIZE] lWallMasks;
    public uint8[Const.TILE_SIZE] rWallMasks;
    public uint8[Const.TILE_SIZE] roofMasks;
}

[CRepr] public struct TileInfo
{
    public uint8 floorAngle;
    public uint8 lWallAngle;
    public uint8 rWallAngle;
    public uint8 roofAngle;
    public uint8 flag;
}

public static class Collision
{
#if RETRO_REV0U
    public static void SetupCollisionConfig(int32 minDistance, uint8 lowTolerance, uint8 highTolerance, uint8 floorAngleTolerance, uint8 wallAngleTolerance,
                                     uint8 roofAngleTolerance)
    {
        RSDKTable.SetupCollisionConfig(minDistance, lowTolerance, highTolerance, floorAngleTolerance, wallAngleTolerance, roofAngleTolerance);
    }
#endif

#if RETRO_REV0U
    public static void CopyCollisionMask(uint16 dst, uint16 src, uint8 cPlane, uint8 cMode) => RSDKTable.CopyCollisionMask(dst, src, cPlane, cMode);
    public static void GetCollisionInfo(RSDK.CollisionMask **masks, RSDK.TileInfo **tileInfo) => RSDKTable.GetCollisionInfo(masks, tileInfo);
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER >= 2
    public static void CopyCollisionMask(uint16 dst, uint16 src, uint8 cPlane, uint8 cMode) => modTable.CopyCollisionMask(dst, src, cPlane, cMode);
    public static void GetCollisionInfo(RSDK.CollisionMask **masks, RSDK.TileInfo **tileInfo) => modTable.GetCollisionInfo(masks, tileInfo);
#endif
}