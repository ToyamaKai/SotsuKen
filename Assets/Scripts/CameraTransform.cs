using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    [SerializeField]
    Transform m_playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(m_playerTransform.position.x - 2, m_playerTransform.position.y + 2f, m_playerTransform.position.z);
    }
}
