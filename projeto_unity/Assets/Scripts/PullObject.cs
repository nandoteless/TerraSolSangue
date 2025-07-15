using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{
   
    public KeyCode pullKey = KeyCode.E;
    private GameObject objectToPull;
    private bool isPulling = false;

    void Update()
    {
        if (Input.GetKey(pullKey) && objectToPull != null)
        {
            isPulling = true;
        }
        else
        {
            isPulling = false;
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
        }
    }
}