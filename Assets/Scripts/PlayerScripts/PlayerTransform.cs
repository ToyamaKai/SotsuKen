using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 0.005f;
            animator.SetBool("is_walking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * 0.005f;
            animator.SetBool("is_walking", true);
        }
        else
        {
            animator.SetBool("is_walking", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0.2f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -0.2f, 0);
        }
    }
}
