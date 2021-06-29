using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer lineRenderer;
    Transform m_transform;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        ShootLaser();
    }

    void ShootLaser()
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if (Physics2D.Raycast(m_transform.position, mouseWorldPos))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, mouseWorldPos);
            DrawRay(laserFirePoint.position, hit.point);
        }
        else
            DrawRay(laserFirePoint.position, laserFirePoint.transform.right* defDistanceRay);
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
