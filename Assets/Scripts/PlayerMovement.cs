
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private CharacterController2D controller = null;

    [SerializeField] private float runSpeed = 40f;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    private void Start()
    {
        if (!IsOwner)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        if (IsLocalPlayer)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }

    public void OnLanding()
    {
        Debug.Log("landed");
    }
}
