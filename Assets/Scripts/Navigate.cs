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
        m_taskText.text = "タスク：緑色の光を目指せ " + (num + 1) + "/4";
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
            m_taskText.text = "タスク：緑色の光を目指せ " + num + "/4";
        }
    }

    private void SelectRandomNumber()
    {
        if (remainingNumbers.Count > 0)
        {
            // ランダムに番号を選択
            int randomIndex = Random.Range(0, remainingNumbers.Count);
            selectedNumber = remainingNumbers[randomIndex];

            // 使用済みの番号をリストから削除
            remainingNumbers.RemoveAt(randomIndex);

            // 位置を変更
            ChangePosition(selectedNumber);
        }
        else
        {
            // 位置を変更
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
            m_text.text = "現在のエリアはエリア" + Manager.m_areaNumber + "です。\n アンケートにお答えください。";
            m_UI.SetActive(true);
        }
        else
        {
            m_text.text = "現在のエリアはエリア" + Manager.m_areaNumber + "です。\n アンケートにお答えください。\n コンテンツは以上となります。\n 引き続き残りのアンケートに\nお答えください。";
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
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}