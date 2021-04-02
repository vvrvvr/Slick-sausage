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
    [SerializeField] private Player player;
    [SerializeField] private float pushForce = 4f;

    //vectors
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 direction;
    private Vector2 force; //но может быть и вектор 3
    float distance;

    void Start()
    {
        cam = Camera.main;
        player.DeactivateRb();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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

    private void OnDragStart()
    {
        player.DeactivateRb();
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce; //!

        Debug.DrawLine(startPoint, endPoint);
    }

    private void OnDragEnd()
    {
        player.ActivateRb();
        player.Push(force); //!
    }

}
