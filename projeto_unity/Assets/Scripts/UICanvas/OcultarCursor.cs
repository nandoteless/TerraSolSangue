using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcultarCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;              // Oculta o cursor
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
    }
}
