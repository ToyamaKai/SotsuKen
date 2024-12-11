using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    int AreaNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "unitychan")
        {
            Manager.m_areaNumber = AreaNumber;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "unitychan")
        {
            Manager.m_areaNumber = 0;
        }
    }
}
