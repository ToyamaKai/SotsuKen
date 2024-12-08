using UnityEngine;

public class FadeInOverlay : MonoBehaviour
{
    public Material overlayMaterial; // 透過テクスチャ用マテリアル
    public float fadeDuration = 2.0f; // フェードインにかかる時間
    private float alpha = 0.0f; // 初期の透明度

    void Update()
    {
        if (alpha < 1.0f)
        {
            alpha += Time.deltaTime / fadeDuration; // 時間に応じて透明度を増加
            alpha = Mathf.Clamp01(alpha); // 0～1にクランプ

            // マテリアルの透明度を更新
            Color color = overlayMaterial.color;
            color.a = alpha;
            overlayMaterial.color = color;
        }
    }
}
