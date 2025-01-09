using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDiningWindow : MonoBehaviour, IReceiveSearch
{
    //���ׂ����̃e�L�X�g�����e�L�X�g�Ǘ��X�N���v�g�ɓn��

    [SerializeField] private DialogueText NormalText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        Debug.Log("���𒲂ׂ�");
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
