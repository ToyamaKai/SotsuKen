using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform target;  // �L�����N�^�[��Transform
    public float distance = 5.0f;  // �L�����N�^�[����̋���
    public float rotationSpeed = 5.0f;  // ��]���x
    public float height = 1.7f;  // �J�����̍���

    private float currentRotationX = 0.0f;  // ������]�p
    private float currentRotationY = 0.0f;  // ������]�p

    void Update()
    {
        // �}�E�X�̈ړ��ʂ��擾
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // ������]�i�L�����N�^�[���͂����j
        currentRotationX += mouseX * rotationSpeed;

        // ������]�i�J�����̍����𒲐��j
        currentRotationY -= mouseY * rotationSpeed;
        currentRotationY = Mathf.Clamp(currentRotationY, -45f, 80f);  // ���������̉�]�͈͂𐧌�

        // �J�����̉�]��K�p
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);

        // �J�����̈ʒu���X�V
        Vector3 direction = new Vector3(0, height, -distance);  // �J�����̈ʒu������
        transform.position = target.position + rotation * direction;

        // ��ɃL�����N�^�[�������悤�ɂ���
        transform.LookAt(target);
    }
}
