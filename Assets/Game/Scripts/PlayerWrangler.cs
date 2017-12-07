using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrangler : MonoBehaviour {

    public static GameObject PlayerOne { get; private set; }
    public static GameObject PlayerTwo { get; private set; }

    public GameObject playerOne;
    public GameObject playerTwo;

    void Start()
    {
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
    }

    public static GameObject GetPlayer(int playerID)
    {
        switch(playerID)
        {
            case 0:
                return PlayerOne;
            case 1:
                return PlayerTwo;
            default:
                return null;
        }
    }
}