using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCol : MonoBehaviour
{

    TruthBulletShot truthBulletShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        //�����������̂ɕ����R���|�[�l���g�������
        if (other.GetComponent<TextMeshProUGUI>())
        {
            Debug.Log("�����ɓ�������");
            truthBulletShot.BulletMoveEnd();

            //�E�B�[�N�|�C���g���m�F

            //�E�B�[�N�|�C���g�Ȃ�_�j�����ӂ��m�F

        }

        else
        {
            Debug.Log("�O�ꂽ");
            //truthBulletShot.BulletMoveEnd();
        }
        
        
    }

    //�����蔻��X�N���v�g�ɃR�g�_�}�Ǘ��X�N���v�g��n��
    public void SetBulletScript(TruthBulletShot truthBullet)
    {
        truthBulletShot = truthBullet;
    }
}
