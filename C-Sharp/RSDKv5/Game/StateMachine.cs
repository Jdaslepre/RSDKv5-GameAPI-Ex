namespace RSDK
{
    public enum Priorities
    {
        PRIORITY_NONE = 0,
        PRIORITY_LOCKED = 0xFF,
    }

    // Native C#-Safe StateMachine
    public struct StateMachine
    {
        public StateMachine() { Init(); }

        public StateMachine(Action state, Priorities priority = Priorities.PRIORITY_NONE)
        {
            Init();
            Set(state, priority);
        }
        public StateMachine(in StateMachine state)
        {
            Init();
            Copy(state);
        }

        public void Init()
        {
            state = null;
            timer = 0;
            priority = Priorities.PRIORITY_NONE;
        }

        public bool Set(Action state, Priorities priority = Priorities.PRIORITY_NONE)
        {
            if (priority < this.priority || this.priority == Priorities.PRIORITY_LOCKED)
                return false;

            this.state = state;
            this.timer = 0;
            this.priority = priority;
            return true;
        }

        public bool SetAndRun(Action state, Priorities priority = Priorities.PRIORITY_NONE)
        {
            bool applied = Set(state, priority);
            if (applied)
                Run();
            return applied;
        }

        public bool QueueForTime(Action state, int duration, Priorities priority = Priorities.PRIORITY_NONE)
        {
            if (priority < this.priority || this.priority == Priorities.PRIORITY_LOCKED)
                return false;

            this.state = state;
            this.timer = duration;
            this.priority = priority;

            return true;
        }

        public bool Matches(Action? other) { return state == other; }

        public void Run()
        {
            if (timer != 0)
                timer--;

            if (state != null)
                state();
        }

        public void Copy(in StateMachine other)
        {
            state = other.state;
            timer = other.timer;
            priority = other.priority;
        }

        public Action? state;
        public int timer;
        public Priorities priority;
    }

    // C API StateMachine
    public unsafe struct StateMachineC
    {
        public delegate* unmanaged<void> state;
    }

    // C++ API StateMachine
    public unsafe struct StateMachineCPP
    {
        public delegate* unmanaged<void> state;
        public int timer;
        public byte unknown1;
        public byte unknown2;
        public byte unknown3;
        public byte priority;
    }
}
