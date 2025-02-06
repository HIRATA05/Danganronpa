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
        ClassRoomF2PhantomThief,//2階教室の怪盗
        ClassRoomF2Lacky,//2階教室の幸運
        MachineRoomHacker,//機械工作室のハッカー
        MonokuGreenMove,//モノクマグリーンの移動
        PressMachine,//プレス機
        DataProcessRoomLacky,//情報処理室の幸運
        DataProcessRoomChara,//情報処理室のキャラ達
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
            if (eventFlagData.AdventureStart && !eventFlagData.F2OpenPhantomThief && !eventFlagData.F2Intrusion)//探索開始から2階侵入後の間表示
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
        else if (objType == ObjType.ClassRoomF2PhantomThief)
        {
            if (eventFlagData.ClassRoomF2StartPhantomThief)//2階侵入後最初だけ表示
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
        else if (objType == ObjType.ClassRoomF2Lacky)
        {
            if (eventFlagData.ClassRoomF2_All)//2階教室探索後表示
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
        else if (objType == ObjType.PressMachine)
        {
            if (eventFlagData.PressMachineShelfUnlock)//プレス機の起動
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
        else if (objType == ObjType.MachineRoomHacker)
        {
            if (eventFlagData.HackerPressMachineRequest)//機械工作室のハッカー
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
        else if (objType == ObjType.MonokuGreenMove)
        {
            if (eventFlagData.MonokumaGreenPreesMove)//モノクマグリーン
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
        else if (objType == ObjType.DataProcessRoomLacky)
        {
            if (eventFlagData.Lacky_DataProcessingRoom)//情報処理室の幸運
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
        else if (objType == ObjType.DataProcessRoomChara)
        {
            if (eventFlagData.Chara_DataProcessingRoom)//情報処理室のキャラ
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
            if (eventFlagData.EscepeSwitch)//脱出イベント時表示
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
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.MonokumaGym)
        {
            eventFlagData.AppMonokuma = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.Digitalclock)
        {
            eventFlagData.Digitalclock = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.WarehouseArcher)
        {
            eventFlagData.WarehouseArcher = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.RopeWindow)
        {
            eventFlagData.RopeWindow = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.GardenArcher)
        {
            eventFlagData.F2OpenArcher = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.ClassRoomF2PhantomThief)
        {
            eventFlagData.ClassRoomF2StartPhantomThief = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.ClassRoomF2Lacky && eventFlagData.ClassRoomF2_Vent && eventFlagData.ClassRoomF2_Window)
        {
            eventFlagData.ClassRoomF2_All = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.PressMachine)
        {
            eventFlagData.PressMachineShelfUnlock = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.MachineRoomHacker)
        {
            eventFlagData.HackerPressMachineRequest = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.MonokuGreenMove)
        {
            eventFlagData.MonokumaGreenPreesMove = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.DataProcessRoomLacky)
        {
            eventFlagData.Lacky_DataProcessingRoom = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.DataProcessRoomChara)
        {
            eventFlagData.Chara_DataProcessingRoom = true;
            DisplayObjSwitch_After();
        }

    }

    public void DisplayObjSwitch_After()
    {
        Befor.SetActive(false);
        After.SetActive(true);
    }

    public void DisplayObjSwitch_Befor()
    {
        Befor.SetActive(true);
        After.SetActive(false);
    }
}
