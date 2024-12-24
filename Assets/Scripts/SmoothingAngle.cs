using UnityEngine;
using System.Collections.Generic;

public class SmoothingAngle : MonoBehaviour
{
    // ���\�b�h�ŃX���[�W���O�p�x��ύX����
    public void SetSmoothingAngle(int smoothingAngle)
    {
        // ���݂̃Q�[���I�u�W�F�N�g���̑S�Ă�MeshFilter���擾
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        foreach (var meshFilter in meshFilters)
        {
            if (meshFilter.sharedMesh == null) continue;

            // ���b�V���𕡐����ĕύX
            Mesh originalMesh = meshFilter.sharedMesh;
            Mesh newMesh = Instantiate(originalMesh);
            meshFilter.sharedMesh = newMesh;

            // �m�[�}�����Čv�Z���A�X���[�W���O�p�x�𔽉f
            RecalculateNormalsWithSmoothingAngle(newMesh, smoothingAngle);
        }
    }

    // �m�[�}�����Čv�Z���郁�\�b�h
    private void RecalculateNormalsWithSmoothingAngle(Mesh mesh, int angle)
    {
        // �p�x�����W�A���ɕϊ�
        float radians = Mathf.Deg2Rad * angle;

        // �e���_�̖@�������v�Z
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = new Vector3[vertices.Length];
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            // �O�p�`�̒��_
            int index0 = triangles[i];
            int index1 = triangles[i + 1];
            int index2 = triangles[i + 2];

            Vector3 v0 = vertices[index0];
            Vector3 v1 = vertices[index1];
            Vector3 v2 = vertices[index2];

            // �@�����v�Z
            Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;

            // �X���[�W���O�p�x�Ɋ�Â��Ė@����K�p
            normals[index0] = Vector3.Slerp(normals[index0], normal, radians);
            normals[index1] = Vector3.Slerp(normals[index1], normal, radians);
            normals[index2] = Vector3.Slerp(normals[index2], normal, radians);
        }

        // ���b�V���ɍČv�Z�����@����K�p
        mesh.normals = normals;
    }
}
