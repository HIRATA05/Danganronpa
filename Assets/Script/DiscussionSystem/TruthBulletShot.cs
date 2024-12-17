using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TruthBulletShot : MonoBehaviour
{
    //照準を動かす
    //ボタンを押すと発射
    //弾が始点から終点に移動するまで照準が消えて移動不可


    //ノンストップ議論の動作を管理
    [SerializeField] private DiscussionManager discussionManager;

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;

    //照準
    [SerializeField] private GameObject aim;
    private Image aimImage;
    private Transform aimImagePos;

    //照準の画像
    [SerializeField] private Sprite aimImageNormal;//通常
    [SerializeField] private Sprite aimImageColText;//発言と重なる

    //コトダマの現在位置
    [SerializeField] private Transform shotPointCurrent;

    // 線形補間の始点
    [SerializeField] private Transform shotPointFrom;

    // 線形補間の終点
    private Vector3 shotPointTo = new Vector3();

    //リロード状態
    private bool reloading = false;
    private float reloadingTime = 3.0f;

    //弾丸が移動中か
    private bool MoveEnd = false;
    //文字に当たったか
    private bool isBulletTextCol = false;

    void Start()
    {
        aimImage = aim.GetComponent<Image>();
        aimImagePos = aim.transform;

        //shotPointFrom = shotPointCurrent;
        shotPointCurrent.GetComponent<BulletCol>().SetBulletScript(this.gameObject.GetComponent<TruthBulletShot>());
        Debug.Log(shotPointFrom.position);
    }

    void Update()
    {
        //議論中か判定
        if (discussionManager.discussionProgress)
        {
            //照準を動かす
            //マウスの位置と照準器の位置を同期させる
            aimImagePos.position = Input.mousePosition;

            RaycastHit hit;

            //MainCameraからマウスの位置にRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("レイが当たった");
                //ボタンを押すと発射、発射後数秒間発射不可
                if (Input.GetKeyUp(KeyCode.Space)/*gameManager.KeyInputSpace()*/ && !reloading)
                {
                    shotPointTo = hit.point;
                    Debug.Log("レイが当たった:" + shotPointTo);
                    shotPointCurrent.position = new Vector3(Camera.main.transform.position.x + 1, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    StartCoroutine(BulletMove());
                    
                }
            }
        }
        
    }

    private IEnumerator BulletMove()
    {
        //弾が始点から終点に移動するまで照準が消えて移動不可

        //リロード開始
        StartCoroutine(Reload());

        Vector3 targetPosition = shotPointTo; //目的の位置の座標を指定
        Vector3 startPosition = shotPointCurrent.position; //ゲームオブジェクトのTransformコンポーネントを取得
        float duration = 0.1f; //着弾までの時間、単位は秒
        float time = 0.0f;  //発射からの経過時間

        //弾の移動処理
        while(!MoveEnd && time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            
            //目的の位置までduration秒をかけて補間で移動
            shotPointCurrent.position = Vector3.Lerp(startPosition, targetPosition, t); // 目的の位置に移動
            yield return null;
        }

        //弾の位置を変化
        BulletDelete();

        //ウィークポイントか確認
        if (discussionManager.TextColWeek())
        {
            //ノンストップ議論終了処理を呼び出す
            discussionManager.ShootingFinish();
        }
        else
        {
            //ウィークポイント出ない場合
            NotBreakBullet();
        }
        /*
        //弾が文字に当たっていた場合
        if (isBulletTextCol)
        {
            BulletTextTouch();
        }
        */
        
        yield return null;
    }

    //コトダマ発射後のリロード
    IEnumerator Reload()
    {
        //リロード判定をtrueにする
        reloading = true;
        yield return new WaitForSeconds(reloadingTime);
        Debug.Log("リロード終了");

        //リロード判定をfalseにする
        reloading = false;
    }

    //初期位置に戻し画面内から弾を消す
    public void BulletDelete()
    {
        Debug.Log("弾丸消去");
        shotPointCurrent.position = shotPointFrom.position;
        Debug.Log(shotPointCurrent.position + " : " + shotPointFrom.position);
    }

    public void BulletTextTouch()
    {
        isBulletTextCol = false;
        MoveEnd = !MoveEnd;
        
        Debug.Log("ノンストップ議論終了");
        //弾の位置を変化
        BulletDelete();

        //ウィークポイントか確認
        if (discussionManager.TextColWeek())
        {
            //ノンストップ議論終了処理を呼び出す
            discussionManager.ShootingFinish();
        }
        else
        {
            //ウィークポイント出ない場合
            NotBreakBullet();
        }
        //ウィークポイントなら論破か同意か確認


    }
    public void NotBreakBullet()
    {
        MoveEnd = !MoveEnd;
        
        Debug.Log("ウィークポイントに接触しなかった");
    }

    public void BulletCol()
    {
        isBulletTextCol = true;
    }
}
