using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBookShelfNoInfoLibrary : MonoBehaviour, IReceiveSearch
{
    //�}�����̖{�I

    [SerializeField] private DialogueText NormalText; //�}�����̖{�I


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

    }
}
