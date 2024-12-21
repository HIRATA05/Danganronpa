using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussionEventManager : MonoBehaviour
{
    //議論会話時のイベント

    private DiscussionTalkModeWindow discussionTalkModeWindow;

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
        GameObject dw = GameObject.Find("DiscussionTalkModeWindow");
        discussionTalkModeWindow = dw.GetComponent<DiscussionTalkModeWindow>();
    }

    void Update()
    {
        
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

}
