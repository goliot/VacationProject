using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    private GameObject hpBarPrefab = null;

    GameObject playerObject;
    GameObject hpBar;

    Camera cam = null;

    private void Start()
    {
        cam = Camera.main;

        playerObject = GameObject.FindGameObjectWithTag("Player");
        hpBar = Instantiate(hpBarPrefab, playerObject.transform.position, Quaternion.identity, transform);
    }

    private void Update()
    {
        hpBar.transform.position = cam.WorldToScreenPoint(playerObject.transform.position + new Vector3(0, 2, 0));
        hpBar.GetComponent<Slider>().value = playerObject.GetComponent<PlayerController>().playerData.health / playerObject.GetComponent<PlayerController>().playerData.maxHealth;
    }
}
