using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractClassRoom_F2 : MonoBehaviour, IReceiveSearch
{
    //2�K�����̃C�x���g�����T���|�C���g

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
