using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfLibrary : MonoBehaviour, IReceiveSearch
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
        if (gameManager.eventFlagData.F2Request && !Search)
        {
            Search = true;
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
