using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "PageCollection", menuName = "UI/PageCollection", order = 1)]
public class PageCollection : ScriptableObject
{
    [System.Serializable]
    public class PageData
    {
        public string title;             // �^�C�g��
        public string subtitle;          // �T�u�^�C�g��
        public Sprite image;             // �摜
        public string description;       // ������
        public Vector2 imageSize = new Vector2(300, 300); // �摜�T�C�Y (�f�t�H���g�l)
    }

    public PageData[] pages; // �y�[�W�f�[�^�̔z��
}
