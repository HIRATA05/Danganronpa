using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruthBulletShot : MonoBehaviour
{
    //照準を動かす
    //ボタンを押すと発射
    //弾が始点から終点に移動するまで照準が消えて移動不可
    //

    //照準
    [SerializeField] private Transform aimImagePos;

    // 線形補間の始点
    [SerializeField] private Transform shotPointFrom;

    // 線形補間の終点
    [SerializeField] private Transform shotPointTo;

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

        }

        

        //ボタンを押すと発射
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(BulletMove());
        }

    }

    private IEnumerator BulletMove()
    {
        //弾が始点から終点に移動するまで照準が消えて移動不可
        
        

        // 始点・終点の位置取得
        var a = shotPointFrom.position;
        var b = shotPointTo.position;

        // 補間位置計算
        var t = Mathf.PingPong(Time.time / _duration, 1);

        // 補間位置を反映
        shotPointFrom.position = Vector3.Lerp(a, b, t);

        yield return new WaitUntil(() => MoveEnd == true);

        Debug.Log("バレット");
    }
    public void BulletMoveEnd()
    {
        MoveEnd = !MoveEnd;
    }
}
