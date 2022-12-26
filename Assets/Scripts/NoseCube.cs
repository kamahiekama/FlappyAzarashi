using System;
using System.Collections;
using System.Collections.Generic;
using TofAr.V0;
using TofAr.V0.Face;
using UnityEngine;

public class NoseCube : MonoBehaviour
{
    public RectTransform Player;

    public float gain = 10;
    public float offset = 6;

    private FaceResult faceResult;

    public Vector3 center;
    public Vector3 centerOnScreen;

    // Start is called before the first frame update
    void Start()
    {
        TofArFaceManager.OnFrameArrived += FaceDetected;
    }

    private void FaceDetected(object sender)
    {
        TofArFaceManager mgr = (TofArFaceManager)sender;

        foreach (FaceResult fr in mgr.FaceData.Data.results)
        {
            faceResult = fr;
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (faceResult == null)
        {
            return;
        }

        TofArTransform facePose = faceResult.pose;
        Vector3 pos = (Vector3)facePose.position;
        Quaternion rot = (Quaternion)facePose.rotation;

        TofArVector3[] vs = faceResult.vertices;
        Vector3 top = (Vector3)vs[23];
        Vector3 bottom = (Vector3)vs[27];

        center = rot * ((top + bottom) / 2) + pos;

        centerOnScreen = Camera.main.WorldToScreenPoint(center + Camera.main.transform.position);

        /*
        centerOnScreen = centerOnScreen / centerOnScreen.z * 0.2f;
        centerOnScreen.x = 0;

        centerOnScreen.y *= gain;
        centerOnScreen.y += offset;
        //*/

        Player.anchoredPosition = centerOnScreen;
    }
}
