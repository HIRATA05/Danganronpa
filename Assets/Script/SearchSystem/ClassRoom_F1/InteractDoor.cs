using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractDoor : MonoBehaviour, IReceiveSearch
{
    //���ׂ����̃e�L�X�g�����e�L�X�g�Ǘ��X�N���v�g�ɓn��

    [SerializeField] private DialogueText GameStartText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        Debug.Log("�h�A�𒲂ׂ�");
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        if (gameManager.eventFlagData.GameStart_All_TalkStart)
        {
            //�ʂ̕����Ɉړ�
            SceneManager.LoadScene("End");
        }
        else
        {
            gameManager.OpenTextWindow(GameStartText);
        }

    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {

    }
}