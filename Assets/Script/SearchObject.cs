using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchObject : MonoBehaviour , IReceiveSearch
{
    //調べた時のテキスト情報をテキスト管理スクリプトに渡す

    public void ReceiveSearch(float damage)
    {
        Debug.Log("Enemy は " + damage + "ダメージ食らった");
        //渡す

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
