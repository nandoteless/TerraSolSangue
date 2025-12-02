using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCenaPorTempo : MonoBehaviour
{
    public string proximaCena;
    public float tempo = 10f;

    void Start()
    {
        StartCoroutine(EsperarETrocar());
    }

    IEnumerator EsperarETrocar()
    {
        yield return new WaitForSeconds(tempo);
        SceneManager.LoadScene(proximaCena);
    }
}
