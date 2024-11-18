using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaFaceChange : MonoBehaviour
{
    //�L�����ɃA�^�b�`����
    //�L�����̕\���ω�������@�C�}�[�W�̃X�v���C�g�������ւ���

    //�L�����̕\��̐��񋓌^�𑝂₷
    public enum Face
    {
        Normal = 0,
        Talk,
        Think,
        Joy,
        Anger,
        Surprise,
        Frightened,
        Rage,
        Excitement,
        Upset,
        Despair
    }

    [Header("0�ʏ�/1�b��/2�v��/3���/4�{��/5����/6����/7���{/8����/9�ł�/10��]")]
    [SerializeField] Sprite[] faceImage;

    //�����ŕ\�������L�����̃f�t�H���g�X�v���C�g
    [SerializeField] Sprite defaultFace;

    void Start()
    {

    }

    void Update()
    {
        
    }

    // �\���ς���
    public void ChangeFace(Face face)
    {
        gameObject.GetComponent<Image>().sprite = faceImage[(int)face];
    }

    public void ChangeFaceNormal() => ChangeFace(Face.Normal);

    public void ChangeFaceTalk() => ChangeFace(Face.Talk);

    public void ChangeFaceThink() => ChangeFace(Face.Think);

    public void ChangeFaceJoy() => ChangeFace(Face.Joy);

    public void ChangeFaceAnger() => ChangeFace(Face.Anger);

    public void ChangeFaceSurprise() => ChangeFace(Face.Surprise);

    public void ChangeFaceFrightened() => ChangeFace(Face.Frightened);

    public void ChangeFaceRage() => ChangeFace(Face.Rage);

    public void ChangeFaceExcitement() => ChangeFace(Face.Excitement);

    public void ChangeFaceUpset() => ChangeFace(Face.Upset);

    public void ChangeFaceDespair() => ChangeFace(Face.Despair);


    //�f�t�H���g�X�v���C�g��ω�
    public void DefaultFaceChange(Face face)
    {
        defaultFace = faceImage[(int)face];
    }

}
