using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    Slider MouseSensitivitySlider;
    [SerializeField]
    InputField inputField;
    [SerializeField]
    Text text;
    [SerializeField]
    Text sayuutext;
    [SerializeField]
    Button button;

    static public float    MouseSensitivity = 2;
    static public int       MouseJougeInversion = 1;
    static public int       MouseSayuuInversion = 1;

    private void Start()
    {
        MouseSensitivitySlider.maxValue = 30;
        MouseSensitivitySlider.value    = 2;
        inputField.text = MouseSensitivitySlider.value.ToString("F0");
    }

    private void Update()
    {
    }

    public void MouseSensitivityChange()
    {
        MouseSensitivity = MouseSensitivitySlider.value;
        inputField.text = MouseSensitivitySlider.value.ToString("F0");
    }

    public void InputMouseSensitivityChange()
    {
        MouseSensitivitySlider.value = float.Parse(inputField.text);
        inputField.text = MouseSensitivitySlider.value.ToString("F0");
    }

    public void MouseJougeInvers()
    {
        if(MouseJougeInversion == 1)
        {
            text.text = "オン";
            MouseJougeInversion = -1;
        }
        else
        {
            text.text = "オフ";
            MouseJougeInversion = 1;
        }
    }

    public void MouseSayuuInvers()
    {
        if (MouseSayuuInversion == 1)
        {
            sayuutext.text = "オン";
            MouseSayuuInversion = -1;
        }
        else
        {
            sayuutext.text = "オフ";
            MouseSayuuInversion = 1;
        }
    }

    public void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
