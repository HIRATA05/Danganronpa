using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractChara : MonoBehaviour, IReceiveSearch
{

    //���ׂ����̃e�L�X�g�����e�L�X�g�Ǘ��X�N���v�g�ɓn��

    [SerializeField] private DialogueText dialogueText;

    private GameManager gameManager;
    private CinemachineVirtualCamera[] virtualCameras;



    public void ReceiveSearch()
    {
        
        Debug.Log("�L�����𒲂ׂ�");
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������

        gameManager.OpenTextWindow(dialogueText, virtualCameras);

    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        virtualCameras = GetComponentsInChildren<CinemachineVirtualCamera>();
    }

    void Update()
    {
        
    }
}
