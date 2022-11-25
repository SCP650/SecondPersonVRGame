using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using BNG;

public class SwapHeads : MonoBehaviour
{
    public Camera localCamera;
    public Camera remoteCamera;
    public GameObject playerController;
    private NetworkedPlayerManager playerManager;
    private bool shouldMove;
    private bool inFisrtPersonView = true;
    private void Awake()
    {
        playerManager = GetComponent<NetworkedPlayerManager>();
        playerManager.avatarCreated += OnPlayerJoin;
   
    }
    private void Update()
    {
        if (shouldMove)
        {
            playerController.transform.position = new Vector3(0, 0, 3);
            playerController.transform.rotation = Quaternion.Euler(0, 180, 0);
            shouldMove = false;
        }
        if (InputBridge.Instance.AButtonDown)
        {
            ToggleFirstvsSecondPersonView();
        }
    }
    private void OnPlayerJoin(NetworkedPlayerManager avatarManager, NetworkedAvatar avatar, bool isLocalAvatar)
    {
        Debug.Log($"ppl join with count {avatarManager.avatars.Count} and is local {isLocalAvatar}");
        //Local joined late, there's already another player there, move my localtion to another place
        if (!isLocalAvatar && avatarManager.avatars.Count == 1) {
            Debug.Log("try to set position for local player");
            //for some reason setting position here won't work, will be reset back
            shouldMove = true;
            localCamera.enabled = false;
            remoteCamera = avatar.head.gameObject.AddComponent<Camera>();
            inFisrtPersonView = false;
        };
        //local joined early, wait for remote
        if (!isLocalAvatar && avatarManager.avatars.Count == 2)
        {
            Debug.Log("local joined early, wait for remote");
            localCamera.enabled = false;
            remoteCamera = avatar.head.gameObject.AddComponent<Camera>();
            inFisrtPersonView = false;
        }
    }

    private void ToggleFirstvsSecondPersonView()
    {
        if (remoteCamera == null) return;
        inFisrtPersonView = !inFisrtPersonView;

        localCamera.enabled = inFisrtPersonView ? true : false;
        remoteCamera.enabled = inFisrtPersonView ? false : true;
        
    }


}
