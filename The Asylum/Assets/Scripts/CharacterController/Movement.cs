using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 3;
    public float runMultiplier = 2f;
    public float crouchSpeedReducer = 3f;

    float crouchHeightReducer = 3f;
    float crouchWidthIncrease = 2f;

    float tmpCrouchHeight;
    float tmpCrouchWidth;
    float tmpMoveSpeed;

    bool isCrouching = false;

    private void Start()
    {
        tmpCrouchHeight = transform.localScale.y - (transform.localScale.y / crouchHeightReducer);
        tmpCrouchWidth = transform.localScale.x - (transform.localScale.x / crouchWidthIncrease);
        tmpMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            moveSpeed *= runMultiplier;
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
            moveSpeed = tmpMoveSpeed;

        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            moveSpeed = tmpMoveSpeed;
            transform.localScale -= new Vector3(-tmpCrouchWidth, tmpCrouchHeight, -tmpCrouchWidth);
            transform.localPosition -= new Vector3(0, tmpCrouchHeight / 2, 0);
            moveSpeed /= crouchSpeedReducer;
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale += new Vector3(-tmpCrouchWidth, tmpCrouchHeight, -tmpCrouchWidth);
            transform.localPosition += new Vector3(0, tmpCrouchHeight / 2, 0);
            moveSpeed = tmpMoveSpeed;
            isCrouching = false;
        }
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
