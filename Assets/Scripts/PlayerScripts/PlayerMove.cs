using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    [Header("移動速度"), Range(0, 30)]
    public float moveSpeed = 5.0f;

    private Rigidbody rigidBody;

    // Use this for initialization
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            Debug.LogError("Unable to GetComponent rigidbody at Awake");
        }
        else if (rigidBody.isKinematic && this.enabled)
        {
            Debug.LogWarning("Rigidbody is Kinematic");
        }
    }

    private void FixedUpdate()
    {
        // 入力の取得
        float input = Input.GetAxis("Vertical"); // W: +1, S: -1, 他: 0

        // 入力に基づいて移動方向を計算
        Vector3 direction = transform.forward * input;

        // 移動処理
        if (input != 0)
        {
            // Rigidbodyに力を加えることで移動させる
            rigidBody.AddForce(direction * moveSpeed, ForceMode.VelocityChange);
        }
        else
        {
            // 停止時に速度をリセット
            rigidBody.velocity = Vector3.zero;
        }
    }
}

