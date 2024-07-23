using System.Collections;
using System.Collections.Generic;
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

    [Header("Camera")]
    public CameraMove cameraMove;  // 카메라 움직임 스크립트를 참조합니다.

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        applySpeed = walkSpeed;
    }

    private void Update()
    {
        Move();
        Jump();
        TryRun();
        TryPunch();
        ChangeAnimation();
    }

    private void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraMove.CinemachineCameraTarget.transform.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);

            rb.MovePosition(transform.position + dir * applySpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
        dir.Normalize();

        if (dir != Vector3.zero)
        {
            if (state == State.Temp || state == State.Idle)
                state = State.Move;
        }
        else
        {
            //if (state != State.Jump)
                //state = State.Idle;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
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
    
    private void TryPunch()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) // 마우스 왼쪽
        {
            state = State.Punch;
        }
    }

    private void ChangeAnimation()
    {
        switch (state)
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
                anim.Play("PunchRight");
                break;
            case State.Die:
                break;
        }
    }
}
