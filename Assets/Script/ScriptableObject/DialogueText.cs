using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] public class TextInfomation
{
    //話者の名前
    public string speakerName;
    //表示するテキスト
    [TextArea(2, 10)]
    public string paragraphs;
}

[CreateAssetMenu(fileName = "DialogueText", menuName = "ScriptableObjects/CreateDialogueText")]
public class DialogueText : ScriptableObject
{
    //会話イベントの番号
    //public int number;
    public string textinfo;

    //会話テキストの情報
    public TextInfomation[] textInfomations;
}
