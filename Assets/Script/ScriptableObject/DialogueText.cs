using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] public class TextInfomation
{
    //カメラ分割の種類
    enum CameraDivision
    {

    }


    //話者の名前
    public string speakerName;
    //表示するテキスト
    [TextArea(2, 10)]
    public string paragraphs;
    //カメラで表示する

    //カメラ分割の形

}

[CreateAssetMenu(fileName = "DialogueText", menuName = "ScriptableObjects/CreateDialogueText")]
public class DialogueText : ScriptableObject
{
    

    public TextInfomation[] textInfomations;

}
