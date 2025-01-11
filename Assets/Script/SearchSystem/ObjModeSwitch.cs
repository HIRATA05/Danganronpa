using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjModeSwitch;

public class ObjModeSwitch : MonoBehaviour
{
    //2つのモードを切り替えて表示するモデルを変更

    //表示する2つのモデル
    [SerializeField] private GameObject Befor;
    [SerializeField] private GameObject After;

    private GameManager gameManager;
    private EventFlagData eventFlagData;

    //オブジェクトの種類
    public enum ObjType
    {
        Digitalclock,//時計
        RopeWindow,//中庭の窓
        GardenStatue,//中庭の石像
    }
    public ObjType objType;

    void Start()
    {
        //ゲームマネージャー取得
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
        //フラグによって表示するオブジェクトを変える
        if (objType == ObjType.Digitalclock)
        {
            if (eventFlagData.Digitalclock)
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.RopeWindow)
        {
            if (eventFlagData.RopeWindow)
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }

        }
        
    }

    void Update()
    {
        
    }

    //表示するオブジェクトを変化
    public void DisplayObjSwitch()
    {
        //設定されら種類によって変更するフラグを変化
        if(objType == ObjType.Digitalclock)
        {
            eventFlagData.Digitalclock = true;
        }
        else if (objType == ObjType.RopeWindow)
        {
            eventFlagData.RopeWindow = true;
        }
        Befor.SetActive(false);
        After.SetActive(true);
    }

}
