using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    float m_speed = 5f; // �ʏ�̈ړ����x
    float m_dash; // �_�b�V�����̔{��
    const float k_runSpeed = 1.5f; // �_�b�V�����x�{��

    [SerializeField] Rigidbody m_rb; // Rigidbody
    [SerializeField] Animator m_animator; // Animator
    [SerializeField] Transform cameraTransform; // �J������Transform���w��

    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        // ���͂��擾
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // �J�����̌����Ɋ�Â��ړ��������v�Z
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // �����ʂ݂̂ɐ���
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // ���͕������J������ŕϊ�
        Vector3 movement = (forward * v + right * h).normalized * m_speed * m_dash;

        // �_�b�V������
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_dash = k_runSpeed;
            m_animator.SetBool("isWalking", false);
            m_animator.SetBool("isRun", true);
        }
        else
        {
            m_dash = 1.0f;
            m_animator.SetBool("isWalking", v != 0 || h != 0);
            m_animator.SetBool("isRun", false);
        }

        // �L�����N�^�[�̉�]
        if (movement.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        // Rigidbody�̑��x��ݒ�
        Vector3 velocity = new Vector3(movement.x, m_rb.velocity.y, movement.z);
        m_rb.velocity = velocity;
    }
}
