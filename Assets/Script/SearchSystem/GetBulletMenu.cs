using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetBulletMenu : MonoBehaviour
{
    //照準探索状態と議論中に入手したコトダマを確認できる
    //マウスカーソルを検知して当たっていたら表示を変える
    //左上のボタンで画面を戻す
    //右下のボタンでタイトルに戻る

    //メニューパネル
    [SerializeField] private GameObject Menu;

    //直前の状態
    private GameManager.PlayerController beforController;

    //カーソルを合わせると表示する解説テキスト
    [SerializeField] private TextMeshProUGUI InfoText;

    //コトダマ
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
    //キャラ
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
        //探索時か議論時に開くことが出来る
        if(gameManager.playerController == GameManager.PlayerController.ReticleMode || gameManager.playerController == GameManager.PlayerController.DiscussionMode)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //ゲームの時間を止める
                Time.timeScale = 0.0f;

                //直前の状態を保存
                beforController = gameManager.playerController;
                //状態を変化
                gameManager.playerController = GameManager.PlayerController.EventScene;

                //メニューを開く
                Menu.SetActive(true);

                //情報メモをフラグによって表示する
                for(int i = 0; i < eventFlagData.itemDataBase.truthBullets.Count; i++)
                {
                    if (eventFlagData.itemDataBase.truthBullets[i].getFlag)
                    {
                        if(!Bullet[i].activeSelf) Bullet[i].SetActive(true);
                    }
                }

                if (eventFlagData.GameStart_All)//探偵と幸運
                {
                    if (!Detective.activeSelf) Detective.SetActive(true);
                    if (!Lacky.activeSelf) Lacky.SetActive(true);
                }
                if (eventFlagData.SelfIntoro_Archer)//弓道家
                {
                    if (!Archer.activeSelf) Archer.SetActive(true);
                }
                if (eventFlagData.SelfIntoro_Hacker)//ハッカー
                {
                    if (!Hacker.activeSelf) Hacker.SetActive(true);
                }
                if (eventFlagData.SelfIntoro_PhantomThief)//怪盗
                {
                    if (!Thief.activeSelf) Thief.SetActive(true);
                }

            }
        }
    }

    //メニューを閉じる
    public void CloseMenu()
    {
        //状態を戻す
        gameManager.playerController = beforController;

        //メニューを開く
        Menu.SetActive(false);

        //ゲームの時間を戻す
        Time.timeScale = 1.0f;
    }

    //タイトルへ戻る
    public void ReturnTitle()
    {
        Debug.Log("終了");
        Application.Quit();//ゲームプレイ終了
    }
}
