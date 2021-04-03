using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public SphereCollider col;
    [HideInInspector] public bool isGrounded;
    [SerializeField] private Material onGroundMaterial;
    [SerializeField] private Material onAirMaterial;
    [SerializeField] private SkinnedMeshRenderer rend;
    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        Changecolor(isGrounded);
    }

    public void Push(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DeactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            Changecolor(isGrounded);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
            Changecolor(isGrounded);
        }
    }

    private void Changecolor(bool grounded)
    {
        //if(grounded)
        //    rend.material = onGroundMaterial;
        //else
        //    rend.material = onAirMaterial;
    }

    public void FinishGame()
    {

    }

}
