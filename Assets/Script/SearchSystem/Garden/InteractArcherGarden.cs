using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArcherGarden : MonoBehaviour, IReceiveSearch
{
    //2�K�J����|����

    [SerializeField] private DialogueText NormalText; //2�K�J�����b

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //2�K�ւ̈ړ��O

        gameManager.OpenTextWindow(NormalText);
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
