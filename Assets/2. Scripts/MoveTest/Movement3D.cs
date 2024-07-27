using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    private Vector3 moveDirection;

    private CharacterController characterController;

    public float MoveSpeed
    {
        set => moveSpeed = Mathf.Clamp(value, 2.0f, 5.0f);
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
    }
}
