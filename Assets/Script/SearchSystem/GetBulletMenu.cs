using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetBulletMenu : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
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
    [SerializeField,Header("�R�g�_�}")] private GameObject[] Bullet;
    private string MonokumaInfo = "";
    private string Rope = "";
    private string ArcherySet = "";
    private string Battery = "";
    private string Wrench = "";
    private string Claw = "";
    private string NotePc = "";
    private string Korosiai = "";
    private string DataProcessingRoomKey = "";

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
    [SerializeField, Header("�L����")] private GameObject Detective;
    [SerializeField] private GameObject Lacky;
    [SerializeField] private GameObject Archer;
    [SerializeField] private GameObject Hacker;
    [SerializeField] private GameObject Thief;

    //�\����
    private string DetectiveText = "�����Z���̒T��@���_���@�V�m�m���g�I��\r\n�c�����ɗ��e���E�l�S�ɎE���ꂽ���Ƃ����������ŒT��ƂȂ���\r\n���E���̖�������T�オ��������g�D�T��}���قɏ������Ă���\r\n�D���Ȃ���:�g�� �����Ȃ���:�E�l\r\n�g��:173cm �̏d:58kg ����:88cm";//�T��\����
    private string LackyText = "�����Z���̍K�^�@�Ջ_�^��@�R�g�M�}�i\r\n�S���̕��ϓI�Ȋw�����璊�I�őI�΂��K�^�̍˔\�ɑI�΂ꂽ\r\n�ދ��ȓ���������A���ɂł������������i\r\n�D���Ȃ���:���N���� �����Ȃ���:���}\r\n�g��:163cm �̏d:68kg ����:94cm";//�K�^�\����
    private string ArcherText = "�����Z���̋|���Ɓ@���z�@���C�[�C����\r\n�������̈�l���A�c������l�X�ȋ�����{���ꂽ�ːF�����̗ߏ�\r\n���w���ォ��|������3�N�A���ŗD�����Ă���\r\n�D���Ȃ���:�q�������A�j�� �����Ȃ���:����\r\n�g��:174cm �̏d:51kg ����:77cm";//�|���ƕ\����
    private string HackerText = "�����Z���̃n�b�J�[�@�������ߍ��@�~�i�Z�c�~�J\r\n���̋@�������n�b�L���O�ł���قǂ̔\�͂����n�b�J�[\r\n�ƒ���ɖ�肪����r�ꂽ�����𑗂��Ă���\r\n�D���Ȃ���:�h�{�h�����N �����Ȃ���:�A�i���O\r\n�g��:158cm �̏d:43kg ����:70cm";//�n�b�J�[�\����
    private string ThiefText = "�����Z���̎�i�t�@��K�������T�@�j�J�C�h�E�n���m\r\n�C�O�𒆐S�Ɋ������Ă���}�W�V����\r\n�D��ȐU�镑���ƌy�₩�ȓ����Ō���҂����|�����i���I����\r\n�D���Ȃ���:���������� �����Ȃ���:�U��\r\n�g��:178cm �̏d:69kg ����:76cm";//�����\����

    private GameManager gameManager;
    private EventFlagData eventFlagData;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;

        MonokumaInfo = eventFlagData.itemDataBase.truthBullets[0].bulletName + "\n" + eventFlagData.itemDataBase.truthBullets[0].infomation;
        Rope = eventFlagData.itemDataBase.truthBullets[1].bulletName + "\n" + eventFlagData.itemDataBase.truthBullets[1].infomation;
        ArcherySet = eventFlagData.itemDataBase.truthBullets[2].bulletName + "\n" + eventFlagData.itemDataBase.truthBullets[2].infomation;
        Battery = eventFlagData.itemDataBase.truthBullets[3].bulletName + "\n" + eventFlagData.itemDataBase.truthBullets[3].infomation;
        Wrench = eventFlagData.itemDataBase.truthBullets[4].bulletName + "\n" + eventFlagData.itemDataBase.truthBullets[4].infomation;
        Claw = eventFlagData.itemDataBase.truthBullets[5].bulletName + "\n" + eventFlagData.itemDataBase.truthBullets[5].infomation;
        NotePc = eventFlagData.itemDataBase.truthBullets[6].bulletName + "\n" + eventFlagData.itemDataBase.truthBullets[6].infomation;
        Korosiai = eventFlagData.itemDataBase.truthBullets[7].bulletName + "\n" + eventFlagData.itemDataBase.truthBullets[7].infomation;
        DataProcessingRoomKey = eventFlagData.itemDataBase.truthBullets[8].bulletName + "\n" + eventFlagData.itemDataBase.truthBullets[8].infomation;

    }

    void Update()
    {
        //�T�������c�_���ɊJ�����Ƃ��o����
        if(!Menu.activeSelf && gameManager.playerController == GameManager.PlayerController.ReticleMode ||
            (gameManager.playerController == GameManager.PlayerController.DiscussionMode && gameManager.disussionManager.discussionProgress))
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
    
    //�l����\��
    public void DetectiveTextDisplay()
    {
        InfoText.text = DetectiveText;
    }
    public void LackyTextDisplay()
    {
        InfoText.text = LackyText;
    }
    public void ArcherTextDisplay()
    {
        InfoText.text = ArcherText;
    }
    public void HackerTextDisplay()
    {
        InfoText.text = HackerText;
    }
    public void ThiefTextDisplay()
    {
        InfoText.text = ThiefText;
    }

    //�R�g�_�}��\��
    public void MonokumaInfoTextDisplay()
    {
        InfoText.text = MonokumaInfo;
    }
    public void RopeTextDisplay()
    {
        InfoText.text = Rope;
    }
    public void ArcherySetTextDisplay()
    {
        InfoText.text = ArcherySet;
    }
    public void BatteryTextDisplay()
    {
        InfoText.text = Battery;
    }
    public void ClawTextDisplay()
    {
        InfoText.text = Claw;
    }
    public void NotePcTextDisplay()
    {
        InfoText.text = NotePc;
    }
    public void KorosiaiTextDisplay()
    {
        InfoText.text = Korosiai;
    }
    public void DataProcessingRoomKeyTextDisplay()
    {
        InfoText.text = DataProcessingRoomKey;
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
