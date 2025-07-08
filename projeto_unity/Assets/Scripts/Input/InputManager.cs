using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instancia;
    public Vector2 movement;
    private bool interact;
    private bool cancelInteract;

    private bool collect;

    
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
        else
            interact = false;
    }

    public void CancelInteractEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
            cancelInteract = true;
        else
            cancelInteract = false;
    }

    public void CollectEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
            collect = true;
        else
            collect = false;
    }

    public bool GetInteract()
    {
        if (interact)
        {
            interact = false;
            return true;
        }
        return false;
    }
    public bool GetCancelInteraction()
    {
        if (cancelInteract)
        {
            cancelInteract = false;
            return true;
        }
        return false;
    }

    public bool GetCollect()
    {
        if (collect)
        {
            collect = false;
            return true;
        }
        return false;
    }


}
