using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Normal.Realtime;

public class RequestGrabOwnership : MonoBehaviour
{
    public void OnGrab(Grabbable grabble)
    {
        RealtimeTransform rtTransform = grabble.GetComponent<RealtimeTransform>();
        if (rtTransform != null) rtTransform.RequestOwnership();
    }
}
