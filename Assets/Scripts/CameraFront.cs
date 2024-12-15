using UnityEngine;

public class CameraFront : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public float radius = 1.0f; // �J�����ƃv���C���[�̋���
    public float heightOffset = 0.5f; // �J�����̍����I�t�Z�b�g
    public LayerMask obstructionMask; // ��Q���Ƃ��Ĕ��肷�郌�C���[

    void LateUpdate()
    {
        // �v���C���[�̐��ʕ������v�Z
        Vector3 forwardOffset = -player.forward; // ���ʕ����̋t�i�J�������v���C���[�̐��ʂ��������߂ɋt�����j

        // �J�����̖ڕW�ʒu���v�Z
        Vector3 targetPosition = player.position + Vector3.up * heightOffset + forwardOffset.normalized * radius;

        // ���C�L���X�g�ŏ�Q�����`�F�b�N
        if (Physics.Raycast(player.position + Vector3.up * heightOffset, forwardOffset.normalized, out RaycastHit hit, radius, obstructionMask))
        {
            // ��Q��������ꍇ�A�J��������Q����O�ɔz�u
            targetPosition = hit.point - forwardOffset.normalized * 0.1f; // 0.1f�ŏ����]�T����������
        }

        // �J�����ʒu�ƌ�����ݒ�
        transform.position = targetPosition;
        transform.LookAt(player.position + Vector3.up * heightOffset);
    }
}
