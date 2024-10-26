using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] public class TextInfomation
{

    public string speakerName;

    [TextArea(2, 10)]
    public string paragraphs;

}

[CreateAssetMenu(fileName = "DialogueText", menuName = "ScriptableObjects/CreateDialogueText")]
public class DialogueText : ScriptableObject
{
    

    public TextInfomation[] textInfomations;

}
