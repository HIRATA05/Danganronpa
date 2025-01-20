using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLibraryBook : MonoBehaviour, IReceiveSearch
{
    //�}�����̖{�I

    [SerializeField] private DialogueText NormalText; //�}�����̖{�I
    [SerializeField] private DialogueText AfterText; //�}�����̖{�I������

    private GameManager gameManager;

    bool Search = false;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������

        //
        if (!gameManager.eventFlagData.LibraryBook)
        {
            gameManager.eventFlagData.LibraryBook = true;
            gameManager.OpenTextWindow(NormalText);
        }
        else
        {
            gameManager.OpenTextWindow(AfterText);
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
