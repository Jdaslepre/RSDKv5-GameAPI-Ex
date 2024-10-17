using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using static RSDK.EngineAPI;
using static RSDK.GameObject;

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
        NEVER, // never update
        ALWAYS, // always update (even if paused/frozen)
        NORMAL, // always update (unless paused/frozen)
        PAUSED, // update only when paused/frozen
        BOUNDS, // update if in x & y bounds
        XBOUNDS, // update only if in x bounds (y bounds dont matter)
        YBOUNDS, // update only if in y bounds (x bounds dont matter)
        RBOUNDS, // update based on radius boundaries (updateRange.x == radius)

        // Not really even a real active value, but some objects set their active states to this so here it is I suppose
        DISABLED = 0xFF,
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
            public abstract void Update();
            public abstract void LateUpdate();
            public abstract static void StaticUpdate();
            public abstract void Draw();
            public abstract void Create(void* data);
            public abstract static void StageLoad();
#if RETRO_REV0U
            public abstract static void StaticLoad(void* sVars);
#endif
            public abstract static void Serialize();
#if GAME_INCLUDE_EDITOR
            public abstract static void EditorLoad();
            public abstract void EditorDraw();
#endif
        }

        [StructLayout(LayoutKind.Sequential)]
        public class Entity : IEntity
        {
            // ----------------------
            // Standard Entity Events
            // ----------------------

            public void Update() { }
            public void LateUpdate() { }
            public static void StaticUpdate() { }
            public void Draw() { }
            public void Create(void* data) { }
            public static void StageLoad() { }
#if RETRO_REV0U
            public static void StaticLoad(void* data)
            {
                var sVars = (Static*)data;

                sVars->classID = (ushort)DefaultObjects.TYPE_DEFAULTOBJECT;
                sVars->active = (byte)ActiveFlags.NEVER;
            }
#endif
            public static void Serialize() { }
#if GAME_INCLUDE_EDITOR
            public static void EditorLoad() { }
            public void EditorDraw() { }
#endif

            // ----------------
            // Entity Variables
            // ----------------

#if RETRO_REV0U
            private void* vfTable;
#endif
            public Vector2 position;
            public Vector2 scale;
            public Vector2 velocity;
            public Vector2 updateRange;
            public int angle;
            public int alpha;
            public int rotation;
            public int groundVel;
            public int zdepth;
            public ushort group;
            public ushort classID;
            public bool32 inRange;
            public bool32 isPermanent;
            public bool32 tileCollisions;
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
            public Boolean<byte> visible;
            public Boolean<byte> onScreen;

            // ----------------------
            // Extra Entity Functions
            // ----------------------

            public void Init()
            {
                active = ActiveFlags.BOUNDS;
                visible = false;
                updateRange.x = MathRSDK.TO_FIXED(128);
                updateRange.y = MathRSDK.TO_FIXED(128);
            }

            // can't really do &this, so hoping that sceneInfo->entity will be fine...

            public ushort Slot() => (ushort)RSDKTable->GetEntitySlot(This<Entity>());
            public void Destroy() => RSDKTable->ResetEntity(This<Entity>(), (ushort)DefaultObjects.TYPE_DEFAULTOBJECT, null);

            public void Reset(uint type, void* data) => RSDKTable->ResetEntity(This<Entity>(), (ushort)type, data);
            public void Reset(uint type, int data) => RSDKTable->ResetEntity(This<Entity>(), (ushort)type, MathRSDK.INT_TO_VOID(data));

            public void Copy(Entity* dst, bool32 clearThis) => RSDKTable->CopyEntity(dst, This<Entity>(), clearThis);

            public bool32 CheckOnScreen(Vector2* range) => RSDKTable->CheckOnScreen(This<Entity>(), range);

            public void AddDrawListRef(byte drawGroup) => RSDKTable->AddDrawListRef(drawGroup, Slot());

            public bool32 TileCollision(ushort collisionLayers, byte collisionMode, byte collisionPlane, int xOffset, int yOffset, bool32 setPos)
            {
                return RSDKTable->ObjectTileCollision(This<Entity>(), collisionLayers, collisionMode, collisionPlane, xOffset, yOffset, setPos);
            }

            public bool32 TileGrip(ushort collisionLayers, byte collisionMode, byte collisionPlane, int xOffset, int yOffset, int tolerance)
            {
                return RSDKTable->ObjectTileGrip(This<Entity>(), collisionLayers, collisionMode, collisionPlane, xOffset, yOffset, tolerance);
            }

            public void ProcessMovement(Hitbox* outerBox, Hitbox* innerBox) => RSDKTable->ProcessObjectMovement(This<Entity>(), outerBox, innerBox);

            public bool32 CheckCollisionTouchBox(Hitbox* thisHitbox, Entity* other, Hitbox* otherHitbox)
            {
                return RSDKTable->CheckObjectCollisionTouchBox(This<Entity>(), thisHitbox, other, otherHitbox);
            }

            public bool32 CheckCollisionTouchCircle(int thisRadius, Entity* other, int otherRadius)
            {
                return RSDKTable->CheckObjectCollisionTouchCircle(This<Entity>(), thisRadius, other, otherRadius);
            }

            public byte CheckCollisionBox(Hitbox* thisHitbox, Entity* other, Hitbox* otherHitbox, uint setPos = 1)
            {
                return RSDKTable->CheckObjectCollisionBox(This<Entity>(), thisHitbox, other, otherHitbox, setPos);
            }

            public bool32 CheckCollisionPlatform(Hitbox* thisHitbox, Entity* other, Hitbox* otherHitbox, uint setPos = 1)
            {
                return RSDKTable->CheckObjectCollisionPlatform(This<Entity>(), thisHitbox, other, otherHitbox, setPos);
            }

#if RETRO_USE_MOD_LOADER
            public void Super(int callback, void* data = null) => modTable->Super(classID, callback, data);
#endif
        }

        public static T* This<T>() => (T*)sceneInfo->entity;

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

        public static void Reset(ushort slot, ushort type, void* data) => RSDKTable->ResetEntitySlot(slot, type, data);
        public static void Reset(ushort slot, ushort type, int data) => RSDKTable->ResetEntitySlot(slot, type, MathRSDK.INT_TO_VOID(data));

        
        public static void Reset<T>(ushort slot, void* data)
        {
            var sVars = (Static*)Managed.GetFieldPtr<Static>(typeof(T), "sVars");
            RSDKTable->ResetEntitySlot(slot, sVars->classID, data);
        }
        public static void Reset<T>(ushort slot, int data)
        {
            /*
            FieldInfo sVarsField = typeof(T).GetField("sVars", BindingFlags.Static | BindingFlags.Public);
            IntPtr sVarsPtr = (IntPtr)sVarsField.GetValue(null);
            Static* sVars = (Static*)sVarsPtr.ToPointer();
            */

            // typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);

            var sVars = (Static*)Managed.GetFieldPtr<Static>(typeof(T), "sVars");
            RSDKTable->ResetEntitySlot(slot, sVars->classID, MathRSDK.INT_TO_VOID(data));
        }

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
                    update = Managed.GetFunctionPtr<Action>(E, "_Update"),
                    lateUpdate = Managed.GetFunctionPtr<Action>(E, "_LateUpdate"),
                    staticUpdate = Managed.GetFunctionPtr<Action>(E, "StaticUpdate"),
                    draw = Managed.GetFunctionPtr<Action>(E, "_Draw"),
                    create = Managed.GetFunctionPtr<DelegateTypes.TakesVoidPtr>(E, "_Create"),
                    stageLoad = Managed.GetFunctionPtr<Action>(E, "StageLoad"),
#if GAME_INCLUDE_EDITOR
                    editorLoad = Managed.GetFunctionPtr<Action>(E, "EditorLoad"),
                    editorDraw = Managed.GetFunctionPtr<Action>(E, "_EditorDraw"),
#endif
                    serialize = Managed.GetFunctionPtr<Action>(E, "Serialize"),
#if RETRO_REV0U
                    staticLoad = Managed.GetFunctionPtr<DelegateTypes.TakesVoidPtr>(E, "StaticLoad"),
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
