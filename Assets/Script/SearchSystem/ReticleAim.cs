using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using TMPro;
//using UnityEngine.UIElements;

public class ReticleAim : MonoBehaviour
{
    //エイムの操作

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;

    //照準
    [SerializeField] private Image aimImage;

    //照準の画像
    [SerializeField] private Sprite OnCollisionAimImage_Obj;
    [SerializeField] private Sprite OnCollisionAimImage_Chara;
    [SerializeField] private Sprite DisCollisionAimImage;
    //照準の大きさ
    [SerializeField] private float OnCollisionAimSize;
    [SerializeField] private float DisCollisionAimSize;

    [SerializeField] private Image SearchObjInformationWindow;
    [SerializeField] private TextMeshProUGUI SearchObjInformationText;

    //回転速度
    private float RotSpeed = 0.1f;


    float viewX; // ビューポート座標のxの値
    float viewY; // ビューポート座標のyの値



    void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;	//カーソルを画面内に閉じ込める
        //Cursor.visible = false;		//カーソルを非表示にする
    }

    void Update()
    {
        if(gameManager.playerController == PlayerController.ReticleMode)
        {
            ReticleMove();
        }

    }

    private void ReticleMove()
    {
        //マウスの位置と照準器の位置を同期させる。
        //transform.position = Input.mousePosition;

        
        //移動後のビューポート座標の値を取得
        viewX = Camera.main.WorldToViewportPoint(Input.mousePosition).x;
        viewY = Camera.main.WorldToViewportPoint(Input.mousePosition).y;
        // もし移動後のビューポート座標が０から１の範囲ならば
        if (0 <= viewX && viewX <= Screen.width && 0 <= viewY && viewY <= Screen.height)
        {
            Debug.Log(viewY);
            //マウスの位置と照準器の位置を同期させる。
            transform.position = Input.mousePosition;
        }
        

        RaycastHit hit;

        //MainCameraからマウスの位置にRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            //探索可能オブジェクトに当たった時
            if (hit.transform.CompareTag("SearchObj") || hit.transform.CompareTag("SearchChara"))
            {
                //当たったオブジェクトによって照準の形を変える
                if (hit.transform.CompareTag("SearchObj"))
                {
                    //照準を変化させる
                    aimImage.sprite = OnCollisionAimImage_Obj;
                    //照準サイズ変更
                    transform.localScale = new Vector3(OnCollisionAimSize, OnCollisionAimSize, OnCollisionAimSize);
                }
                else if (hit.transform.CompareTag("SearchChara"))
                {
                    //回転を終了
                    //transformを取得
                    Transform myTransform = this.transform;
                    //回転値をゼロ
                    Vector3 worldAngle = new (0.0f, 0.0f, 0.0f);
                    //回転角度を設定
                    myTransform.eulerAngles = worldAngle;

                    //照準を変化させる
                    aimImage.sprite = OnCollisionAimImage_Chara;
                    //照準サイズ変更
                    transform.localScale = new Vector3(OnCollisionAimSize, OnCollisionAimSize, OnCollisionAimSize);
                }


                //探索可能オブジェクト情報のウィンドウを可視化する
                if (SearchObjInformationWindow.color != Color.white)
                    SearchObjInformationWindow.color = Color.white;
                //オブジェクトの名前になっていないなら表示するテキストを変える
                if(SearchObjInformationText.text != hit.transform.GetComponent<ObjectName>().ObjName)
                    SearchObjInformationText.text = hit.transform.GetComponent<ObjectName>().ObjName;
                if (SearchObjInformationText.color != Color.black)
                    SearchObjInformationText.color = Color.black;

                //この状態で調べるとテキスト表示
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //インタフェースを確認する
                    IReceiveSearch SearchObj = hit.transform.GetComponent<IReceiveSearch>();

                    if (SearchObj != null)
                    {
                        SearchObj.ReceiveSearch();
                    }

                    //探索可能オブジェクト情報のウィンドウを透明にする
                    if (SearchObjInformationWindow.color != Color.clear)
                        SearchObjInformationWindow.color = Color.clear;
                    if (SearchObjInformationText.color != Color.clear)
                        SearchObjInformationText.color = Color.clear;
                }

            }
            else
            {
                //オブジェクトに当たっていない時
                DisColliderAim();
            }
        }
        else
        {
            //オブジェクトに当たっていない時
            DisColliderAim();
        }
    }

    //探索可能オブジェクトに当たっていない時
    private void DisColliderAim()
    {
        //照準の回転
        //transformを取得
        Transform myTransform = this.transform;

        //ワールド座標を基準に回転を取得
        Vector3 worldAngle = myTransform.eulerAngles;
        worldAngle.x = 0.0f;
        worldAngle.y = 0.0f;
        worldAngle.z += RotSpeed;//回転を加算
        myTransform.eulerAngles = worldAngle;//回転角度を設定

        //照準を変化させる
        aimImage.sprite = DisCollisionAimImage;
        //照準サイズ変更
        transform.localScale = new Vector3(DisCollisionAimSize, DisCollisionAimSize, DisCollisionAimSize);

        //探索可能オブジェクト情報のウィンドウを透明にする
        if (SearchObjInformationWindow.color != Color.clear)
            SearchObjInformationWindow.color = Color.clear;
        if (SearchObjInformationText.color != Color.clear)
            SearchObjInformationText.color = Color.clear;
    }

}
