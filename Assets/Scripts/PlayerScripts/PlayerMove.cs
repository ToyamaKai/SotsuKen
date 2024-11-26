using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float m_speed = 5f;

    [SerializeField]
    Rigidbody m_rb;

    [SerializeField]
    Animator m_animator;

    void Awake()
    {
    }

    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
            m_animator.SetBool("isWalking", true);
            m_rb.velocity = new Vector3(v, 0, 0).normalized * m_speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_animator.SetBool("isWalking", true);
            m_rb.velocity = new Vector3(v, 0, 0).normalized * m_speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_animator.SetBool("isWalking", true);
            m_rb.velocity = new Vector3(0, 0, -h).normalized * m_speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_animator.SetBool("isWalking", true);
            m_rb.velocity = new Vector3(0, 0, -h).normalized * m_speed;
        }
        else
        {
            m_animator.SetBool("isWalking", false);
            m_rb.velocity = new Vector3(0, 0, 0).normalized * m_speed;
        }
    }
}