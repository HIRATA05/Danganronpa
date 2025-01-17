using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBulletMenu : MonoBehaviour
{
    //照準探索状態と議論中に入手したコトダマを確認できる
    //マウスカーソルを検知して当たっていたら表示を変える
    //左上のボタンで画面を戻す
    //右下のボタンでタイトルに戻る


    private GameManager gameManager;
    private EventFlagData eventFlagData;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.playerController == GameManager.PlayerController.ReticleMode)
        {

        }
    }
}
