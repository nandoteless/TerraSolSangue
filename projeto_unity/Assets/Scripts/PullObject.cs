using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

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
        if (Input.GetKey(pullKey) && objectToPull != null)
        {
            if (!isPulling)
            {
                isPulling = true;
                StartDraggingSound();
            }
        }
        else
        {
            if (isPulling)
            {
                isPulling = false;
                StopDraggingSound();
            }
        }

        if (isPulling && objectToPull != null)
        {
            PullTheObject();
        }
    }

    void PullTheObject()
    {
        objectToPull.transform.position = Vector3.Lerp(objectToPull.transform.position, transform.position, Time.deltaTime * 5f);
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
            StopDraggingSound();
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
