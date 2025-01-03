using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;

public class PageUI : MonoBehaviour
{
    public Text titleText;         // タイトル
    public Image contentImage;     // 画像
    public RectTransform imageRectTransform; // 画像のRectTransform (サイズ変更用)
    public Text descriptionText;   // 説明文
    public Button nextButton;      // 次へボタン
    public Button previousButton;  // 前へボタン
    public Button closeButton;     // 閉じるボタン
    public GameObject UI;

    public PageCollection pageCollection; // ページデータをまとめたScriptableObject

    public Navigate navigate;
    private int currentPageIndex = 0;
    private List<int> activePages = new List<int>(); // 現在のエリアで表示するページのインデックス

    void Start()
    {

        // エリア1を初期設定（必要に応じて変更）
        SetAreaPages(new List<int> {8});

        // ボタンイベントを登録する前に既存のリスナーを削除
        nextButton.onClick.RemoveAllListeners();
        previousButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        // ボタンイベントを登録
        nextButton.onClick.AddListener(() => NextPage());
        previousButton.onClick.AddListener(() => PreviousPage());
        closeButton.onClick.AddListener(() => CloseUI());
    }


    public void UpdatePage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= activePages.Count) return;

        int actualPageIndex = activePages[pageIndex];
        var page = pageCollection.pages[actualPageIndex];

        titleText.text = page.title;
        descriptionText.text = page.description;
        contentImage.gameObject.SetActive(true);
        contentImage.sprite = page.image;

        // サイズ変更を強制適用
        imageRectTransform.sizeDelta = page.imageSize;

        // サイズを強制適用する代替方法
        imageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, page.imageSize.x);
        imageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, page.imageSize.y);

        // レイアウトを強制更新
        LayoutRebuilder.ForceRebuildLayoutImmediate(imageRectTransform);

        previousButton.interactable = pageIndex > 0;
        nextButton.interactable = pageIndex < activePages.Count - 1;
        closeButton.gameObject.SetActive(pageIndex == activePages.Count - 1);

        currentPageIndex = pageIndex;
    }


    public void NextPage()
    {
        if (currentPageIndex < activePages.Count - 1)
        {
            currentPageIndex++; // 現在のページインデックスを増加
            UpdatePage(currentPageIndex); // ページ更新

            // ボタンを一時的に無効化
            DisableButtonTemporarily(nextButton, 0.3f);
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--; // 現在のページインデックスを減少
            UpdatePage(currentPageIndex); // ページ更新

            // ボタンを一時的に無効化
            DisableButtonTemporarily(previousButton, 0.3f);
        }
    }

    private void DisableButtonTemporarily(Button button, float delay)
    {
        button.interactable = false; // ボタンを無効化
        Invoke(nameof(EnableButton), delay); // 指定した時間後に有効化する
    }

    private void EnableButton()
    {
        nextButton.interactable = true;
        previousButton.interactable = true;
    }


    public void CloseUI()
    {
        // UIを非アクティブにするか、必要に応じて他の動作を実行
        UI.SetActive(false);
        navigate.OpenUI(Navigate.hoge);
    }
    public void SetAreaPages(List<int> pages)
    {
        // エリアごとのページを設定
        activePages = new List<int>(pages); // コピーを作成してリストを管理
        currentPageIndex = 0; // 初期ページをリセット

        // 最初のページを表示
        UpdatePage(currentPageIndex);
    }

}
