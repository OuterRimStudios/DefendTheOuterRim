using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class SharedInput : MonoBehaviour 
{
	Player playerOne;
	Player playerTwo;

	PlayerInput playerOneInput;
	PlayerInput playerTwoInput;

	bool initialized;   

	void Initialize () 
	{
		playerOne = ReInput.players.GetPlayer (0);
		playerTwo = ReInput.players.GetPlayer (1);

		playerOneInput = GameObject.Find ("PlayerOne").GetComponent<PlayerInput> ();

		if(GameObject.Find("PlayerTwo") != null)
			playerTwoInput = GameObject.Find ("PlayerTwo").GetComponent<PlayerInput> ();
	}

	void Update () 
	{
		if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
		if (!initialized) Initialize();

		if (playerOne.GetButton ("Thrust") || playerTwo.GetButton ("Thrust")) 
		{
			playerOneInput.thrust = true;
			playerTwoInput.thrust = true;
		} 
		else 
		{
			playerOneInput.thrust = false;
			playerTwoInput.thrust = false;
		}
	}
}
