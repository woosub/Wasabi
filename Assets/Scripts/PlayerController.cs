using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    CameraControl cameraControl;
    PlayEvent playEvent;
    Character character;

	// Use this for initialization
	void Start () {
        cameraControl = GetComponentInChildren<CameraControl>();
        character = GetComponentInChildren<Character>();
        playEvent = GetComponent<PlayEvent>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    cameraControl.ChangeCameraView(CameraView.Rear);

        //    cameraControl.ChangeFov(38, 2);
        //    playEvent.StartEvent(PlayEventType.EnemyRaid);

        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    cameraControl.ChangeCameraView(CameraView.Side);
        //    cameraControl.ReturnFov();
        //    playEvent.Reset();
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            character.Attack();
        }
    }

   
}
