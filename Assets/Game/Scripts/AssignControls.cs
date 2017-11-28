using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class AssignControls : MonoBehaviour {

    bool keyboardAssigned;

	void Update () {
        if (!ReInput.isReady) return;
        AssignControl();
	}

    void AssignControl()
    {
        print("AssignControl start");
        if(!keyboardAssigned)
        {
            print("keyboard not assigned");

            if(ReInput.controllers.Keyboard.GetAnyButtonDown())
            {
                Player player = FindPlayerWithoutJoystick();
                if (player == null) return;

                for(int i = 0; i < ReInput.players.Players.Count; i++)
                {
                    ReInput.players.Players[i].controllers.hasKeyboard = false;
                    ReInput.players.Players[i].controllers.hasMouse = false;
                }

                player.controllers.hasKeyboard = true;
                player.controllers.hasMouse = true;
                player.controllers.excludeFromControllerAutoAssignment = true;

                keyboardAssigned = true;
            }
        }

        IList<Joystick> joysticks = ReInput.controllers.Joysticks;
        for (int i = 0; i < joysticks.Count; i++)
        {
            Joystick joystick = joysticks[i];
            if (ReInput.controllers.IsControllerAssigned(joystick.type, joystick.id)) continue; // joystick is already assigned to a Player

            // Chec if a button was pressed on the joystick
            if (joystick.GetAnyButtonDown())
            {

                // Find the next Player without a Joystick
                Player player = FindPlayerWithoutJoystick();
                if (player == null) return; // no free joysticks

                // Assign the joystick to this Player
                player.controllers.AddController(joystick, false);
            }
        }

        if (DoAllPlayersHaveJoysticks())
        {
            ReInput.configuration.autoAssignJoysticks = true;
            this.enabled = false; // disable this script
        }
    }

    // Searches all Players to find the next Player without a Joystick assigned
    private Player FindPlayerWithoutJoystick()
    {
        IList<Player> players = ReInput.players.Players;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].controllers.joystickCount > 0) continue;
            return players[i];
        }
        return null;
    }

    private bool DoAllPlayersHaveJoysticks()
    {
        return FindPlayerWithoutJoystick() == null;
    }
}
