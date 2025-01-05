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

    //議論時のUI
    [SerializeField] private DiscussionUI discussionUI;

    //ゲームマネージャー
    [SerializeField] private GameManager gameManager;

    //照準
    [SerializeField] private GameObject aim;
    //private Image aimImage;
    private Transform aimImagePos;
    /*
    //照準の画像
    [SerializeField] private Sprite aimImageNormal;//通常
    [SerializeField] private Sprite aimImageColText;//発言と重なる
    */
    //コトダマの文章
    [SerializeField] private TextMeshProUGUI bulletText;

    //コトダマの位置
    [SerializeField] private Vector3 bulletPoint;

    //コトダマの現在位置
    [SerializeField] private Transform shotPointCurrent;

    // 線形補間の始点
    [SerializeField] private Transform shotPointFrom;

    // 線形補間の終点
    private Vector3 shotPointTo = new Vector3();

    //リロード状態
    private bool reloading = false;
    private float reloadingTime = 2.0f;

    //弾丸が移動中か
    private bool MoveEnd = false;
    //文字に当たったか
    private bool isBulletTextCol = false;

    void Start()
    {
        //aimImage = aim.GetComponent<Image>();
        aimImagePos = aim.transform;

        //shotPointFrom = shotPointCurrent;
        shotPointCurrent.GetComponent<BulletCol>().SetBulletScript(this.gameObject.GetComponent<TruthBulletShot>());


    }

    void Update()
    {
        //議論中か判定
        if (discussionManager.discussionProgress)
        {
            //照準を動かす
            //マウスの位置と照準の位置を同期させる
            aimImagePos.position = Input.mousePosition;

            RaycastHit hit;

            //MainCameraからマウスの位置にRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                /*
                //当たった物が発言の時照準の画像を変化
                //タグ
                Debug.Log("hit name:" + hit.collider.name);
                if (hit.collider.CompareTag("Speech"))
                {
                    aimImage.sprite = aimImageColText;
                }
                else
                {
                    aimImage.sprite = aimImageNormal;
                }
                */

                //Debug.Log("レイが当たった");
                //ボタンを押すと発射、発射後数秒間発射不可
                if (Input.GetKeyUp(KeyCode.Space)/*gameManager.KeyInputSpace()*/ && !reloading)
                {
                    shotPointTo = hit.point;
                    Debug.Log("レイが当たった:" + shotPointTo);
                    shotPointCurrent.GetComponent<RectTransform>().localPosition = bulletPoint;
                    StartCoroutine(BulletMove());
                    
                }
            }

            
        }
        
    }

    private IEnumerator BulletMove()
    {
        //弾が始点から終点に移動するまで照準が消えて移動不可

        //照準を消す
        aim.GetComponent<Image>().enabled = false;

        //現在のコトダマの名前を取得
        bulletText.text = discussionUI.Bullet[discussionUI.BulletCount].truthBullets.bulletName;

        //リロード開始
        StartCoroutine(Reload());

        Vector3 targetPosition = shotPointTo; //目的の位置の座標を指定
        Vector3 startPosition = shotPointCurrent.position; //ゲームオブジェクトのTransformコンポーネントを取得
        float duration = 0.6f; //着弾までの時間、単位は秒
        float time = 0.0f;  //発射からの経過時間

        //着弾地点によって角度を変化


        //弾の移動処理
        while(/*!MoveEnd && */time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            
            //目的の位置までduration秒をかけて補間で移動
            shotPointCurrent.position = Vector3.Lerp(startPosition, targetPosition, t); //目的の位置に移動
            yield return null;
        }

        //弾の位置を変化させて画面外に移動
        BulletDelete(startPosition);

        //ウィークポイントか確認
        if (discussionManager.TextColWeek())
        {
            //現在のコトダマでウィークポイントが正しいか判定
            if (discussionManager.TruthBulletCompare())
            {
                //ノンストップ議論終了処理を呼び出す
                discussionManager.ShootingFinish();
            }
            else
            {
                //失敗演出の後に発言ごとの失敗テキスト発生
                discussionManager.DiscussionFailureChange(targetPosition);
                Debug.Log("ウィークポイント間違えた時の会話");
                
                NotBreakBullet();
            }
            
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

        //照準を表示
        aim.GetComponent<Image>().enabled = true;
    }

    //コトダマ発射後のリロード
    IEnumerator Reload()
    {
        //発射時のシリンダーの動作
        discussionUI.CylinderTextErase();
        //リロード判定をtrueにする
        reloading = true;
        yield return new WaitForSeconds(reloadingTime);
        Debug.Log("リロード終了");
        //シリンダーの文字を戻す
        discussionUI.CylinderTextReturn();
        //リロード判定をfalseにする
        reloading = false;
    }

    //初期位置に戻し画面内から弾を消す
    public void BulletDelete(Vector3 startPos)
    {
        Debug.Log("弾丸消去");
        //現在のコトダマの名前を非表示
        bulletText.text = "";
        //初期位置に戻す
        shotPointCurrent.position = startPos;
    }
    /*
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
    */
    public void NotBreakBullet()
    {
        MoveEnd = false;
        
        Debug.Log("ウィークポイントに接触しなかった");
    }

    public void BulletCol()
    {
        isBulletTextCol = true;
    }
}
