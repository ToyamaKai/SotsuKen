using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    float m_speed = 5f;

    float m_dash;

    const float k_runSpeed = 1.5f;


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
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_animator.SetBool("isWalking", false);
                m_animator.SetBool("isRun", true);
                m_dash = k_runSpeed;
            }
            else
            {
                m_animator.SetBool("isWalking", true);
                m_animator.SetBool("isRun", false);
                m_dash = 1.0f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 45, 0);
                m_rb.velocity = new Vector3(v, 0, -h).normalized * m_speed * m_dash;
            }
            else if(Input.GetKey(KeyCode.D))
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 135, 0);
                m_rb.velocity = new Vector3(v, 0, -h).normalized * m_speed * m_dash;
            }
            else
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 90, 0);
                m_rb.velocity = new Vector3(v, 0, 0).normalized * m_speed * m_dash;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_animator.SetBool("isWalking", false);
                m_animator.SetBool("isRun", true);
                m_dash = k_runSpeed;
            }
            else
            {
                m_animator.SetBool("isWalking", true);
                m_animator.SetBool("isRun", false);
                m_dash = 1.0f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 315, 0);
                m_rb.velocity = new Vector3(v, 0, -h).normalized * m_speed * m_dash;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 225, 0);
                m_rb.velocity = new Vector3(v, 0, -h).normalized * m_speed * m_dash;
            }
            else
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 270, 0);
                m_rb.velocity = new Vector3(v, 0, 0).normalized * m_speed * m_dash;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_animator.SetBool("isWalking", false);
                m_animator.SetBool("isRun", true);
                m_dash = k_runSpeed;
            }
            else
            {
                m_animator.SetBool("isWalking", true);
                m_animator.SetBool("isRun", false);
                m_dash = 1.0f;
            }

            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 45, 0);
                m_rb.velocity = new Vector3(v, 0, -h).normalized * m_speed * m_dash;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 315, 0);
                m_rb.velocity = new Vector3(v, 0, -h).normalized * m_speed * m_dash;
            }
            else
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                m_rb.velocity = new Vector3(0, 0, -h).normalized * m_speed * m_dash;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_animator.SetBool("isWalking", false);
                m_animator.SetBool("isRun", true);
                m_dash = k_runSpeed;
            }
            else
            {
                m_animator.SetBool("isWalking", true);
                m_animator.SetBool("isRun", false);
                m_dash = 1.0f;
            }
            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 135, 0);
                m_rb.velocity = new Vector3(v, 0, -h).normalized * m_speed * m_dash;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 225, 0);
                m_rb.velocity = new Vector3(v, 0, -h).normalized * m_speed * m_dash;
            }
            else
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 180, 0);
                m_rb.velocity = new Vector3(0, 0, -h).normalized * m_speed * m_dash;
            }
        }
        else
        {
            m_animator.SetBool("isWalking", false);
            m_animator.SetBool("isRun", false);
            m_rb.velocity = new Vector3(0, 0, 0);
        }
    }
}