using System.Xml.Linq;
using System;
using static RSDK.EngineAPI;

namespace RSDK
{
    public struct ScanlineInfo
    {
        public RSDK.Vector2 position;
        public RSDK.Vector2 deform;
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

        public Tile() { id = 0xFFFF; }
        public Tile(ushort id) { this.id = id; }

        public ushort Index() { return (ushort)(id & 0x3FF); }
        public byte Direction() { return (byte)((id >> 10) & 3); }
        public byte SolidA() { return (byte)((id >> 12) & 3); }
        public byte SolidB() { return (byte)((id >> 14) & 3); }

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

        public static void Copy(ushort dst, ushort src, ushort count = 1) => RSDKTable.CopyTile(dst, src, count);
        public int GetAngle(byte cPlane, byte cMode) { return RSDKTable.GetTileAngle(id, cPlane, cMode); }
        public void SetAngle(byte cPlane, byte cMode, byte angle) => RSDKTable.SetTileAngle(id, cPlane, cMode, angle);
        public byte GetFlags(byte cPlane) { return RSDKTable.GetTileFlags(id, cPlane); }
        public void SetFlags(byte cPlane, byte flag) => RSDKTable.SetTileFlags(id, cPlane, flag);
    }

    public unsafe struct TileLayer
    {
        public byte type;
        public byte[] drawGroup = new byte[4];
        public byte widthShift;
        public byte heightShift;
        public ushort width;
        public ushort height;
        public RSDK.Vector2 position;
        public int parallaxFactor;
        public int scrollSpeed;
        public int scrollPos;
        public int deformationOffset;
        public int deformationOffsetW;
        public int[] deformationData = new int[0x400];
        public int[] deformationDataW = new int[0x400];
        public delegate* unmanaged<RSDK.ScanlineInfo*, void> scanlineCallback;
        public ushort scrollInfoCount;
        public RSDK.ScrollInfo[] scrollInfo = new RSDK.ScrollInfo[0x100];
        public uint[] name = new uint[4];
        public RSDK.Tile* layout;
        public byte* lineScroll;

        public void ProcessParallax()
        {
            fixed (RSDK.TileLayer* l = &this) { RSDKTable.ProcessParallax(l); }
        }

        public TileLayer() { }
    }

    public unsafe struct SceneLayer
    {
        public ushort id;

        public void Init() => id = unchecked((ushort)-1);
        public void Get(string name) => id = RSDKTable.GetTileLayerID(name);

        public void Set(ushort id) { this.id = id; }

        public bool32 Loaded() { return id != unchecked((ushort)-1); }

        public bool32 Matches(SceneLayer other) { return id == other.id; }
        public bool32 Matches(SceneLayer* other)
        {
            if (other != null)
                return id == other->id;
            else
                return id == unchecked((ushort)-1);
        }

        public RSDK.TileLayer* GetTileLayer() { return RSDKTable.GetTileLayer(id); }
        public void Size(RSDK.Vector2* size, bool32 usePixelUnits) => RSDKTable.GetLayerSize(id, size, usePixelUnits);

        public RSDK.Tile GetTile(int x, int y) { return new RSDK.Tile(RSDKTable.GetTile(id, x, y)); }
        public void SetTile(int x, int y, RSDK.Tile tile) => RSDKTable.SetTile(id, x, y, tile.id);

        public static RSDK.TileLayer* GetTileLayer(string name)
        {
            return RSDKTable.GetTileLayer(RSDKTable.GetTileLayerID(name));
        }
        public static RSDK.TileLayer* GetTileLayer(ushort id) { return RSDKTable.GetTileLayer(id); }
        public static void Copy(RSDK.SceneLayer dstLayer, int dstStartX, int dstStartY, RSDK.SceneLayer srcLayer, int srcStartX, int srcStartY,
                                int countX, int countY)
        {
            RSDKTable.CopyTileLayer(dstLayer.id, dstStartX, dstStartY, srcLayer.id, srcStartX, srcStartY, countX, countY);
        }
    }
}
