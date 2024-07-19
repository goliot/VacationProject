using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : CreatureState
{
    [Header("# Speed")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applySpeed;
    [SerializeField]
    private float crouchSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float rotSpeed;

    private Rigidbody rb;
    private CapsuleCollider cc;
    private Animator anim;
    private Vector3 dir = Vector3.zero;

    private State state;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = rb.GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        applySpeed = walkSpeed;
    }

    private void Update()
    {
        Move();
        Jump();
        TryRun();
        ChangeAnimation();
    }

    private void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                transform.Rotate(0, 1, 0);
            }
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
        }

        rb.MovePosition(transform.position + dir * applySpeed * Time.deltaTime);
    }

    private void Move()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
        dir.Normalize();

        if (dir != Vector3.zero)
        {
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                transform.Rotate(0, 1, 0);
            }
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);

            if (state == State.Temp || state == State.Idle)
                state = State.Move;
        }
        else
        {
            if(state != State.Jump)
                state = State.Idle;
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            rb.velocity = Vector3.up * jumpForce;
            if (dir == Vector3.zero)
                state = State.Jump;
            else
                state = State.JumpWhileRun;
        }
    }

    private bool CheckGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, cc.bounds.extents.y + 0.1f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            state = State.Temp;
        }
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && dir != Vector3.zero)
        {
            Running();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        applySpeed = runSpeed;
        state = State.Dash;
    }

    private void RunningCancel()
    {
        applySpeed = walkSpeed;
        state = State.Temp;
    }

    private void ChangeAnimation()
    {
        switch(state)
        {
            case State.Temp: //다른 스테이트로 전환을 위한 null 상태
                break;
            case State.Idle:
                anim.Play("Idle");
                break;
            case State.Move:
                anim.Play("RunForward");
                break;
            case State.Dash:
                anim.Play("Sprint");
                break;
            case State.Jump:
                anim.Play("Jump");
                break;
            case State.JumpWhileRun:
                anim.Play("JumpWhileRunning");
                break;
            case State.Punch:
                break;
            case State.Die:
                break;
        }
    }
}
