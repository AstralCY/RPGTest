using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [Header("绑定")]
    public Transform target;
    public Transform cameraHolder;
    public Camera firstRoleCamera;

    [Header("参数")]
    public float followSpeed = 5f;
    public float zoomSpeed = 10f;
    public Vector3 offset = new(0, 3f, -5f);

    [Header("旋转控制")]
    public float rotateSpeed = 180f;
    public float tiltAngle = 40f;

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + target.rotation * offset;
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
        transform.LookAt(target.position);
        HandleCameraZoom();
        HandleCameraRotation();
    }

    private void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        firstRoleCamera.fieldOfView += scroll * zoomSpeed;
        firstRoleCamera.fieldOfView = Mathf.Clamp(firstRoleCamera.fieldOfView, 40, 70);
    }

    private void HandleCameraRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            offset = Quaternion.AngleAxis(mouseX, Vector3.up) * offset;

            float newY = Mathf.Clamp(offset.y - mouseY, 1f, 10f);
            offset = new Vector3(offset.x, newY, offset.z);
        }

        offset.y = Mathf.Lerp(offset.y, 3f, Time.deltaTime);
    }
}
