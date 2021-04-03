using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    private Camera cam;
    private bool isDragging;
    [SerializeField] private Trajectory trajectory;
    [SerializeField] private Player player;
    [SerializeField] private float pushForce = 4f;
    [SerializeField] private LayerMask colliderPlaneLayer;
    public Transform startpoint;
    public Transform dragPoint;
    private bool isPressedDuringOnGround;

    //vectors
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 direction;
    private Vector3 force;
    float distance;

    void Start()
    {
        cam = Camera.main;
        //player.DeactivateRb();
    }

    void Update()
    {
        if (player.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isPressedDuringOnGround = true;
                isDragging = true;
                OnDragStart();

            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                OnDragEnd();
            }
            if (isDragging)
            {
                OnDrag();
            }
        }
        else
        {
            isPressedDuringOnGround = false;
            trajectory.Hide();
        }
    }

    private void OnDragStart()
    {
        //player.DeactivateRb();
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 10;
        startPoint = cam.ScreenToWorldPoint(mousepos);
        startpoint.position = startPoint;
        trajectory.Show();
    }
    private void OnDrag()
    {
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 10;
        endPoint = cam.ScreenToWorldPoint(mousepos);
        dragPoint.position = endPoint;
        distance = Vector3.Distance(startpoint.localPosition, dragPoint.localPosition);
        direction = (startpoint.localPosition - dragPoint.localPosition).normalized;
        force = direction * (distance / 6) * pushForce;
        trajectory.UpdateDots(player.pos, force);
    }

    private void OnDragEnd()
    {
        //player.ActivateRb();
        if(isPressedDuringOnGround)
            player.Push(force);
        dragPoint.position = Vector3.zero;
        startpoint.position = Vector3.zero;
        trajectory.Hide();
    }
}
