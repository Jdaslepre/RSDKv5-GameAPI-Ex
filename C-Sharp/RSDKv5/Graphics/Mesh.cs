using static RSDK.EngineAPI;

namespace RSDK
{
    public unsafe struct Mesh
    {
        public ushort id;

        public void Init() => id = unchecked((ushort)-1);

        public void Load(string path, Scopes scope) => id = RSDKTable.LoadMesh(path, (byte)scope);
        public bool32 Loaded() { return id != unchecked((ushort)-1); }

        public bool32 Matches(RSDK.Mesh other) { return id == other.id; }
        public bool32 Matches(RSDK.Mesh* other)
        {
            if (other != null)

                return id == other->id;
            else
                return id == unchecked((ushort)-1);
        }
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

        public void Init() => id = unchecked((ushort)-1);

        public void Create(string identifier, ushort faceCount, Scopes scope) => id = RSDKTable.Create3DScene(identifier, faceCount, (byte)scope);
        public void Prepare() => RSDKTable.Prepare3DScene(id);
        public void Draw() => RSDKTable.Draw3DScene(id);

        public void SetDiffuseColor(byte x, byte y, byte z) => RSDKTable.SetDiffuseColor(id, x, y, z);
        public void SetDiffuseIntensity(byte x, byte y, byte z) => RSDKTable.SetDiffuseIntensity(id, x, y, z);
        public void SetSpecularIntensity(byte x, byte y, byte z) => RSDKTable.SetSpecularIntensity(id, x, y, z);

        public void AddModel(Mesh modelFrames, DrawTypes drawMode, ref Matrix matWorld, ref Matrix matView, uint color)
        {
            fixed (Matrix* w = &matWorld)
            fixed (Matrix* v = &matView)
                RSDKTable.AddModelTo3DScene(modelFrames.id, id, (byte)drawMode, w, v, color);
        }
        public void AddMesh(RSDK.Mesh modelFrames, RSDK.Animator* animator, RSDK.Scene3D.DrawTypes drawMode, RSDK.Matrix* matWorld, RSDK.Matrix* matNormal, uint color) => RSDKTable.AddMeshFrameTo3DScene(modelFrames.id, id, animator, (byte)drawMode, matWorld, matNormal, color);

        public bool32 Loaded() { return id != unchecked((ushort)-1); }

        public bool32 Matches(RSDK.Mesh other) { return id == other.id; }
        public bool32 Matches(RSDK.Mesh* other)
        {
            return other != null ? id == other->id : id == unchecked((ushort)-1);
        }
    }
}
