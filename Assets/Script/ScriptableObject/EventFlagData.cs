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
    public bool EscepeSwitch;

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
    public bool SelfIntoro_Call;//�S�Ċ����ヂ�m�N�}�̌Ăяo��

    //���m�N�}�o��
    public bool AppMonokuma;
    //���m�N�}�ޏ�
    public bool AppMonokumaAfter;
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
    //2�K�J���̂��߂̉����̒��
    public bool F2Request;
    //������������2�K�J���C�x���g�����\
    public bool F2Open;
    //���[�v�t����
    public bool RopeWindow;
    //2�K�N����
    public bool F2Intrusion;
    //2�K�J���C�x���g�̊ԉ����\��
    public bool F2OpenPhantomThief;
    //2�K�J���C�x���g�̊ԋ|���ƕ\��
    public bool F2OpenArcher;

    //�q��
    [Header("�q��")]
    public bool IronBars;//�S�i�q�T��
    public bool WarehouseArcher;//�q�ɂɂ���|����
    public bool IronBarsOpen;//�S�i�q����

    //2�K����
    [Header("2�K����")]
    public bool ClassRoomF2StartPhantomThief;//2�K�����C�x���g�ŏ��̉���
    public bool ClassRoomF2_Vent;//2�K�����̒ʋC���T��
    public bool ClassRoomF2_Window;//2�K�����̑��T��
    public bool ClassRoomF2_All;//2�K�����̑S�T��
    public bool ClassRoomF2_All_TalkStart;//2�K�����̑S�T����̉�b����

    //�}����
    [Header("�}����")]
    public bool BookShelf;//�{�I
    public bool LibraryBook;//�{
    public bool DuraluminCaseInteract;//�P�[�X
    public bool DuraluminCaseLogic;//�P�[�X������O
    public bool MemoMachineWork;//�{�I�T����

    //�@�B�H�쎺
    [Header("�@�B�H�쎺")]
    public bool PressMachineLock;//�v���X�@���b�N�m�F�@�n�b�J�[�֌�����
    public bool HackerPressMachineRequest;//�n�b�J�[�֋��͗v�������@�n�b�J�[�o��
    public bool PreesMoveDown;//�v���X�@�̋N��
    public bool MonokumaGreenPreesMove;//���m�N�}�O���[�����v���X�@�܂ňړ�
    public bool PressMachineShelfUnlock;//�v���X�Ń��m�N�}�j��@�B�H�쎺�I����

    

    //�E�o�C�x���g
    public bool EscepeEvent;

    //True�̎��ɃC�x���g������
    [Header("�����ɓ��������ɔ�������V�i���I�t���O")]
    public bool GameStart_ClassRoom;
    public void GameStart_ClassRoom_True() { GameStart_ClassRoom = true; }
    public void GameStart_ClassRoom_False() { GameStart_ClassRoom = false; }

    //�H���ɓ�������
    public bool RoomIn_Dining;
    //����ɓ�������
    public bool RoomIn_Garden;
    //���փz�[���ɓ�������
    public bool RoomIn_EntranceHall;
    //�̈�قɓ�������
    public bool RoomIn_Gym;
    //�q�ɂɓ�������
    public bool RoomIn_Warehouse;
    //�|����ɓ�������
    public bool RoomIn_KyudoHall;
    //2�K����
    public bool RoomIn_ClassRoom_F2;
    //�}����
    public bool RoomIn_Library;
    //�@�B�H�쎺
    public bool RoomIn_MachineWorkRoom;
    //�n�b�J�[�̌�
    public bool RoomIn_HackerRoom;

    public bool RoomIn;
    public void RoomIn_True() { RoomIn = true; }
    public void RoomIn_False() { RoomIn = false; }

    //���U���g�̃X�R�A
    [Header("�X�R�A")] public int Score = 0;

}
