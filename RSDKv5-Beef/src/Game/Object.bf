using System.Collections;

namespace RSDK;

public enum ActiveFlags : uint8
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

    [System.CRepr] public struct Entity
    {
#if RETRO_REV0U
        // other languages have this vfTable field specifically for beef classes
        // we aren't actually able to make use of classes though, because all objects
        // would have to be REV0U-exclusive...
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

        public void Init() mut
        {
            active        = .ACTIVE_BOUNDS;
            visible       = false;
            updateRange.x = TO_FIXED(128);
            updateRange.y = TO_FIXED(128);
        }

        public uint16 Slot() mut  => (.)RSDKTable.GetEntitySlot(&this);
        public void Destroy() mut => RSDKTable.ResetEntity(&this, (.)DefaultObjects.TYPE_DEFAULTOBJECT, null);

        public void Reset(uint32 type, void* data) mut => RSDKTable.ResetEntity(&this, (.)type, data);
        public void Reset(uint32 type, int32 data) mut => RSDKTable.ResetEntity(&this, (.)type, INT_TO_VOID(data));

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
    public static void InitStatic<T>(T* sVars) => System.Internal.MemSet(sVars, 0, sizeof(T));;

    public static GameObject.Entity* Create(void* data, int32 x, int32 y) => (.)RSDKTable.CreateEntity((.)DefaultObjects.TYPE_DEFAULTOBJECT, data, x, y);
    public static GameObject.Entity* Create(int32 data, int32 x, int32 y) => (.)RSDKTable.CreateEntity((.)DefaultObjects.TYPE_DEFAULTOBJECT, INT_TO_VOID(data), x, y);

    public static GameObject.Entity* Create(void* data, Vector2 position) => Create(data, position.x, position.y);
    public static GameObject.Entity* Create(int32 data, Vector2 position) => Create(data, position.x, position.y);

    public static GameObject.Entity* Create(void* data, Vector2* position) => Create(data, position.x, position.y);
    public static GameObject.Entity* Create(int32 data, Vector2* position) => Create(data, position.x, position.y);

    public static T* Create<T>(void* data, int32 x, int32 y) where T : struct
    {
        typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);
        return (.)RSDKTable.CreateEntity(fStatic.classID, data, x, y);
    }

    public static T* Create<T>(int32 data, int32 x, int32 y) where T : GameObject.Entity
    {
        typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);
        return (.)RSDKTable.CreateEntity(fStatic.classID, INT_TO_VOID(data), x, y);
    }

    public static T* Create<T>(void* data, Vector2 position) where T : struct => Create<T>(data, position.x, position.y);
    public static T* Create<T>(int32 data, Vector2 position) where T : GameObject.Entity => Create<T>(data, position.x, position.y);

    public static T* Create<T>(void* data, Vector2* position) where T : struct => Create<T>(data, position.x, position.y);
    public static T* Create<T>(int32 data, Vector2* position) where T : GameObject.Entity => Create<T>(data, position.x, position.y);

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
    public static void Reset(uint16 slot, uint16 type, int32 data) => RSDKTable.ResetEntitySlot(slot, type, INT_TO_VOID(data));

    public static void Reset<T>(uint16 slot, void* data)
    {
        typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);
        RSDKTable.ResetEntitySlot(slot, fStatic.classID, data);
    }
    public static void Reset<T>(uint16 slot, int32 data)
    {
        typeof(T).GetField("sVars").Value.GetValue<Static*>(null, var fStatic);
        RSDKTable.ResetEntitySlot(slot, fStatic.classID, INT_TO_VOID(data));
    }

    public static void Destroy(uint16 slot) => Reset(slot, (.)DefaultObjects.TYPE_DEFAULT_COUNT, null);

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

    public struct ObjectRegistration
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
    }

    [System.CRepr] public struct EntityBase : GameObject.Entity
    {
        public void[0x100]* data;
#if RETRO_REV0U
        public void *unknown;
#endif
    }

    public static int32 registerObjectListCount = 0;
    public static ObjectRegistration[Const.OBJECT_COUNT] registerObjectList;

#if RETRO_REV02
    public static int32 registerStaticListCount = 0;
    public static ObjectRegistration[Const.OBJECT_COUNT] registerStaticList;
#endif

    // Don't directly call these functions, use the attributes instead

    private static staticVars* RegisterObject<entity, staticVars>(ref staticVars* sVars, char8* name,
        function void() update, function void() lateUpdate, function void() staticUpdate, function void() draw,
        function void(void* data) create, function void() stageLoad, function void() editorLoad, function void() editorDraw,
        function void() serialize, function void(void* staticVars) staticLoad)
    {
        if (registerObjectListCount < Const.OBJECT_COUNT)
        {
            var object = &registerObjectList[registerObjectListCount++];
            System.Internal.MemSet(object, 0, sizeof(ObjectRegistration));
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
            object.staticVars      = (.)&sVars;
            object.entityClassSize = (.)sizeof(entity);
            object.staticClassSize = (.)sizeof(staticVars);
        }

        sVars = null;
        return null;
    }

#if RETRO_REV02
    private static staticVars* RegisterStaticVars<staticVars>(ref staticVars* sVars, char8* name)
    {
        if (registerStaticListCount < Const.OBJECT_COUNT)
        {
            var object = &registerStaticList[registerStaticListCount++];
            System.Internal.MemSet(object, 0, sizeof(ObjectRegistration));
            object.name = name;
            object.staticVars      = (.)(void**)&sVars;
            object.staticClassSize = (.)sizeof(staticVars);
        }

        sVars = null;
        return null;
    }
#endif

#if RETRO_USE_MOD_LOADER
    private static staticV* ModRegisterObject<entity, staticV, modStaticV>(ref staticV* sVars, ref modStaticV* modsVars, char8* name,
        function void() update, function void() lateUpdate, function void() staticUpdate, function void() draw,
        function void(void* data) create, function void() stageLoad, function void() editorLoad, function void() editorDraw,
        function void() serialize, function void(void* staticVars) staticLoad, char8* inherit)
    {
        if (registerObjectListCount < Const.OBJECT_COUNT)
        {
            var object = &registerObjectList[registerObjectListCount++];
            System.Internal.MemSet(object, 0, sizeof(ObjectRegistration));
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
            object.staticVars         = (.)&sVars;
            object.modStaticVars      = (.)&modsVars;
            object.entityClassSize    = (.)sizeof(entity);
            object.staticClassSize    = (.)sizeof(staticV);
            object.modStaticClassSize = (.)sizeof(modStaticV);

            object.isModded = true;
            object.inherit  = inherit;
        }

        sVars = null;
        modsVars = null;
        return null;
    }
#endif
}