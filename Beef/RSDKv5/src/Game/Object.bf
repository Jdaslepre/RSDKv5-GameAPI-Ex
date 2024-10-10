using System.Collections;

namespace RSDK;

public enum ActiveFlags : uint8
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

public enum VariableTypes : uint8
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
    ALL_ENTITIES,
    ACTIVE_ENTITIES,
#if RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
    GROUP_ENTITIES,
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

public enum DefaultObjects : uint16
{
    TYPE_DEFAULTOBJECT,
#if RETRO_REV02
    TYPE_DEVOUTPUT,
#endif
    TYPE_DEFAULT_COUNT, // max
}

public enum TileCollisionModes : uint8
{
    TILECOLLISION_NONE, // no tile collisions
    TILECOLLISION_DOWN, // downwards tile collisions
#if RETRO_REV0U
    TILECOLLISION_UP, // upwards tile collisions
#endif
}

[System.CRepr] public class GameObject
{
    // --------------
    // Implementation
    // --------------

    [System.CRepr] public struct Static
    {
        public uint16 classID;
        public uint8 active;

        public void EditableVar(VariableTypes type, char8* name, uint32 offset) => RSDKTable.SetEditableVar((.)type, name, (.)classID, (.)offset);
        public int32 Count(bool32 isActive = false)                             => RSDKTable.GetEntityCount(classID, isActive);

#if RETRO_USE_MOD_LOADER
        public void Super(int32 callback, void *data = null) => modTable.Super(classID, callback, data);
#endif
    }

    public interface IEntity
    {
        public void Update();
        public void LateUpdate();
        public static void StaticUpdate();
        public void Draw();
        public void Create(void* data);
        public static void StageLoad();
#if RETRO_REV0U
        public static void StaticLoad(void* sVars);
#endif
        public static void Serialize();
#if GAME_INCLUDE_EDITOR
        public static void EditorLoad();
        public void EditorDraw();
#endif
    }

    [System.CRepr] public struct Entity : IEntity
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
        public static void StaticLoad(void* sVars) => System.Internal.MemSet(sVars, 0, sizeof(Static));
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
        // used for languages such as beeflang that always have vfTables in classes
        // note from jd, if our object type was class instead of struct, we could've had this commented out - but unfortunately, inheriting a class
        // will add another vTable i think..? so there'd be this vfTable field, and then *another* vfTable that'd completely ruin entity alignment :pensive:
        // having entities as structs is good anyways - because *if* the class idea worked, the objects would absolutely have to be v5U exclusive :D
        private void* vfTable;
#endif

        public Vector2 position;
        public Vector2 scale;
        public Vector2 velocity;
        public Vector2 updateRange;
        public int32 angle;
        public int32 alpha;
        public int32 rotation;
        public int32 groundVel;
        public int32 zdepth;
        public uint16 group;
        public uint16 classID;
        public bool32 inRange;
        public bool32 isPermanent;
        public bool32 tileCollisions;
        public bool32 interaction;
        public bool32 onGround;
        public ActiveFlags active;
#if RETRO_REV02
        public uint8 filter;
#endif
        public FlipFlags direction;
        public uint8 drawGroup;
        public uint8 collisionLayers;
        public uint8 collisionPlane;
        public TileCollisionModes collisionMode;
        public DrawFX drawFX;
        public InkEffects inkEffect;
        public Boolean<uint8> visible;
        public Boolean<uint8> onScreen;

        // ----------------------
        // Extra Entity Functions
        // ----------------------

        public void Init() mut
        {
            active        = .BOUNDS;
            visible       = false;
            updateRange.x = Math.TO_FIXED(128);
            updateRange.y = Math.TO_FIXED(128);
        }

        public uint16 Slot() mut  => (.)RSDKTable.GetEntitySlot(&this);
        public void Destroy() mut => RSDKTable.ResetEntity(&this, (.)DefaultObjects.TYPE_DEFAULTOBJECT, null);

        public void Reset(uint32 type, void* data) mut => RSDKTable.ResetEntity(&this, (.)type, data);
        public void Reset(uint32 type, int32 data) mut => RSDKTable.ResetEntity(&this, (.)type, Math.INT_TO_VOID(data));

        public void Copy(GameObject.Entity* dst, bool32 clearThis) mut => RSDKTable.CopyEntity(dst, &this, clearThis);

        public bool32 CheckOnScreen(Vector2* range) mut => RSDKTable.CheckOnScreen(&this, range);

        public void AddDrawListRef(uint8 drawGroup) mut => RSDKTable.AddDrawListRef(drawGroup, Slot());

        public bool32 TileCollision(uint16 collisionLayers, uint8 collisionMode, uint8 collisionPlane, int32 xOffset, int32 yOffset, bool32 setPos) mut
        {
            return RSDKTable.ObjectTileCollision(&this, collisionLayers, collisionMode, collisionPlane, xOffset, yOffset, setPos);
        }

        public bool32 TileGrip(uint16 collisionLayers, uint8 collisionMode, uint8 collisionPlane, int32 xOffset, int32 yOffset, int32 tolerance) mut
        {
            return RSDKTable.ObjectTileGrip(&this, collisionLayers, collisionMode, collisionPlane, xOffset, yOffset, tolerance);
        }

        public void ProcessMovement(Hitbox* outerBox, Hitbox* innerBox) mut => RSDKTable.ProcessObjectMovement(&this, outerBox, innerBox);

        public bool32 CheckCollisionTouchBox(Hitbox* thisHitbox, GameObject.Entity* other, Hitbox* otherHitbox) mut
        {
            return RSDKTable.CheckObjectCollisionTouchBox(&this, thisHitbox, other, otherHitbox);
        }

        public bool32 CheckCollisionTouchCircle(int32 thisRadius, GameObject.Entity* other, int32 otherRadius) mut
        {
            return RSDKTable.CheckObjectCollisionTouchCircle(&this, thisRadius, other, otherRadius);
        }

        public uint8 CheckCollisionBox(Hitbox* thisHitbox, GameObject.Entity* other, Hitbox* otherHitbox, bool32 setPos = true) mut
        {
            return RSDKTable.CheckObjectCollisionBox(&this, thisHitbox, other, otherHitbox, setPos);
        }

        public bool32 CheckCollisionPlatform(Hitbox* thisHitbox, GameObject.Entity* other, Hitbox* otherHitbox, bool32 setPos = true) mut
        {
            return RSDKTable.CheckObjectCollisionPlatform(&this, thisHitbox, other, otherHitbox, setPos);
        }

#if RETRO_USE_MOD_LOADER
        public void Super(int32 callback, void *data = null) => modTable.Super(classID, callback, data);
#endif
    }

    public static T* This<T>() => (.)sceneInfo.entity;

    public static GameObject.Entity* Create(void* data, int32 x, int32 y) => (.)RSDKTable.CreateEntity((.)DefaultObjects.TYPE_DEFAULTOBJECT, data, x, y);
    public static GameObject.Entity* Create(int32 data, int32 x, int32 y) => (.)RSDKTable.CreateEntity((.)DefaultObjects.TYPE_DEFAULTOBJECT, Math.INT_TO_VOID(data), x, y);

    public static T* Create<T>(void* data, int32 x, int32 y) where T : struct
    {
        typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);
        return (.)RSDKTable.CreateEntity(fStatic.classID, data, x, y);
    }

    public static T* Create<T>(int32 data, int32 x, int32 y) where T : GameObject.Entity
    {
        typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);
        return (.)RSDKTable.CreateEntity(fStatic.classID, Math.INT_TO_VOID(data), x, y);
    }

    public static GameObject.Entity* Get(int32 slot)      => (.)RSDKTable.GetEntity((.)slot);
    public static GameObject.Entity* Get(uint16 slot)     => (.)RSDKTable.GetEntity(slot);
    public static T* Get<T>(int32 slot) where T : struct  => (.)RSDKTable.GetEntity((.)slot);
    public static T* Get<T>(uint16 slot) where T : struct => (.)RSDKTable.GetEntity(slot);

    public static int32 Count<T>(bool32 isActive = false) where T : struct
    {
        typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);
        return RSDKTable.GetEntityCount(fStatic.classID, isActive);
    }

    public static void Copy(GameObject.Entity* dst, GameObject.Entity* src, bool32 clearSrc) => RSDKTable.CopyEntity(dst, src, clearSrc);
    public static void Copy(void* dst, void* src, bool32 clearSrc)                           => RSDKTable.CopyEntity(dst, src, clearSrc);

    public static void Reset(uint16 slot, uint16 type, void* data) => RSDKTable.ResetEntitySlot(slot, type, data);
    public static void Reset(uint16 slot, uint16 type, int32 data) => RSDKTable.ResetEntitySlot(slot, type, Math.INT_TO_VOID(data));

    public static void Reset<T>(uint16 slot, void* data)
    {
        typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);
        RSDKTable.ResetEntitySlot(slot, fStatic.classID, data);
    }
    public static void Reset<T>(uint16 slot, int32 data)
    {
        typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);
        RSDKTable.ResetEntitySlot(slot, fStatic.classID, Math.INT_TO_VOID(data));
    }

    // Example usage:
    // for (var entity in GameObject.GetEntities<Type>(.ALL_ENTITIES, scope .()))

    public static LinkedList<T*> GetEntities<T>(ForeachTypes type, LinkedList<T*> list) where T : struct
    {
        typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);

        uint16 group = fStatic != null ? fStatic.classID : (.)ForeachGroups.GROUP_ALL;

        T* entity = null; defer delete entity;
        switch (type)
        {
            case .ALL_ENTITIES:
                while (RSDKTable.GetAllEntities(group, (void**)&entity)) list.AddLast(entity);
                break;
            case .ACTIVE_ENTITIES:
                while (RSDKTable.GetActiveEntities(group, (void**)&entity)) list.AddLast(entity);
                break;
#if RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
            case .GROUP_ENTITIES:
                while (modTable.GetGroupEntities(group, (void**)&entity)) list.AddLast(entity);
                break;
#endif
            default: break;
        }

        return list;
    }

    // Example usage:
    // for (var entity in GameObject.GetEntities<Type>(.ALL_ENTITIES, OtherType.sVars.classID, scope .()))

    public static LinkedList<T*> GetEntities<T>(ForeachTypes type, uint16 group, LinkedList<T*> list) where T : struct
    {
        T* entity = null; defer delete entity;
        switch (type)
        {
            case .ALL_ENTITIES:
                while (RSDKTable.GetAllEntities(group, (void**)&entity)) list.AddLast(entity);
                break;
            case .ACTIVE_ENTITIES:
                while (RSDKTable.GetActiveEntities(group, (void**)&entity)) list.AddLast(entity);
                break;
#if RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
            case .GROUP_ENTITIES:
                while (modTable.GetGroupEntities(group, (void**)&entity)) list.AddLast(entity);
                break;
#endif
            default: break;
        }

        return list;
    }

    // Example usage:
    // for (var entity in GameObject.GetEntities(.ALL_ENTITIES, scope .()))

    public static LinkedList<GameObject.Entity*> GetEntities(ForeachTypes type, LinkedList<GameObject.Entity*> list)
    {
        GameObject.Entity* entity = null; defer delete entity;

        switch (type)
        {
            case .ALL_ENTITIES:
                while (RSDKTable.GetAllEntities(.(ForeachGroups.GROUP_ALL), (void**)&entity)) list.AddLast(entity);
                break;
            case .ACTIVE_ENTITIES:
                while (RSDKTable.GetActiveEntities(.(ForeachGroups.GROUP_ALL), (void**)&entity)) list.AddLast(entity);
                break;
#if RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
            case .GROUP_ENTITIES:
                while (modTable.GetGroupEntities(.(ForeachGroups.GROUP_ALL), (void**)&entity)) list.AddLast(entity);
                break;
#endif
            default: break;
        }

        return list;
    }

    // Example usage:
    // for (var entity in GameObject.GetEntities(.ALL_ENTITIES, OtherType.sVars.classID, scope .()))

    public static LinkedList<GameObject.Entity*> GetEntities(ForeachTypes type, uint16 group, LinkedList<Entity*> list)
    {
        GameObject.Entity* entity = null; defer delete entity;

        switch (type)
        {
            case .ALL_ENTITIES:
                while (RSDKTable.GetAllEntities(group, (void**)&entity)) list.AddLast(entity);
                break;
            case .ACTIVE_ENTITIES:
                while (RSDKTable.GetActiveEntities(group, (void**)&entity)) list.AddLast(entity);
                break;
#if RETRO_USE_MOD_LOADER && RETRO_MOD_LOADER_VER_2
            case .GROUP_ENTITIES:
                while (modTable.GetGroupEntities(group, (void**)&entity)) list.AddLast(entity);
                break;
#endif
            default: break;
        }

        return list;
    }

    public static int32 Find(char8* name) => RSDKTable.FindObject(name);

#if RETRO_USE_MOD_LOADER
    public static void *FindClass(char8 *name) => modTable.FindObject(name);
#endif

#if GAME_INCLUDE_EDITOR
    public static void SetActiveVariable(int32 classID, char8 *name) => RSDKTable.SetActiveVariable(classID, name);
    public static void AddVarEnumValue(char8 *name)                  => RSDKTable.AddVarEnumValue(name);
#endif

    [System.CRepr] public struct EntityBase : GameObject.Entity
    {
        public void[0x100]* data;
#if RETRO_REV0U
        public void *unknown;
#endif
    };

    // ------------
    // Registration
    // ------------

    public struct Registration
    {
        public char8* name;
        private void* padding;
        public function void() update;
        public function void() lateUpdate;
        public function void() staticUpdate;
        public function void() draw;
        public function void(void* data) create;
        public function void() stageLoad;
#if GAME_INCLUDE_EDITOR
        public function void() editorLoad;
        public function void() editorDraw;
#endif
        public function void() serialize;
#if RETRO_REV0U
        public function void(void*) staticLoad;
#endif
        public void** staticVars;
        public uint32 entityClassSize;
        public uint32 staticClassSize;
#if RETRO_USE_MOD_LOADER
        public void** modStaticVars;
        public uint32 modStaticClassSize;
        public char8* inherit;
        public bool32 isModded;
#endif
    };

    public static int32 registerObjectListCount = 0;
    public static Registration[Const.OBJECT_COUNT] registerObjectList;

#if RETRO_REV02
    public static int32 registerStaticListCount = 0;
    public static Registration[Const.OBJECT_COUNT] registerStaticList;
#endif

    private static staticVars* RegisterInternal<entity, staticVars>(ref staticVars* sVars, char8* name, function void() update, function void() lateUpdate, function void() staticUpdate, function void() draw,
        function void(void* data) create, function void() stageLoad, function void() editorLoad, function void() editorDraw,
        function void() serialize, function void(void* staticVars) staticLoad)
    {
        if (registerObjectListCount < Const.OBJECT_COUNT)
        {
            var object = &registerObjectList[registerObjectListCount++];
            System.Internal.MemSet(object, 0, sizeof(Registration));
            object.name = name;
            object.update = update;
            object.lateUpdate = lateUpdate;
            object.staticUpdate = staticUpdate;
            object.draw = draw;
            object.create = create;
            object.stageLoad = stageLoad;
#if GAME_INCLUDE_EDITOR
            object.editorLoad = editorLoad;
            object.editorDraw = editorDraw;
#endif
            object.serialize = serialize;
#if RETRO_REV0U
            object.staticLoad = staticLoad;
#endif
            object.staticVars      = (.)(void**)&sVars;
            object.entityClassSize = (.)sizeof(entity);
            object.staticClassSize = (.)sizeof(staticVars);
        }

        sVars = null;
        return null;
    }
}
