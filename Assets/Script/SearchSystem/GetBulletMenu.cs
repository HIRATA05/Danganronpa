using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetBulletMenu : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
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
    [SerializeField,Header("コトダマ")] private GameObject[] Bullet;
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
    //キャラ
    [SerializeField, Header("キャラ")] private GameObject Detective;
    [SerializeField] private GameObject Lacky;
    [SerializeField] private GameObject Archer;
    [SerializeField] private GameObject Hacker;
    [SerializeField] private GameObject Thief;

    //表示文
    private string DetectiveText = "超高校級の探偵　東雲透　シノノメトオル\r\n幼少期に両親を殺人鬼に殺されたことがきっかけで探偵となった\r\n世界中の名だたる探偵が所属する組織探偵図書館に所属している\r\n好きなもの:紅茶 嫌いなもの:殺人\r\n身長:173cm 体重:58kg 胸囲:88cm";//探偵表示文
    private string LackyText = "超高校級の幸運　琴祇真凪　コトギマナ\r\n全国の平均的な学生から抽選で選ばれる幸運の才能に選ばれた\r\n退屈な日常を嫌い、何にでも興味を持つ性格\r\n好きなもの:少年漫画 嫌いなもの:平凡\r\n身長:163cm 体重:68kg 胸囲:94cm";//幸運表示文
    private string ArcherText = "超高校級の弓道家　冷泉凛　レイゼイリン\r\n冷泉財閥の一人娘、幼少から様々な教育を施された才色兼備の令嬢\r\n中学時代から弓道大会で3年連続で優勝してきた\r\n好きなもの:子供向けアニメ 嫌いなもの:水辺\r\n身長:174cm 体重:51kg 胸囲:77cm";//弓道家表示文
    private string HackerText = "超高校級のハッカー　水無瀬罪香　ミナセツミカ\r\n国の機密情報をハッキングできるほどの能力を持つハッカー\r\n家庭環境に問題があり荒れた生活を送ってきた\r\n好きなもの:栄養ドリンク 嫌いなもの:アナログ\r\n身長:158cm 体重:43kg 胸囲:70cm";//ハッカー表示文
    private string ThiefText = "超高校級の手品師　二階堂初流乃　ニカイドウハルノ\r\n海外を中心に活動しているマジシャン\r\n優雅な振る舞いと軽やかな動きで見る者を圧倒する手品を披露する\r\n好きなもの:美しいもの 嫌いなもの:偽物\r\n身長:178cm 体重:69kg 胸囲:76cm";//怪盗表示文

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
        //探索時か議論時に開くことが出来る
        if(!Menu.activeSelf && gameManager.playerController == GameManager.PlayerController.ReticleMode ||
            (gameManager.playerController == GameManager.PlayerController.DiscussionMode && gameManager.disussionManager.discussionProgress))
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
    
    //人物を表示
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

    //コトダマを表示
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
