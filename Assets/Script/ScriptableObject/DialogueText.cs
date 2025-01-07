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
    //表示するテキストの色タイプ
    public enum TextColorType
    {
        Normal,//通常の白
        Blue//思考の青
    }
    public TextColorType colorType = TextColorType.Normal;
    //テキストウィンドウのタイプ
    public enum TextWindowType
    {
        Normal,
        Dark,
        Normal_NonUI,
        Dark_NonUI
    }
    public TextWindowType windowType = TextWindowType.Normal;
}

[CreateAssetMenu(fileName = "DialogueText", menuName = "ScriptableObjects/CreateDialogueText")]
public class DialogueText : ScriptableObject
{
    //会話イベントのキー
    public string textinfo;

    //会話テキストの情報
    public TextInfomation[] textInfomations;
}
