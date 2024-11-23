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
    //private Transform shotPointFrom;

    // 線形補間の終点
    private Vector3 shotPointTo = new Vector3();

    // 移動時間[s]
    [SerializeField] private float _duration = 1;


    private bool MoveEnd = false;

    void Start()
    {
        
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

            if (hit.transform.CompareTag("SearchObj"))
            {
                Debug.Log("タグ");
            }
            //ボタンを押すと発射
            if (Input.GetKeyUp(KeyCode.Space))
            {
                shotPointTo = hit.point;
                Debug.Log("レイが当たった:"+ shotPointTo);
                shotPointCurrent.position = new Vector3(aimImagePos.position.x, aimImagePos.position.x, -1200.0f);
                StartCoroutine(BulletMove());
            }
        }


    }

    private IEnumerator BulletMove()
    {
        //弾が始点から終点に移動するまで照準が消えて移動不可
        
        

        // 始点・終点の位置取得
        var a = shotPointCurrent.position;
        var b = shotPointTo;

        // 補間位置計算
        var t = Mathf.PingPong(Time.time / _duration, 1);

        // 補間位置を反映
        shotPointCurrent.position = Vector3.Lerp(a, b, t);

        if(shotPointCurrent.position == shotPointTo)
        {
            BulletMoveEnd();
        }
        
        yield return new WaitUntil(() => MoveEnd == false);

        Debug.Log("バレット");
    }
    public void BulletMoveEnd()
    {
        MoveEnd = !MoveEnd;
    }
}
