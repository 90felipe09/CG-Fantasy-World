using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    public float minZoom;
    public float maxZoom;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newLocalPosition;

    private float distance;
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newLocalPosition = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }

    void handleInput()
    {
        handleMovement();
        handleRotation();
        handleZoom();
    }

    void handleMovement()
    {
        distance = movementSpeed * Time.deltaTime;
        transform.position += (transform.forward * distance * Input.GetAxis("Vertical"));
        transform.position += (transform.right * distance * Input.GetAxis("Horizontal"));
    }
    void handleZoom()
    {
        newLocalPosition += Input.mouseScrollDelta.y * zoomAmount * Time.deltaTime;
        bool isMin = newLocalPosition.y < minZoom;
        bool isMax = newLocalPosition.y > maxZoom;
        if (!isMin && !isMax)
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newLocalPosition, Time.deltaTime * movementTime);
        }
        if (isMin)
        {
            newLocalPosition.y = minZoom;
            newLocalPosition.z = -minZoom;
        }
        if (isMax)
        {
            newLocalPosition.y = maxZoom;
            newLocalPosition.z = -maxZoom;
        }
    }

    void handleRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount * Time.deltaTime);
        }

        transform.rotation = newRotation;
    }



}

