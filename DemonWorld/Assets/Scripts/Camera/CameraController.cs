using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float cameraSpeed;
    private Camera _camera;
    private float verticalInput;
    private float horizontalInput;

    private void Awake()
    {
        this.GetComponent<Camera>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        this.transform.position += new Vector3(horizontalInput, 0f, verticalInput) * cameraSpeed * Time.deltaTime;
    }

}
