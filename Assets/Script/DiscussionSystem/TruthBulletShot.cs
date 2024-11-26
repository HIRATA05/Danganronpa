using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TruthBulletShot : MonoBehaviour
{
    //照準を動かす
    //ボタンを押すと発射
    //弾が始点から終点に移動するまで照準が消えて移動不可
    //

    //照準
    [SerializeField] private Transform aimImagePos;

    [SerializeField] private Transform shotPointCurrent;

    // 線形補間の始点
    private Transform shotPointFrom;

    // 線形補間の終点
    private Vector3 shotPointTo = new Vector3();

    // 移動時間[s]
    //[SerializeField] private float _duration = 10;

    bool reloading = false;

    private bool MoveEnd = false;



    void Start()
    {
        
        shotPointFrom = shotPointCurrent;
        shotPointCurrent.GetComponent<BulletCol>().SetBulletScript(this.gameObject.GetComponent<TruthBulletShot>());
        Debug.Log(shotPointFrom.position);
    }

    void Update()
    {
        //照準を動かす
        //マウスの位置と照準器の位置を同期させる。
        aimImagePos.position = Input.mousePosition;

        RaycastHit hit;

        //MainCameraからマウスの位置にRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            //Debug.Log("レイが当たった");
            //ボタンを押すと発射、発射後数秒間発射不可
            if (Input.GetKeyUp(KeyCode.Space) && !reloading)
            {
                shotPointTo = hit.point;
                Debug.Log("レイが当たった:"+ shotPointTo);
                shotPointCurrent.position = new Vector3(Camera.main.transform.position.x + 1, Camera.main.transform.position.y, Camera.main.transform.position.z);
                StartCoroutine(BulletMove());
            }
        }
        
    }

    private IEnumerator BulletMove()
    {
        //弾が始点から終点に移動するまで照準が消えて移動不可

        //リロード開始
        StartCoroutine(Reload());

        Vector3 targetPosition = shotPointTo; // 目的の位置の座標を指定
        Vector3 startPosition = shotPointCurrent.position; // ゲームオブジェクトのTransformコンポーネントを取得
        float duration = 0.1f; // 着弾までの時間、単位は秒
        float time = 0.0f;  // 発射からの経過時間

        // 弾の移動処理
        while(!MoveEnd && time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            
            // 目的の位置までduration秒をかけて補間で移動
            shotPointCurrent.position = Vector3.Lerp(startPosition, targetPosition, t); // 目的の位置に移動
            yield return null;
        }

        //BulletMoveEnd();
        BulletDelete();


        yield return null;
    }
    //元の位置に戻し画面内から弾を消す
    public void BulletDelete()
    {
        shotPointCurrent = shotPointFrom;
    }

    public void BulletTextTouch()
    {
        MoveEnd = !MoveEnd;
        
        Debug.Log("文字接触");
    }
    public void BulletMoveEnd()
    {
        MoveEnd = !MoveEnd;
        
        Debug.Log("文字に接触しなかった");
    }
    IEnumerator Reload()
    {
        reloading = true; //リロード判定をtrueにする
        yield return new WaitForSeconds(3); //3秒待機
        Debug.Log("リロード終了");
        reloading = false; //リロード判定をfalseにする
    }

}
