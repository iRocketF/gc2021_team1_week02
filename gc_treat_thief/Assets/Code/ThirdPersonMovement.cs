using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCam;
    public Animator anims;

    public float speed;
    public float gravity;
    public float jumpHeight;

    private Vector3 velocity;

    public float turnSmoothTime;

    float turnSmoothVelocity;

    public bool isHidden = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            anims.SetTrigger("Jump");
        }

        if (controller.isGrounded)
            anims.SetBool("isAirborne", false);
        else
            anims.SetBool("isAirborne", true);

        velocity.y += gravity * Time.deltaTime;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(((moveDir.normalized * speed) * Time.deltaTime));

            anims.SetBool("isWalking", true);
        }
        else
            anims.SetBool("isWalking", false);

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        isHidden = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isHidden = false;
    }
}
