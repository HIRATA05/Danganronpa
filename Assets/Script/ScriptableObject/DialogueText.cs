using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] public class TextInfomation
{
    //�J���������̎��
    enum CameraDivision
    {

    }


    //�b�҂̖��O
    public string speakerName;
    //�\������e�L�X�g
    [TextArea(2, 10)]
    public string paragraphs;
    //�J�����ŕ\������

    //�J���������̌`

}

[CreateAssetMenu(fileName = "DialogueText", menuName = "ScriptableObjects/CreateDialogueText")]
public class DialogueText : ScriptableObject
{
    

    public TextInfomation[] textInfomations;

}
