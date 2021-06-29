using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser2 : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AudioManager.Instance.Play("atk");
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            

            _lineRenderer.SetPosition(1, transform.position);
            _lineRenderer.SetPosition(0, mousePos);
            

            _lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            _lineRenderer.enabled = false;
        }

    }
}
