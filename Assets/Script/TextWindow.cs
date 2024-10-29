using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextWindow : MonoBehaviour
{
    //テキストウィンドウ

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;

    [NonSerialized] public DialogueText dialogueText;
    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    int index = 0;
    bool isTyping;

    private Coroutine dialogueCoroutine;


    //会話を行う主人公キャラのオブジェクトパーツ
    [SerializeField] private GameObject mainTaklChara;
    [SerializeField] private Vector3 mainTaklCharaPos;
    GameObject mainChara;
    //配置する主人公の親オブジェクト
    [SerializeField] private GameObject charaSetCanvas;


    //カメラ
    [SerializeField] private Camera TaklCamera_1;
    [SerializeField] private Camera TaklCamera_2;
    [SerializeField] private Camera TaklCamera_3;

    

    private void Start()
    {
        

        TaklCamera_1.rect = new Rect(0.25f, 0.0f, 0.5f, 1f);//真ん中
        TaklCamera_2.rect = new Rect(0.0f, 0.0f, 0.25f, 0.9f);//左
        TaklCamera_3.rect = new Rect(0.75f, 0.0f, 0.25f, 0.8f);//右

        

    }

    void Update()
    {
        //カメラテスト
        if (Input.GetKeyUp(KeyCode.R))
        {
            TaklCamera_1.rect = new Rect(0.25f, 0.0f, 0.5f, 1f);//真ん中
            TaklCamera_2.rect = new Rect(0.0f, 0.0f, 0.25f, 0.9f);//左
            TaklCamera_3.rect = new Rect(0.75f, 0.0f, 0.25f, 0.8f);//右
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            TaklCamera_1.rect = new Rect(0.15f, 0.0f, 0.5f, 1f);//真ん中
            TaklCamera_2.rect = new Rect(-0.1f, 0.0f, 0.25f, 0.9f);//画面外
            TaklCamera_3.rect = new Rect(0.65f, 0.0f, 0.25f, 0.8f);//右
        }
        


        //テキストを表示
        if (gameManager.playerController == GameManager.PlayerController.TextWindowMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //会話文表示処理を実行する
                displayDialogueText();

            }
        }   
    }

    /// <summary>
    /// 会話文表示処理
    /// </summary>
    public void displayDialogueText()
    {
        //パネルが非表示なら表示する
        if (!panelObject.activeSelf)
        {
            panelObject.SetActive(true);


            Debug.Log("主人公のオブジェクト配置");
            // 配置する座標を設定
            Vector3 placePosition = new Vector3(Camera.main.transform.position.x + mainTaklCharaPos.x,
                0+ mainTaklCharaPos.y, Camera.main.transform.position.z + mainTaklCharaPos.z);
            // 配置する回転角を設定
            Quaternion quate = new Quaternion();
            quate = Quaternion.identity;
            //親オブジェクト設定
            var parent = charaSetCanvas.transform;
            // ブロックの複製
            mainChara = Instantiate(mainTaklChara, placePosition, quate, parent);
            //シネマシーンに設定

            //

            //カメラの注視先を設定

            //会話用カメラの起動
            CameraEnabled();


        }

        //scriptableObjectの情報をパネルに表示する
        if (dialogueText.textInfomations.Length > index)
        {
            

            //話者の名前を表示
            speakerNameText.text = dialogueText.textInfomations[index].speakerName;

            if (!isTyping)
            {
                dialogueCoroutine = StartCoroutine(TypeDialogueText(dialogueText.textInfomations[index].paragraphs));
            }
            else
            {
                stopTyping();
            }
        }
        else
        {
            //会話が終了したためパネルを非表示にする
            speakerNameText.text = "";
            speakerDialogueText.text = "";
            panelObject.SetActive(false);

            //主人公のマップオブジェクト削除
            if(mainChara != null) Destroy(mainChara);

            //会話用カメラの非表示
            CameraEnabled();

            //状態を初期化して照準モードにする
            gameManager.playerController = GameManager.PlayerController.ReticleMode;

            index = 0;
        }
        
    }

    /// <summary>
    /// ダイアログのテキストを一文字づつ表示していきます
    /// </summary>
    /// <param name="paragraph">会話の1文</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator TypeDialogueText(string paragraph)
    {
        string displayText = "";
        isTyping = true;
        int colorIndex = 0;
        foreach (char c in paragraph)
        {
            colorIndex++;
            speakerDialogueText.text = paragraph;
            displayText = speakerDialogueText.text.Insert(colorIndex, "<color=#00000000>");
            speakerDialogueText.text = displayText;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        //次の会話文インデックスを進める
        index++;
    }

    /// <summary>
    /// 1文字づつ表示を終了しテキスト全文を表示する
    /// </summary>
    private void stopTyping()
    {
        StopCoroutine(dialogueCoroutine);
        //未表示の会話文を全文表示する
        speakerDialogueText.text = dialogueText.textInfomations[index].paragraphs;
        isTyping = false;
        //次の会話文インデックスを進める
        index++;
    }

    //会話カメラの起動
    public void CameraEnabled()
    {
        //FALSEの時TRUE
        //    if (!TaklCamera_1.enabled) TaklCamera_1.enabled = true;
        //    if (!TaklCamera_2.enabled) TaklCamera_2.enabled = true;
        //    if (!TaklCamera_3.enabled) TaklCamera_3.enabled = true;

        //    //TRUEの時FALSE
        //    if (TaklCamera_1.enabled) TaklCamera_1.enabled = false;
        //    if (TaklCamera_2.enabled) TaklCamera_2.enabled = false;
        //    if (TaklCamera_3.enabled) TaklCamera_3.enabled = false;

        TaklCamera_1.enabled = !TaklCamera_1.enabled;
        TaklCamera_2.enabled = !TaklCamera_2.enabled;
        TaklCamera_3.enabled = !TaklCamera_3.enabled;

    }
}
