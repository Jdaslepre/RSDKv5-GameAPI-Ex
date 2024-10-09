using System;

namespace RSDK;

/* old attribute, leaving this here just incase
[AttributeUsage(.Class | .Struct, .ReflectAttribute, ReflectUser = .All)]
public struct RegisterClassAttribute : Attribute, IOnTypeInit
{
    public this() { }

    [Comptime]
    public void OnTypeInit(Type entityType, Self* prev)
    {
        if (entityType.IsGenericParam)
            return;

        System.String obj = entityType.GetName(.. scope .());
        System.String regFns = scope .();

#if GAME_INCLUDE_EDITOR
        #if RETRO_REV0U
        regFns.Append("=> _Update, => _LateUpdate, => StaticUpdate, => _Draw, => _Create, => StageLoad, => EditorLoad, => _EditorDraw, => Serialize, => StaticLoad");
        #else
        regFns.Append("=> _Update, => _LateUpdate, => StaticUpdate, => _Draw, => _Create, => StageLoad, => EditorLoad, => _EditorDraw, => Serialize, => null");
        #endif
#else
        #if RETRO_REV0U
        regFns.Append("=> _Update, => _LateUpdate, => StaticUpdate, => _Draw, => _Create, => StageLoad, => null, => null, => Serialize, => StaticLoad");
        #else
        regFns.Append("=> _Update, => _LateUpdate, => StaticUpdate, => _Draw, => Create, => StageLoad, => null, => null, => Serialize, => null");
        #endif
#endif

        Compiler.EmitTypeBody(entityType, scope $"""
                                [System.Reflect] public static Static* sVars = GameObject.[System.Friend]RegisterInternal<Self, Static>(ref sVars, "{obj}", {regFns});

                                public static void _Create(void* data) => (*(Self*)(void*)&sceneInfo.entity).Create(data);
                                public static void _Draw() => (*(Self*)(void*)&sceneInfo.entity).Draw();
                                public static void _Update() => (*(Self*)(void*)&sceneInfo.entity).Update();
                                public static void _LateUpdate() => (*(Self*)(void*)&sceneInfo.entity).LateUpdate();\n
                                """);

#if GAME_INCLUDE_EDITOR
        Compiler.EmitTypeBody(entityType, scope $"public static void _EditorDraw() => (*(Self*)(void*)&sceneInfo.entity).EditorDraw();");
#endif
    }
}
*/

[AttributeUsage(.Class | .Struct, .ReflectAttribute, ReflectUser = .All)]
public struct RegisterClassAttribute : Attribute, IOnTypeInit
{
    public this() { }

    [Comptime]
    public void OnTypeInit(Type entityType, Self* prev)
    {
        if (entityType.IsGenericParam)
            return;

        System.String obj = entityType.GetName(.. scope .());
        System.String regFns = scope .();

        regFns.Append(scope $"""
                                    () => ((Self*)sceneInfo.entity).Update(), () => ((Self*)sceneInfo.entity).LateUpdate(), () => Self.StaticUpdate(),
                                    () => ((Self*)sceneInfo.entity).Draw(), (data) => ((Self*)sceneInfo.entity).Create(data), () => Self.StageLoad(),
                                #if GAME_INCLUDE_EDITOR
                                    () => Self.EditorLoad(), () => ((Self*)sceneInfo.entity).EditorDraw(),
                                #else
                                    () => null, () => null,
                                #endif
                                    () => Self.Serialize(),
                                #if RETRO_REV0U
                                    (data) => Self.StaticLoad(data)
                                #else
                                    (data) => null,
                                #endif\n
                                """);


        Compiler.EmitTypeBody(entityType, scope $"""
                                [System.Reflect]\npublic static Static* sVars = GameObject.[System.Friend]RegisterInternal<Self, Static>(ref sVars, "{obj}", \n{regFns});\n
                                """);
    }
}