using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscussionUI : MonoBehaviour
{
    //議論のUI


    //テキスト参照
    //現在の発言番号
    [SerializeField] private TextMeshProUGUI SpeechNumCurrentText;
    //発言番号の最大
    [SerializeField] private TextMeshProUGUI SpeechNumMaxText;
    //発言者の名前
    [SerializeField] private TextMeshProUGUI SpeakerNameText;
    //発言力の画像
    [SerializeField] private Image LifeImage;

    //現在の発言番号
    private int SpeechNumCurrent;
    //発言番号の最大
    private int SpeechNumMax;
    //発言者の名前
    private string SpeakerName;
    //主人公の発言力
    private int Life = 5;


    //現在の発言番号を設定
    public void SpeechNumSet(int num)
    {
        SpeechNumCurrentText.text = num.ToString();
    }

    //発言番号の最大を設定
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
