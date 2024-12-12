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

    static public int m_areaNumber;

    private void Start()
    {

        m_whiteDust.Stop();
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

        if(m_areaNumber != 0 && m_areaNumber != 4)
        {
            m_animator.SetBool("isCold", true);
        }
        else
        {
            m_animator.SetBool("isCold", false);
        }
    }

    public void Area4Effect()
    {
        m_whiteDust.Stop();
        m_animator.SetBool("isCold", true);
    }
}
