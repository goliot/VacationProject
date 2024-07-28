using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class PlayerController : MonoBehaviour
{
    [Header("# Speed")]
    private float applySpeed;
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

    string xmlFileName = "PlayerData";

    public PlayerData playerData;

    [Header("Camera")]
    public CameraMove cameraMove;  // 카메라 움직임 스크립트를 참조합니다.

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
        LoadXML(xmlFileName);
        applySpeed = playerData.speed;
        isGround = true;
    }

    private void LoadXML(string fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load(fileName);
        if (txtAsset == null)
        {
            Debug.LogError("Failed to load XML file: " + fileName);
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtAsset.text);

        XmlNodeList all_nodes = xmlDoc.SelectNodes("root/Sheet1");
        foreach (XmlNode node in all_nodes)
        {
            playerData = new PlayerData();

            playerData.health = float.Parse(node.SelectSingleNode("health").InnerText);
            playerData.maxHealth = playerData.health;
            playerData.speed = float.Parse(node.SelectSingleNode("speed").InnerText);
            playerData.attack = float.Parse(node.SelectSingleNode("attack").InnerText);
            playerData.attackRange = float.Parse(node.SelectSingleNode("attackRange").InnerText);
            playerData.attackSpeed = float.Parse(node.SelectSingleNode("attackSpeed").InnerText);
        }
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
        applySpeed = playerData.speed * 2;
    }

    private void DashCancel()
    {
        applySpeed = playerData.speed;
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
