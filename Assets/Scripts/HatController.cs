using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{

    public Camera cam;
    private Rigidbody2D _rigidbody;
    private Renderer _renderer;
    private float maxWidthArea;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float hatWidth = _renderer.bounds.extents.x;
        maxWidthArea = targetWidth.x - hatWidth/2;

    }

    void FixedUpdate()
    {
        Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = new Vector3(rawPosition.x, 0.0f, 0.0f);
        float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidthArea, maxWidthArea);
        targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
        _rigidbody.MovePosition(targetPosition);
    }
}
