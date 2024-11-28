using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussionEventManager : MonoBehaviour
{
    //�c�_��b���̃C�x���g

    private DiscussionTalkModeWindow discussionTalkModeWindow;

    //��b���̃C�x���g
    [System.Serializable]
    public class DiscussionEvent
    {
        //��b����������C�x���g
        public UnityEngine.Events.UnityEvent[] OnTalkEvent;
    }

    [System.Serializable]
    public class DiscussionEventSet
    {
        //��b�ԍ��@Scriptable�e�L�X�g�Ɠ������邽�߂̃L�[
        public string textinfo;

        //������b�C�x���g
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

    //�c�_��b�I����̈ڍs��ύX
    public void DiscussionTalkFinishDiscussion()
    {
        discussionTalkModeWindow.talkFinish = DiscussionTalkModeWindow.TalkFinish.DiscussionMode;
    }
    public void DiscussionTalkFinishAdventure()
    {
        discussionTalkModeWindow.talkFinish = DiscussionTalkModeWindow.TalkFinish.AdventureMode;
    }

}
