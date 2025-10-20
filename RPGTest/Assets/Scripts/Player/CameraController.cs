using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("绑定")]
    public Transform target;
    public Camera thirdRoleCamera;

    [Header("参数")]
    public float followSpeed = 5f;
    public float zoomSpeed = 10f;
    public Vector3 offset = new(0, 3f, -5f);

    [Header("旋转控制")]
    public float rotateSpeed = 180f;
    public float tiltAngle = 40f;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                followSpeed * Time.deltaTime);
        }
        HandleCameraZoom();
    }

    private void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        thirdRoleCamera.fieldOfView += scroll * zoomSpeed;
        thirdRoleCamera.fieldOfView = Mathf.Clamp(thirdRoleCamera.fieldOfView, 40, 70);
    }
}
