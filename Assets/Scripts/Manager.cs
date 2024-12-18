using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    GameObject m_setting;
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

    static public int m_areaNumber;

    private void Start()
    {
        m_whiteDust.Stop();
        m_CharaEffect.enabled = false;
        m_FXseries.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_areaNumber);
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(m_setting.activeSelf == false)
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

        if(m_FXseries.activeSelf)
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
    }

    //エリア1は環境系エフェクト(吹雪・降雪・画面全体のラスタースクロール)
    public void Area1Effect()
    {
        m_FXseries.SetActive(true);

        m_postEffect.enabled = true;
    }

    //エリア2はキャラ関係のエフェクト(モーション・テクスチャ・キャララスタースクロール)
    public void Area2Effect()
    {
        if (m_animator.GetBool("isCold"))
        {
            m_animator.SetBool("isCold", true);
        }

        m_CharaEffect.enabled = true;
        m_CharaEffect2.enabled = true;

        //テクスチャ変化ってどうしよう
    }

    //エリア3は要検討
    public void Area3Effect()
    {

    }

    //エリア4は全盛り朝食バイキングくらい
    public void Area4Effect()
    {
        if (!m_whiteDust.isPlaying)
        {
            m_whiteDust.Play();
        }
        m_animator.SetBool("isCold", true);
        m_FXseries.SetActive(true);
        m_CharaEffect.enabled = true;
    }
}
