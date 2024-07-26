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
    [SerializeField]
    private GameObject[] weapons;
    private int currentWeaponIdx;
    private Rigidbody rb;
    private CapsuleCollider cc;
    private Animator anim;

    private Vector3 forward;
    private Vector3 right;
    private Vector3 dir = Vector3.zero;

    public State state;
    private bool isGround;
    private int attackCount;

    public Stats stat;

    [Header("Camera")]
    public CameraMove cameraMove;  // 카메라 움직임 스크립트를 참조합니다.

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        applySpeed = walkSpeed;
        state = State.Idle;
        isGround = true;

        stat = new Stats();
        stat.atk = 10;
        stat.speed = walkSpeed;
        stat.atkSpeed = 1f;
    }

    private void Update()
    {
        StateMachine();
        if(state != State.Idle)
            CheckAnimationEnd();
        ChangeAnimation();
    }

    protected override void StateMachine()
    {
        forward = cameraMove.CinemachineCameraTarget.transform.forward;
        right = cameraMove.CinemachineCameraTarget.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        dir = (forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal")).normalized;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeapon();

        switch (state)
        {
            case State.Idle:
                if (dir != Vector3.zero)
                {
                    state = State.Move;
                }
                else if (Input.GetKeyDown(KeyCode.Space) && isGround)
                {
                    state = State.Jump;
                    Jump();
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    state = State.Attack;
                }
                break;
            case State.Move:
                if (dir == Vector3.zero)
                {
                    state = State.Idle;
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    state = State.Dash;
                    Dash();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && isGround)
                {
                    state = State.JumpWhileRun;
                    Jump();
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    state = State.Attack;
                }
                break;
            case State.Dash:
                if (Input.GetKeyDown(KeyCode.Space) && isGround)
                {
                    state = State.JumpWhileRun;
                    Jump();
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    state = State.Idle;
                    DashCancel();
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    state = State.Attack;
                }
                break;
            case State.Jump:
                break;
            case State.JumpWhileRun:
                break;
            case State.Attack:
                break;
        }
    }

    private void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.Euler(0, cameraMove.CinemachineCameraTarget.transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);

        if(state != State.Attack)
            rb.MovePosition(transform.position + dir * applySpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
            state = State.Idle;
        }
    }

    private void Jump()
    {
        isGround = false;
        rb.velocity = Vector3.up * jumpForce;
    }

    private void Dash()
    {
        applySpeed = runSpeed;
    }

    private void DashCancel()
    {
        applySpeed = walkSpeed;
    }
    
    private void Punch()
    {
        //TODO : 공격 로직
    }

    protected override void CheckAnimationEnd()
    {
        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (animStateInfo.IsName("Jump") || animStateInfo.IsName("JumpWhileRunning"))
        {
            //anim.Play("FallingLoop");
            return;
        }

        if(animStateInfo.IsName("OneHand_Up_Attack_1") && Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.Play("OneHand_Up_Attack_2");
            Debug.Log("Combo1");    
        }
        if(animStateInfo.IsName("OneHand_Up_Attack_2") && Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.Play("OneHand_Up_Attack_3");
            Debug.Log("Combo2");
        }

        if (animStateInfo.normalizedTime >= 1.0f && !animStateInfo.loop)
        {
            state = State.Idle;
            attackCount = 0;
        }
    }

    protected override void ChangeAnimation()
    {
        switch (state)
        {
            case State.Temp: //다른 스테이트로 전환을 위한 null 상태
                break;
            case State.Idle:
                anim.Play("Idle");
                break;
            case State.Move:
                if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") > 0)
                    anim.Play("RunRight");
                else if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") < 0)
                    anim.Play("RunLeft");
                else if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") == 0)
                    anim.Play("RunForward");
                else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") > 0)
                    anim.Play("RunBackwardRight");
                else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") < 0)
                    anim.Play("RunBackwardLeft");
                else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") == 0)
                    anim.Play("RunBackward");
                else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") > 0)
                    anim.Play("RunRight");
                else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") < 0)
                    anim.Play("RunLeft");
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
            case State.Attack:
                Attack();
                break;
            case State.Die:
                break;
        }
        Debug.Log(state);
    }

    private void ChangeWeapon()
    {
        weapons[currentWeaponIdx].gameObject.SetActive(false);
        weapons[++currentWeaponIdx].gameObject.SetActive(true);
    }

    private void Attack()
    {
        anim.Play("OneHand_Up_Attack_1");
    }
}
