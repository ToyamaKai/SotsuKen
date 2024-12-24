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
    }
}
