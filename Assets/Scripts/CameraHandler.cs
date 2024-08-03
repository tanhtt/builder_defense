using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance { get; private set; }
    [SerializeField] private CinemachineVirtualCamera cinemacineVirtualCamera;

    private float orthographicSize;
    private float targetOrthographicSize;
    private bool isEdgeScrolling;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        orthographicSize = cinemacineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
        isEdgeScrolling = PlayerPrefs.GetInt("edgeScrolling", 1) == 1;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isEdgeScrolling)
        {
            float edgeScrollingSize = 20;
            if (Input.mousePosition.x > Screen.width - edgeScrollingSize)
            {
                x = 1;
            }
            if (Input.mousePosition.x < edgeScrollingSize)
            {
                x = -1;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrollingSize)
            {
                y = 1;
            }
            if (Input.mousePosition.y < edgeScrollingSize)
            {
                y = -1;
            }
        }

        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 5f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float zoomAmount = 2f;
        targetOrthographicSize += Input.mouseScrollDelta.y * zoomAmount; ;

        float minOrthographicSize = 10;
        float maxOrthographicSize = 30;

        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        cinemacineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    public void SetEdgeScrolling(bool set)
    {
        this.isEdgeScrolling = set;
        PlayerPrefs.SetInt("edgeScrolling", set ? 1 : 0);
    }

    public bool IsEdgeScrolling()
    {
        return this.isEdgeScrolling;
    }
}
