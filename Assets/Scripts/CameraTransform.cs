using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player; // プレイヤーのTransformを指定
    public GameObject player1; // プレイヤーのTransformを指定
    public GameObject player2; // プレイヤーのTransformを指定
    public float radius = 1.0f; // カメラとプレイヤーの距離
    public float heightOffset = 0.5f; // カメラの高さオフセット
    public float mouseSensitivity = 100f; // マウス感度
    public LayerMask obstructionMask; // 障害物として判定するレイヤー

    private float yaw = 0f; // 水平方向の回転
    private float pitch = 0f; // 垂直方向の回転

    private Settings m_setting;

    private void Start()
    {
    }

    void Update()
    {
        if (player1.activeSelf)
        {
            player = player1.transform;
        }
        else
        {
            player = player2.transform;
        }
        // マウスの移動量を取得
        float mouseX = Input.GetAxis("Mouse X") * Settings.MouseSensitivity * 50 * Time.deltaTime * Settings.MouseSayuuInversion;
        float mouseY = Input.GetAxis("Mouse Y") * Settings.MouseSensitivity * 50 * Time.deltaTime * Settings.MouseJougeInversion;

        // 回転角度を更新
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -89f, 89f); // ピッチを制限（垂直方向の制限）

        // 球面座標からカメラ位置を計算
        Vector3 direction = new Vector3(
            Mathf.Sin(Mathf.Deg2Rad * yaw) * Mathf.Cos(Mathf.Deg2Rad * pitch),
            Mathf.Sin(Mathf.Deg2Rad * pitch),
            Mathf.Cos(Mathf.Deg2Rad * yaw) * Mathf.Cos(Mathf.Deg2Rad * pitch)
        );

        // カメラのターゲット位置（高さオフセットを追加）
        Vector3 targetPosition = player.position + Vector3.up * heightOffset - direction * radius;

        // レイキャストで障害物をチェック
        if (Physics.Raycast(player.position + Vector3.up * heightOffset, -direction, out RaycastHit hit, radius, obstructionMask))
        {
            // 障害物がある場合、カメラを障害物手前に配置
            targetPosition = hit.point + direction * 0.1f; // 0.1fは余裕を持たせる
        }

        // カメラ位置と向きを設定
        transform.position = targetPosition;
        transform.LookAt(player.position + Vector3.up * heightOffset);
    }
}
