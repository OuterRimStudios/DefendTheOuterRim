using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInput : MonoBehaviour {

    public int playerID = 0;

    private Player player;
    private Vector2 cursorVector;
    private Vector2 movementVector;
    private bool fire;
    private bool thrust;

    [System.NonSerialized] // Don't serialize this so the value is lost on an editor script recompile.
    private bool initialized;

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        player = ReInput.players.GetPlayer(playerID);
        initialized = true;
    }

    void Update () {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!initialized) Initialize();

        print("Player ID: " + playerID + ". Horizontal: " + player.GetAxis("Horizontal"));        
    }
}
