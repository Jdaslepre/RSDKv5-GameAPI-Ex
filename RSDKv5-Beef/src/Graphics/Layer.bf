namespace RSDK;

[System.CRepr] public struct ScanlineInfo
{
    public Vector2 position;
    public Vector2 deform;
}

[System.CRepr] public struct ScrollInfo
{
    public int32 tilePos;
    public int32 parallaxFactor;
    public int32 scrollSpeed;
    public int32 scrollPos;
    public uint8 deform;
    public uint8 unknown;
}

public struct Tile
{
    public uint16 id;

    public this() => id = 0xFFFF;
    public this(uint16 ID) => id = ID;

    public uint16 Index() => id & 0x3FF;
    public uint8 Direction() => (.)((id >> 10) & 3);
    public uint8 SolidA() => (.)((id >> 12) & 3);
    public uint8 SolidB() => (.)((id >> 14) & 3);

    public void SetIndex(uint16 index) mut
    {
        id &= ~0x3FF;
        id |= (index & 0x3FF);
    }
    public void SetDirection(uint16 dir) mut
    {
        id &= ~(3 << 10);
        id |= (dir & 3) << 10;
    }
    public void SetSolidA(uint16 solid) mut
    {
        id &= ~(3 << 12);
        id |= (solid & 3) << 12;
    }
    public void SetSolidB(uint16 solid) mut
    {
        id &= ~(3 << 14);
        id |= (solid & 3) << 14;
    }

    public static void Copy(uint16 dst, uint16 src, uint16 count = 1) => RSDKTable.CopyTile(dst, src, count);

    public int32 GetAngle(uint8 cPlane, uint8 cMode) => RSDKTable.GetTileAngle(id, cPlane, cMode);
    public void SetAngle(uint8 cPlane, uint8 cMode, uint8 angle) => RSDKTable.SetTileAngle(id, cPlane, cMode, angle);
    public uint8 GetFlags(uint8 cPlane) => RSDKTable.GetTileFlags(id, cPlane);
    public void SetFlags(uint8 cPlane, uint8 flag) => RSDKTable.SetTileFlags(id, cPlane, flag);
}

[System.CRepr] public struct TileLayer
{
    public uint8 type;
    public uint8[Const.CAMERA_COUNT] drawGroup;
    public uint8 widthShift;
    public uint8 heightShift;
    public uint16 width;
    public uint16 height;
    public Vector2 position;
    public int32 parallaxFactor;
    public int32 scrollSpeed;
    public int32 scrollPos;
    public int32 deformationOffset;
    public int32 deformationOffsetW;
    public int32[0x400] deformationData;
    public int32[0x400] deformationDataW;
    public function void(ref ScanlineInfo*) scanlineCallback;
    public uint16 scrollInfoCount;
    public ScrollInfo[0x100] scrollInfo;
    public uint32[4] name;
    public Tile* layout;
    public uint8* lineScroll;

    public void ProcessParallax() mut => RSDKTable.ProcessParallax(&this);
}

public struct SceneLayer
{
    public uint16 id;

    public void Init() mut => id = (.)(-1);

    public void Get(char8* name) mut => id = RSDKTable.GetTileLayerID(name);
    public void Set(uint16 ID) mut => id = ID;

    public TileLayer* GetTileLayer() => RSDKTable.GetTileLayer(id);

    public void Size(Vector2* size, bool32 usePixelUnits) => RSDKTable.GetLayerSize(id, size, usePixelUnits);

    public Tile GetTile(int32 x, int32 y) => Tile(RSDKTable.GetTile(id, x, y));
    public void SetTile(int32 x, int32 y, Tile tile) => RSDKTable.SetTile(id, x, y, tile.id);

    public static TileLayer* GetTileLayer(char8* name) => RSDKTable.GetTileLayer(RSDKTable.GetTileLayerID(name));
    public static TileLayer* GetTileLayer(uint16 id) => RSDKTable.GetTileLayer(id);

    public static void Copy(Self dstLayer, int32 dstStartX, int32 dstStartY, Self srcLayer, int32 srcStartX, int32 srcStartY,
        int32 countX, int32 countY)
    {
        RSDKTable.CopyTileLayer(dstLayer.id, dstStartX, dstStartY, srcLayer.id, srcStartX, srcStartY, countX, countY);
    }

    public bool32 Loaded() => id != (.)(-1);

    public bool32 Matches(Self other) => id == other.id;
    public bool32 Matches(Self* other) => other != null ? id == other.id : id == (.)(-1);
}