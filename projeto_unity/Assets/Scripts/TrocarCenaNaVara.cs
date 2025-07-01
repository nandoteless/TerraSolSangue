using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCenaNaVara : MonoBehaviour
{
    public string nomeDaCena = "CenaDaVara";

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.name == "Vara")
        {
            SceneManager.LoadScene(nomeDaCena);
        }
    }
}