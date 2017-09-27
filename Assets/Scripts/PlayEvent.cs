using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEvent : MonoBehaviour {

    PlayEventType currentEvent;
    bool isPlay = false;
    bool isSlowMotion = false;

    float playTime = 0.0f;

    GameObject pos1;
    GameObject pos2;
    GameObject pos3;
    GameObject pos4;
    GameObject pos5;

    List<Vector3> originPosList = new List<Vector3>();
    List<GameObject> objList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        pos1 = GameObject.Find("RaidPos_1");
        pos2 = GameObject.Find("RaidPos_2");
        pos3 = GameObject.Find("RaidPos_3");
        pos4 = GameObject.Find("RaidPos_4");
        pos5 = GameObject.Find("RaidPos_5");

        objList.Add(pos1);
        objList.Add(pos2);
        objList.Add(pos3);
        objList.Add(pos4);
        objList.Add(pos5);

        originPosList.Add(pos1.transform.GetChild(0).localPosition);
        originPosList.Add(pos2.transform.GetChild(0).localPosition);
        originPosList.Add(pos3.transform.GetChild(0).localPosition);
        originPosList.Add(pos4.transform.GetChild(0).localPosition);
        originPosList.Add(pos5.transform.GetChild(0).localPosition);

        for (int i = 0; i < objList.Count; i++)
        {
            objList[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPlay)
            return;

        if (currentEvent == PlayEventType.EnemyRaid)
        {
            playTime += Time.deltaTime * 2;

            for (int i = 0; i < objList.Count; i++)
            {
                objList[i].transform.GetChild(0).localPosition = Vector3.Lerp(originPosList[i], Vector3.zero, playTime);
            }

            if (!isSlowMotion)
            {
                if (playTime > 0.9f)
                {
                    isSlowMotion = true;
                    Time.timeScale = 0.03f;
                }
            }

            if (playTime >= 1.0f)
            {
                Time.timeScale = 1.0f;
                isPlay = false;
            }
        }
	}


    public void StartEvent(PlayEventType type)
    {
        if (isPlay)
            return;

        playTime = 0.0f;
        isSlowMotion = false;

        currentEvent = type;
        isPlay = true;

        for (int i = 0; i < objList.Count; i++)
        {
            objList[i].SetActive(true);
            objList[i].transform.GetChild(0).localPosition = originPosList[i];
        }
    }

    public void Reset()
    {
        for (int i = 0; i < objList.Count; i++)
        {
            objList[i].SetActive(false);
            objList[i].transform.GetChild(0).localPosition = originPosList[i];
        }
    }
}
