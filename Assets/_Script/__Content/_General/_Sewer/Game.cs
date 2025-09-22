using System;
using UnityEngine;

public static partial class Game
{
    public static bool ShowCursor
    {
        get => showCursor;
        set
        {
            Debug.Assert(Cursor.visible != value);

            showCursor = value;
            if (value)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    private static bool showCursor;
}
