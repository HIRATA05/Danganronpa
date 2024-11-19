using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruthBulletShot : MonoBehaviour
{
    //�Ə��𓮂���
    //�{�^���������Ɣ���
    //�e���n�_����I�_�Ɉړ�����܂ŏƏ��������Ĉړ��s��
    //

    //�Ə�
    [SerializeField] private Transform aimImagePos;

    // ���`��Ԃ̎n�_
    [SerializeField] private Transform shotPointFrom;

    // ���`��Ԃ̏I�_
    [SerializeField] private Transform shotPointTo;

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

        }

        

        //�{�^���������Ɣ���
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(BulletMove());
        }

    }

    private IEnumerator BulletMove()
    {
        //�e���n�_����I�_�Ɉړ�����܂ŏƏ��������Ĉړ��s��
        
        

        // �n�_�E�I�_�̈ʒu�擾
        var a = shotPointFrom.position;
        var b = shotPointTo.position;

        // ��Ԉʒu�v�Z
        var t = Mathf.PingPong(Time.time / _duration, 1);

        // ��Ԉʒu�𔽉f
        shotPointFrom.position = Vector3.Lerp(a, b, t);

        yield return new WaitUntil(() => MoveEnd == true);

        Debug.Log("�o���b�g");
    }
    public void BulletMoveEnd()
    {
        MoveEnd = !MoveEnd;
    }
}
