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

        switch(playerNumber)
        {
            case 0:
                player.transform.Find("Reticle").GetComponent<Image>().color = Color.blue;
                player.transform.Find("PlayerTag").GetComponent<Image>().color = Color.blue;
                player.transform.Find("PlayerNumber").GetComponent<Text>().color = Color.blue;
                break;
            case 1:
                player.transform.Find("Reticle").GetComponent<Image>().color = Color.red;
                player.transform.Find("PlayerTag").GetComponent<Image>().color = Color.red;
                player.transform.Find("PlayerNumber").GetComponent<Text>().color = Color.red;
                break;
            case 2:
                player.transform.Find("Reticle").GetComponent<Image>().color = Color.yellow;
                player.transform.Find("PlayerTag").GetComponent<Image>().color = Color.yellow;
                player.transform.Find("PlayerNumber").GetComponent<Text>().color = Color.yellow;
                break;
            case 3:
                player.transform.Find("Reticle").GetComponent<Image>().color = Color.green;
                player.transform.Find("PlayerTag").GetComponent<Image>().color = Color.green;
                player.transform.Find("PlayerNumber").GetComponent<Text>().color = Color.green;
                break;
        }
    }

    public static GameObject GetPlayer(int playerID)
    {
        return players[playerID];
    }
}