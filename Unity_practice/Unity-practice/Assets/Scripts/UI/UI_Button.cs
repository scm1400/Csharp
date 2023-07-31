using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Button : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private int _score = 0;
    
    public void OnButtonClicked()
    {
        Debug.Log("ButtonClicked");
        _score++;
        _textMeshPro.text = "점수 : " + _score.ToString();
    }
}
