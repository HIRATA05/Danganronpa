using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDuraluminCase : MonoBehaviour, IReceiveSearch
{
    //�}�����̃P�[�X

    [SerializeField] private DialogueText NormalText; //�}�����̃P�[�X�����O
    [SerializeField] private DialogueText AfterText; //�}�����̃P�[�X������

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������

        if (!gameManager.eventFlagData.DuraluminCaseInteract && !gameManager.eventFlagData.DuraluminCaseLogic)
        {
            gameManager.eventFlagData.DuraluminCaseInteract = true;
            gameManager.OpenTextWindow(NormalText);
        }
        else if(gameManager.eventFlagData.DuraluminCaseInteract && !gameManager.eventFlagData.DuraluminCaseLogic)
        {
            Debug.Log("�������͏����𔭐�������");
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
