using UnityEngine;

public class FadeInOverlay : MonoBehaviour
{
    public Material overlayMaterial; // ���߃e�N�X�`���p�}�e���A��
    public float fadeDuration = 2.0f; // �t�F�[�h�C���ɂ����鎞��
    private float alpha = 0.0f; // �����̓����x

    void Update()
    {
        if (alpha < 1.0f)
        {
            alpha += Time.deltaTime / fadeDuration; // ���Ԃɉ����ē����x�𑝉�
            alpha = Mathf.Clamp01(alpha); // 0�`1�ɃN�����v

            // �}�e���A���̓����x���X�V
            Color color = overlayMaterial.color;
            color.a = alpha;
            overlayMaterial.color = color;
        }
    }
}
