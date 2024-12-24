using UnityEngine;
using System.Collections.Generic;

public class SmoothingAngle : MonoBehaviour
{
    // メソッドでスムージング角度を変更する
    public void SetSmoothingAngle(int smoothingAngle)
    {
        // 現在のゲームオブジェクト内の全てのMeshFilterを取得
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        foreach (var meshFilter in meshFilters)
        {
            if (meshFilter.sharedMesh == null) continue;

            // メッシュを複製して変更
            Mesh originalMesh = meshFilter.sharedMesh;
            Mesh newMesh = Instantiate(originalMesh);
            meshFilter.sharedMesh = newMesh;

            // ノーマルを再計算し、スムージング角度を反映
            RecalculateNormalsWithSmoothingAngle(newMesh, smoothingAngle);
        }
    }

    // ノーマルを再計算するメソッド
    private void RecalculateNormalsWithSmoothingAngle(Mesh mesh, int angle)
    {
        // 角度をラジアンに変換
        float radians = Mathf.Deg2Rad * angle;

        // 各頂点の法線情報を計算
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = new Vector3[vertices.Length];
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            // 三角形の頂点
            int index0 = triangles[i];
            int index1 = triangles[i + 1];
            int index2 = triangles[i + 2];

            Vector3 v0 = vertices[index0];
            Vector3 v1 = vertices[index1];
            Vector3 v2 = vertices[index2];

            // 法線を計算
            Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;

            // スムージング角度に基づいて法線を適用
            normals[index0] = Vector3.Slerp(normals[index0], normal, radians);
            normals[index1] = Vector3.Slerp(normals[index1], normal, radians);
            normals[index2] = Vector3.Slerp(normals[index2], normal, radians);
        }

        // メッシュに再計算した法線を適用
        mesh.normals = normals;
    }
}
