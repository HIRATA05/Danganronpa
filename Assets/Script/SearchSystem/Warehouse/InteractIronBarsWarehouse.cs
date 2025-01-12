using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIronBarsWarehouse : MonoBehaviour, IReceiveSearch
{
    //倉庫鉄格子

    [SerializeField] private DialogueText NormalText;//鉄格子を調べる

    [SerializeField] private GameObject IronBars;

    [SerializeField] private GameObject Cam;
    enum CamPosMode
    {
        Front,
        Back
    }
    CamPosMode camPos = CamPosMode.Front;
    //カメラ位置
    [SerializeField] private Vector3 Front;
    [SerializeField] private Vector3 Back;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //鉄格子解除後
        if (gameManager.eventFlagData.IronBarsOpen)
        {
            //カメラ切り替え　カメラの位置変化
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
        //フラグで非表示
        if (gameManager.eventFlagData.IronBarsOpen)
        {
            IronBars.SetActive(false);

        }
    }
}
