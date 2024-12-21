using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscussionUI : MonoBehaviour
{
    //�c�_��UI


    //�e�L�X�g�Q��
    //���݂̔����ԍ�
    [SerializeField] private TextMeshProUGUI SpeechNumCurrentText;
    //�����ԍ��̍ő�
    [SerializeField] private TextMeshProUGUI SpeechNumMaxText;
    //�����҂̖��O
    [SerializeField] private TextMeshProUGUI SpeakerNameText;
    //�����͂̉摜
    [SerializeField] private Image LifeImage;

    //���݂̔����ԍ�
    private int SpeechNumCurrent;
    //�����ԍ��̍ő�
    private int SpeechNumMax;
    //�����҂̖��O
    private string SpeakerName;
    //��l���̔�����
    private int Life = 5;


    //���݂̔����ԍ���ݒ�
    public void SpeechNumSet(int num)
    {
        SpeechNumCurrentText.text = num.ToString();
    }

    //�����ԍ��̍ő��ݒ�
    public void SpeechNumMaxSet(int num)
    {
        SpeechNumMaxText.text = num.ToString();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
