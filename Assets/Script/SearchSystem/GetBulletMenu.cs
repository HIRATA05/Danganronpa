using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBulletMenu : MonoBehaviour
{
    //�Ə��T����ԂƋc�_���ɓ��肵���R�g�_�}���m�F�ł���
    //�}�E�X�J�[�\�������m���ē������Ă�����\����ς���
    //����̃{�^���ŉ�ʂ�߂�
    //�E���̃{�^���Ń^�C�g���ɖ߂�


    private GameManager gameManager;
    private EventFlagData eventFlagData;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.playerController == GameManager.PlayerController.ReticleMode)
        {

        }
    }
}
