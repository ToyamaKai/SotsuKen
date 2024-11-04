using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBless : MonoBehaviour
{
    //パーティクルのアタッチを行う
    [SerializeField]
    private ParticleSystem m_whiteBless;
    
    private void Update()
    {
        //マウスの左クリック時にパーティクルの発生を行う
        if(Input.GetMouseButtonDown(0))
        {
            m_whiteBless.Play();
        }
    }
}
