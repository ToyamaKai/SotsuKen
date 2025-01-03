using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;

public class PageUI : MonoBehaviour
{
    public Text titleText;         // �^�C�g��
    public Image contentImage;     // �摜
    public RectTransform imageRectTransform; // �摜��RectTransform (�T�C�Y�ύX�p)
    public Text descriptionText;   // ������
    public Button nextButton;      // ���փ{�^��
    public Button previousButton;  // �O�փ{�^��
    public Button closeButton;     // ����{�^��
    public GameObject UI;

    public PageCollection pageCollection; // �y�[�W�f�[�^���܂Ƃ߂�ScriptableObject

    public Navigate navigate;
    private int currentPageIndex = 0;
    private List<int> activePages = new List<int>(); // ���݂̃G���A�ŕ\������y�[�W�̃C���f�b�N�X

    void Start()
    {

        // �G���A1�������ݒ�i�K�v�ɉ����ĕύX�j
        SetAreaPages(new List<int> {8});

        // �{�^���C�x���g��o�^����O�Ɋ����̃��X�i�[���폜
        nextButton.onClick.RemoveAllListeners();
        previousButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        // �{�^���C�x���g��o�^
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

        // �T�C�Y�ύX�������K�p
        imageRectTransform.sizeDelta = page.imageSize;

        // �T�C�Y�������K�p�����֕��@
        imageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, page.imageSize.x);
        imageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, page.imageSize.y);

        // ���C�A�E�g�������X�V
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
            currentPageIndex++; // ���݂̃y�[�W�C���f�b�N�X�𑝉�
            UpdatePage(currentPageIndex); // �y�[�W�X�V

            // �{�^�����ꎞ�I�ɖ�����
            DisableButtonTemporarily(nextButton, 0.3f);
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--; // ���݂̃y�[�W�C���f�b�N�X������
            UpdatePage(currentPageIndex); // �y�[�W�X�V

            // �{�^�����ꎞ�I�ɖ�����
            DisableButtonTemporarily(previousButton, 0.3f);
        }
    }

    private void DisableButtonTemporarily(Button button, float delay)
    {
        button.interactable = false; // �{�^���𖳌���
        Invoke(nameof(EnableButton), delay); // �w�肵�����Ԍ�ɗL��������
    }

    private void EnableButton()
    {
        nextButton.interactable = true;
        previousButton.interactable = true;
    }


    public void CloseUI()
    {
        // UI���A�N�e�B�u�ɂ��邩�A�K�v�ɉ����đ��̓�������s
        UI.SetActive(false);
        navigate.OpenUI(Navigate.hoge);
    }
    public void SetAreaPages(List<int> pages)
    {
        // �G���A���Ƃ̃y�[�W��ݒ�
        activePages = new List<int>(pages); // �R�s�[���쐬���ă��X�g���Ǘ�
        currentPageIndex = 0; // �����y�[�W�����Z�b�g

        // �ŏ��̃y�[�W��\��
        UpdatePage(currentPageIndex);
    }

}
