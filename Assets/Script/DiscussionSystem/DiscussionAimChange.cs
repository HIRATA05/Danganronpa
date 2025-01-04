using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiscussionAimChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //議論中の照準を発言に接触した時に変化させる

    float distance = 100;
    float duration = 3;
    //照準の画像
    [SerializeField] private Image aimImage;
    [SerializeField] private Sprite aimImageNormal;//通常
    [SerializeField] private Sprite aimImageTextCol;//発言と重なる
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        aimImage.sprite = aimImageTextCol;
        Debug.Log("発言にマウスが重なる");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        aimImage.sprite = aimImageNormal;
        Debug.Log("発言にマウスが離れる");
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
        Debug.Log("発言にマウスが重なる AimChange");
    }

    public void Test()
    {

        Debug.Log("テストイベント");
    }

    void Start()
    {

    }

    void Update()
    {
        /*
        RaycastHit hit;

        //MainCameraからマウスの位置にRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);

        if (Physics.Raycast(ray, out hit))
        {

            //当たった物が発言の時照準の画像を変化
            //タグ
            if (hit.collider.CompareTag("Speech"))
            {
                aimImage.sprite = aimImageTextCol;
                Debug.Log("テストイベント");
            }
            else
            {
                aimImage.sprite = aimImageNormal;
            }

        }
        */
    }
}