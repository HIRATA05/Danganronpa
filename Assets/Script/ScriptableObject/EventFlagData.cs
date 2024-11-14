using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventFlag", menuName = "ScriptableObjects/CreateEventFlagData")]
public class EventFlagData : ScriptableObject
{
    //�A�C�e��
    [Header("�A�C�e��")]
    public bool rope;

    //�R�g�_�}
    [Header("�R�g�_�}")]
    public bool evidence;

    //�V�i���I�i�s�̂��߂̃t���O�ݒ�
    [Header("�V�i���I�t���O")]
    public bool GameStart;

}
