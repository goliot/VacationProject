using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZoneController : MonoBehaviour
{
    public float captureRate = 0.05f;

    [NonSerialized]
    public float redCaptureValue = 0f;
    [NonSerialized]
    public float blueCaptureValue = 0f;

    private bool isInRedZone = false;
    private bool isInBlueZone = false;
    private int inRedZoneCount = 0;
    private int inBlueZoneCount = 0;

    private void Update()
    {
        // 블루 팀 점령 게이지 증가
        if (isInBlueZone)
        {
            blueCaptureValue += captureRate * Time.deltaTime;
            blueCaptureValue = Mathf.Clamp(blueCaptureValue, 0, 100); // 0에서 100 사이로 제한
        }

        // 레드 팀 점령 게이지 증가
        if (isInRedZone)
        {
            redCaptureValue += captureRate * Time.deltaTime;
            redCaptureValue = Mathf.Clamp(redCaptureValue, 0, 100); // 0에서 100 사이로 제한
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == 8) // blue
        {
            if (other.gameObject.layer == 9)
            {
                inBlueZoneCount++;
                isInBlueZone = true;
                Debug.Log("In Blue Zone");
            }
        }
        else if(gameObject.layer == 9) // red
        {
            if (other.gameObject.layer == 8 || other.gameObject.layer == 6)
            {
                inRedZoneCount++;
                isInRedZone = true;
                Debug.Log("In Red Zone");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.layer == 8) // blue
        {
            if (other.gameObject.layer == 9)
            {
                inBlueZoneCount--;
                if(inBlueZoneCount <= 0)
                    isInBlueZone = false;
                Debug.Log("Out Blue Zone");
            }
        }
        else if (gameObject.layer == 9) // red
        {
            if (other.gameObject.layer == 8 || other.gameObject.layer == 6)
            {
                inRedZoneCount--;
                if(inRedZoneCount <= 0)
                    isInRedZone = false;
                Debug.Log("Out Red Zone");
            }
        }
    }
}
