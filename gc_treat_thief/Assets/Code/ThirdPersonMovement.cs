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
    public bool isInvulnerable = false;
    public float invulnerableTimer = 2f;

    public PlayerInventory inventory;

    public SkinnedMeshRenderer playerMesh;
    private Material playerMaterial;
    private Color pOriginalColor;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerMaterial = playerMesh.material;
        pOriginalColor = playerMaterial.color;
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

        if (isInvulnerable)
        {
            ChangeColor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " +  other.gameObject.name);
        if (other.gameObject.layer == 9)
        {
            isHidden = true;
        }
        else if (other.gameObject.layer == 6 && !isInvulnerable)
        {
            isInvulnerable = true;

            if (inventory.treatsCollected >= 2)
            {
                inventory.treatsCollected -= 2;
            }
            else
            {
                inventory.treatsCollected = 0;
            }

            Invoke("VulnerableAgain", invulnerableTimer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            isHidden = false;
        }
    }

    private void VulnerableAgain()
    {
        isInvulnerable = false;
        playerMaterial.color = pOriginalColor;
    }

    private void ChangeColor()
    {
        playerMaterial.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
    }
}
