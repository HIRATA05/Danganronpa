using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLibraryBook : MonoBehaviour, IReceiveSearch
{
    //図書室の本棚

    [SerializeField] private DialogueText NormalText; //図書室の本棚
    [SerializeField] private DialogueText AfterText; //図書室の本棚調査後

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐

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
