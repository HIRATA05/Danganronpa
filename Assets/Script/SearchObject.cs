using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchObject : MonoBehaviour , IReceiveSearch
{
    //���ׂ����̃e�L�X�g�����e�L�X�g�Ǘ��X�N���v�g�ɓn��

    public void ReceiveSearch(float damage)
    {
        Debug.Log("Enemy �� " + damage + "�_���[�W�H�����");
        //�n��

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
