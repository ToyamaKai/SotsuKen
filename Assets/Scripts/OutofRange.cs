using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutofRange : MonoBehaviour
{
    [SerializeField]
    GameObject m_player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_player.transform.position = new Vector3(23, 20, -107);
        }
    }
}
