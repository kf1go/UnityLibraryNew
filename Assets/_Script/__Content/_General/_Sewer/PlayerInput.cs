using System;
using UnityEngine;

/// <summary>
/// class for general player input
/// TODO : THIS DOTESNT CONTAIN ALL THE INPUTS
/// obsolete.
/// </summary>
[DefaultExecutionOrder(-200)]
public class PlayerInput : MonoBehaviour
{
    #region Weapon
    public event Action<InputArgument> Fire;
    public event Action<InputArgument> Fire_Alt;
    //public event Action<InputArgument> Aim;
    public event Action<InputArgument> Reload;
    //public event Action<InputArgument> Cock;
    //public event Action<InputArgument> Unload;
    #endregion

    private void Update()
    {
        //#region GeneralInput

        //Vector3 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //mouseDelta *= MouseAccelation;
        //MouseLocal += new Vector3(mouseDelta.x, 0, mouseDelta.y);

        //MouseLocal = Vector3.ClampMagnitude(MouseLocal, MaxMouseLocalLength);

        //Vector3 mousePos = Input.mousePosition;
        //Ray mouseRay = _playerCamera.StaticCamera.ScreenPointToRay(mousePos);

        //if (_tempPlane.Raycast(mouseRay, out float distance))
        //{
        //    Vector3 hitPoint = mouseRay.GetPoint(distance);
        //    /*WorldMosuePosition = hitPoint;*/
        //    /*WorldMouseDirection = hitPoint - _player.transform.position;*/
        //}

        //InputMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //InputMovementNormalized = InputMovement.normalized;
        //#endregion

        #region ItemInput
        //if (UIManager.UIStack.Count == 0)
        //{
        //    //general weapon
        //    //firearm
        //    ProcessInput(KeyCode.Mouse0, Fire);
        //    ProcessInput(KeyCode.Mouse1, Aim);
        //    ProcessInput(KeyCode.X, Unload);
        //    ProcessInput(KeyCode.R, Reload);
        //    ProcessInput(KeyCode.C, Cock);
        //}

        ProcessInput(KeyCode.Mouse0, Fire);
        //ProcessInput(KeyCode.Mouse1, Aim);
        ProcessInput(KeyCode.Mouse2, Fire_Alt);
        ProcessInput(KeyCode.R, Reload);

        //ProcessInput(KeyCode.X, Unload);
        #endregion

        //#region UI  
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (OnUICancel != null)
        //    {
        //        OnUICancel.Invoke();
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    if (OnInventoryOpen != null)
        //    {
        //        OnInventoryOpen.Invoke();
        //    }
        //}
        //#endregion

        /*#region Cheat
        ProcessInput(KeyCode.F1, F1);
        ProcessInput(KeyCode.F2, F2);
        ProcessInput(KeyCode.F3, F3);
        ProcessInput(KeyCode.F4, F4);
        ProcessInput(KeyCode.F5, F5);
        ProcessInput(KeyCode.F6, F6);
        ProcessInput(KeyCode.F7, F7);
        ProcessInput(KeyCode.F8, F8);
        #endregion*/
    }
    public static bool TryGetInput(KeyCode keyCode, out InputArgument inputArgument)
    {
        bool result = Input.GetKey(keyCode);

        inputArgument = new InputArgument(); // TODO : this should be null object if result is false

        if (result)
        {
            bool isPressedInThisFame = Input.GetKeyDown(keyCode);

            inputArgument.isPressedInThisFrame = isPressedInThisFame;
            if (isPressedInThisFame)
            {
                inputArgument.firstPressedTime = Time.time;
            }
            inputArgument.timeSincePressed = Time.time - inputArgument.firstPressedTime;
        }

        inputArgument.isKeyUP = Input.GetKeyUp(keyCode);

        return result;
    }
    private static void ProcessInput(KeyCode keyCode, Action<InputArgument> action)
    {
        if (!TryGetInput(keyCode, out InputArgument inputArgument))
        {
            inputArgument.isKeyUP = true;
        }
        if (action != null)
        {
            action.Invoke(inputArgument);
        }
    }
}
