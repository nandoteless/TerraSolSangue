using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.InputSystem; // Novo Input System

public class PullObject : MonoBehaviour
{
    public KeyCode pullKey = KeyCode.E;
    private GameObject objectToPull;
    private bool isPulling = false;

    [Header("FMOD")]
    [SerializeField] private EventReference draggingSound; // Som de arrastar do FMOD
    private EventInstance draggingInstance;

    void Start()
    {
        draggingInstance = RuntimeManager.CreateInstance(draggingSound);
    }

    void Update()
    {
        // --- CONTROLE PELO TECLADO ---
        if (Input.GetKeyDown(pullKey))
        {
            StartPull();
        }
        else if (Input.GetKeyUp(pullKey))
        {
            StopPull();
        }

        // Se estiver puxando
        if (isPulling && objectToPull != null)
        {
            PullTheObject();
        }
    }

    // --- CONTROLE PELO INPUT SYSTEM ---
    public void OnPullInput(InputAction.CallbackContext context)
    {
        if (context.performed)   // botão/controle pressionado
        {
            StartPull();
        }
        else if (context.canceled) // botão/controle solto
        {
            StopPull();
        }
    }

    public void StartPull()
    {
        if (objectToPull != null && !isPulling)
        {
            isPulling = true;
            StartDraggingSound();
        }
    }

    public void StopPull()
    {
        if (isPulling)
        {
            isPulling = false;
            StopDraggingSound();
        }
    }

    private void PullTheObject()
    {
        objectToPull.transform.position = Vector3.Lerp(
            objectToPull.transform.position,
            transform.position,
            Time.deltaTime * 5f
        );
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barril"))
        {
            objectToPull = collision.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barril"))
        {
            objectToPull = null;
            StopPull();
        }
    }

    void StartDraggingSound()
    {
        draggingInstance.start();
    }

    void StopDraggingSound()
    {
        draggingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
