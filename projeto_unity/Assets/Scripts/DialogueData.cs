using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Dialogue {
	public string name;
	[TextArea(5, 10)]
	public string text;
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObject/TalkScript", order = 1)]
public class DialogueData : ScriptableObject
{
	   public List<Dialogue> talkScript;
	
}
