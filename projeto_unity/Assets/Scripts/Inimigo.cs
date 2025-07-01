using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
   
    public void TomarDano()
    {
        Destroy(gameObject);
    }
}
