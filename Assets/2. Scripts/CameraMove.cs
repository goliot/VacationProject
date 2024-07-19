using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 15, -10);

    private void Update()
    {
        gameObject.transform.position = player.transform.position + offset;
    }
}
