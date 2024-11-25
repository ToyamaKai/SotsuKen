using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    [Header("�ړ����x"), Range(0, 30)]
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
        // ���͂̎擾
        float input = Input.GetAxis("Vertical"); // W: +1, S: -1, ��: 0

        // ���͂Ɋ�Â��Ĉړ��������v�Z
        Vector3 direction = transform.forward * input;

        // �ړ�����
        if (input != 0)
        {
            // Rigidbody�ɗ͂������邱�Ƃňړ�������
            rigidBody.AddForce(direction * moveSpeed, ForceMode.VelocityChange);
        }
        else
        {
            // ��~���ɑ��x�����Z�b�g
            rigidBody.velocity = Vector3.zero;
        }
    }
}

