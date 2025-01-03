using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    GameObject m_setting;
    [SerializeField]
    GameObject m_operate;
    [SerializeField]
    Animator m_animator;
    [SerializeField]
    Animator m_animator2;
    [SerializeField]
    ParticleSystem m_whiteDust;
    [SerializeField]
    PostEffect m_CharaEffect;
    [SerializeField]
    PostEffect m_CharaEffect2;
    [SerializeField]
    PostEffect m_postEffect;
    [SerializeField]
    GameObject m_FXseries;
    [SerializeField]
    AudioMixer m_audio;

    [SerializeField]
    Renderer m_body;
    [SerializeField]
    Texture m_chilblainsBody;
    [SerializeField]
    Texture m_chilblainsBody2;
    [SerializeField]
    Texture m_bodytex;

    [SerializeField]
    Renderer m_face;
    [SerializeField]
    Renderer m_eyes;
    [SerializeField]
    Texture m_chilblainsface;
    [SerializeField]
    Texture m_chilblainsface2;
    [SerializeField]
    Texture m_facetex;

    [SerializeField]
    ParticleSystem m_whiteBless;

    [SerializeField]
    private Material material1;
    [SerializeField]
    private Material material2;
    [SerializeField]
    private Material material3;

    [SerializeField]
    private Material material4;

    [SerializeField]
    Text areaNum;

    static Material Material2;
    static Material Material3;
    static Material Material1;
    static Material Material4;

    static Shader standardShader;
    static Shader toonShader;

    [SerializeField]
    GameObject[] Canvas;
    [SerializeField]
    AnimationClip[] faceAnim;

    static public int m_areaNumber;

    float timer = 0f;

    private void Awake()
    {
        standardShader = Shader.Find("Standard");
        toonShader = Shader.Find("Toon");
        Material1 = material1;
        Material2 = material2;
        Material3 = material3;
        Material4 = material4;
        Material1.shader = toonShader;
        Material2.shader = toonShader;
        Material3.shader = toonShader;
        Material4.shader = toonShader;
    }

    private void Start()
    {
        m_whiteDust.Stop();
        m_CharaEffect.enabled = false;
        m_CharaEffect2.enabled = false;
        m_FXseries.SetActive(false);
        m_operate.SetActive(true);
        m_setting.SetActive(false);
        m_animator.SetLayerWeight(0, 1f);
        m_animator2.SetLayerWeight(0,1f);
    }

    // Update is called once per frame
    void Update()
    {
        m_audio.SetFloat("Master", Mathf.Clamp(Mathf.Log10(Mathf.Clamp(Settings.soundVolume / 100, 0f, 1f)) * 20f, -80f, 0f));
        if (AreAllInactive())
        {
            mouse(true);
        }
        else
        {
            mouse(false);
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!m_setting.activeSelf && !m_operate.activeSelf)
            {
                m_setting.SetActive(true);
            }
            else
            {
                m_setting.SetActive(false);
            }
        }

        //エリアごとにエフェクトを変える(0は真ん中あたり)
        if(m_areaNumber == 0)
        {
            EffectsOff();
        }
        else if(m_areaNumber == 1)
        {
            Area1Effect();
        }
        else if(m_areaNumber == 2)
        {
            Area2Effect();
        }
        else if(m_areaNumber == 3)
        {
            Area3Effect();
        }
        else
        {
            Area4Effect();
        }

        areaNum.text = "エリア番号" + m_areaNumber;
    }

    public void EffectsOff()
    {
        if(m_whiteDust.isPlaying)
        {
            m_whiteDust.Stop();
            m_whiteDust.Clear();
        }

        if (m_animator.GetBool("isCold"))
        {
            m_animator.SetBool("isCold", false);
            m_animator2.SetBool("isCold", false);
        }

        if (m_FXseries.activeSelf)
        {
            m_FXseries.SetActive(false);
        }

        if(m_CharaEffect.isActiveAndEnabled)
        {
            m_CharaEffect.enabled = false;
            m_CharaEffect2.enabled = false;
        }

        if (m_CharaEffect.isActiveAndEnabled)
        {
            m_postEffect.enabled = false;
        }

        AnimatorFPSController._fps = 60;

        SwitchTexture(true, 0);
    }

    //エリア1は環境系エフェクト(吹雪・降雪)
    public void Area1Effect()
    {
        m_FXseries.SetActive(true);
        AnimatorFPSController._fps = 60;
    }

    //エリア2はキャラ関係のエフェクト(モーション・テクスチャ・キャララスタースクロール)
    //寒がるモーション、しもやけテクスチャ(赤)、ラスタースクロール、モーションキーフレーム落とし、ローポリ化、ホワイトダスト
    public void Area2Effect()
    {
        if (!m_whiteDust.isPlaying)
        {
            m_whiteDust.Play();
        }

        m_animator.SetBool("isCold", true);
        m_animator2.SetBool("isCold", true);

        m_CharaEffect.enabled = true;
        m_CharaEffect2.enabled = true;

        SwitchTexture(false, 1);
        hoge();
        AnimatorFPSController._fps = 15;
    }

    //エリア3はエリア2の亜種(モーション・テクスチャ・キャララスタースクロール)
    //寒がるモーション、しもやけテクスチャ(紫)、ラスタースクロール、モーションキーフレーム落とし、ホワイトダスト
    public void Area3Effect()
    {
        if (!m_whiteDust.isPlaying)
        {
            m_whiteDust.Play();
        }

        m_animator.SetBool("isCold", true);
        m_animator2.SetBool("isCold", true);

        m_CharaEffect.enabled = false;
        m_CharaEffect2.enabled = false;

        SwitchTexture(false, 0);
        hoge();
        AnimatorFPSController._fps = 15;
    }

    //エリア4は全盛り
    //降雪・吹雪、寒がるモーション、しもやけテクスチャ(紫)、ラスタースクロール、モーションキーフレーム落とし、ローポリ化、ホワイトダスト
    public void Area4Effect()
    {
        if (!m_whiteDust.isPlaying)
        {
            m_whiteDust.Play();
        }

        m_animator.SetBool("isCold", true);
        m_animator2.SetBool("isCold", true);
        m_FXseries.SetActive(true);
        m_CharaEffect.enabled = true;
        m_CharaEffect2.enabled = false; 
        SwitchTexture(false, 0);
        hoge();
        AnimatorFPSController._fps = 15;
    }

    private void SwitchTexture(bool isTexture2, int hoge)
    {
        if (!isTexture2 && hoge == 0)
        {
            m_body.material.mainTexture = m_chilblainsBody;
            m_face.material.mainTexture = m_chilblainsface;
            m_eyes.material.mainTexture = m_chilblainsface;
        }
        else if (!isTexture2 && hoge == 1)
        {
            m_body.material.mainTexture = m_chilblainsBody2;
            m_face.material.mainTexture = m_chilblainsface2;
            m_eyes.material.mainTexture = m_chilblainsface2;
        }
        else
        {
            m_body.material.mainTexture = m_bodytex;
            m_face.material.mainTexture = m_facetex;
            m_eyes.material.mainTexture = m_facetex;
        }
    }

    public void CloseOperate()
    {
        m_operate.SetActive(false);
    }

    private void hoge()
    {
        timer += Time.deltaTime;
        if(timer >= 5.0f)
        {
            if(m_whiteBless != null)
            {
                m_whiteBless.Play();
            }
            timer = 0f;
        }
    }

    static public void ChangeMaterial(int num)
    {
        if(num == 3 || num == 4 )
        {
            Material1.shader = standardShader;
            Material2.shader = standardShader;
            Material3.shader = standardShader;
            Material4.shader = standardShader;
        }
        else
        {
            Material1.shader = toonShader;
            Material2.shader = toonShader;
            Material3.shader = toonShader;
            Material4.shader = toonShader;
        }
    }

    private void mouse(bool isLock)
    {
        if (isLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    // 全てのGameObjectが非アクティブかをチェック
    private bool AreAllInactive()
    {
        foreach (var obj in Canvas)
        {
            if (obj.activeSelf) // アクティブなものが1つでもある場合
            {
                return false;
            }
        }
        return true; // 全て非アクティブの場合
    }
}