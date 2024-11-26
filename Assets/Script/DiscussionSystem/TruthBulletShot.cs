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
    private Transform shotPointFrom;

    // ���`��Ԃ̏I�_
    private Vector3 shotPointTo = new Vector3();

    // �ړ�����[s]
    //[SerializeField] private float _duration = 10;

    bool reloading = false;

    private bool MoveEnd = false;



    void Start()
    {
        
        shotPointFrom = shotPointCurrent;
        shotPointCurrent.GetComponent<BulletCol>().SetBulletScript(this.gameObject.GetComponent<TruthBulletShot>());
        Debug.Log(shotPointFrom.position);
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
            //�{�^���������Ɣ��ˁA���ˌ㐔�b�Ԕ��˕s��
            if (Input.GetKeyUp(KeyCode.Space) && !reloading)
            {
                shotPointTo = hit.point;
                Debug.Log("���C����������:"+ shotPointTo);
                shotPointCurrent.position = new Vector3(Camera.main.transform.position.x + 1, Camera.main.transform.position.y, Camera.main.transform.position.z);
                StartCoroutine(BulletMove());
            }
        }
        
    }

    private IEnumerator BulletMove()
    {
        //�e���n�_����I�_�Ɉړ�����܂ŏƏ��������Ĉړ��s��

        //�����[�h�J�n
        StartCoroutine(Reload());

        Vector3 targetPosition = shotPointTo; // �ړI�̈ʒu�̍��W���w��
        Vector3 startPosition = shotPointCurrent.position; // �Q�[���I�u�W�F�N�g��Transform�R���|�[�l���g���擾
        float duration = 0.1f; // ���e�܂ł̎��ԁA�P�ʂ͕b
        float time = 0.0f;  // ���˂���̌o�ߎ���

        // �e�̈ړ�����
        while(!MoveEnd && time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            
            // �ړI�̈ʒu�܂�duration�b�������ĕ�Ԃňړ�
            shotPointCurrent.position = Vector3.Lerp(startPosition, targetPosition, t); // �ړI�̈ʒu�Ɉړ�
            yield return null;
        }

        //BulletMoveEnd();
        BulletDelete();


        yield return null;
    }
    //���̈ʒu�ɖ߂���ʓ�����e������
    public void BulletDelete()
    {
        shotPointCurrent = shotPointFrom;
    }

    public void BulletTextTouch()
    {
        MoveEnd = !MoveEnd;
        
        Debug.Log("�����ڐG");
    }
    public void BulletMoveEnd()
    {
        MoveEnd = !MoveEnd;
        
        Debug.Log("�����ɐڐG���Ȃ�����");
    }
    IEnumerator Reload()
    {
        reloading = true; //�����[�h�����true�ɂ���
        yield return new WaitForSeconds(3); //3�b�ҋ@
        Debug.Log("�����[�h�I��");
        reloading = false; //�����[�h�����false�ɂ���
    }

}
