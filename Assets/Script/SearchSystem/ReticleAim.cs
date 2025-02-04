using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using TMPro;

public class ReticleAim : MonoBehaviour
{
    //�G�C���̑���

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;

    //�Ə�
    public Image aimImage;

    //�Ə��̉摜
    [SerializeField] private Sprite OnCollisionAimImage_Obj;
    [SerializeField] private Sprite OnCollisionAimImage_Chara;
    [SerializeField] private Sprite DisCollisionAimImage;
    //�Ə��̑傫��
    [SerializeField] private float OnCollisionAimSize;
    [SerializeField] private float DisCollisionAimSize;

    [SerializeField] private Image SearchObjInformationWindow;
    [SerializeField] private TextMeshProUGUI SearchObjInformationText;

    //��]���x
    private float RotSpeed = 0.1f;



    void Start()
    {
        
    }

    void Update()
    {
        if(gameManager.playerController == PlayerController.ReticleMode
       || gameManager.playerController == PlayerController.MysteryMode)
        {
            ReticleMove();
        }

    }

    private void ReticleMove()
    {
        //�}�E�X�̈ʒu�ƏƏ���̈ʒu�𓯊�������B
        transform.position = Input.mousePosition;


        //�Ə��������̎����ɖ߂�
        if (this.gameObject.GetComponent<Image>().color == Color.clear)
            this.gameObject.GetComponent<Image>().color = Color.white;

        RaycastHit hit;

        //MainCamera����}�E�X�̈ʒu��Ray���΂�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            //�T���\�I�u�W�F�N�g�ɓ���������
            if (hit.transform.CompareTag("SearchObj") || hit.transform.CompareTag("SearchChara"))
            {
                //���������I�u�W�F�N�g�ɂ���ďƏ��̌`��ς���
                if (hit.transform.CompareTag("SearchObj"))
                {
                    //�Ə���ω�������
                    aimImage.sprite = OnCollisionAimImage_Obj;
                    //�Ə��T�C�Y�ύX
                    transform.localScale = new Vector3(OnCollisionAimSize, OnCollisionAimSize, OnCollisionAimSize);
                }
                else if (hit.transform.CompareTag("SearchChara"))
                {
                    //��]���I��
                    //transform���擾
                    Transform myTransform = this.transform;
                    //��]�l���[��
                    Vector3 worldAngle = new (0.0f, 0.0f, 0.0f);
                    //��]�p�x��ݒ�
                    myTransform.eulerAngles = worldAngle;

                    //�Ə���ω�������
                    aimImage.sprite = OnCollisionAimImage_Chara;
                    //�Ə��T�C�Y�ύX
                    transform.localScale = new Vector3(OnCollisionAimSize, OnCollisionAimSize, OnCollisionAimSize);
                }

                //�Ə�����������
                if (aimImage.color != Color.white)
                    aimImage.color = Color.white;
                //�T���\�I�u�W�F�N�g���̃E�B���h�E����������
                if (SearchObjInformationWindow.color != Color.white)
                    SearchObjInformationWindow.color = Color.white;
                //�I�u�W�F�N�g�̖��O�ɂȂ��Ă��Ȃ��Ȃ�\������e�L�X�g��ς���
                if(SearchObjInformationText.text != hit.transform.GetComponent<ObjectName>().ObjName)
                    SearchObjInformationText.text = hit.transform.GetComponent<ObjectName>().ObjName;
                if (SearchObjInformationText.color != Color.black)
                    SearchObjInformationText.color = Color.black;

                //���̏�ԂŒ��ׂ�ƃe�L�X�g�\��
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    //�C���^�t�F�[�X���m�F����
                    IReceiveSearch SearchObj = hit.transform.GetComponent<IReceiveSearch>();

                    if (SearchObj != null)
                    {
                        SearchObj.ReceiveSearch();

                        
                    }

                }

            }
            else
            {
                //�I�u�W�F�N�g�ɓ������Ă��Ȃ���
                DisColliderAim();
            }
        }
        else
        {
            //�I�u�W�F�N�g�ɓ������Ă��Ȃ���
            DisColliderAim();
        }
    }

    //�T���\�I�u�W�F�N�g�ɓ������Ă��Ȃ���
    private void DisColliderAim()
    {
        //�Ə��̉�]
        //transform���擾
        Transform myTransform = this.transform;

        //���[���h���W����ɉ�]���擾
        Vector3 worldAngle = myTransform.eulerAngles;
        worldAngle.x = 0.0f;
        worldAngle.y = 0.0f;
        worldAngle.z += RotSpeed;//��]�����Z
        myTransform.eulerAngles = worldAngle;//��]�p�x��ݒ�

        //�Ə���ω�������
        aimImage.sprite = DisCollisionAimImage;
        //�Ə��T�C�Y�ύX
        transform.localScale = new Vector3(DisCollisionAimSize, DisCollisionAimSize, DisCollisionAimSize);

        //�T���\�I�u�W�F�N�g���̃E�B���h�E�𓧖��ɂ���
        ColorChangeClaer();

    }

    //�F�𓧖��ɕς���
    public void ColorChangeClaer()
    {
        /*
        if (aimImage.color != Color.clear)
            aimImage.color = Color.clear;
        */
        if (SearchObjInformationWindow.color != Color.clear)
            SearchObjInformationWindow.color = Color.clear;
        if (SearchObjInformationText.color != Color.clear)
            SearchObjInformationText.color = Color.clear;
    }
}
