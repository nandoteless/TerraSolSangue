using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueUI : MonoBehaviour
{
    Image background;
    TextMeshProUGUI nameText;
    TextMeshProUGUI talkText;

    public float speed = 10f;
    bool open = false;

    void Awake()
    {
        background = transform.GetChild(0).GetComponent<Image>();
        nameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        talkText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (open)
        {
            background.fillAmount = Mathf.Lerp(background.fillAmount, 1, speed * Time.deltaTime);
        }
        else
        {
            background.fillAmount = Mathf.Lerp(background.fillAmount, 0, speed * Time.deltaTime);
        }


        // DMVS - revejam se precisa, pois está dando erro após ser destruído
        // caso precise deve ser adaptado ao input system
         if (Input.GetKey(KeyCode.F))
        {
             Destroy(background); // Destroys gameobject when user presses 'Space' key.
         }

         if (Input.GetKey(KeyCode.F))
         {
             Destroy(nameText); // Destroys gameobject when user presses 'Space' key.
        }

         if (Input.GetKey(KeyCode.F))
        {
             Destroy(talkText); // Destroys gameobject when user presses 'Space' key.
         }

    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void Enable()
    {
        background.fillAmount = 0;
        open = true;
    }

    public void Disable()
    {
        open = false;
        nameText.text = "";
        talkText.text = "";
    }

}



