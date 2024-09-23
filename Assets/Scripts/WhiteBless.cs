using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBless : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_whiteBless;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            m_whiteBless.Play();
        }
    }
}
