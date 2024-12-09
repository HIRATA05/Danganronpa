using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class GameStartEvent : MonoBehaviour
{
    //�Q�[���J�n���̉�b����

    [SerializeField, Header("�ŏ��̉�b�e�L�X�g")] private DialogueText StartText;

    private GameManager gameManager;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();

        //Debug.Log("GameStart_ClassRoom");
        //gameManager.OpenTextWindow(StartText);
        if (gameManager.eventFlagData.GameStart)
        {
            Debug.Log("GameStart_ClassRoom");
            gameManager.eventFlagData.GameStart = false;
            gameManager.OpenTextWindow(StartText);
        }
    }

    void Update()
    {
        
    }
}
