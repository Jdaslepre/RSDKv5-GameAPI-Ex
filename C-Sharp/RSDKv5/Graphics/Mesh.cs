namespace RSDK;

public struct Mesh
{
    public ushort id;

    public void Init() => id = 0xFFFF;

    public unsafe void Load(string path, Scopes scope) => id = RSDKTable.LoadMesh(path, (byte)scope);

    public bool32 Loaded() => id != 0xFFFF;

    public bool32 Matches(Mesh other) => id == other.id;
    public unsafe bool32 Matches(Mesh* other) => other != null ? id == other->id : id == 0xFFFF;
}

public unsafe struct Scene3D
{
    public enum DrawTypes : byte
    {
        DRAWTYPE_WIREFRAME,
        DRAWTYPE_SOLIDCOLOR,
        DRAWTYPE_UNUSED1,
        DRAWTYPE_UNUSED2,
        DRAWTYPE_WIREFRAME_SHADED,
        DRAWTYPE_SOLIDCOLOR_SHADED,
        DRAWTYPE_SOLIDCOLOR_SHADED_BLENDED,
        DRAWTYPE_WIREFRAME_SCREEN,
        DRAWTYPE_SOLIDCOLOR_SCREEN,
        DRAWTYPE_WIREFRAME_SHADED_SCREEN,
        DRAWTYPE_SOLIDCOLOR_SHADED_SCREEN,
        DRAWTYPE_SOLIDCOLOR_SHADED_BLENDED_SCREEN,
    }

    public ushort id;

    public void Init() => id = 0xFFFF;

    public void Create(string identifier, ushort faceCount, Scopes scope) => id = RSDKTable.Create3DScene(identifier, faceCount, (byte)scope);
    public void Prepare() => RSDKTable.Prepare3DScene(id);
    public void Draw() => RSDKTable.Draw3DScene(id);

    public void SetDiffuseColor(byte x, byte y, byte z) => RSDKTable.SetDiffuseColor(id, x, y, z);
    public void SetDiffuseIntensity(byte x, byte y, byte z) => RSDKTable.SetDiffuseIntensity(id, x, y, z);
    public void SetSpecularIntensity(byte x, byte y, byte z) => RSDKTable.SetSpecularIntensity(id, x, y, z);

    public void AddModel(Mesh modelFrames, DrawTypes drawMode, Matrix matWorld, Matrix matView, uint color)
    {
        RSDKTable.AddModelTo3DScene(modelFrames.id, id, (byte)drawMode, ref matWorld, ref matView, color);
    }
    public void AddMesh(Mesh modelFrames, Animator animator, DrawTypes drawMode, Matrix matWorld, Matrix matNormal, uint color) => RSDKTable.AddMeshFrameTo3DScene(modelFrames.id, id, ref animator, (byte)drawMode, ref matWorld, ref matNormal, color);

    public bool32 Loaded() => id != 0xFFFF;

    public bool32 Matches(Scene3D other) => id == other.id;
    public unsafe bool32 Matches(Scene3D* other) => other != null ? id == other->id : id == 0xFFFF;
}
