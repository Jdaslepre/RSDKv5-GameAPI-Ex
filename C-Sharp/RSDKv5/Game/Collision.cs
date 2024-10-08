using static RSDK.EngineAPI;

namespace RSDK
{
    public enum CollisionModes
    {
        CMODE_FLOOR,
        CMODE_LWALL,
        CMODE_ROOF,
        CMODE_RWALL,
    }

    public enum CollisionSides
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
    }

#if RETRO_REV0U || RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
    public unsafe struct CollisionSensor
    {
        public RSDK.Vector2 position;
        public bool32 collided;
        public byte angle;

#if RETRO_REV0U
        // expects an array of 5 sensors
        public static void SetPathGripSensors(RSDK.CollisionSensor* sensors) { RSDKTable.SetPathGripSensors(sensors); }

        public void FindFloorPosition() { fixed (RSDK.CollisionSensor* s = &this) RSDKTable.FindFloorPosition(s); }
        public void FindLWallPosition() { fixed (RSDK.CollisionSensor* s = &this) RSDKTable.FindLWallPosition(s); }
        public void FindRoofPosition() { fixed (RSDK.CollisionSensor* s = &this) RSDKTable.FindRoofPosition(s); }
        public void FindRWallPosition() { fixed (RSDK.CollisionSensor* s = &this) RSDKTable.FindRWallPosition(s); }
        public void FloorCollision() { fixed (RSDK.CollisionSensor* s = &this) RSDKTable.FloorCollision(s); }
        public void LWallCollision() { fixed (RSDK.CollisionSensor* s = &this) RSDKTable.LWallCollision(s); }
        public void RoofCollision() { fixed (RSDK.CollisionSensor* s = &this) RSDKTable.RoofCollision(s); }
        public void RWallCollision() { fixed (RSDK.CollisionSensor* s = &this) RSDKTable.RWallCollision(s); }
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
        // expects an array of 5 sensors
        public static void SetPathGripSensors(RSDK.CollisionSensor* sensors) { modTable.SetPathGripSensors(sensors); }

        public void FindFloorPosition() { fixed (RSDK.CollisionSensor* s = &this) modTable.FindFloorPosition(s); }
        public void FindLWallPosition() { fixed (RSDK.CollisionSensor* s = &this) modTable.FindLWallPosition(s); }
        public void FindRoofPosition() { fixed (RSDK.CollisionSensor* s = &this) modTable.FindRoofPosition(s); }
        public void FindRWallPosition() { fixed (RSDK.CollisionSensor* s = &this) modTable.FindRWallPosition(s); }
        public void FloorCollision() { fixed (RSDK.CollisionSensor* s = &this) modTable.FloorCollision(s); }
        public void LWallCollision() { fixed (RSDK.CollisionSensor* s = &this) modTable.LWallCollision(s); }
        public void RoofCollision() { fixed (RSDK.CollisionSensor* s = &this) modTable.RoofCollision(s); }
        public void RWallCollision() { fixed (RSDK.CollisionSensor* s = &this) modTable.RWallCollision(s); }
#endif
    }
#endif

    public struct CollisionMask
    {
        public static byte[] floorMasks = new byte[Const.TILE_SIZE];
        public static byte[] lWallMasks = new byte[Const.TILE_SIZE];
        public static byte[] rWallMasks = new byte[Const.TILE_SIZE];
        public static byte[] roofMasks = new byte[Const.TILE_SIZE];
    }

    public struct TileInfo
    {
        public byte floorAngle;
        public byte lWallAngle;
        public byte rWallAngle;
        public byte roofAngle;
        public byte flag;
    }

    public unsafe class Collision 
    {
#if RETRO_REV0U
        public void SetupCollisionConfig(int minDistance, byte lowTolerance, byte highTolerance, byte floorAngleTolerance, byte wallAngleTolerance,
                                         byte roofAngleTolerance)
        {
            RSDKTable.SetupCollisionConfig(minDistance, lowTolerance, highTolerance, floorAngleTolerance, wallAngleTolerance, roofAngleTolerance);
        }
#endif

#if RETRO_REV0U
        public void CopyCollisionMask(ushort dst, ushort src, byte cPlane, byte cMode) { RSDKTable.CopyCollisionMask(dst, src, cPlane, cMode); }
        public void GetCollisionInfo(RSDK.CollisionMask** masks, RSDK.TileInfo** tileInfo) { RSDKTable.GetCollisionInfo(masks, tileInfo); }
#elif RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
        public void CopyCollisionMask(ushort dst, ushort src, byte cPlane, byte cMode) { modTable.CopyCollisionMask(dst, src, cPlane, cMode); }
        public void GetCollisionInfo(RSDK.CollisionMask **masks, RSDK.TileInfo **tileInfo) { modTable.GetCollisionInfo(masks, tileInfo); }
#endif
    }
}
