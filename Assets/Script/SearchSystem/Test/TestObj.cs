using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObj : MonoBehaviour, IReceiveSearch
{

    //���ׂ����̃e�L�X�g�����e�L�X�g�Ǘ��X�N���v�g�ɓn��

    [SerializeField] private DialogueText dialogueText;

    GameManager gameManager;
    TextWindow textWindow;


    public void ReceiveSearch()
    {
        Debug.Log("���𒲂ׂ�");
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������

        //�n��
        textWindow.dialogueText = dialogueText;

        gameManager.playerController = GameManager.PlayerController.TextWindowMode;
    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        GameObject tw = GameObject.Find("TextWindow");
        textWindow = tw.GetComponent<TextWindow>();

    }

    void Update()
    {
        
    }
}
