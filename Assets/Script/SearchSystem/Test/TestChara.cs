using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChara : MonoBehaviour, IReceiveSearch
{

    //���ׂ����̃e�L�X�g�����e�L�X�g�Ǘ��X�N���v�g�ɓn��

    GameManager gameManager;
    TextWindow textWindow;

    //TextInfomation TextInfomation;

    public void ReceiveSearch()
    {
        Debug.Log("Enemy �� 1�_���[�W�H�����");
        //�n��
        //textWindow.testtext = TextInfomation.text;
        //textWindow.xyz = TextInfomation.xyz;
        gameManager.playerController = GameManager.PlayerController.TextWindowMode;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        GameObject tw = GameObject.Find("TextWindow");
        textWindow = gm.GetComponent<TextWindow>();

        //TextInfomation.text = "53";
        //TextInfomation.xyz = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
