using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCol : MonoBehaviour
{

    TruthBulletShot truthBulletShot;


    void Start()
    {
        
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        //�����������̂ɕ����R���|�[�l���g�������
        if (other.GetComponent<TextMeshProUGUI>())
        {
            Debug.Log("�����ɓ�������");


            truthBulletShot.BulletCol();

        }

    }

    //�����蔻��X�N���v�g�ɃR�g�_�}�Ǘ��X�N���v�g��n��
    public void SetBulletScript(TruthBulletShot truthBullet)
    {
        truthBulletShot = truthBullet;
    }
}
