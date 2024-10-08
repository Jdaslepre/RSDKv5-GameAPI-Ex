using System;

namespace GameLogic;

[AllowDuplicates]
public enum ReservedEntities : uint16
{
    SLOT_PLAYER1 = 0,
    SLOT_PLAYER2 = 1,

    SLOT_PLAYER3    = 2,
    SLOT_PLAYER4    = 3,
    SLOT_POWERUP1   = 4,
    SLOT_PLAYER5    = 4, // Used in TMZ2Outro
    SLOT_POWERUP2   = 5,
    SLOT_PLAYER6    = 5, // Used in TMZ2Outro
    SLOT_POWERUP3   = 6,
    SLOT_POWERUP4   = 7,
    SLOT_POWERUP1_2 = 8,
    SLOT_POWERUP2_2 = 9,
    SLOT_POWERUP3_2 = 10,
    SLOT_POWERUP4_2 = 11,

    SLOT_BSS_SETUP   = 8,
    SLOT_PBL_SETUP   = 8,
    SLOT_UFO_SETUP   = 8,
    SLOT_MUSIC       = 9,
    SLOT_BSS_HUD     = 10,
    SLOT_UFO_CAMERA  = 10,
    SLOT_PBL_CAMERA  = 10,
    SLOT_BSS_MESSAGE = 11,
    SLOT_UFO_HUD     = 11,
    SLOT_ZONE        = 12,
    // 13 = ???
    // 14 = ???
    SLOT_CUTSCENESEQ         = 15,
    SLOT_PAUSEMENU           = 16,
    SLOT_GAMEOVER            = 16,
    SLOT_ACTCLEAR            = 16,
    SLOT_PAUSEMENU_UICONTROL = 17,
    SLOT_PAUSEMENU_BUTTON1   = 18,
    SLOT_PAUSEMENU_BUTTON2   = 19,
    SLOT_PAUSEMENU_BUTTON3   = 20,
    SLOT_DIALOG              = 21,
    SLOT_DIALOG_UICONTROL    = 22,
    SLOT_DIALOG_BUTTONS      = 23,
    SLOT_DIALOG_BUTTON2      = 24,
    SLOT_DIALOG_BUTTON3      = 25,

    SLOT_POPOVER           = 26,
    SLOT_POPOVER_UICONTROL = 27,
    SLOT_POPOVER_BUTTONS   = 28,
    SLOT_POPOVER_BUTTON2   = 29,
    SLOT_POPOVER_BUTTON3   = 30,
    SLOT_POPOVER_BUTTON4   = 31,

    SLOT_BIGBUBBLE_P1 = 32,
    SLOT_BIGBUBBLE_P2 = 33,

    SLOT_BIGBUBBLE_P3 = 34,
    SLOT_BIGBUBBLE_P4 = 36,

    SLOT_BSS_HORIZON    = 32,
    SLOT_UFO_SPEEDLINES = 34,
    SLOT_UFO_PLASMA     = 36,

    SLOT_REPLAYRECORDER_PLAYBACK = 36,
    SLOT_REPLAYRECORDER_RECORD   = 37,
    SLOT_MUSICSTACK_START        = 40,
    //[41-47] are part of the music stack
    SLOT_MUSICSTACK_END = 48,

    SLOT_CAMERA1 = 60,
    SLOT_CAMERA2 = 61,
    SLOT_CAMERA3 = 62,
    SLOT_CAMERA4 = 63,
}

public class GameVariables
{
    public static void** registerGlobals    = null;
    public static int32 registerGlobalsSize = 0;

    public static function void(void* globals) registerGlobalsInitCB = null;

    public void RegisterGlobals(void** globals, int32 size, function void(void* globals) initCB)
    {
        registerGlobals       = globals;
        registerGlobalsSize   = size;
        registerGlobalsInitCB = initCB;
    }
}
