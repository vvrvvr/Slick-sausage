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
    [SerializeField] private LayerMask colliderPlaneLayer;
    public Transform startpoint;
    public Transform dragPoint;

    //vectors
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 direction;
    private Vector3 force; //но может быть и вектор 3
    float distance;

    void Start()
    {
        cam = Camera.main;
        //player.DeactivateRb();
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

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, colliderPlaneLayer))
        //{
        //    startPoint = hit.point;
        //}
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 10;
       
        startPoint = cam.ScreenToWorldPoint(mousepos);
        startpoint.position = startPoint;

    }
    private void OnDrag()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, colliderPlaneLayer))
        //{
        //    endPoint = hit.point;
        //}
        //distance = Vector2.Distance(startPoint, endPoint);
        //Debug.Log(distance);
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 10;
        endPoint = cam.ScreenToWorldPoint(mousepos);
        dragPoint.position = endPoint;

        distance = Vector3.Distance(startpoint.localPosition, dragPoint.localPosition);
        direction = (startpoint.localPosition - dragPoint.localPosition).normalized;

        ////;
        force = direction * distance * pushForce; //!

        //Debug.DrawLine(startpoint.localPosition, dragPoint.localPosition,Color.red);
        //Debug.Log(direction);
    }

    private void OnDragEnd()
    {
        player.ActivateRb();
        player.Push(force); //!
        dragPoint.position = Vector3.zero;
        startpoint.position = Vector3.zero;
    }



    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }
}
