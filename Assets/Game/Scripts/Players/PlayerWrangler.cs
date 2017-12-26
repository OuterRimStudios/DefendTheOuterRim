using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWrangler : MonoBehaviour
{
    public static List<GameObject> players = new List<GameObject>();
    public static int playerCount = 1;

    public GameObject playerPrefab;
    public Vector3 spawnOffset = new Vector3(0, 0, 5);

    Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;

        for(int i = 0; i < playerCount; i++)
        {
            GameObject player = Instantiate(playerPrefab, mainCam.transform.position + spawnOffset, mainCam.transform.rotation, mainCam.transform) as GameObject;
            players.Add(player);
            SetPlayerInformation(player);
        }
    }

    void SetPlayerInformation(GameObject player)
    {
        int playerNumber = players.IndexOf(player);
        player.GetComponent<PlayerInput>().playerID = playerNumber;

        PlayerReferences playerRefs = player.GetComponent<PlayerReferences>();

        switch(playerNumber)
        {
            case 0:
                playerRefs.playerReticle.color = Color.blue;
                playerRefs.playerArrow.color = Color.blue;
                playerRefs.playerNumberTag.color = Color.blue;
                playerRefs.playerNumberTag.text = "P1";
                break;
            case 1:
                playerRefs.playerReticle.color = Color.red;
                playerRefs.playerArrow.color = Color.red;
                playerRefs.playerNumberTag.color = Color.red;
                playerRefs.playerNumberTag.text = "P2";
                break;
            case 2:
                playerRefs.playerReticle.color = Color.yellow;
                playerRefs.playerArrow.color = Color.yellow;
                playerRefs.playerNumberTag.color = Color.yellow;
                playerRefs.playerNumberTag.text = "P3";
                break;
            case 3:
                playerRefs.playerReticle.color = Color.green;
                playerRefs.playerArrow.color = Color.green;
                playerRefs.playerNumberTag.color = Color.green;
                playerRefs.playerNumberTag.text = "P4";
                break;
        }
    }

    public static GameObject GetPlayer(int playerID)
    {
        return players[playerID];
    }
}