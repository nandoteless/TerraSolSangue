using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchControll : MonoBehaviour
{
    void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }
}
