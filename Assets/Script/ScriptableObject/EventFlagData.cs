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

    //�Q�[���J�n�������̒T��
    public bool GameStart_Window;
    public bool GameStart_Monitor;
    public bool GameStart_Lacky;
    public bool GameStart_All;//�S�Ċ���
    public bool GameStart_All_TalkStart;

    //���ȏЉ�
    public bool SelfIntoro_Archer;
    public bool SelfIntoro_Hacker;
    public bool SelfIntoro_PhantomThief;
    public bool SelfIntoro_All;//�S�Ċ���

    //���m�N�}�����ŒT���J�n��
    public bool AdventureStart;

    //�H��
    //�H���ł̋c�_����
    public bool DiningDiscStart;
    //�q�ɊJ���̂��߂̋|���Ƃւ̗v��
    public bool WarehouseRequest;
    //�f�W�^�����v
    public bool Digitalclock;

    //����
    //public bool Battery
    //���[�v�t����
    public bool RopeWindow;

    //�q��
    public bool IronBars;//�S�i�q�T��

    //True�̎��ɃC�x���g������
    [Header("�����ɓ��������ɔ�������V�i���I�t���O")]
    public bool GameStart_ClassRoom;
    public void GameStart_ClassRoom_True() { GameStart_ClassRoom = true; }
    public void GameStart_ClassRoom_False() { GameStart_ClassRoom = false; }

    //�H���ɓ�������
    public bool RoomIn_Dining;

    public bool RoomIn;
    public void RoomIn_True() { RoomIn = true; }
    public void RoomIn_False() { RoomIn = false; }

    //�L�����̔z�u�t���O
    

}
