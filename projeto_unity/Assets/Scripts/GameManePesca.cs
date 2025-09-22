using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManePesca : MonoBehaviour
{
   public static GameManePesca Instance;
    private int peixesPescados = 0;
    public int totalPeixes = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // mant�m o GameManager entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PescadoPeixe()
    {
        peixesPescados++;
        Debug.Log($"Pescou: {peixesPescados}/{totalPeixes}");

        if (peixesPescados >= totalPeixes)
        {
            GameObject musica = GameObject.FindGameObjectWithTag("Musica");
        if (musica != null)
        {
            Destroy(musica);
        }
            SceneManager.LoadScene("Cuts2"); // troca de fase
        }
    }
}