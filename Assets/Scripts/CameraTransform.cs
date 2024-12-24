using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player; // �v���C���[��Transform���w��
    public GameObject player1; // �v���C���[��Transform���w��
    public GameObject player2; // �v���C���[��Transform���w��
    public float radius = 1.0f; // �J�����ƃv���C���[�̋���
    public float heightOffset = 0.5f; // �J�����̍����I�t�Z�b�g
    public float mouseSensitivity = 100f; // �}�E�X���x
    public LayerMask obstructionMask; // ��Q���Ƃ��Ĕ��肷�郌�C���[

    private float yaw = 0f; // ���������̉�]
    private float pitch = 0f; // ���������̉�]

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
        // �}�E�X�̈ړ��ʂ��擾
        float mouseX = Input.GetAxis("Mouse X") * Settings.MouseSensitivity * 50 * Time.deltaTime * Settings.MouseSayuuInversion;
        float mouseY = Input.GetAxis("Mouse Y") * Settings.MouseSensitivity * 50 * Time.deltaTime * Settings.MouseJougeInversion;

        // ��]�p�x���X�V
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -89f, 89f); // �s�b�`�𐧌��i���������̐����j

        // ���ʍ��W����J�����ʒu���v�Z
        Vector3 direction = new Vector3(
            Mathf.Sin(Mathf.Deg2Rad * yaw) * Mathf.Cos(Mathf.Deg2Rad * pitch),
            Mathf.Sin(Mathf.Deg2Rad * pitch),
            Mathf.Cos(Mathf.Deg2Rad * yaw) * Mathf.Cos(Mathf.Deg2Rad * pitch)
        );

        // �J�����̃^�[�Q�b�g�ʒu�i�����I�t�Z�b�g��ǉ��j
        Vector3 targetPosition = player.position + Vector3.up * heightOffset - direction * radius;

        // ���C�L���X�g�ŏ�Q�����`�F�b�N
        if (Physics.Raycast(player.position + Vector3.up * heightOffset, -direction, out RaycastHit hit, radius, obstructionMask))
        {
            // ��Q��������ꍇ�A�J��������Q����O�ɔz�u
            targetPosition = hit.point + direction * 0.1f; // 0.1f�͗]�T����������
        }

        // �J�����ʒu�ƌ�����ݒ�
        transform.position = targetPosition;
        transform.LookAt(player.position + Vector3.up * heightOffset);
    }
}
