using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "PageCollection", menuName = "UI/PageCollection", order = 1)]
public class PageCollection : ScriptableObject
{
    [System.Serializable]
    public class PageData
    {
        public string title;             // タイトル
        public string subtitle;          // サブタイトル
        public Sprite image;             // 画像
        public string description;       // 説明文
        public Vector2 imageSize = new Vector2(300, 300); // 画像サイズ (デフォルト値)
    }

    public PageData[] pages; // ページデータの配列
}
