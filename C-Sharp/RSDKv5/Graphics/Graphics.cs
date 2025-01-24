namespace RSDK;

public enum DrawFX : byte
{
    FX_NONE = 0,
    FX_FLIP = 1,
    FX_ROTATE = 2,
    FX_SCALE = 4,
}

public enum FlipFlags : byte
{
    FLIP_NONE,
    FLIP_X,
    FLIP_Y,
    FLIP_XY,
}

public enum Alignments : int
{
    ALIGN_LEFT,
    ALIGN_RIGHT,
    ALIGN_CENTER,
}

public enum InkEffects : byte
{
    INK_NONE,
    INK_BLEND,
    INK_ALPHA,
    INK_ADD,
    INK_SUB,
    INK_TINT,
    INK_MASKED,
    INK_UNMASKED,
}

public enum VideoSettingsValues : int
{
    VIDEOSETTING_WINDOWED,
    VIDEOSETTING_BORDERED,
    VIDEOSETTING_EXCLUSIVEFS,
    VIDEOSETTING_VSYNC,
    VIDEOSETTING_TRIPLEBUFFERED,
    VIDEOSETTING_WINDOW_WIDTH,
    VIDEOSETTING_WINDOW_HEIGHT,
    VIDEOSETTING_FSWIDTH,
    VIDEOSETTING_FSHEIGHT,
    VIDEOSETTING_REFRESHRATE,
    VIDEOSETTING_SHADERSUPPORT,
    VIDEOSETTING_SHADERID,
    VIDEOSETTING_SCREENCOUNT,
#if RETRO_REV02
    VIDEOSETTING_DIMTIMER,
#endif
    VIDEOSETTING_STREAMSENABLED,
    VIDEOSETTING_STREAM_VOL,
    VIDEOSETTING_SFX_VOL,
    VIDEOSETTING_LANGUAGE,
    VIDEOSETTING_STORE,
    VIDEOSETTING_RELOAD,
    VIDEOSETTING_CHANGED,
    VIDEOSETTING_WRITE,
}

public unsafe struct Palette
{
    public Palette() => id = 0;
    public Palette(byte bankID) => id = bankID;

#if RETRO_REV02
    public void Load(string path, ushort disabledRows) => RSDKTable.LoadPalette(id, path, disabledRows);
#endif

#if RETRO_REV01
        public static ushort *GetTintLookupTable() { return RSDKTable.GetTintLookupTable(); }
#endif

#if RETRO_REV02
    public static void SetTintLookupTable(ushort* lookupTable) => RSDKTable.SetTintLookupTable(lookupTable);
#endif

    public static void SetPaletteMask(uint maskColor) => RSDKTable.SetPaletteMask(maskColor);

    public void SetEntry(byte index, uint color) => RSDKTable.SetPaletteEntry(id, index, color);
    public uint GetEntry(byte index) => RSDKTable.GetPaletteEntry(id, index);

    public void SetActivePalette(int startLine, int endLine) => RSDKTable.SetActivePalette(id, startLine, endLine);

    public void Rotate(byte startIndex, byte endIndex, bool32 right)
    {
        RSDKTable.RotatePalette(id, startIndex, endIndex, right);
    }
    public void Copy(byte sourceBank, byte srcBankStart, byte destBankStart, byte count)
    {
        RSDKTable.CopyPalette(sourceBank, srcBankStart, id, destBankStart, count);
    }
    public void SetLimitedFade(byte srcBankA, byte srcBankB, short blendAmount, int startIndex, int endIndex)
    {
        RSDKTable.SetLimitedFade(id, srcBankA, srcBankB, blendAmount, startIndex, endIndex);
    }

#if RETRO_REV02
    public void BlendColors(uint* srcColorsA, uint* srcColorsB, int blendAmount, int startIndex, int count)
    {
        RSDKTable.BlendColors(id, srcColorsA, srcColorsB, blendAmount, startIndex, count);
    }
#endif

    public byte id;
}

public unsafe static class Graphics
{
    public static Palette[] paletteBank = [new(0), new(1), new(2), new(3), new(4), new(5), new(6), new(7)];

    // -------
    // Cameras
    // -------

    public static void AddCamera(ref Vector2 targetPos, int offsetX, int offsetY, bool32 worldRelative) => RSDKTable.AddCamera(ref targetPos, offsetX, offsetY, worldRelative);
    public static void ClearCameras() => RSDKTable.ClearCameras();

    // -------
    // Drawing
    // -------

    public static void DrawLine(int x1, int y1, int x2, int y2, uint color, int alpha, InkEffects inkEffect, bool32 screenRelative)
    {
        RSDKTable.DrawLine(x1, y1, x2, y2, color, alpha, (byte)inkEffect, screenRelative);
    }

    public static void DrawRect(int x, int y, int width, int height, uint color, int alpha, InkEffects inkEffect, bool32 screenRelative)
    {
        RSDKTable.DrawRect(x, y, width, height, color, alpha, (byte)inkEffect, screenRelative);
    }

    public static void DrawCircle(int x, int y, int radius, uint color, int alpha, InkEffects inkEffect, bool32 screenRelative)
    {
        RSDKTable.DrawCircle(x, y, radius, color, alpha, (byte)inkEffect, screenRelative);
    }

    public static void DrawCircleOutline(int x, int y, int innerRadius, int outerRadius, uint color, int alpha, InkEffects inkEffect,
                                  bool32 screenRelative)
    {
        RSDKTable.DrawCircleOutline(x, y, innerRadius, outerRadius, color, alpha, (byte)inkEffect, screenRelative);
    }

    public static void DrawFace(ref Vector2 vertices, int vertCount, int r, int g, int b, int alpha, InkEffects inkEffect)
    {
        RSDKTable.DrawFace(ref vertices, vertCount, r, g, b, alpha, (int)inkEffect);
    }

    public static void DrawBlendedFace(ref Vector2 vertices, uint* vertColors, int vertCount, int alpha, InkEffects inkEffect)
    {
        RSDKTable.DrawBlendedFace(ref vertices, vertColors, vertCount, alpha, (int)inkEffect);
    }

    public static void DrawDeformedSprite(SpriteSheet sheet, int inkEffect, bool32 screenRelative)
    {
        RSDKTable.DrawDeformedSprite(sheet.id, inkEffect, screenRelative);
    }

    public static void DrawTile(Tile* tiles, int countX, int countY, ref Vector2 position, ref Vector2 offset, bool32 screenRelative)
    {
        RSDKTable.DrawTile((ushort*)tiles, countX, countY, ref position, ref offset, screenRelative);
    }

    public static void CopyTile(ushort dest, ushort src, ushort count)
    {
        RSDKTable.CopyTile(dest, src, count);
    }

    public static void DrawAniTiles(SpriteSheet sheet, ushort tileIndex, ushort srcX, ushort srcY, ushort width, ushort height)
    {
        RSDKTable.DrawAniTiles(sheet.id, tileIndex, srcX, srcY, width, height);
    }

    public static void FillScreen(uint color, int alphaR, int alphaG, int alphaB)
    {
        RSDKTable.FillScreen(color, alphaR, alphaG, alphaB);
    }

    // ------------------
    // Screens & Displays
    // ------------------

    public static void GetDisplayInfo(int* displayID, int* width, int* height, int* refreshRate, string text)
    {
        fixed (char* txt = text)
            RSDKTable.GetDisplayInfo(displayID, width, height, refreshRate, txt);
    }
    public static void GetWindowSize(int* width, int* height)
    {
        RSDKTable.GetWindowSize(width, height);
    }
    public static int SetScreenSize(byte screenID, ushort width, ushort height)
    {
        return RSDKTable.SetScreenSize(screenID, width, height);
    }
    public static void SetClipBounds(byte screenID, int x1, int y1, int x2, int y2)
    {
        RSDKTable.SetClipBounds(screenID, x1, y1, x2, y2);
    }
#if RETRO_REV02
    public static void SetScreenVertices(byte startVert2P_S1, byte startVert2P_S2, byte startVert3P_S1, byte startVert3P_S2, byte startVert3P_S3)
    {
        RSDKTable.SetScreenVertices(startVert2P_S1, startVert2P_S2, startVert3P_S1, startVert3P_S2, startVert3P_S3);
    }
#endif

    // ---------------------
    // Window/Video Settings
    // ---------------------

    public static int GetVideoSetting(VideoSettingsValues id) => RSDKTable.GetVideoSetting((int)id);
    public static void SetVideoSetting(VideoSettingsValues id, int value) { RSDKTable.SetVideoSetting((int)id, value); }
    public static void UpdateWindow() => RSDKTable.UpdateWindow();

    // ------------------
    // Entities & Objects
    // ------------------

    public static int GetDrawListRefSlot(byte drawGroup, ushort listPos) => RSDKTable.GetDrawListRefSlot(drawGroup, listPos);
    public static void* GetDrawListRef(byte drawGroup, ushort listPos) => RSDKTable.GetDrawListRef(drawGroup, listPos);
    public static T* GetDrawListRef<T>(byte drawGroup, ushort entitySlot) => (T*)RSDKTable.GetDrawListRef(drawGroup, entitySlot);

    public static void AddDrawListRef(byte drawGroup, ushort entitySlot) => RSDKTable.AddDrawListRef(drawGroup, entitySlot);

    public static void SwapDrawListEntries(byte drawGroup, ushort slot1, ushort slot2, ushort count) => RSDKTable.SwapDrawListEntries(drawGroup, slot1, slot2, count);
    public static void SetDrawGroupProperties(byte drawGroup, bool32 sorted, delegate* unmanaged<void> hookCB) { RSDKTable.SetDrawGroupProperties(drawGroup, sorted, hookCB); }
}