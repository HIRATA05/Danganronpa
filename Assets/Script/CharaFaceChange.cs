using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaFaceChange : MonoBehaviour
{
    //�L�����ɃA�^�b�`����
    //�L�����̕\���ω�������@�C�}�[�W�̃X�v���C�g�������ւ���

    //�L�����̕\��̐��񋓌^�𑝂₷
    enum Face
    {
        Normal = 0,
        
    }

    [Header("0�ʏ�/1")]
    [SerializeField] Sprite[] faceImage;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void FaceChangeNormal()
    {
        Debug.Log("�\���ʏ�ɕω�");
        gameObject.GetComponent<Image>().sprite = faceImage[(int)Face.Normal];
    }
}
