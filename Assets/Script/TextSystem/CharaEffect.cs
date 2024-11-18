using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharaEffect : MonoBehaviour
{
    //��b���ɃL�����ɔ��������鉉�o��
    //UnityEvent�͈�����1�����ݒ�ł��Ȃ����ߒ���

    [SerializeField] private Image eventcgImage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //�����͕\���ω�������L�����A�I�u�W�F�N�g���L�������ǂ�������
    //�X�v���C�g��ʂ̕\��摜�ɕω�
    public void FaceChange_Normal(GameObject chara)
    {
        //CharaFaceChange���A�^�b�`����Ă��邩�m�F
        if(chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            Debug.Log("CharaEffect��CharaFaceChange���m�F");
            chara.GetComponent<CharaFaceChange>().ChangeFaceNormal();
        }
    }
    public void FaceChange_Talk(GameObject chara)
    {
        //CharaFaceChange���A�^�b�`����Ă��邩�m�F
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceTalk();
        }
    }
    public void ChangeFace_Think(GameObject chara)
    {
        //CharaFaceChange���A�^�b�`����Ă��邩�m�F
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceThink();
        }
    }
    public void ChangeFace_Joy(GameObject chara)
    {
        //CharaFaceChange���A�^�b�`����Ă��邩�m�F
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceJoy();
        }
    }
    public void ChangeFace_Anger(GameObject chara)
    {
        //CharaFaceChange���A�^�b�`����Ă��邩�m�F
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceAnger();
        }
    }
    public void ChangeFace_Surprise(GameObject chara)
    {
        //CharaFaceChange���A�^�b�`����Ă��邩�m�F
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceSurprise();
        }
    }
    public void ChangeFace_Frightened(GameObject chara)
    {
        //CharaFaceChange���A�^�b�`����Ă��邩�m�F
        if (chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            chara.GetComponent<CharaFaceChange>().ChangeFaceFrightened();
        }
    }

    //�C�x���gCG��\��
    public void EventImageDisplay(Sprite sprite)
    {
        eventcgImage.sprite = sprite;
        eventcgImage.enabled = true;
    }
    //�C�x���gCG���\��
    public void EventImageDelete() { eventcgImage.enabled = false; }

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
