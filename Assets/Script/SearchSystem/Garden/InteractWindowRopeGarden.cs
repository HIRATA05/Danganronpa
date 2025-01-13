using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractWindowRopeGarden : MonoBehaviour, IReceiveSearch
{
    //2�K�J���ネ�[�v�t����

    //[SerializeField] private DialogueText F2MoveText; //2�K�ւ̈ړ�
    [SerializeField] private DialogueText MoveAfterText;//2�K�ړ���

    [SerializeField] private string MoveScene;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //2�K�ւ̈ړ��O
        if (!gameManager.eventFlagData.F2Intrusion)
        {
            //�����̈ړ� 2�K�̋���
            gameManager.eventFlagData.currentRoom = MoveScene;
            gameManager.eventFlagData.F2Intrusion = true;
            SceneManager.LoadScene(MoveScene);
        }
        //2�K�ւ̈ړ���
        else
        {
            gameManager.OpenTextWindow(MoveAfterText);
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
