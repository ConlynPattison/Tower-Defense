using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool _doMovement = true;
    
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
            _doMovement = !_doMovement;
        
        if (!_doMovement)
            return;
        
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Time.deltaTime * panSpeed * Vector3.forward, Space.World);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Time.deltaTime * panSpeed * Vector3.back, Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Time.deltaTime * panSpeed * Vector3.right, Space.World);
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Time.deltaTime * panSpeed * Vector3.left, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        Vector3 pos = transform.position;

        pos.y -= Time.deltaTime * scroll * scrollSpeed * 1000;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
