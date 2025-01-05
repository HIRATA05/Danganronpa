using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TruthBulletShot : MonoBehaviour
{
    //�Ə��𓮂���
    //�{�^���������Ɣ���
    //�e���n�_����I�_�Ɉړ�����܂ŏƏ��������Ĉړ��s��


    //�m���X�g�b�v�c�_�̓�����Ǘ�
    [SerializeField] private DiscussionManager discussionManager;

    //�c�_����UI
    [SerializeField] private DiscussionUI discussionUI;

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;

    //�Ə�
    [SerializeField] private GameObject aim;
    //private Image aimImage;
    private Transform aimImagePos;
    /*
    //�Ə��̉摜
    [SerializeField] private Sprite aimImageNormal;//�ʏ�
    [SerializeField] private Sprite aimImageColText;//�����Əd�Ȃ�
    */
    //�R�g�_�}�̕���
    [SerializeField] private TextMeshProUGUI bulletText;

    //�R�g�_�}�̈ʒu
    [SerializeField] private Vector3 bulletPoint;

    //�R�g�_�}�̌��݈ʒu
    [SerializeField] private Transform shotPointCurrent;

    // ���`��Ԃ̎n�_
    [SerializeField] private Transform shotPointFrom;

    // ���`��Ԃ̏I�_
    private Vector3 shotPointTo = new Vector3();

    //�����[�h���
    private bool reloading = false;
    private float reloadingTime = 2.0f;

    //�e�ۂ��ړ�����
    private bool MoveEnd = false;
    //�����ɓ���������
    private bool isBulletTextCol = false;

    void Start()
    {
        //aimImage = aim.GetComponent<Image>();
        aimImagePos = aim.transform;

        //shotPointFrom = shotPointCurrent;
        shotPointCurrent.GetComponent<BulletCol>().SetBulletScript(this.gameObject.GetComponent<TruthBulletShot>());


    }

    void Update()
    {
        //�c�_��������
        if (discussionManager.discussionProgress)
        {
            //�Ə��𓮂���
            //�}�E�X�̈ʒu�ƏƏ��̈ʒu�𓯊�������
            aimImagePos.position = Input.mousePosition;

            RaycastHit hit;

            //MainCamera����}�E�X�̈ʒu��Ray���΂�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                /*
                //�����������������̎��Ə��̉摜��ω�
                //�^�O
                Debug.Log("hit name:" + hit.collider.name);
                if (hit.collider.CompareTag("Speech"))
                {
                    aimImage.sprite = aimImageColText;
                }
                else
                {
                    aimImage.sprite = aimImageNormal;
                }
                */

                //Debug.Log("���C����������");
                //�{�^���������Ɣ��ˁA���ˌ㐔�b�Ԕ��˕s��
                if (Input.GetKeyUp(KeyCode.Space)/*gameManager.KeyInputSpace()*/ && !reloading)
                {
                    shotPointTo = hit.point;
                    Debug.Log("���C����������:" + shotPointTo);
                    shotPointCurrent.GetComponent<RectTransform>().localPosition = bulletPoint;
                    StartCoroutine(BulletMove());
                    
                }
            }

            
        }
        
    }

    private IEnumerator BulletMove()
    {
        //�e���n�_����I�_�Ɉړ�����܂ŏƏ��������Ĉړ��s��

        //�Ə�������
        aim.GetComponent<Image>().enabled = false;

        //���݂̃R�g�_�}�̖��O���擾
        bulletText.text = discussionUI.Bullet[discussionUI.BulletCount].truthBullets.bulletName;

        //�����[�h�J�n
        StartCoroutine(Reload());

        Vector3 targetPosition = shotPointTo; //�ړI�̈ʒu�̍��W���w��
        Vector3 startPosition = shotPointCurrent.position; //�Q�[���I�u�W�F�N�g��Transform�R���|�[�l���g���擾
        float duration = 0.6f; //���e�܂ł̎��ԁA�P�ʂ͕b
        float time = 0.0f;  //���˂���̌o�ߎ���

        //���e�n�_�ɂ���Ċp�x��ω�


        //�e�̈ړ�����
        while(/*!MoveEnd && */time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            
            //�ړI�̈ʒu�܂�duration�b�������ĕ�Ԃňړ�
            shotPointCurrent.position = Vector3.Lerp(startPosition, targetPosition, t); //�ړI�̈ʒu�Ɉړ�
            yield return null;
        }

        //�e�̈ʒu��ω������ĉ�ʊO�Ɉړ�
        BulletDelete(startPosition);

        //�E�B�[�N�|�C���g���m�F
        if (discussionManager.TextColWeek())
        {
            //���݂̃R�g�_�}�ŃE�B�[�N�|�C���g��������������
            if (discussionManager.TruthBulletCompare())
            {
                //�m���X�g�b�v�c�_�I���������Ăяo��
                discussionManager.ShootingFinish();
            }
            else
            {
                //���s���o�̌�ɔ������Ƃ̎��s�e�L�X�g����
                discussionManager.DiscussionFailureChange(targetPosition);
                Debug.Log("�E�B�[�N�|�C���g�ԈႦ�����̉�b");
                
                NotBreakBullet();
            }
            
        }
        else
        {
            //�E�B�[�N�|�C���g�o�Ȃ��ꍇ
            NotBreakBullet();
        }
        /*
        //�e�������ɓ������Ă����ꍇ
        if (isBulletTextCol)
        {
            BulletTextTouch();
        }
        */
        
        yield return null;

        //�Ə���\��
        aim.GetComponent<Image>().enabled = true;
    }

    //�R�g�_�}���ˌ�̃����[�h
    IEnumerator Reload()
    {
        //���ˎ��̃V�����_�[�̓���
        discussionUI.CylinderTextErase();
        //�����[�h�����true�ɂ���
        reloading = true;
        yield return new WaitForSeconds(reloadingTime);
        Debug.Log("�����[�h�I��");
        //�V�����_�[�̕�����߂�
        discussionUI.CylinderTextReturn();
        //�����[�h�����false�ɂ���
        reloading = false;
    }

    //�����ʒu�ɖ߂���ʓ�����e������
    public void BulletDelete(Vector3 startPos)
    {
        Debug.Log("�e�ۏ���");
        //���݂̃R�g�_�}�̖��O���\��
        bulletText.text = "";
        //�����ʒu�ɖ߂�
        shotPointCurrent.position = startPos;
    }
    /*
    public void BulletTextTouch()
    {
        isBulletTextCol = false;
        MoveEnd = !MoveEnd;
        
        Debug.Log("�m���X�g�b�v�c�_�I��");
        //�e�̈ʒu��ω�
        BulletDelete();

        //�E�B�[�N�|�C���g���m�F
        if (discussionManager.TextColWeek())
        {
            //�m���X�g�b�v�c�_�I���������Ăяo��
            discussionManager.ShootingFinish();
        }
        else
        {
            //�E�B�[�N�|�C���g�o�Ȃ��ꍇ
            NotBreakBullet();
        }
        //�E�B�[�N�|�C���g�Ȃ�_�j�����ӂ��m�F


    }
    */
    public void NotBreakBullet()
    {
        MoveEnd = false;
        
        Debug.Log("�E�B�[�N�|�C���g�ɐڐG���Ȃ�����");
    }

    public void BulletCol()
    {
        isBulletTextCol = true;
    }
}
