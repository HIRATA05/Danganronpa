using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWindow : MonoBehaviour
{
    //�e�L�X�g�E�B���h�E

    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        text.text = "Test_Text";
    }

    void Update()
    {
        
    }
}
