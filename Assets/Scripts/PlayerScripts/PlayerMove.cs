using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    float m_speed = 5f; // 通常の移動速度
    float m_dash; // ダッシュ時の倍率
    const float k_runSpeed = 1.5f; // ダッシュ速度倍率

    [SerializeField] Rigidbody m_rb; // Rigidbody
    [SerializeField] Animator m_animator; // Animator
    [SerializeField] Transform cameraTransform; // カメラのTransformを指定

    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        // 入力を取得
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // カメラの向きに基づく移動方向を計算
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // 水平面のみに制限
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // 入力方向をカメラ基準で変換
        Vector3 movement = (forward * v + right * h).normalized * m_speed * m_dash;

        // ダッシュ判定
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

        // キャラクターの回転
        if (movement.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        // Rigidbodyの速度を設定
        Vector3 velocity = new Vector3(movement.x, m_rb.velocity.y, movement.z);
        m_rb.velocity = velocity;
    }
}
