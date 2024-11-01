using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] public class TextInfomation
{
    //�b�҂̖��O
    public string speakerName;
    //�\������e�L�X�g
    [TextArea(2, 10)]
    public string paragraphs;

    //�J���������̎��
    enum CameraDivision
    {
        //
        //
        //
    }
    //�J�����ŕ\������I�u�W�F�N�g

    //�J���������̌`

}

[CreateAssetMenu(fileName = "DialogueText", menuName = "ScriptableObjects/CreateDialogueText")]
public class DialogueText : ScriptableObject
{
    //��b�C�x���g�̔ԍ�
    public int number;

    //��b�e�L�X�g�̏��
    public TextInfomation[] textInfomations;
}
