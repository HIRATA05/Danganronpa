using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Hirata : MonoBehaviour
{
    //テスト用の簡易的なタイトルスクリプト

    //シーン名
    [SerializeField] private string NextScene;
    //フラグ
    [SerializeField, Header("初期化用フラグデータ")] private EventFlagData InitFlag;
    [SerializeField, Header("上書きフラグデータ")] private EventFlagData SaveFlag;

    //シーンを移動する
    public void SceneMove()
    {
        //フラグを初期化
        InitFlag.itemDataBase = new ItemDataBase();
        SaveFlag = InitFlag;

        SceneManager.LoadScene(NextScene);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
