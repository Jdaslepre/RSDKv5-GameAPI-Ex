namespace RSDK;

static
{
    public static char8* currentState = null;
}

[System.CRepr] public struct StateMachine<T>
{
    public enum Priority : uint8
    {
        NORMAL   = 0,
        LOCKED = 0xFF,
    }

    public void Init() mut => System.Internal.MemSet(&this, 0, sizeof(Self));

    public bool32 Set(function void(T this) statePtr, Priority priorityType = .NORMAL) mut
    {
        if (priorityType < this.priority || this.priority == .LOCKED)
            return false;

        ptr = statePtr;
        timer = 0;
        priority = priorityType;

        return true;
    }

    public bool32 SetAndRun(function void(T this) statePtr, T self, Priority priorityType = .NORMAL) mut
    {
        bool32 applied = Set(statePtr, priorityType);
        if (applied)
            Run(self);
        return applied;
    }

    public bool32 QueueForTime(function void(T this) statePtr, uint32 duration, Priority priorityType = .NORMAL) mut
    {
        if (priorityType < this.priority || this.priority == .LOCKED)
            return false;

        ptr = statePtr;
        timer    = (.)duration;
        priority = priorityType;

        return true;
    }

    public void Run(T entity) mut
    {
        if (timer != 0)
            timer--;
        if (ptr == null)
            return;

        currentState = null;
#if RETRO_USE_MOD_LOADER
        modTable.StateMachineRun(*(function void()*)&ptr);
#else
        ptr(entity);
#endif
    }

    // TODO?
    public bool32 Matches(function void(T this) other) => ptr == other;

    public void Copy(Self* other) mut => System.Internal.MemCpy(&this, other, sizeof(Self));

    private function void(T this) ptr;
    public int32 timer;
    private uint8[3] unknown;
    public Priority priority;
}

// C API StateMachine
[System.CRepr] public struct StateMachineC<T>
{
    public bool32 Set(function void(T this) statePtr) mut
    {
        ptr = statePtr;

        return true;
    }

    public void Run(T entity) mut
    {
#if RETRO_USE_MOD_LOADER
        // Nasty void(T this) -> void() conversion
        modTable.StateMachineRun(*(function void()*)&ptr);
#else
        ptr(entity);
#endif
    }

    // TODO?
    public bool32 Matches(function void(T this) other) { return ptr == other; }

    private function void(T this) ptr;
}