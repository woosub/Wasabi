using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    
    Transform sideViewTr;
    Transform rearViewTr;

    bool isMovingCam = false;

    Camera cam;
    const float camMoveSpeed = 6.0f;

    // Use this for initialization
    void Start ()
    {
        sideViewTr = transform.parent.Find("SideView");
        rearViewTr = transform.parent.Find("RearView");

        cam = GetComponent<Camera>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReturnFov()
    {
        StartCoroutine(StartChangeFov(60, camMoveSpeed));
    }

    public void ChangeFov(float val, float speed)
    {
        StartCoroutine(StartChangeFov(val, speed));
    }

    IEnumerator StartChangeFov(float val, float speed)
    {
        float time = 0.0f;
        float gap = cam.fieldOfView - val;
        
        while (true)
        {
            time += Time.deltaTime * speed;

            cam.fieldOfView += gap * -1 * Time.deltaTime * speed;

            if (time >= 1.0f)
            {
                break;
            }

            yield return null;
        }

        cam.fieldOfView = val;
    }

    public void ChangeCameraView(CameraView view)
    {
        if (isMovingCam)
            return;

        isMovingCam = true;

        if (view == CameraView.Side)
        {
            StartCoroutine(MoveToSide());
        }
        else
        {
            StartCoroutine(MoveToRear());
        }
    }

    IEnumerator MoveToSide()
    {
        float val = 0.0f;

        while (true)
        {
            val += camMoveSpeed * Time.deltaTime;

            cam.transform.localPosition = Vector3.Lerp(rearViewTr.localPosition, sideViewTr.localPosition, val);
            cam.transform.localRotation = Quaternion.Lerp(rearViewTr.localRotation, sideViewTr.localRotation, val);

            if (val >= 1.0f)
            {
                break;
            }

            yield return null;
        }

        cam.transform.localPosition = sideViewTr.localPosition;
        cam.transform.localRotation = sideViewTr.localRotation;

        isMovingCam = false;
    }

    IEnumerator MoveToRear()
    {
        float val = 0.0f;

        while (true)
        {
            val += camMoveSpeed * Time.deltaTime;

            cam.transform.localPosition = Vector3.Lerp(sideViewTr.localPosition, rearViewTr.localPosition, val);
            cam.transform.localRotation = Quaternion.Lerp(sideViewTr.localRotation, rearViewTr.localRotation, val);

            if (val >= 1.0f)
            {
                break;
            }

            yield return null;
        }

        cam.transform.localPosition = rearViewTr.localPosition;
        cam.transform.localRotation = rearViewTr.localRotation;

        isMovingCam = false;
    }
}
