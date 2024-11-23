using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TruthBulletShot : MonoBehaviour
{
    //�Ə��𓮂���
    //�{�^���������Ɣ���
    //�e���n�_����I�_�Ɉړ�����܂ŏƏ��������Ĉړ��s��
    //

    //�Ə�
    [SerializeField] private Transform aimImagePos;

    [SerializeField] private Transform shotPointCurrent;

    // ���`��Ԃ̎n�_
    //private Transform shotPointFrom;

    // ���`��Ԃ̏I�_
    private Vector3 shotPointTo = new Vector3();

    // �ړ�����[s]
    [SerializeField] private float _duration = 1;


    private bool MoveEnd = false;

    void Start()
    {
        
    }

    void Update()
    {
        //�Ə��𓮂���
        //�}�E�X�̈ʒu�ƏƏ���̈ʒu�𓯊�������B
        aimImagePos.position = Input.mousePosition;

        RaycastHit hit;

        //MainCamera����}�E�X�̈ʒu��Ray���΂�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            //Debug.Log("���C����������");

            if (hit.transform.CompareTag("SearchObj"))
            {
                Debug.Log("�^�O");
            }
            //�{�^���������Ɣ���
            if (Input.GetKeyUp(KeyCode.Space))
            {
                shotPointTo = hit.point;
                Debug.Log("���C����������:"+ shotPointTo);
                shotPointCurrent.position = new Vector3(aimImagePos.position.x, aimImagePos.position.x, -1200.0f);
                StartCoroutine(BulletMove());
            }
        }


    }

    private IEnumerator BulletMove()
    {
        //�e���n�_����I�_�Ɉړ�����܂ŏƏ��������Ĉړ��s��
        
        

        // �n�_�E�I�_�̈ʒu�擾
        var a = shotPointCurrent.position;
        var b = shotPointTo;

        // ��Ԉʒu�v�Z
        var t = Mathf.PingPong(Time.time / _duration, 1);

        // ��Ԉʒu�𔽉f
        shotPointCurrent.position = Vector3.Lerp(a, b, t);

        if(shotPointCurrent.position == shotPointTo)
        {
            BulletMoveEnd();
        }
        
        yield return new WaitUntil(() => MoveEnd == false);

        Debug.Log("�o���b�g");
    }
    public void BulletMoveEnd()
    {
        MoveEnd = !MoveEnd;
    }
}
