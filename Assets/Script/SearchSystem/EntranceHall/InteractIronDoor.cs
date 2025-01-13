using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIronDoor : MonoBehaviour, IReceiveSearch
{
    //���փz�[���̔�

    [SerializeField] private DialogueText NormalText;

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
