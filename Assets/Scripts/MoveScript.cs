using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_rigidBody;

    [SerializeField]
    private Transform m_transform;

    [SerializeField]
    float c_playerMoveSpeed = 1.0f;

    private Vector3 m_movingDirection;
    
    private Vector3 m_movingVelocity;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void FixedUpdate()
    {
        m_rigidBody.velocity = new Vector3(m_movingVelocity.x, m_rigidBody.velocity.y, m_movingVelocity.z);
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        m_movingDirection = new Vector3(x, 0, z);
        m_movingDirection.Normalize();
        m_movingVelocity = m_movingDirection * c_playerMoveSpeed;
    }
}
