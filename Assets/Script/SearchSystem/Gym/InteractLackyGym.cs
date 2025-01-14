using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLackyGym : MonoBehaviour, IReceiveSearch
{
    //‘ÌˆçŠÙ‚ÌK‰^

    [SerializeField] private DialogueText NormalText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //ê‡‚É‚æ‚Á‚Ä‚Íƒtƒ‰ƒO‚É‚æ‚Á‚ÄğŒ•ªŠò

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
