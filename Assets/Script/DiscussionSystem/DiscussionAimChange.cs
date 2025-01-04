using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiscussionAimChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //�c�_���̏Ə��𔭌��ɐڐG�������ɕω�������

    float distance = 100;
    float duration = 3;
    //�Ə��̉摜
    [SerializeField] private Image aimImage;
    [SerializeField] private Sprite aimImageNormal;//�ʏ�
    [SerializeField] private Sprite aimImageTextCol;//�����Əd�Ȃ�
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        aimImage.sprite = aimImageTextCol;
        Debug.Log("�����Ƀ}�E�X���d�Ȃ�");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        aimImage.sprite = aimImageNormal;
        Debug.Log("�����Ƀ}�E�X�������");
    }

    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
    }

    public void AimChange()
    {
        aimImage.sprite = aimImageTextCol;
        Debug.Log("�����Ƀ}�E�X���d�Ȃ� AimChange");
    }

    public void Test()
    {

        Debug.Log("�e�X�g�C�x���g");
    }

    void Start()
    {

    }

    void Update()
    {
        /*
        RaycastHit hit;

        //MainCamera����}�E�X�̈ʒu��Ray���΂�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);

        if (Physics.Raycast(ray, out hit))
        {

            //�����������������̎��Ə��̉摜��ω�
            //�^�O
            if (hit.collider.CompareTag("Speech"))
            {
                aimImage.sprite = aimImageTextCol;
                Debug.Log("�e�X�g�C�x���g");
            }
            else
            {
                aimImage.sprite = aimImageNormal;
            }

        }
        */
    }
}