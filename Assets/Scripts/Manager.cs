using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    GameObject m_setting;

    static public int m_areaNumber;
    // Start is called before the first frame update
    void Start()
    {
        m_setting.SetActive(false);
        m_areaNumber = 0;
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
    }
}
