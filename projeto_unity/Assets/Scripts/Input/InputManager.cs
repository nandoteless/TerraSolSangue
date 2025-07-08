using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instancia;
    public Vector2 movement;
    public bool interact;
    public bool cancelInteract;

    public static bool collect;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); //Para manter entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }


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

    public void CancelInteractEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
            cancelInteract = true;
        if (context.canceled)
            cancelInteract = false;
    }

    public void CollectEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
            collect = true;
        if (context.canceled)
            collect = false;
    }
}
