using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    private PlayerAnimator playerAnimator;

    private Vector3 forward;
    private Vector3 right;
    private Vector3 dir = Vector3.zero;

    private bool isGround;
    private int attackCount;

    public Stats stat;

    [Header("Camera")]
    public CameraMove cameraMove;  // 카메라 움직임 스크립트를 참조합니다.

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        playerAnimator = GetComponent<PlayerAnimator>();
        applySpeed = walkSpeed;
        isGround = true;

        stat = new Stats();
        stat.atk = 10;
        stat.speed = walkSpeed;
        stat.atkSpeed = 1f;
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        forward = cameraMove.CinemachineCameraTarget.transform.forward;
        right = cameraMove.CinemachineCameraTarget.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        dir = (forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal")).normalized;

        playerAnimator.OnMovement(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Dash();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            DashCancel();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGround)
                Jump();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ComboAttack();
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(playerAnimator.animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"));
        //Quaternion targetRotation = Quaternion.Euler(0, cameraMove.CinemachineCameraTarget.transform.rotation.eulerAngles.y, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, cameraMove.CinemachineCameraTarget.transform.rotation.eulerAngles.y, 0);

        if(playerAnimator.animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
            rb.MovePosition(transform.position + dir * applySpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void Jump()
    {
        isGround = false;
        rb.velocity = Vector3.up * jumpForce;
        playerAnimator.OnJump();
    }

    private void Dash()
    {
        applySpeed = runSpeed;
    }

    private void DashCancel()
    {
        applySpeed = walkSpeed;
    }

    private void Attack()
    {
        playerAnimator.OnAttack();
    }

    private void ComboAttack()
    {
        playerAnimator.OnComboAttack();
    }
}
