using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform target;  // キャラクターのTransform
    public float distance = 5.0f;  // キャラクターからの距離
    public float rotationSpeed = 5.0f;  // 回転速度
    public float height = 1.7f;  // カメラの高さ

    private float currentRotationX = 0.0f;  // 水平回転角
    private float currentRotationY = 0.0f;  // 垂直回転角

    void Update()
    {
        // マウスの移動量を取得
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 水平回転（キャラクター周囲を回る）
        currentRotationX += mouseX * rotationSpeed;

        // 垂直回転（カメラの高さを調整）
        currentRotationY -= mouseY * rotationSpeed;
        currentRotationY = Mathf.Clamp(currentRotationY, -45f, 80f);  // 垂直方向の回転範囲を制限

        // カメラの回転を適用
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);

        // カメラの位置を更新
        Vector3 direction = new Vector3(0, height, -distance);  // カメラの位置を決定
        transform.position = target.position + rotation * direction;

        // 常にキャラクターを向くようにする
        transform.LookAt(target);
    }
}
