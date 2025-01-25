using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMachineWorkRoom : MonoBehaviour, IReceiveSearch
{
    //�@�B�H�쎺�̃C�x���g�����I�u�W�F�N�g

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
