using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static TalkCameraManager;

public class GameManager : MonoBehaviour
{
    //ゲーム全体を管理する

    //プレイヤー操作の状態
    public enum PlayerController
    {
        ReticleMode,//探索
        TextWindowMode,//会話
        DiscussionMode,//議論
    }
    [NonSerialized] public PlayerController playerController;

    private TextWindow textWindow;
    private ReticleAim reticleAim;

    private DiscussionTalkModeWindow discussionTalkModeWindow;

    public EventFlagData eventFlagData;


    [SerializeField] private PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;

    //会話終了時議論開始のフラグ
    public bool isDiscussionStart = false;

    //場面切り替え用
    [SerializeField] private GameObject AdventureScene;
    [SerializeField] private GameObject DiscussionScene;

    void Start()
    {
        eventFlagData.RoomIn = true;//テスト用

        GameObject tw = GameObject.Find("TextWindow");
        textWindow = tw.GetComponent<TextWindow>();
        GameObject ra = GameObject.Find("CorsorImage");
        reticleAim = ra.GetComponent<ReticleAim>();

        GameObject dw = GameObject.Find("DiscussionTalkModeWindow");
        discussionTalkModeWindow = dw.GetComponent<DiscussionTalkModeWindow>();

        playerController = PlayerController.ReticleMode;
        DiscussionScene.SetActive(!DiscussionScene.activeSelf);
    }

    void Update()
    {
        
    }

    //ReticleModeに変更
    public void ReticleModeChange()
    {
        playerController = PlayerController.ReticleMode;
        Debug.Log("照準操作探索に移行");
        SwitchScene();
    }

    //TextWindowModeに変更
    public void TextWindowModeChange()
    {
        playerController = PlayerController.TextWindowMode;
        Debug.Log("テキストウィンドウに移行");
    }

    //DiscussionModeに変更
    public void DiscussionModeChange()
    {
        playerController = PlayerController.DiscussionMode;
        SwitchScene();
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
    public void OpenDiscussionWindow(DialogueText dialogueText)
    {
        Debug.Log("議論用テキストウィンドウを開く");

        //テキストウィンドウを表示
        discussionTalkModeWindow.TextSet(dialogueText);
    }
}
