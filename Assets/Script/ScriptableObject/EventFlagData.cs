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
    [Header("�v�����[�O")]
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
    [Header("�H��")]
    //�H���ł̋c�_����
    public bool DiningDiscStart;
    //�q�ɊJ���̂��߂̋|���Ƃւ̗v��
    public bool WarehouseRequest;
    //�f�W�^�����v
    public bool Digitalclock;

    //����
    [Header("����")]
    //���C�g����o�b�e���[����
    public bool BatteryInLight;
    //

    //2�K�J���̂��߂̋|���Ƃւ̗v��
    public bool F2Request;
    //������������2�K�J���C�x���g�����\
    public bool F2Open;
    //���[�v�t����
    public bool RopeWindow;
    //2�K�N����
    public bool F2Intrusion;
    //2�K�J���C�x���g�̊ԋ|���ƕ\��
    public bool F2OpenArcher;

    //�q��
    [Header("�q��")]
    public bool IronBars;//�S�i�q�T��
    public bool WarehouseArcher;//�q�ɂɂ���|����
    public bool IronBarsOpen;//�S�i�q����

    //True�̎��ɃC�x���g������
    [Header("�����ɓ��������ɔ�������V�i���I�t���O")]
    public bool GameStart_ClassRoom;
    public void GameStart_ClassRoom_True() { GameStart_ClassRoom = true; }
    public void GameStart_ClassRoom_False() { GameStart_ClassRoom = false; }

    //�H���ɓ�������
    public bool RoomIn_Dining;
    //����ɓ�������
    public bool RoomIn_Garden;
    //�q�ɂɓ�������
    public bool RoomIn_Warehouse;

    public bool RoomIn;
    public void RoomIn_True() { RoomIn = true; }
    public void RoomIn_False() { RoomIn = false; }

    //�L�����̔z�u�t���O
    

}
