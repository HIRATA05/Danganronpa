using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjModeSwitch : MonoBehaviour
{
    //2つのモードを切り替えて表示するモデルを変更

    //表示する2つのモデル
    [SerializeField] private GameObject Befor;
    [SerializeField] private GameObject After;

    private GameManager gameManager;
    private EventFlagData eventFlagData;

    void Start()
    {
        //ゲームマネージャー取得
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
        //フラグによって表示するオブジェクトを変える
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

    void Update()
    {
        
    }

    //表示するオブジェクトを変化
    public void DisplayObjSwitch()
    {
        eventFlagData.Digitalclock = true;
        Befor.SetActive(false);
        After.SetActive(true);
    }

}
