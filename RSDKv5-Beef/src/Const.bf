namespace RSDK;

public class Const
{
    // RSDKv5/EngineAPI.bf
    public const int SCREEN_XMAX = 1280;
    public const int SCREEN_YSIZE = 240;

    // RSDKv5/Game/Collision.bf
    public const int TILE_SIZE = 16;

    // RSDKv5/Audio/Audio.bf
    public const int CHANNEL_COUNT = 0x10;

    // RSDKv5/Game/Object.bf
    public const int OBJECT_COUNT = 0x400;
    public const int RESERVE_ENTITY_COUNT = 0x40;
    public const int TEMPENTITY_COUNT = 0x100;
    public const int SCENEENTITY_COUNT = 0x800;
    public const int ENTITY_COUNT = RESERVE_ENTITY_COUNT + SCENEENTITY_COUNT + TEMPENTITY_COUNT;
    public const int TEMPENTITY_START = ENTITY_COUNT - TEMPENTITY_COUNT;

    public const int TYPE_COUNT = 0x100;
    public const int TYPEGROUP_COUNT = TYPE_COUNT + 4;

    // RSDKv5/Graphics/Graphics.bf
    public const int PALETTE_BANK_COUNT = 0x8;
    public const int PALETTE_BANK_SIZE  = 0x100;
    public const int SCREEN_YCENTER = SCREEN_YSIZE / 2;
    public const int LAYER_COUNT = 8;
    public const int DRAWGROUP_COUNT = 16;
#if RETRO_REV02
    public const int SCREEN_COUNT = 4;
#else
    public const int SCREEN_COUNT = 2;
#endif
    public const int CAMERA_COUNT = 4;
}