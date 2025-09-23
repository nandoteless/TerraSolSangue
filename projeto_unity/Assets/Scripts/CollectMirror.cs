using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectMirror : MonoBehaviour
{
    public TextMeshProUGUI mirrorCountTMP;
    private int mirrorCount = 0;
    private int mirrorsNeeded = 3;

    private bool canCollect = false; // se o player está colidindo com o espelho
    private GameObject mirrorToCollect;

    void Start()
    {
        UpdateMirrorCountText();
    }

    public void OnCollect(InputAction.CallbackContext context)
    {
        if (context.performed && canCollect && mirrorToCollect != null)
        {
            Destroy(mirrorToCollect);
            mirrorCount++;
            UpdateMirrorCountText();
        }

        if (context.performed && mirrorCount >= mirrorsNeeded)
        {
            SceneManager.LoadScene("Fase2pt2");
        }
    }
    public void CollectFromUIButton()
{
    if (canCollect && mirrorToCollect != null)
    {
        Destroy(mirrorToCollect);
        mirrorCount++;
        UpdateMirrorCountText();
    }

    if (mirrorCount >= mirrorsNeeded)
    {
        SceneManager.LoadScene("Fase2pt2");
    }
}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mirror"))
        {
            canCollect = true;
            mirrorToCollect = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mirror"))
        {
            canCollect = false;
            mirrorToCollect = null;
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
