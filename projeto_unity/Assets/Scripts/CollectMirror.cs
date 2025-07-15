using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // IMPORTANTE para usar TMP

public class CollectMirror : MonoBehaviour
{
   public TextMeshProUGUI mirrorCountTMP; // Referência ao TMP Text
    private int mirrorCount = 0;
    private int mirrorsNeeded = 3;

    void Start()
    {
        UpdateMirrorCountText();
    }

    void Update()
    {
        if (mirrorCount >= mirrorsNeeded && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Fase2pt2"); // substitua pelo nome da próxima cena
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mirror") && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(collision.gameObject);
            mirrorCount++;
            UpdateMirrorCountText();
        }
    }

    void UpdateMirrorCountText()
    {
        if (mirrorCountTMP != null)
        {
            mirrorCountTMP.text = mirrorCount + "/" + mirrorsNeeded + " ";
        }
    }
}
