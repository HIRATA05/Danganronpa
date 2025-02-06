using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussionEventManager : MonoBehaviour
{
    //議論会話時のイベント

    private DiscussionManager discussionManager;
    private DiscussionTalkModeWindow discussionTalkModeWindow;
    [SerializeField] private DiscussionUI discussionUI;

    //会話時のイベント
    [System.Serializable]
    public class DiscussionEvent
    {
        //会話時発生するイベント
        public UnityEngine.Events.UnityEvent[] OnTalkEvent;
    }

    [System.Serializable]
    public class DiscussionEventSet
    {
        //会話番号　Scriptableテキストと同期するためのキー
        public string textinfo;

        //複数会話イベント
        public DiscussionEvent discussionEvent;
    }
    public DiscussionEventSet[] discussionEventSet;

    void Start()
    {
        GameObject dm = GameObject.Find("DiscussionManager");
        discussionManager = dm.GetComponent<DiscussionManager>();

        GameObject dw = GameObject.Find("DiscussionTalkModeWindow");
        discussionTalkModeWindow = dw.GetComponent<DiscussionTalkModeWindow>();

        //GameObject du = GameObject.Find("DiscussionUI");
        //discussionUI = du.GetComponent<DiscussionUI>();
    }

    void Update()
    {
        
    }

    //カメラの移動
    public void TalkCameraMove()
    {
        //discussionTalkModeWindow.SpeechCameraSet();

    }

    //議論で発生したものをリセットする処理を呼び出す　議論終了時に設定する
    public void DiscussionEndCall()
    {
        discussionManager.DiscussionEnd();
    }

    //議論会話終了後の移行先変更
    public void DiscussionTalkFinishDiscussion()
    {
        discussionTalkModeWindow.talkFinish = DiscussionTalkModeWindow.TalkFinish.DiscussionMode;
    }
    public void DiscussionTalkFinishAdventure()
    {
        discussionTalkModeWindow.talkFinish = DiscussionTalkModeWindow.TalkFinish.AdventureMode;
    }

    //発言力を減少の処理を呼び出す　論破失敗時に設定する
    public void SpeechPoworDamageDisp()
    {
        Debug.Log("発言力を減少の処理発生");
        //画面を揺らす


        //発言力を減少させる
        discussionUI.SpeechPoworDamage();
    }

}
