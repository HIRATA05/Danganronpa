using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCharaGym : MonoBehaviour, IReceiveSearch
{
    //�̈�ق̃L����

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
        //�T���J�n�O�͕\������
        if (gameManager.eventFlagData.AdventureStart)
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}
