using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    bool isPaused = false;

    public float lookSmooth = 2.0f;
    private Vector2 lookDirection;

    public float SensitivityX = 10.0f;
    public float SensitivityY = 10.0f;

    public float MaxY = 90f;
    public float MinY = -90f;

    public GameObject Character;

    void Start()
    {
        HideMouseCursor();
        Character = transform.parent.gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;

            if (isPaused == true)
            {
                ShowMouseCursor();
            }
            else
            {
                HideMouseCursor();
            }
        }

        Vector2 mouseDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseDir = Vector2.Scale(mouseDir, new Vector2(SensitivityX, SensitivityY));

        Vector2 lookDelta = new Vector2();
        lookDelta.x = Mathf.Lerp(lookDelta.x, mouseDir.x, 1.0f / lookSmooth);
        lookDelta.y = Mathf.Lerp(lookDelta.y, mouseDir.y, 1.0f / lookSmooth);
        lookDirection += lookDelta;

        //RotationX += Input.GetAxis("Mouse X") * SensitivityX;

        //RotationY += Input.GetAxis("Mouse Y") * SensitivityY;
        lookDirection.y = Mathf.Clamp(lookDirection.y, MinY, MaxY);

//      transform.localEulerAngles = new Vector3(-lookDirection.y, lookDirection.x, 0);

        transform.localRotation = Quaternion.AngleAxis(-lookDirection.y, Vector3.right);
        Character.transform.localRotation = Quaternion.AngleAxis(lookDirection.x, Character.transform.up);
    }

    void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
