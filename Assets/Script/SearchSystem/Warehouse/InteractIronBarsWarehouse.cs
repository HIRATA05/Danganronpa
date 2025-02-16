using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIronBarsWarehouse : MonoBehaviour, IReceiveSearch
{
    //�q�ɓS�i�q

    [SerializeField] private DialogueText NormalText;//�S�i�q�𒲂ׂ�

    [SerializeField] private GameObject IronBars;

    [SerializeField] private GameObject Cam;
    enum CamPosMode
    {
        Front,
        Back
    }
    CamPosMode camPos = CamPosMode.Front;
    //�J�����ʒu
    [SerializeField] private Vector3 Front;
    [SerializeField] private Vector3 Back;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //�S�i�q������
        if (gameManager.eventFlagData.IronBarsOpen)
        {
            //�J�����؂�ւ��@�J�����̈ʒu�ω�
            if(camPos == CamPosMode.Front)
            {
                camPos = CamPosMode.Back;
                Cam.transform.localPosition = Back;
                //���̌����ɕς���
                //Cam.transform.Rotate(new Vector3(0,-40,0));
                Cam.transform.eulerAngles = new Vector3(0, -34, 0);
                //transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else
            {
                camPos = CamPosMode.Front;
                Cam.transform.localPosition = Front;
                //�O�̌����ɕς���
                //Cam.transform.Rotate(new Vector3(0, 40, 0));
                Cam.transform.eulerAngles = new Vector3(0, 40, 0);
            }
        }
        else
        {
            gameManager.OpenTextWindow(NormalText);
        }
        
    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        //�t���O�Ŕ�\��
        if (gameManager.eventFlagData.IronBarsOpen)
        {
            if(IronBars.activeSelf) IronBars.SetActive(false);

        }
    }
}
