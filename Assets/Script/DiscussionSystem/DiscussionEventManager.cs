using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussionEventManager : MonoBehaviour
{
    //�c�_��b���̃C�x���g

    private DiscussionManager discussionManager;
    private DiscussionTalkModeWindow discussionTalkModeWindow;
    [SerializeField] private DiscussionUI discussionUI;

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

    //�J�����̈ړ�
    public void TalkCameraMove()
    {
        //discussionTalkModeWindow.SpeechCameraSet();

    }

    //�c�_�Ŕ����������̂����Z�b�g���鏈�����Ăяo���@�c�_�I�����ɐݒ肷��
    public void DiscussionEndCall()
    {
        discussionManager.DiscussionEnd();
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

    //�����͂������̏������Ăяo���@�_�j���s���ɐݒ肷��
    public void SpeechPoworDamageDisp()
    {
        Debug.Log("�����͂������̏�������");
        //��ʂ�h�炷


        //�����͂�����������
        discussionUI.SpeechPoworDamage();
    }

}
