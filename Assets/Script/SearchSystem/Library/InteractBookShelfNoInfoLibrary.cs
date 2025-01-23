using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBookShelfNoInfoLibrary : MonoBehaviour, IReceiveSearch
{
    //図書室の本棚

    [SerializeField] private DialogueText NormalText; //図書室の本棚


    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐

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
