using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetBulletMenu : MonoBehaviour
{
    //�Ə��T����ԂƋc�_���ɓ��肵���R�g�_�}���m�F�ł���
    //�}�E�X�J�[�\�������m���ē������Ă�����\����ς���
    //����̃{�^���ŉ�ʂ�߂�
    //�E���̃{�^���Ń^�C�g���ɖ߂�

    //���j���[�p�l��
    [SerializeField] private GameObject Menu;

    //���O�̏��
    private GameManager.PlayerController beforController;

    //�J�[�\�������킹��ƕ\���������e�L�X�g
    [SerializeField] private TextMeshProUGUI InfoText;

    //�R�g�_�}
    [SerializeField] private GameObject[] Bullet;
    /*
    [SerializeField] private GameObject MonokumaInfo;
    [SerializeField] private GameObject Rope;
    [SerializeField] private GameObject ArcherySet;
    [SerializeField] private GameObject Battery;
    [SerializeField] private GameObject Wrench;
    [SerializeField] private GameObject Claw;
    [SerializeField] private GameObject NotePc;
    [SerializeField] private GameObject Korosiai;
    [SerializeField] private GameObject DataProcessingRoomKey;
    */
    //�L����
    [SerializeField] private GameObject Detective;
    [SerializeField] private GameObject Lacky;
    [SerializeField] private GameObject Archer;
    [SerializeField] private GameObject Hacker;
    [SerializeField] private GameObject Thief;

    private GameManager gameManager;
    private EventFlagData eventFlagData;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
    }

    void Update()
    {
        //�T�������c�_���ɊJ�����Ƃ��o����
        if(gameManager.playerController == GameManager.PlayerController.ReticleMode || gameManager.playerController == GameManager.PlayerController.DiscussionMode)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //�Q�[���̎��Ԃ��~�߂�
                Time.timeScale = 0.0f;

                //���O�̏�Ԃ�ۑ�
                beforController = gameManager.playerController;
                //��Ԃ�ω�
                gameManager.playerController = GameManager.PlayerController.EventScene;

                //���j���[���J��
                Menu.SetActive(true);

                //��񃁃����t���O�ɂ���ĕ\������
                for(int i = 0; i < eventFlagData.itemDataBase.truthBullets.Count; i++)
                {
                    if (eventFlagData.itemDataBase.truthBullets[i].getFlag)
                    {
                        if(!Bullet[i].activeSelf) Bullet[i].SetActive(true);
                    }
                }

                if (eventFlagData.GameStart_All)//�T��ƍK�^
                {
                    if (!Detective.activeSelf) Detective.SetActive(true);
                    if (!Lacky.activeSelf) Lacky.SetActive(true);
                }
                if (eventFlagData.SelfIntoro_Archer)//�|����
                {
                    if (!Archer.activeSelf) Archer.SetActive(true);
                }
                if (eventFlagData.SelfIntoro_Hacker)//�n�b�J�[
                {
                    if (!Hacker.activeSelf) Hacker.SetActive(true);
                }
                if (eventFlagData.SelfIntoro_PhantomThief)//����
                {
                    if (!Thief.activeSelf) Thief.SetActive(true);
                }

            }
        }
    }

    //���j���[�����
    public void CloseMenu()
    {
        //��Ԃ�߂�
        gameManager.playerController = beforController;

        //���j���[���J��
        Menu.SetActive(false);

        //�Q�[���̎��Ԃ�߂�
        Time.timeScale = 1.0f;
    }

    //�^�C�g���֖߂�
    public void ReturnTitle()
    {
        Debug.Log("�I��");
        Application.Quit();//�Q�[���v���C�I��
    }
}
