using UnityEngine;

public class CameraSide : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float radius = 1.0f; // カメラとプレイヤーの距離
    public float heightOffset = 0.5f; // カメラの高さオフセット
    public LayerMask obstructionMask; // 障害物として判定するレイヤー

    void LateUpdate()
    {
        // プレイヤーの右側方向を計算（Y軸を基準に+90°回転）
        Vector3 rightOffset = Quaternion.Euler(0, 90f, 0) * player.forward;

        // カメラの目標位置を計算
        Vector3 targetPosition = player.position + Vector3.up * heightOffset + rightOffset.normalized * radius;

        // レイキャストで障害物をチェック
        if (Physics.Raycast(player.position + Vector3.up * heightOffset, rightOffset.normalized, out RaycastHit hit, radius, obstructionMask))
        {
            // 障害物がある場合、カメラを障害物手前に配置
            targetPosition = hit.point - rightOffset.normalized * 0.1f; // 0.1fで少し余裕を持たせる
        }

        // カメラ位置と向きを設定
        transform.position = targetPosition;
        transform.LookAt(player.position + Vector3.up * heightOffset);
    }
}
