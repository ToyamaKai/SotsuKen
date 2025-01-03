using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigate : MonoBehaviour
{
    private Vector3 Area1 = new Vector3(0, 0, 0);
    private Vector3 Area2 = new Vector3(0, 0, 0);
    private Vector3 Area3 = new Vector3(0, 0, 0);
    private Vector3 Area4 = new Vector3(0, 0, 0);

    private List<int> remainingNumbers = new List<int> { 1, 2, 3, 4 };
    private int selectedNumber;

    [SerializeField]
    GameObject m_UI;

    [SerializeField]
    Text m_text;

    [SerializeField]
    GameObject m_player;

    [SerializeField]
    Text m_taskText;
    [SerializeField]
    GameObject m_setumei;

    bool isFinish;

    int num;

    static public bool hoge;

    // Start is called before the first frame update
    void Start()
    {
        m_UI.SetActive(false);
        SelectRandomNumber();
        isFinish = false;
        num = 0;
        m_taskText.text = "�^�X�N�F�ΐF�̌���ڎw�� " + (num + 1) + "/4";
        hoge = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        num++;
        if (other.CompareTag("Player"))
        {
            SelectRandomNumber();
            if(num < 4)
            {
                m_setumei.SetActive(true);
                hoge = false;
            }
            else
            {
                m_setumei.SetActive(true);
                hoge = true;
            }
            m_taskText.text = "�^�X�N�F�ΐF�̌���ڎw�� " + num + "/4";
        }
    }

    private void SelectRandomNumber()
    {
        if (remainingNumbers.Count > 0)
        {
            // �����_���ɔԍ���I��
            int randomIndex = Random.Range(0, remainingNumbers.Count);
            selectedNumber = remainingNumbers[randomIndex];

            // �g�p�ς݂̔ԍ������X�g����폜
            remainingNumbers.RemoveAt(randomIndex);

            // �ʒu��ύX
            ChangePosition(selectedNumber);
        }
        else
        {
            // �ʒu��ύX
            ChangePosition(5);
        }
    }

    private void ChangePosition(int hoge)
    {
        if (hoge == 1)
        {
            transform.position = new Vector3(-246, 274, 172);
        }
        else if (hoge == 2)
        {
            transform.position = new Vector3(232, 274, 223);
        }
        else if (hoge == 3)
        {
            transform.position = new Vector3(-247, 274, -301);
        }
        else if (hoge == 4)
        {
            transform.position = new Vector3(350, 274, -206);
        }
        else
        {
            transform.position = new Vector3(99999, 999999, 99999);
        }
    }

    public void OpenUI(bool isFin)
    {
        if(!isFin)
        {
            m_text.text = "���݂̃G���A�̓G���A" + Manager.m_areaNumber + "�ł��B\n �A���P�[�g�ɂ��������������B";
            m_UI.SetActive(true);
        }
        else
        {
            m_text.text = "���݂̃G���A�̓G���A" + Manager.m_areaNumber + "�ł��B\n �A���P�[�g�ɂ��������������B\n �R���e���c�͈ȏ�ƂȂ�܂��B\n ���������c��̃A���P�[�g��\n���������������B";
            m_UI.SetActive(true);
        }
    }

    public void transportOrigin()
    {
        if (!isFinish)
        {
            m_player.transform.position = new Vector3(0, 34, 0);
            m_player.transform.rotation = Quaternion.Euler(0, 180, 0);
            m_UI.SetActive(false);
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }
}