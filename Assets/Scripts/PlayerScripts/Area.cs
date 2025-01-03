using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    GameObject Player1;
    [SerializeField]
    GameObject Player2;
    [SerializeField]
    PageUI m_PageUI;

    [SerializeField]
    int AreaNumber;

    private void Awake()
    {
        Player1.SetActive(true);
        Player2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "unitychan")
        {
            Manager.m_areaNumber = AreaNumber;
            Manager.ChangeMaterial(AreaNumber);
            if(AreaNumber == 3 || AreaNumber == 4)
            {
                Player1.SetActive(false);
                Player2.SetActive(true);
            }
        }
        if(AreaNumber == 1)
        {
            m_PageUI.SetAreaPages(new List<int> { 7 });
        }
        else if(AreaNumber == 2)
        {
            m_PageUI.SetAreaPages(new List<int> { 0, 1, 3, 4, 6 });
        }
        else if(AreaNumber == 3)
        {
            m_PageUI.SetAreaPages(new List<int> { 0, 2, 4, 5, 6 });
        }
        else if(AreaNumber == 4)
        {
            m_PageUI.SetAreaPages(new List<int> { 0, 2, 3, 4, 5, 6, 7 });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "unitychan")
        {
            Manager.m_areaNumber = 0;
            Manager.ChangeMaterial(0);
            if (AreaNumber == 3 || AreaNumber == 4)
            {
                Player2.SetActive(false);
                Player1.SetActive(true);
            }
        }

        m_PageUI.SetAreaPages(new List<int> { 8 });
    }
}
