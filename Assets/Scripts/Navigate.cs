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
    GameObject[] m_areaCollision;

    [SerializeField]
    GameObject m_button;

    [SerializeField]
    GameObject Player1;
    [SerializeField]
    GameObject Player2;
    [SerializeField]
    Button m_buttonHoge;
    [SerializeField]
    Text text;

    //[SerializeField]
    //GameObject m_setumei;

    private bool isFinish;
    private int num;
    private int hogenum;

    static public bool hoge;
    static public int m_nowNum;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, false);
        foreach (GameObject obj in m_areaCollision)
        {
            obj.SetActive(false);
        }

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
        if (other.CompareTag("Player"))
        {
            Debug.Log(num);
            if(num < 3)
            {
                //m_setumei.SetActive(true);
                OpenUI(false);
                hoge = false;
            }
            else
            {
                //m_setumei.SetActive(true);
                OpenUI(true);
                hoge = true;
            }

            if (m_nowNum == 1)
            {
                m_player.transform.position = new Vector3(-100, 38, 100);
            }
            else if (m_nowNum == 2)
            {
                m_player.transform.position = new Vector3(175, 28, 50);
            }
            else if (m_nowNum == 3)
            {
                m_player.transform.position = new Vector3(-75, 24, -300);
            }
            else if (m_nowNum == 4)
            {
                m_player.transform.position = new Vector3(175, 35, -65);
            }
        }
    }

    private void SelectRandomNumber()
    {
        if (remainingNumbers.Count > 0)
        {
            // ランダムに番号を選択
            int randomIndex = Random.Range(0, remainingNumbers.Count);
            m_nowNum = remainingNumbers[randomIndex];
            m_areaCollision[m_nowNum-1].SetActive(true);

            // 使用済みの番号をリストから削除
            remainingNumbers.RemoveAt(randomIndex);

            // 位置を変更
            ChangePosition(m_nowNum);
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
            transform.position = new Vector3(-125, 200, 130);
        }
        else if (hoge == 2)
        {
            transform.position = new Vector3(200, 200, 75);
        }
        else if (hoge == 3)
        {
            transform.position = new Vector3(-100, 200, -330);
        }
        else if (hoge == 4)
        {
            transform.position = new Vector3(200, 200, -90);
        }
        else
        {
            transform.position = new Vector3(99999, 999999, 99999);
        }
    }

    public void announce()
    {
        m_button.SetActive(false);
        text.text = "閉じる";

        // リスナーをクリア
        m_buttonHoge.onClick.RemoveAllListeners();

        // 必要なリスナーを追加
        m_buttonHoge.onClick.AddListener(CloseUI);

        if (m_nowNum == 1)
        {
            m_text.text = "エリア" + m_nowNum + "に入りました。 \n 画面の効果に注目して \n 操作してください。";
            m_UI.SetActive(true);
        }
        else if (m_nowNum == 4)
        {
            m_text.text = "エリア" + m_nowNum + "に入りました。 \n 画面全体に注目して \n 操作してください。";
            m_UI.SetActive(true);
        }
        else
        {
            m_text.text = "エリア" + m_nowNum + "に入りました。 \n キャラクターに注目して \n 操作してください。";
            m_UI.SetActive(true);
        }
    }

    public void CloseUI()
    {
        m_UI.SetActive(false);
    }

    public void OpenUI(bool isFin)
    {
        m_button.SetActive(true);
        text.text = "閉じて画面を確認";
        // リスナーをクリア
        m_buttonHoge.onClick.RemoveAllListeners();

        // 必要なリスナーを追加
        m_buttonHoge.onClick.AddListener(CloseUI);
        if (!isFin)
        {
            m_text.text = "現在のエリアはエリア" + m_nowNum + "です。\n アンケートにお答えください。";
            m_UI.SetActive(true);
        }
        else
        {
            m_text.text = "現在のエリアはエリア" + m_nowNum + "です。\n アンケートにお答えください。\n コンテンツは以上となります。\n 引き続き残りのアンケートに\nお答えください。";
            m_UI.SetActive(true);
        }
    }

    public void cancel()
    {
        m_UI.SetActive(false);
    }

    public void transportOrigin()
    {
        num++;
        hogenum = m_nowNum - 1;
        SelectRandomNumber();
        m_taskText.text = "タスク：緑色の光を目指せ " + num + "/4";

        if (!isFinish)
        {
            Manager.ChangeMaterial(0);
            if (Manager.m_areaNumber == 3 || Manager.m_areaNumber == 4)
            {
                Player2.SetActive(false);
                Player1.SetActive(true);
            }
            Manager.m_areaNumber = 0;
            m_player.transform.position = new Vector3(23, 20, -107);
            m_player.transform.rotation = Quaternion.Euler(0, 180, 0);
            Debug.Log(2);
            m_UI.SetActive(false);
            m_areaCollision[hogenum].SetActive(false);
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