using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody targetRb;
    [SerializeField] float jumpForce;
    void Start()
    {
       
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
