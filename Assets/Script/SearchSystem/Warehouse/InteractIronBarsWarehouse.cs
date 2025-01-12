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
                Cam.transform.position = Back;
            }
            else
            {
                camPos = CamPosMode.Front;
                Cam.transform.position = Front;
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
            IronBars.SetActive(false);

        }
    }
}
