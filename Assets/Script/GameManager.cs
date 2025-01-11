using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //ゲーム全体を管理する

    //プレイヤー操作の状態
    public enum PlayerController
    {
        ReticleMode,//探索
        TextWindowMode,//会話
        DiscussionMode,//議論
        EventScene//イベントシーン
    }
    [NonSerialized] public PlayerController playerController;

    private TextWindow textWindow;
    private ReticleAim reticleAim;
    private DiscussionManager disussionManager;
    private DiscussionTalkModeWindow discussionTalkModeWindow;

    public EventFlagData eventFlagData;


    [SerializeField] private PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;

    //会話終了時議論開始のフラグ
    public bool isDiscussionStart = false;

    //フェード用演出パネル
    [SerializeField, Header("フェード演出用画像")] private Image FadeEffect;
    //フェード完了のフラグ
    bool fadeComp = false;

    //場面切り替え用
    [SerializeField] private GameObject AdventureScene;
    [SerializeField] private GameObject DiscussionScene;

    //会話に設定したイベントの発生フラグ
    [NonSerialized] public static bool isTalkEvent = false;
    //会話との間に待機時間が発生するイベントフラグ
    [NonSerialized] public static bool isTalkPause = false;

    //キーインプット
    private float keyElapsedTime = 0;
    private float keyWaitTime = 0.5f;
    //スペースキーの有効
    private bool isSpaceKey = true;
    //キーの計測状態
    private bool keyElapsed = false;


    void Start()
    {
        /*
        eventFlagData.GameStart = true;
        eventFlagData.GameStart_Window = false;
        eventFlagData.GameStart_Monitor = false;
        eventFlagData.GameStart_Lacky = false;
        eventFlagData.GameStart_All = false;
        eventFlagData.GameStart_All_TalkStart = false;
        eventFlagData.RoomIn = true;//テスト用
        eventFlagData.GameStart_ClassRoom_True();

        GameObject tw = GameObject.Find("TextWindow");
        textWindow = tw.GetComponent<TextWindow>();
        GameObject ra = GameObject.Find("CorsorImage");
        reticleAim = ra.GetComponent<ReticleAim>();
        */
        GameObject dm = GameObject.Find("DiscussionManager");
        disussionManager = dm.GetComponent<DiscussionManager>();
        GameObject dw = GameObject.Find("DiscussionTalkModeWindow");
        discussionTalkModeWindow = dw.GetComponent<DiscussionTalkModeWindow>();

        playerController = PlayerController.ReticleMode;
        DiscussionScene.SetActive(!DiscussionScene.activeSelf);
    }

    void Update()
    {
        //キーインプットの計測開始
        if (keyElapsed)
        {
            keyElapsedTime += Time.deltaTime;
            
            if (keyElapsedTime > keyWaitTime)
            {
                keyElapsedTime = 0;
                isSpaceKey = true;//キーを有効
                keyElapsed = false;
            }
        }
        
    }


    //ReticleModeに変更
    public void ReticleModeChange()
    {
        playerController = PlayerController.ReticleMode;
        Debug.Log("照準操作探索に移行");
        SwitchScene();

        //議論終了後の会話発生
        OpenTextWindow(disussionManager.AfterText);
    }

    //TextWindowModeに変更
    public void TextWindowModeChange()
    {
        playerController = PlayerController.TextWindowMode;
        Debug.Log("テキストウィンドウに移行");
    }

    //DiscussionModeに変更
    public async void DiscussionModeChange()
    {
        fadeComp = false;
        //フェードインさせる
        StartCoroutine(FadeScreen(true));
        
        //フェードイン完了まで待機
        await UniTask.WaitUntil(() => fadeComp);

        playerController = PlayerController.DiscussionMode;
        //シーン切り替え
        SwitchScene();
        //フェードアウトさせる
        StartCoroutine(FadeScreen(false));
        //議論開始を呼び出す
        disussionManager.DiscInitCallOn();
    }

    //探索シーンと議論シーンの切り替え
    public void SwitchScene()
    {
        Debug.Log("シーンの切り替え");
        AdventureScene.SetActive(!AdventureScene.activeSelf);
        DiscussionScene.SetActive(!DiscussionScene.activeSelf);
    }

    //画面をぼやけさせる効果の切り替え
    public void SwitchDepthOfField(bool switchBleary)
    {
        //depthOfFieldを取得
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out depthOfField);

        if (switchBleary)
        {
            depthOfField.active = true;
        }
        else
        {
            depthOfField.active = false;
        }
    }

    //部屋移動時に呼び出し、探索中のテキストウィンドウをその部屋のテキストに変える
    public void RoomChange(TextWindow textData, DiscussionTalkModeWindow discussionTalkData)
    {
        textWindow = textData;
        discussionTalkModeWindow = discussionTalkData;
    }

    //リクエストに応えてテキストウインドウを開く
    public void OpenTextWindow(DialogueText dialogueText)
    {
        Debug.Log("テキストウィンドウを開く");

        //表示を透明化する
        reticleAim.aimImage.color = Color.clear;
        reticleAim.ColorChangeClaer();

        //テキストウィンドウを表示
        textWindow.dialogueText = dialogueText;
        playerController = PlayerController.TextWindowMode;

        //画面効果発生
        SwitchDepthOfField(true);
    }

    //リクエストに応えて議論用テキストウインドウを開く
    public void OpenDiscussionWindow(DialogueText dialogueText, DiscussionTalkModeWindow.TalkFinish talkFinishSet)
    {
        Debug.Log("議論用テキストウィンドウを開く");

        //会話終了時の移行先を設定
        discussionTalkModeWindow.talkFinish = talkFinishSet;

        //テキストウィンドウを表示
        discussionTalkModeWindow.TextSet(dialogueText);
    }
    /*
    //スペースキーの入力待機
    public bool KeyInputSpace()
    {
        //input = Input.GetKey(KeyCode.Space);

        //keyElapsedTime += Time.deltaTime;

        //スペースキーの入力
        if (Input.GetKey(KeyCode.Space) && isSpaceKey)
        {
            isSpaceKey = false;//キーを無効
            keyElapsed = true;

            return true;
        }
        

        return false;
    }*/

    //画面にフェード演出を発生
    public IEnumerator FadeScreen(bool isFade)
    {
        Color color = FadeEffect.color;
        float duration = 1.0f;
        int FadeAlpha;
        //trueでフェードイン
        if (isFade)
        {
            FadeAlpha = 1;
        }
        //falseでフェードアウト
        else
        {
            FadeAlpha = 0;
        }
         
        //フェードインさせる
        while (!Mathf.Approximately(color.a, FadeAlpha))
        {
            float changePerFrame = Time.deltaTime / duration;
            color.a = Mathf.MoveTowards(color.a, FadeAlpha, changePerFrame);
            FadeEffect.color = color;
            yield return null;
        }

        //フェードインの時フェード完了フラグを立てる
        if (isFade)
        {
            fadeComp = true;
        }
        
    }
}
