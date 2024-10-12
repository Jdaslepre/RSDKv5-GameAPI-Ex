namespace RSDK;

public struct ScanlineInfo
{
    public Vector2 position;
    public Vector2 deform;
}

public struct ScrollInfo
{
    public int tilePos;
    public int parallaxFactor;
    public int scrollSpeed;
    public int scrollPos;
    public byte deform;
    public byte unknown;
}

public unsafe struct Tile
{
    public ushort id;

    public Tile() => id = 0xFFFF;
    public Tile(ushort id) => this.id = id;

    public ushort Index() => (ushort)(id & 0x3FF);
    public byte Direction() => (byte)((id >> 10) & 3);
    public byte SolidA() => (byte)((id >> 12) & 3);
    public byte SolidB() => (byte)((id >> 14) & 3);

    public void SetIndex(ushort index)
    {
        id &= unchecked((ushort)~0x3FF);
        id |= (ushort)(index & 0x3FF);
    }
    public void SetDirection(byte dir)
    {
        id &= unchecked((ushort)~(3 << 10));
        id |= (ushort)((dir & 3) << 10);
    }
    public void SetSolidA(byte solid)
    {
        id &= unchecked(unchecked((ushort)~(3 << 12)));
        id |= (ushort)((solid & 3) << 12);
    }
    public void SetSolidB(byte solid)
    {
        id &= unchecked((ushort)~(3 << 14));
        id |= (ushort)((solid & 3) << 14);
    }

    public static void Copy(ushort dst, ushort src, ushort count = 1) => RSDKTable->CopyTile(dst, src, count);

    public int GetAngle(byte cPlane, byte cMode) => RSDKTable->GetTileAngle(id, cPlane, cMode);
    public void SetAngle(byte cPlane, byte cMode, byte angle) => RSDKTable->SetTileAngle(id, cPlane, cMode, angle);
    public byte GetFlags(byte cPlane) => RSDKTable->GetTileFlags(id, cPlane);
    public void SetFlags(byte cPlane, byte flag) => RSDKTable->SetTileFlags(id, cPlane, flag);
}

public unsafe struct TileLayer
{
    public byte type;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.CAMERA_COUNT)]
    public byte[] drawGroup;

    public byte widthShift;
    public byte heightShift;
    public ushort width;
    public ushort height;
    public Vector2 position;
    public int parallaxFactor;
    public int scrollSpeed;
    public int scrollPos;
    public int deformationOffset;
    public int deformationOffsetW;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x400)]
    public int[] deformationData;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x400)]
    public int[] deformationDataW;

    public delegate* unmanaged<ScanlineInfo*, void> scanlineCallback;
    public ushort scrollInfoCount;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x100)]
    public ScrollInfo[] scrollInfo;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public uint[] name;

    public Tile* layout;
    public byte* lineScroll;

    public void ProcessParallax() { fixed (TileLayer* l = &this) RSDKTable->ProcessParallax(l); }
}

public unsafe struct SceneLayer
{
    public ushort id;

    public void Init() => id = 0xFFFF;

    public void Get(string name) => id = RSDKTable->GetTileLayerID(name);
    public void Set(ushort id) => this.id = id;

    public TileLayer* GetTileLayer() => RSDKTable->GetTileLayer(id);
    public void Size(Vector2* size, bool32 usePixelUnits) => RSDKTable->GetLayerSize(id, size, usePixelUnits);

    public Tile GetTile(int x, int y) => new Tile(RSDKTable->GetTile(id, x, y));
    public void SetTile(int x, int y, Tile tile) => RSDKTable->SetTile(id, x, y, tile.id);

    public static TileLayer* GetTileLayer(string name) => RSDKTable->GetTileLayer(RSDKTable->GetTileLayerID(name));
    public static TileLayer* GetTileLayer(ushort id) => RSDKTable->GetTileLayer(id);

    public static void Copy(SceneLayer dstLayer, int dstStartX, int dstStartY, SceneLayer srcLayer, int srcStartX, int srcStartY,
                            int countX, int countY)
    {
        RSDKTable->CopyTileLayer(dstLayer.id, dstStartX, dstStartY, srcLayer.id, srcStartX, srcStartY, countX, countY);
    }

    public bool32 Loaded() => id != 0xFFFF;

    public bool32 Matches(SceneLayer other) => id == other.id;
    public bool32 Matches(SceneLayer* other) => other != null ? id == other->id : id == 0xFFFF;
}