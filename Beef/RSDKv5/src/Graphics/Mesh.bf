namespace RSDK;

public struct Mesh
{
    public uint16 id;

    public void Init() mut => id = (.)(-1);

    public void Load(char8* path, Scopes scopeType) mut => id = RSDKTable.LoadMesh(path, (.)scopeType);

    public bool32 Loaded() { return id != (.)(-1); }

    public bool32 Matches(RSDK.Mesh other) { return this.id == other.id; }
    public bool32 Matches(RSDK.Mesh* other)
    {
        return other != null ? id == other.id : id == (.)(-1);
    }
}

public struct Scene3D
{
    public enum DrawTypes : uint8
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

    public uint16 id;

    public void Init() mut => id = (.)(-1);

    public void Create(char8* identifier, uint16 faceCount, Scopes scopeType) mut => id = RSDKTable.Create3DScene(identifier, faceCount, (.)scopeType);
    public void Prepare() => RSDKTable.Prepare3DScene(id);
    public void Draw() => RSDKTable.Draw3DScene(id);

    public void SetDiffuseColor(uint8 x, uint8 y, uint8 z) => RSDKTable.SetDiffuseColor(id, x, y, z);
    public void SetDiffuseIntensity(uint8 x, uint8 y, uint8 z) => RSDKTable.SetDiffuseIntensity(id, x, y, z);
    public void SetSpecularIntensity(uint8 x, uint8 y, uint8 z) => RSDKTable.SetSpecularIntensity(id, x, y, z);

    public void AddModel(RSDK.Mesh modelFrames, DrawTypes drawMode, RSDK.Matrix* matWorld, RSDK.Matrix* matView, color color)
    {
        RSDKTable.AddModelTo3DScene(modelFrames.id, id, (.)drawMode, matWorld, matView, color);
    }
    public void AddMesh(RSDK.Mesh modelFrames, RSDK.Animator* animator, DrawTypes drawMode, RSDK.Matrix* matWorld, RSDK.Matrix* matNormal, color color)
    {
        RSDKTable.AddMeshFrameTo3DScene(modelFrames.id, id, animator, (.)drawMode, matWorld, matNormal, color);
    }

    public bool32 Loaded() { return id != (.)(-1); }

    public bool32 Matches(RSDK.Scene3D other) { return this.id == other.id; }
    public bool32 Matches(RSDK.Scene3D* other)
    {
        return other != null ? id == other.id : id == (.)(-1);
    }
}