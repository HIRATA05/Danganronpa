using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIronDoor : MonoBehaviour, IReceiveSearch
{
    //玄関ホールの扉

    [SerializeField] private DialogueText NormalText;

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
