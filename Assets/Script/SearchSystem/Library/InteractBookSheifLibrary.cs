using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBookSheifLibrary : MonoBehaviour, IReceiveSearch
{
    //図書室の本棚

    [SerializeField] private DialogueText NormalText; //図書室の本棚
    [SerializeField] private DialogueText AfterText; //図書室の本棚調査後

    private GameManager gameManager;

    bool Search = false;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐

        //
        if (!gameManager.eventFlagData.BookShelf)
        {
            gameManager.eventFlagData.BookShelf = true;
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
