using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLackyClassRoom : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText SelfIntroAfterText;
    [SerializeField] private DialogueText GameStartText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        Debug.Log("�K�^��b");
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        if (gameManager.eventFlagData.GameStart_All)
        {
            gameManager.OpenTextWindow(GameStartText);
        }
        else
        {
            gameManager.OpenTextWindow(SelfIntroAfterText);
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
