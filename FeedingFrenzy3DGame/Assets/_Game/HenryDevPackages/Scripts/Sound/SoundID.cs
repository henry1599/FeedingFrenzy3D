using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AudioPlayer
{
    [Serializable]
    public enum SoundID
    {
        NONE,
        ____GAMPLAY____ = 10,
        SFX_CHOOSE_COLLECTABLE_OBJECT,
        SFX_CHOOSE_WRONG_COLLECTABLE_OBJECT,
        SFX_CHOOSE_EASTER_EGG_OBJECT,
        ____UI____ = 100,
        SFX_UI_BUTTON_CLICK,
        SFX_UI_WIN,
        SFX_UI_START_MAIN
    }
}