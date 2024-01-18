using System.Collections;
using System.Collections.Generic;

namespace StatesEnum 
{
    public enum GameState
    {
        mainMenu,
        paused,
        cutscene,
        dialogue,
        gameLoop,
        boss
    }

    public enum PlayerState
    {
        moving,

    }

    public enum MovementType
    {
        standard,
        dino,
        snowMobile,
        tank,
        ufo
    }
}
