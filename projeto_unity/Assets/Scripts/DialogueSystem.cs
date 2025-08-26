using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using TMPro;
public enum STATE
{
    DISABLED,
    WAITING,
    TYPING
}

public class DialogueSystem : MonoBehaviour
{

    public DialogueData dialogueData;

    int currentText = 0;
    bool finished = false;

    TypeTextAnimation typeText;
    DialogueUI dialogueUI;

    STATE state;

    void Awake()
    {

        typeText = FindObjectOfType<TypeTextAnimation>();
        dialogueUI = FindObjectOfType<DialogueUI>();

        typeText.TypeFinished = OnTypeFinishe;

    }

    void Start()
    {
        state = STATE.DISABLED;
    }


    public void StartDialogue()
    {
        if (state == STATE.DISABLED)
        {
            dialogueUI.Enable();
            Next();
        }
    }

    public void Next()
    {

        if (state != STATE.TYPING)
        {

            dialogueUI.SetName(dialogueData.talkScript[currentText].name);
            typeText.fullText = dialogueData.talkScript[currentText].text;
            currentText++;
            typeText.StartTyping();
            state = STATE.TYPING;
            if (currentText == dialogueData.talkScript.Count) {
                finished = true;
            }
            
        }
    }

    void OnTypeFinishe()
    {
        state = STATE.WAITING;
    }

    void Waiting()
    {
        if (!finished)
        {
            Next();
        }
        else
        {
            dialogueUI.Disable();
            state = STATE.DISABLED;
            currentText = dialogueData.talkScript.Count - 1;
            finished = false;
        }



    }

    void Typing()
    {
        typeText.Skip();
        state = STATE.WAITING;
    }

    public void CancelInteractEventEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (state == STATE.DISABLED) return;

            switch (state)
            {
                case STATE.WAITING:
                    Waiting();
                    break;
                case STATE.TYPING:
                    Typing();
                    break;
            }
        }
    }
}















