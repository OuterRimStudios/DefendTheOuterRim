using UnityEngine;
using Rewired;

public class PlayerInput : MonoBehaviour 
{
    public int playerID = 0;

    private Player player;
    private Vector3 cursorVector;
    private Vector2 radialVector;
    private bool raidialActive;

    public CursorMovement cursor;

	[HideInInspector]
	public bool thrust;
    private bool fire;
    private bool aim;

    private bool dodgeRight;
    private bool dodgeLeft;

    private bool skipText;
    private bool pause;
    private bool back;
    private bool confirm;

    [System.NonSerialized] // Don't serialize this so the value is lost on an editor script recompile.
    private bool initialized;

    PlayerMovement playerMovement;
    PlayerAnimator playerAnimator;

    private void Awake()
    {
        GetReferences();
    }

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        player = ReInput.players.GetPlayer(playerID);
        initialized = true;
    }

    void GetReferences()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<PlayerAnimator>();

        if(playerMovement)
            playerMovement.playerID = playerID;
    }

    void Update ()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!initialized) Initialize();

        RecieveInput();

        //cursor.MoveCursor(cursorVector.x, cursorVector.y, player.controllers.hasMouse);

        if(playerMovement)
            playerMovement.Move(cursorVector, thrust, player.controllers.hasMouse);

        if(playerAnimator)
        {
            playerAnimator.PlayerMovement(cursorVector.x, cursorVector.y);
            playerAnimator.PlayerAcceleration(thrust);


            if (cursorVector == Vector3.zero)
                playerAnimator.IsIdle(true);
            else
                playerAnimator.IsIdle(false);

            if (fire)
                playerAnimator.Fire();

        }
        //print("Player ID: " + playerID + ". ReticleX: " + player.GetAxis("ReticleX"));
        //print("Player ID: " + playerID + ". ReticleY: " + player.GetAxis("ReticleY"));
        //print("Player ID: " + playerID + ". Horizontal2: " + player.GetAxis("Horizontal2"));
        //print("Player ID: " + playerID + ". Verticle2: " + player.GetAxis("Verticle2"));
        //print("Player ID: " + playerID + ". Thrust: " + player.GetAxis("Thrust"));
        //print("Player ID: " + playerID + ". Fire: " + player.GetAxis("Fire"));
        //print("Player ID: " + playerID + ". Aim: " + player.GetAxis("Aim"));
        //print("Player ID: " + playerID + ". DodgeRight: " + player.GetAxis("DodgeRight"));
        //print("Player ID: " + playerID + ". DodgeLeft: " + player.GetAxis("DodgeLeft"));
        //print("Player ID: " + playerID + ". SkipText: " + player.GetAxis("SkipText"));
        //print("Player ID: " + playerID + ". Pause: " + player.GetAxis("Pause"));
        //print("Player ID: " + playerID + ". Back: " + player.GetAxis("Back"));
        //print("Player ID: " + playerID + ". Confirm: " + player.GetAxis("Confirm"));
    }

    void RecieveInput()
    {
        cursorVector = new Vector3(player.GetAxis("ReticleX"), player.GetAxis("ReticleY"),0);

        radialVector = new Vector2(player.GetAxis("Horizontal2"), player.GetAxis("Vertical2"));

        if (radialVector != Vector2.zero)
            raidialActive = true;
        else
            raidialActive = false;

        thrust = player.GetButton("Thrust");
        fire = player.GetButton("Fire");
        aim = player.GetButton("Aim");

        dodgeLeft = player.GetButton("DodgeLeft");
        dodgeRight = player.GetButton("DodgeRight");

        skipText = player.GetButton("SkipText");
        pause = player.GetButton("Pause");
        back = player.GetButton("Back");
        confirm = player.GetButton("Confirm");
    }
}
