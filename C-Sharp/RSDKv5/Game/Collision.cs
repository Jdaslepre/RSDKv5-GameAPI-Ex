namespace RSDK;

public enum CollisionModes : byte
{
    CMODE_FLOOR,
    CMODE_LWALL,
    CMODE_ROOF,
    CMODE_RWALL,
}

public enum CollisionSides : byte
{
    C_NONE,
    C_TOP,
    C_LEFT,
    C_RIGHT,
    C_BOTTOM,
}

public struct Hitbox
{
    public short left;
    public short top;
    public short right;
    public short bottom;

    public Hitbox()
    {
        left = 0;
        top = 0;
        right = 0;
        bottom = 0;
    }

    public Hitbox(short Left, short Top, short Right, short Bottom)
    {
        left = Left;
        top = Top;
        right = Right;
        bottom = Bottom;
    }
}

#if RETRO_REV0U || RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
public unsafe struct CollisionSensor
{
    public Vector2 position;
    public bool32 collided;
    public byte angle;

#if RETRO_REV0U
    // expects an array of 5 sensors
    public static void SetPathGripSensors(CollisionSensor* sensors) => RSDKTable->SetPathGripSensors(sensors);

    public void FindFloorPosition() { fixed (CollisionSensor* s = &this) RSDKTable->FindFloorPosition(s); }
    public void FindLWallPosition() { fixed (CollisionSensor* s = &this) RSDKTable->FindLWallPosition(s); }
    public void FindRoofPosition() { fixed (CollisionSensor* s = &this) RSDKTable->FindRoofPosition(s); }
    public void FindRWallPosition() { fixed (CollisionSensor* s = &this) RSDKTable->FindRWallPosition(s); }
    public void FloorCollision() { fixed (CollisionSensor* s = &this) RSDKTable->FloorCollision(s); }
    public void LWallCollision() { fixed (CollisionSensor* s = &this) RSDKTable->LWallCollision(s); }
    public void RoofCollision() { fixed (CollisionSensor* s = &this) RSDKTable->RoofCollision(s); }
    public void RWallCollision() { fixed (CollisionSensor* s = &this) RSDKTable->RWallCollision(s); }
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
    // expects an array of 5 sensors
    public static void SetPathGripSensors(CollisionSensor* sensors) => modTable.SetPathGripSensors(sensors);

    public void FindFloorPosition() { fixed (CollisionSensor* s = &this) modTable.FindFloorPosition(s); }
    public void FindLWallPosition() { fixed (CollisionSensor* s = &this) modTable.FindLWallPosition(s); }
    public void FindRoofPosition() { fixed (CollisionSensor* s = &this) modTable.FindRoofPosition(s); }
    public void FindRWallPosition() { fixed (CollisionSensor* s = &this) modTable.FindRWallPosition(s); }
    public void FloorCollision() { fixed (CollisionSensor* s = &this) modTable.FloorCollision(s); }
    public void LWallCollision() { fixed (CollisionSensor* s = &this) modTable.LWallCollision(s); }
    public void RoofCollision() { fixed (CollisionSensor* s = &this) modTable.RoofCollision(s); }
    public void RWallCollision() { fixed (CollisionSensor* s = &this) modTable.RWallCollision(s); }
#endif
}
#endif

public struct CollisionMask
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.TILE_SIZE)]
    public byte[] floorMasks;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.TILE_SIZE)]
    public byte[] lWallMasks;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.TILE_SIZE)]
    public byte[] rWallMasks;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.TILE_SIZE)]
    public byte[] roofMasks;
}

public struct TileInfo
{
    public byte floorAngle;
    public byte lWallAngle;
    public byte rWallAngle;
    public byte roofAngle;
    public byte flag;
}

public unsafe static class Collision
{
#if RETRO_REV0U
    public static void SetupCollisionConfig(int minDistance, byte lowTolerance, byte highTolerance, byte floorAngleTolerance, byte wallAngleTolerance,
                                     byte roofAngleTolerance)
    {
        RSDKTable->SetupCollisionConfig(minDistance, lowTolerance, highTolerance, floorAngleTolerance, wallAngleTolerance, roofAngleTolerance);
    }
#endif

#if RETRO_REV0U
    public static void CopyCollisionMask(ushort dst, ushort src, byte cPlane, byte cMode) => RSDKTable->CopyCollisionMask(dst, src, cPlane, cMode);
    public static void GetCollisionInfo(CollisionMask** masks, TileInfo** tileInfo) => RSDKTable->GetCollisionInfo(masks, tileInfo);
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
    public static void CopyCollisionMask(ushort dst, ushort src, byte cPlane, byte cMode) => modTable.CopyCollisionMask(dst, src, cPlane, cMode);
    public static void GetCollisionInfo(CollisionMask **masks, TileInfo **tileInfo) => modTable.GetCollisionInfo(masks, tileInfo);
#endif
}