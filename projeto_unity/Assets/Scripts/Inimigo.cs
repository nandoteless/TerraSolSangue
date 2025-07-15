using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class Inimigo : MonoBehaviour
{
    [Header("FMOD")]
    [SerializeField] private EventReference som;
    public void TomarDano()
    {   
        RuntimeManager.PlayOneShot(som, transform.position);
        Destroy(gameObject);
    }
}
