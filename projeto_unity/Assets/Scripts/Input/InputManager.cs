using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 movement;
    public static bool interact;

    public void MoveEvent(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void InteractEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
            interact = true;
        if (context.canceled)
            interact = false;
    }
}
