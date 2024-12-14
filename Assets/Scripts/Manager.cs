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
    PostEffect m_postEffect;

    static public int m_areaNumber;

    private void Start()
    {
        m_whiteDust.Stop();
        m_postEffect.enabled = false;
        Area4Effect();
    }

    // Update is called once per frame
    void Update()
    {
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
        if(m_areaNumber == 4)
        {
            Area4Effect();
        }
    }

    public void Area4Effect()
    {
        if(!m_whiteDust.isPlaying)
        {
            m_whiteDust.Play();
        }
        m_animator.SetBool("isCold", true);
        m_postEffect.enabled = true;
    }
}
