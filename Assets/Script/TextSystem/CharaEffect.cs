using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharaEffect : MonoBehaviour
{
    //��b���ɃL�����ɔ��������鉉�o��
    //UnityEvent�͈�����1�����ݒ�ł��Ȃ����ߒ���

    private GameManager gameManager;

    [SerializeField, Header("�C�x���gCG�摜")] private Image eventcgImage;
    [SerializeField, Header("�t�F�[�h�p�̃C�x���gCG�̗���")] private Image eventcgBack;

    Color fadeClearColor = new Color(255, 255, 255, 0);
    Color fadeColor = Color.white;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    //�����͕\���ω�������L�����A�I�u�W�F�N�g���L�������ǂ�������
    //�X�v���C�g��ʂ̕\��摜�ɕω�
    public void ChangeFace_Normal(GameObject chara)
    {
        //CharaFaceChange���A�^�b�`����Ă��邩�m�F
        if(chara != null && chara.GetComponent<CharaFaceChange>() != null)
        {
            Debug.Log("CharaEffect��CharaFaceChange���m�F");
            chara.GetComponent<CharaFaceChange>().ChangeFaceNormal();
        }
    }
    public void ChangeFace_Talk(GameObject chara)
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
    //�C�x���gCG���t�F�[�h���ĕ\��
    public void EventImageDisplayFade(Sprite sprite)
    {
        Color spriteColor;
        float duration = 0.5f;

        eventcgBack.enabled = true;

        eventcgImage.sprite = sprite;
        
        eventcgImage.color = fadeClearColor;
        eventcgImage.enabled = true;

        spriteColor = eventcgImage.color;
        //eventcgImage.sprite = BlackImage;
        StartCoroutine(Fade(1, spriteColor, duration, false));

    }
    //�C�x���gCG���\��
    public void EventImageDelete()
    {
        eventcgImage.sprite = null;
        eventcgImage.enabled = false;
    }
    //�C�x���gCG���t�F�[�h���Ĕ�\��
    public void EventImageDeleteFade()
    {
        Color spriteColor;
        float duration = 0.3f;

        eventcgImage.color = fadeColor;
        
        spriteColor = eventcgImage.color;
        StartCoroutine(Fade(0, spriteColor, duration, true));
        //eventcgImage.sprite = null;
        //eventcgImage.enabled = false;
    }
    //���ȏЉ�C�x���g��\��
    public void SelfIntoroDisplay(Sprite sprite)
    {
        GameManager.isTalkPause = true;
        eventcgImage.sprite = sprite;
        eventcgImage.enabled = true;

        //����s�\�ɂ���
        gameManager.playerController = GameManager.PlayerController.EventScene;

        //��莞�ԑҋ@
        StartCoroutine(EventCGDisplay());
        
        

        //����\
        //gameManager.playerController = GameManager.PlayerController.TextWindowMode;
    }

    private IEnumerator EventCGDisplay()
    {
        //�C�x���g���ŕ�������ł��Ȃ�
        GameManager.isTalkEvent = false;

        //����s�\�ɂ���
        //gameManager.playerController = GameManager.PlayerController.EventScene;

        //�w�莞�ԑҋ@
        yield return new WaitForSeconds(3f);

        //�C�x���g�I���ŕ�������ł���悤�ɂȂ�
        GameManager.isTalkEvent = true;

        //���Ԃŉ摜����
        eventcgImage.sprite = null;
        eventcgImage.enabled = false;

        GameManager.isTalkPause = false;

        //����\
        gameManager.playerController = GameManager.PlayerController.TextWindowMode;
    }

    //SE�𔭐�������



    IEnumerator Fade(float targetAlpha, Color spriteColor, float duration, bool isFadeOut)
    {
        while (!Mathf.Approximately(spriteColor.a, targetAlpha))
        {
            float changePerFrame = Time.deltaTime / duration;
            spriteColor.a = Mathf.MoveTowards(spriteColor.a, targetAlpha, changePerFrame);
            eventcgImage.color = spriteColor;
            yield return null;
        }
        //�摜���t�F�[�h�A�E�g���鎞
        if (isFadeOut)
        {
            eventcgImage.enabled = false;
            eventcgImage.sprite = null;
            eventcgImage.color = fadeColor;
        }
        else
        {
            eventcgBack.enabled = false;
            //eventcgImage.color = fadeColor;
        }
       
    }

    //�I�u�W�F�N�g�̏�Ԃ̐؂�ւ��@���v�̐����Ȃ�
    public void ObjSwitch(ObjModeSwitch objModeSwitch)
    {
        //�؂�ւ���ɕω�
        objModeSwitch.DisplayObjSwitch();
    }
    /*
    //�I�u�W�F�N�g�̏�Ԃ̐؂�ւ��@����̑�
    public void WindowObjSwitch(ObjModeSwitch objModeSwitch)
    {
        //�؂�ւ���ɕω�
        objModeSwitch.DisplayObjSwitch();
    }*/

    //�G���f�B���O�V�[���ɑJ��
    public void CallEndingScene()
    {
        SceneManager.LoadScene(gameManager.EndingScene);
    }

}
