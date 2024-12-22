using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    GameObject m_setting;
    [SerializeField]
    GameObject m_operate;
    [SerializeField]
    Animator m_animator;
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
    Material Material1;
    [SerializeField]
    Material Material2;
    [SerializeField]
    Material Material3;
    [SerializeField]
    Material Material4;

    Shader standardShader;
    Shader toonShader;

    [SerializeField]
    AnimationClip[] faceAnim;

    static public int m_areaNumber;

    float timer = 0f;

    private void Start()
    {
        standardShader = Shader.Find("Standard");
        toonShader = Shader.Find("Toon");
        m_whiteDust.Stop();
        m_CharaEffect.enabled = false;
        m_FXseries.SetActive(false);
        m_operate.SetActive(true);
        m_setting.SetActive(false);
        m_animator.SetLayerWeight(0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
        }

        if (m_FXseries.activeSelf)
        {
            m_FXseries.SetActive(false);
        }

        if(m_CharaEffect.isActiveAndEnabled)
        {
            m_CharaEffect.enabled = false;
        }

        if (m_CharaEffect.isActiveAndEnabled)
        {
            m_postEffect.enabled = false;
        }


        SwitchTexture(true);

        ChangeMaterial();
    }

    //エリア1は環境系エフェクト(吹雪・降雪・画面全体のラスタースクロール)
    public void Area1Effect()
    {
        m_FXseries.SetActive(true);

        m_postEffect.enabled = true;
        ChangeMaterial();
    }

    //エリア2はキャラ関係のエフェクト(モーション・テクスチャ・キャララスタースクロール)
    //寒がるモーション、しもやけテクスチャ(赤)、ラスタースクロール、モーションキーフレーム落とし、ローポリ化
    public void Area2Effect()
    {
        m_animator.SetBool("isCold", true);

        m_CharaEffect.enabled = true;
        m_CharaEffect2.enabled = true;

        SwitchTexture(false);
        hoge();
        ChangeMaterial();
    }

    //エリア3はエリア2の亜種(モーション・テクスチャ・キャララスタースクロール)
    //寒がるモーション、しもやけテクスチャ(紫)、ラスタースクロール、モーションキーフレーム落とし、ローポリ化
    public void Area3Effect()
    {
        m_animator.SetBool("isCold", true);

        m_CharaEffect.enabled = true;
        m_CharaEffect2.enabled = true;

        SwitchTexture(false);
        hoge();
        ChangeMaterial();
    }

    //エリア4は全盛り
    public void Area4Effect()
    {
        if (!m_whiteDust.isPlaying)
        {
            m_whiteDust.Play();
        }
        m_animator.SetBool("isCold", true);
        //m_animator.Play(faceAnim[0].name);
        m_FXseries.SetActive(true);
        m_CharaEffect.enabled = true;
        SwitchTexture(false);
        hoge();
        ChangeMaterial();
    }

    private void SwitchTexture(bool isTexture2)
    {
        if(!isTexture2)
        {
            m_body.material.mainTexture = m_chilblainsBody;
            m_face.material.mainTexture = m_chilblainsface;
            m_eyes.material.mainTexture = m_chilblainsface;
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

    private void ChangeMaterial()
    {
        if(Material1.shader != standardShader)
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
}
