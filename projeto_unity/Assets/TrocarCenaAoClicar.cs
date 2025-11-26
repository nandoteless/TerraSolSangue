using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrocarCenaAoClicar : MonoBehaviour
{
     public string nomeCena;

    private void OnMouseDown()
    {
        // Troca de cena quando o sprite for clicado
        SceneManager.LoadScene(nomeCena);
    }
}
