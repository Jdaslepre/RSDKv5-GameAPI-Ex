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
    public Byte angle;

#if RETRO_REV0U
    // expects an array of 5 sensors
    public static void SetPathGripSensors(CollisionSensor sensors) => RSDKTable.SetPathGripSensors(ref sensors);

    public void FindFloorPosition() => RSDKTable.FindFloorPosition(ref this);
    public void FindLWallPosition() => RSDKTable.FindLWallPosition(ref this);
    public void FindRoofPosition() => RSDKTable.FindRoofPosition(ref this);
    public void FindRWallPosition() => RSDKTable.FindRWallPosition(ref this);
    public void FloorCollision() => RSDKTable.FloorCollision(ref this);
    public void LWallCollision() => RSDKTable.LWallCollision(ref this);
    public void RoofCollision() => RSDKTable.RoofCollision(ref this);
    public void RWallCollision() => RSDKTable.RWallCollision(ref this);
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
    // expects an array of 5 sensors
    public static void SetPathGripSensors(CollisionSensor sensors) => modTable.SetPathGripSensors(ref sensors);

    public void FindFloorPosition() => modTable.FindFloorPosition(ref this);
    public void FindLWallPosition() => modTable.FindLWallPosition(ref this);
    public void FindRoofPosition() => modTable.FindRoofPosition(ref this);
    public void FindRWallPosition() => modTable.FindRWallPosition(ref this);
    public void FloorCollision() => modTable.FloorCollision(ref this);
    public void LWallCollision() => modTable.LWallCollision(ref this);
    public void RoofCollision() => modTable.RoofCollision(ref this);
    public void RWallCollision() => modTable.RWallCollision(ref this);
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
        RSDKTable.SetupCollisionConfig(minDistance, lowTolerance, highTolerance, floorAngleTolerance, wallAngleTolerance, roofAngleTolerance);
    }
#endif

#if RETRO_REV0U
    public static void CopyCollisionMask(ushort dst, ushort src, byte cPlane, byte cMode) => RSDKTable.CopyCollisionMask(dst, src, cPlane, cMode);
    public static void GetCollisionInfo(CollisionMask** masks, TileInfo** tileInfo) => RSDKTable.GetCollisionInfo(masks, tileInfo);
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
    public static void CopyCollisionMask(ushort dst, ushort src, byte cPlane, byte cMode) => modTable.CopyCollisionMask(dst, src, cPlane, cMode);
    public static void GetCollisionInfo(CollisionMask **masks, TileInfo **tileInfo) => modTable.GetCollisionInfo(masks, tileInfo);
#endif
}