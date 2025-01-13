using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLackyGarden : MonoBehaviour, IReceiveSearch
{
    //����̍K�^

    [SerializeField] private DialogueText NormalText; //2�K�J���C�x���g�ȊO
    [SerializeField] private DialogueText F2OpenEventText; //2�K�J���C�x���g��
    [SerializeField] private DialogueText F2OpenAfterText; //2�K�J�����b

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //2�K�ւ̈ړ��O
        if (gameManager.eventFlagData.RopeWindow && !gameManager.eventFlagData.F2Intrusion)
        {
            gameManager.OpenTextWindow(F2OpenAfterText);
        }
        //2�K�J���O�Ń��[�v�������O
        else if (gameManager.eventFlagData.F2Request && !gameManager.eventFlagData.F2Open && !gameManager.eventFlagData.RopeWindow)
        {
            gameManager.OpenTextWindow(F2OpenEventText);
        }
        else
        {
            gameManager.OpenTextWindow(NormalText);
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
