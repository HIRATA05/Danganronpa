using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventFlag", menuName = "ScriptableObjects/CreateEventFlagData")]
public class EventFlagData : ScriptableObject
{
    //���݂̕���
    [Header("���݂̕���")]
    public string currentRoom;

    //�A�C�e��
    [Header("�A�C�e��")]
    public ItemDataBase itemDataBase;

    //�V�i���I�i�s�̂��߂̃t���O�ݒ�
    [Header("�V�i���I�t���O")]
    public bool GameStart;

    //True�̎��ɃC�x���g������
    [Header("�����ɓ��������ɔ�������V�i���I�t���O")]
    public bool GameStart_ClassRoom;
    public void GameStart_ClassRoom_True() { GameStart_ClassRoom = true; }
    public void GameStart_ClassRoom_False() { GameStart_ClassRoom = false; }

    public bool RoomIn;
    public void RoomIn_True() { RoomIn = true; }
    public void RoomIn_False() { RoomIn = false; }

}
