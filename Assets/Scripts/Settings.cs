using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField]
    Slider Sound;
    [SerializeField]
    Slider MouseSensitivitySlider;
    [SerializeField]
    InputField inputField;
    [SerializeField]
    InputField soundInputField;
    [SerializeField]
    Text text;
    [SerializeField]
    Text sayuutext;
    [SerializeField]
    Button button;
    [SerializeField]
    GameObject SettingUI;
    [SerializeField]
    AudioMixer m_audio;
    [SerializeField]
    GameObject hoge;

    static public float    MouseSensitivity = 2;
    static public float soundVolume = 50;
    static public int       MouseJougeInversion = 1;
    static public int       MouseSayuuInversion = 1;

    private void Start()
    {
        Sound.maxValue = 100;
        Sound.value = 50;
        soundInputField.text = Sound.value.ToString("F0");
        MouseSensitivitySlider.maxValue = 30;
        MouseSensitivitySlider.value    = 2;
        inputField.text = MouseSensitivitySlider.value.ToString("F0");
        m_audio.SetFloat("Master", 0);
    }

   private void Update()
    {
        m_audio.SetFloat("Master", Mathf.Clamp(Mathf.Log10(Mathf.Clamp(soundVolume / 100, 0f, 1f)) * 20f, -80f, 0f));
    }

    public void MouseSensitivityChange()
    {
        MouseSensitivity = MouseSensitivitySlider.value;
        inputField.text = MouseSensitivitySlider.value.ToString("F0");
    }

    public void SoundVolumChange()
    {
        soundVolume = Sound.value;
        soundInputField.text = Sound.value.ToString("F0");
    }

    public void InputMouseSensitivityChange()
    {
        MouseSensitivitySlider.value = float.Parse(inputField.text);
        inputField.text = MouseSensitivitySlider.value.ToString("F0");
    }
    public void InputSoundVolumChange()
    {
        Sound.value = float.Parse(soundInputField.text);
        soundInputField.text = Sound.value.ToString("F0");
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

    public void CloseSetting()
    {
        SettingUI.SetActive(false);
    }

    public void CheckEnshutu()
    {
        hoge.SetActive(true);
        SettingUI.SetActive(false);
    }
}
