using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharaEffect : MonoBehaviour
{
    //��b���ɃL�����ɔ��������鉉�o


    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //�����̓Q�[���I�u�W�F�N�g�ɂ��āA�I�u�W�F�N�g���L�������ǂ�������
    //�X�v���C�g��ʂ̕\��摜�ɕω�
    public void FaceChangeNormal(GameObject chara)
    {
        //CharaFaceChange���A�^�b�`����Ă��邩�m�F
        if(chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            Debug.Log("CharaEffect��CharaFaceChange���m�F");
            chara.GetComponent<CharaFaceChange>().FaceChangeNormal();
        }
    }

    //��ۓI�Ȕ����̎��ɔ������锒���t�F�[�h
    //SE�𔭐�������u�摜�𔒂����Č��ɖ߂�
    public void ImageFedeWhite(Image charaImage)
    {
        //�F���擾
        Color color = charaImage.color;

        //SE����


        //�摜�̃t�F�[�h����

    }

    //����SE�𔭐�������


    public void OnClickEvent()
    {
        Debug.Log("Click");
    }

}
