using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArcherGarden : MonoBehaviour, IReceiveSearch
{
    //2階開放後弓道家

    [SerializeField] private DialogueText NormalText; //2階開放後会話

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //2階への移動前

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
