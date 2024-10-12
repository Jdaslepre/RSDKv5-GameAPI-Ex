using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using static RSDK.EngineAPI;

namespace RSDK
{
    public class Object
    {
        public static int registerListCount = 0;
        public static ObjectRegistration[] registerList = new ObjectRegistration[Const.OBJECT_COUNT];
    }

    public unsafe struct ObjectRegistration
    {
        public string name;
        public IntPtr update;
        public IntPtr lateUpdate;
        public IntPtr staticUpdate;
        public IntPtr draw;
        public IntPtr create;
        public IntPtr stageLoad;
        public IntPtr serialize;
        public IntPtr editorLoad;
        public IntPtr editorDraw;
        public IntPtr staticLoad;

        public void** staticVars;
        public int entityClassSize;
        public int staticClassSize;
    }

    public enum ActiveFlags : byte
    {
        ACTIVE_NEVER, // never update
        ACTIVE_ALWAYS, // always update (even if paused/frozen)
        ACTIVE_NORMAL, // always update (unless paused/frozen)
        ACTIVE_PAUSED, // update only when paused/frozen
        ACTIVE_BOUNDS, // update if in x & y bounds
        ACTIVE_XBOUNDS, // update only if in x bounds (y bounds dont matter)
        ACTIVE_YBOUNDS, // update only if in y bounds (x bounds dont matter)
        ACTIVE_RBOUNDS, // update based on radius boundaries (updateRange.x == radius)

        // Not really even a real active value, but some objects set their active states to this so here it is I suppose
        ACTIVE_DISABLED = 0xFF,
    }

    public enum VariableTypes : byte
    {
        VAR_UINT8,
        VAR_UINT16,
        VAR_UINT32,
        VAR_INT8,
        VAR_INT16,
        VAR_INT32,
        VAR_ENUM,
        VAR_BOOL,
        VAR_STRING,
        VAR_VECTOR2,
        VAR_FLOAT, // Not actually used in Sonic Mania so it's just an assumption, but this is the only thing that'd fit the 32 bit limit and make sense
        VAR_COLOR,
    }

    public enum ForeachTypes
    {
        FOR_ALL_ENTITIES,
        FOR_ACTIVE_ENTITIES,
#if RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
        FOR_GROUP_ENTITIES,
#endif
    }

    public enum ForeachGroups
    {
        GROUP_ALL,

        GROUP_CUSTOM0 = Const.TYPE_COUNT,
        GROUP_CUSTOM1,
        GROUP_CUSTOM2,
        GROUP_CUSTOM3,
    }

    public enum DefaultObjects : ushort
    {
        TYPE_DEFAULTOBJECT = 0,
#if RETRO_REV02
        TYPE_DEVOUTPUT,
#endif
        TYPE_DEFAULT_COUNT, // max
    }

    public enum TileCollisionModes : byte
    {
        TILECOLLISION_NONE, // no tile collisions
        TILECOLLISION_DOWN, // downwards tile collisions
#if RETRO_REV0U
        TILECOLLISION_UP, // upwards tile collisions
#endif
    }

    public unsafe class GameObject
    {
        public struct Static
        {
            public ushort classID;
            public byte active;

            public unsafe void EditableVar(VariableTypes type, string name, int offset) => RSDKTable->SetEditableVar((byte)type, name, (byte)classID, offset);
            public unsafe int Count(uint isActive = 0) => RSDKTable->GetEntityCount(classID, isActive);
        }

        public interface IEntity
        {
            void Create(void* data);
            void Update();
            void Draw();
            void LateUpdate();
#if GAME_INCLUDE_EDITOR
            void EditorDraw();
#endif
            void StageLoad();
            void Serialize();
            void StaticUpdate();
            void StaticLoad(void* sVars);
#if GAME_INCLUDE_EDITOR
            void EditorLoad();
#endif
        }

        [StructLayout(LayoutKind.Sequential)]
        public class Entity : IEntity
        {
            public void Create(void* data) { }
            public void Update() { }
            public void Draw() { }
            public void LateUpdate() { }
#if GAME_INCLUDE_EDITOR
            public void EditorDraw() { }
#endif
            public void StageLoad() { }
            public void Serialize() { }
            public void StaticUpdate() { }
            public void StaticLoad(void* data)
            {
                var sVars = (Static*)data;

                sVars->classID = (ushort)DefaultObjects.TYPE_DEFAULTOBJECT;
                sVars->active = (byte)ActiveFlags.ACTIVE_NEVER;
            }
#if GAME_INCLUDE_EDITOR
            public void EditorLoad() { }
#endif

#if RETRO_REV0U
            private void* vfTable;
#endif
            public RSDK.Vector2 position;
            public RSDK.Vector2 scale;
            public RSDK.Vector2 velocity;
            public RSDK.Vector2 updateRange;
            public int angle;
            public int alpha;
            public int rotation;
            public int groundVel;
            public int zdepth;
            public ushort group;
            public ushort classID;
            public bool32 inRange;
            public bool32 isPermanent;
            public int tileCollisions;
            public bool32 interaction;
            public bool32 onGround;
            public ActiveFlags active;
#if RETRO_REV02
            public byte filter;
#endif
            public FlipFlags direction;
            public byte drawGroup;
            public byte collisionLayers;
            public byte collisionPlane;
            public TileCollisionModes collisionMode;
            public DrawFX drawFX;
            public InkEffects inkEffect;
            public byte visible;
            public byte onScreen;
        }


        public static Entity* Create(void* data, int x, int y)
        {
            return (Entity*)RSDKTable->CreateEntity((ushort)DefaultObjects.TYPE_DEFAULTOBJECT, data, x, y);
        }
        public static Entity* Create(int data, int x, int y)
        {
            return (Entity*)RSDKTable->CreateEntity((ushort)DefaultObjects.TYPE_DEFAULTOBJECT, MathRSDK.INT_TO_VOID(data), x, y);
        }
        public static T* Create<T>(void* data, int x, int y)
        {
            /*
            Type entityType = typeof(T);
            FieldInfo fieldSVars = entityType.GetField("sVars", BindingFlags.Static | BindingFlags.Public);
            var sVars = (GameObject.Static*)fieldSVars.GetValue(null);
            
            return (T*)RSDKTable.CreateEntity(sVars->classID, data, x, y);
            */

            return null;
        }
        /*
        template<typename T> static inline T *Create(int32 data, int32 x, int32 y)
        {
            return (T*)RSDKTable->CreateEntity(T::sVars->classID, INT_TO_VOID(data), x, y);
        }
        */

        // ... TODO


        public static LinkedList<IntPtr> GetEntities<T>(ForeachTypes type)
        {
            LinkedList<IntPtr> list = new();

            // dont mind me, just writing the worst code known to man :)
            FieldInfo sVarsField = typeof(T).GetField("sVars", BindingFlags.Static | BindingFlags.Public);
            IntPtr sVarsPtr = (IntPtr)sVarsField.GetValue(null);
            Static* sVars = (Static*)sVarsPtr.ToPointer();

            ushort group = sVars != null ? sVars->classID : (ushort)ForeachGroups.GROUP_ALL;

            T* entity = null;
            if (type == ForeachTypes.FOR_ALL_ENTITIES)
            {
                while (RSDKTable->GetAllEntities(group, (void**)&entity)) list.AddLast((IntPtr)entity);
            }
            else if (type == ForeachTypes.FOR_ACTIVE_ENTITIES)
            {
                while (RSDKTable->GetActiveEntities(group, (void**)&entity)) list.AddLast((IntPtr)entity);
            }

            return list;
        }




        public unsafe struct DelegateTypes
        {
            public delegate void TakesVoidPtr(void* data);
        }

        public static unsafe void Register<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] _TypeEntity, _TypeStatic>(ref _TypeStatic* sVars) where _TypeEntity : class
        {
            if (Object.registerListCount < Const.OBJECT_COUNT)
            {
                Type E = typeof(_TypeEntity);
                Object.registerList[Object.registerListCount] = new ObjectRegistration
                {
                    name = E.Name,
                    update = Managed.GetFieldPtr<Action>(E, "_Update"),
                    lateUpdate = Managed.GetFieldPtr<Action>(E, "_LateUpdate"),
                    staticUpdate = Managed.GetFieldPtr<Action>(E, "StaticUpdate"),
                    draw = Managed.GetFieldPtr<Action>(E, "_Draw"),
                    create = Managed.GetFieldPtr<DelegateTypes.TakesVoidPtr>(E, "_Create"),
                    stageLoad = Managed.GetFieldPtr<Action>(E, "StageLoad"),
#if GAME_INCLUDE_EDITOR
                    editorLoad = Managed.GetFieldPtr<Action>(E, "EditorLoad"),
                    editorDraw = Managed.GetFieldPtr<Action>(E, "_EditorDraw"),
#endif
                    serialize = Managed.GetFieldPtr<Action>(E, "Serialize"),
#if RETRO_REV0U
                    staticLoad = Managed.GetFieldPtr<DelegateTypes.TakesVoidPtr>(E, "StaticLoad"),
#endif
                };

                fixed (_TypeStatic** pSvars = &sVars)
                {
                    RSDKTable->RegisterObject(
                        (void**)pSvars,
                        Object.registerList[Object.registerListCount].name,
                        (uint)sizeof(_TypeEntity),
                        sizeof(_TypeStatic),
                        Object.registerList[Object.registerListCount].update,
                        Object.registerList[Object.registerListCount].lateUpdate,
                        Object.registerList[Object.registerListCount].staticUpdate,
                        Object.registerList[Object.registerListCount].draw,
                        Object.registerList[Object.registerListCount].create,
                        Object.registerList[Object.registerListCount].stageLoad,
                        Object.registerList[Object.registerListCount].editorLoad,
                        Object.registerList[Object.registerListCount].editorDraw,
                        Object.registerList[Object.registerListCount].serialize,
                        Object.registerList[Object.registerListCount].staticLoad
                    );
                }

                ++Object.registerListCount;
            }

            sVars = null;
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class RSDKDeclareAttribute : Attribute
    {
        private static Type _objType;

        public RSDKDeclareAttribute(Type objType)
        {
            _objType = objType;
        }

        // TODO:
    }


}
