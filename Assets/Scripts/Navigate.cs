using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigate : MonoBehaviour
{
    private Vector3 Area1 = new Vector3(0, 0, 0);
    private Vector3 Area2 = new Vector3(0, 0, 0);
    private Vector3 Area3 = new Vector3(0, 0, 0);
    private Vector3 Area4 = new Vector3(0, 0, 0);

    private List<int> remainingNumbers = new List<int> { 1, 2, 3, 4 };
    private int selectedNumber;

    // Start is called before the first frame update
    void Start()
    {
        SelectRandomNumber();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && remainingNumbers.Count > 0)
        {
            SelectRandomNumber();
            other.transform.position = new Vector3(0, 34, 0);
            other.transform.rotation = Quaternion.Euler(0, 0, 0);
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
            Debug.Log("終わり");
        }
    }

    private void ChangePosition(int hoge)
    {
        if (hoge == 1)
        {
            transform.position = new Vector3(-353, 377, 59);
        }
        else if (hoge == 2)
        {
            transform.position = new Vector3(282.6f, 377, 342.1f);
        }
        else if (hoge == 3)
        {
            transform.position = new Vector3(-271, 377, -411);
        }
        else if (hoge == 4)
        {
            transform.position = new Vector3(373, 377, -424.7f);
        }
    }
}