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
        GameStartGym,//体育館のキャラ表示
        MonokumaGym,//体育館のモノクマ表示
        Digitalclock,//時計
        WarehouseArcher,//倉庫の弓道家
        RopeWindow,//中庭の窓
        GardenHacker,//中庭のハッカー
        GardenPhantomThief,//中庭の怪盗
        GardenArcher,//中庭の弓道家

        Ending//脱出
    }
    public ObjType objType;

    void Start()
    {
        //ゲームマネージャー取得
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
        //フラグによって表示するオブジェクトを変える
        if (objType == ObjType.GameStartGym)
        {
            if (eventFlagData.AdventureStart)
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
        else if (objType == ObjType.MonokumaGym)
        {
            if (eventFlagData.AppMonokuma)
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
        else if (objType == ObjType.Digitalclock)
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
        else if (objType == ObjType.WarehouseArcher)
        {
            if (eventFlagData.WarehouseArcher)
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
        else if (objType == ObjType.GardenHacker)
        {
            if (eventFlagData.AdventureStart)
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
        else if (objType == ObjType.GardenPhantomThief)
        {
            if (eventFlagData.AdventureStart && eventFlagData.F2OpenPhantomThief && !eventFlagData.F2Intrusion)//探索開始から2階侵入後の間表示
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
        else if (objType == ObjType.GardenArcher)
        {
            if (eventFlagData.F2OpenArcher && !eventFlagData.F2Intrusion)//2階侵入イベントの間表示
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

        else if (objType == ObjType.Ending)
        {
            if (eventFlagData.itemDataBase.truthBullets[8].getFlag)//脱出イベント時表示
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
        if (objType == ObjType.GameStartGym)
        {
            eventFlagData.AdventureStart = true;
        }
        else if (objType == ObjType.MonokumaGym)
        {
            eventFlagData.AppMonokuma = true;
        }
        else if (objType == ObjType.Digitalclock)
        {
            eventFlagData.Digitalclock = true;
        }
        else if (objType == ObjType.WarehouseArcher)
        {
            eventFlagData.WarehouseArcher = true;
        }
        else if (objType == ObjType.RopeWindow)
        {
            eventFlagData.RopeWindow = true;
        }
        else if (objType == ObjType.GardenArcher)
        {
            eventFlagData.F2OpenArcher = true;
        }

        Befor.SetActive(false);
        After.SetActive(true);
    }

}
