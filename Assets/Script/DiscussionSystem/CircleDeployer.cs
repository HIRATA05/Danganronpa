using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子にあるオブジェクトを円状に配置するクラス
/// </summary>
public class CircleDeployer : MonoBehaviour
{
    //円形の並びを作るスクリプト

    //半径
    [SerializeField, Header("この値で円形範囲の半径を調整")]
    private float _radius;


    private void Awake()
    {
        Deploy();
    }

    //Inspectorの内容(半径)が変更された時に実行
    private void OnValidate()
    {
        Deploy();
    }

    //子を円状に配置する(ContextMenuで鍵マークの所にメニュー追加)
    [ContextMenu("Deploy")]
    public void Deploy()
    {

        //子を取得
        List<GameObject> childList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            childList.Add(child.gameObject);
        }

        //数値、アルファベット順にソート
        childList.Sort(
          (a, b) => {
              return string.Compare(a.name, b.name);
          }
        );

        //オブジェクト間の角度差
        float angleDiff = 360f / (float)childList.Count;

        //各オブジェクトを円状に配置
        for (int i = 0; i < childList.Count; i++)
        {
            Vector3 childPostion = transform.position;

            float angle = (90 - angleDiff * i) * Mathf.Deg2Rad;
            childPostion.x += _radius * Mathf.Cos(angle);
            childPostion.z += _radius * Mathf.Sin(angle);

            childList[i].transform.position = childPostion;
            /*
            //向きを中央に
            var dir = transform.position - childList[i].transform.position;
            childList[i].transform.LookAt(dir);
            */

            childList[i].transform.rotation = Quaternion.LookRotation(transform.position);

        }

    }

}
