using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPhantomThiefEntranceHall : MonoBehaviour, IReceiveSearch
{
    //���փz�[����

    [SerializeField] private DialogueText NormalText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������

        gameManager.OpenTextWindow(NormalText);
    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        //�T���J�n�O��E�o���\������
        if (!gameManager.eventFlagData.AdventureStart)
        {
            gameObject.SetActive(true);
        }
        if (gameManager.eventFlagData.itemDataBase.truthBullets[8].getFlag)
        {
            gameObject.SetActive(true);
        }
    }
}
