using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endingroll : MonoBehaviour
{
    Vector3 Staffrollposition;
    public RectTransform rectTransform;
    public float Endpos;
    public GameObject ThanksText;

    float QuitTime = 10.0f;
    float time = 0.0f;

    void Start()
    {
        Staffrollposition = rectTransform.anchoredPosition;

    }

    void Update()
    {

        if (rectTransform.anchoredPosition.y < Endpos)
        {

            Staffrollposition.y += 0.5f;
            rectTransform.anchoredPosition = Staffrollposition;
        }
        else
        {
            if(!ThanksText.activeSelf)
                ThanksText.SetActive(true);

            time += Time.deltaTime;
            if(QuitTime < time)
            {
                Debug.Log("終了");
                Application.Quit();//ゲームプレイ終了
            }
        }

    }
}
