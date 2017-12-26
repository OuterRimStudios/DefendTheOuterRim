using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;
using TMPro;

public class AssignControls : MonoBehaviour {

    bool keyboardAssigned;
    bool checkForInput;

    Menu menu;

    Button campaignButton;
    Button cBackButton;
    TextMeshProUGUI cAddPlayersText;

    Button endlessButton;
    Button eBackButton;
    TextMeshProUGUI eAddPlayersText;

    [HideInInspector]
    public List<bool> controlsAssigned;

    int controllerInUse; //False if this is 1

    void Awake()
    {
        menu = GameObject.Find("Manager").GetComponent<Menu>();
        SetButtons();
    }

    void OnLevelWasLoaded(int index)
    {
        if(index == 0)
        {
            Menu menu = GameObject.Find("Manager").GetComponent<Menu>();
            SetButtons();
        }
    }

    void SetButtons()
    {
        campaignButton = menu.menuVariableHandler.campaignButton;
        campaignButton.onClick.AddListener(ToggleCheckForInput);
        cBackButton = menu.menuVariableHandler.cBackButton;
        cBackButton.onClick.AddListener(ToggleCheckForInput);
        cAddPlayersText = menu.menuVariableHandler.cAddPlayersText;

        endlessButton = menu.menuVariableHandler.endlessButton;
        endlessButton.onClick.AddListener(ToggleCheckForInput);
        eBackButton = menu.menuVariableHandler.eBackButton;
        eBackButton.onClick.AddListener(ToggleCheckForInput);
        eAddPlayersText = menu.menuVariableHandler.eAddPlayersText;
    }

    void ToggleCheckForInput()
    {
        checkForInput = !checkForInput;
    }

    void Update()
    {
        if (!ReInput.isReady) return;
        AssignControl();

        if (Input.GetKeyDown(KeyCode.E))
        {
            IList<Player> players = ReInput.players.Players;
            for (int i = 0; i < players.Count; i++)
            {
                print("Player " + i + " Joystick Count: " + players[i].controllers.joystickCount);
            }
        }
    }
    void AssignControl()
    {
        if(controlsAssigned.Count < 1)
        {
            Player playerOne = ReInput.players.GetPlayer(0);

            if (PlatformManager.systemType == PlatformManager.SystemType.PC)
            {

                for (int i = 0; i < ReInput.players.Players.Count; i++)
                {
                    ReInput.players.Players[i].controllers.hasKeyboard = false;
                    ReInput.players.Players[i].controllers.hasMouse = false;
                }

                controllerInUse = 1;

                playerOne.controllers.hasKeyboard = true;
                playerOne.controllers.hasMouse = true;
                playerOne.controllers.excludeFromControllerAutoAssignment = true;

                controlsAssigned.Add(true);
                UpdatePlayerCount();

                keyboardAssigned = true;
            }
            else if(PlatformManager.systemType == PlatformManager.SystemType.XBOX || PlatformManager.systemType == PlatformManager.SystemType.PS4)
            {
                // Assign the joystick to this Player
                playerOne.controllers.AddController(ReInput.controllers.Joysticks[0], false);

                controlsAssigned.Add(true);
                UpdatePlayerCount();
            }
        }

        if (checkForInput)
        {

            //if (!keyboardAssigned)
            //{
            //    if (ReInput.controllers.Keyboard.GetAnyButtonDown() || ReInput.controllers.Mouse.GetAnyButtonDown())
            //    {
            //        Player player = FindPlayerWithoutJoystick();
            //        if (player == null) return;

            //        

            //    }
            //}

            IList<Joystick> joysticks = ReInput.controllers.Joysticks;
            for (int i = 0 + controllerInUse; i < joysticks.Count; i++)
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
                    controlsAssigned.Add(true);
                    UpdatePlayerCount();
                }
            }

            if (DoAllPlayersHaveJoysticks())
            {
                ReInput.configuration.autoAssignJoysticks = true;
                this.enabled = false; // disable this script
            }

            CheckForInput();
        }
    }

    // Searches all Players to find the next Player without a Joystick assigned
    private Player FindPlayerWithoutJoystick()
    {
        IList<Player> players = ReInput.players.Players;
        for (int i = 0 + controllerInUse; i < players.Count; i++)
        {
            print(players[i].controllers.joystickCount);
            if (players[i].controllers.joystickCount > 0) continue;
            return players[i];
        }
        return null;
    }

    private bool DoAllPlayersHaveJoysticks()
    {
        return FindPlayerWithoutJoystick() == null;
    }

    void UpdatePlayerCount()
    {
        PlayerWrangler.playerCount = controlsAssigned.Count;
        if(controlsAssigned.Count == 4)
        {
            cAddPlayersText.gameObject.SetActive(false);
            eAddPlayersText.gameObject.SetActive(false);
        }

        cAddPlayersText.text = "Player " + (controlsAssigned.Count + 1) + " press any button to join.";
        eAddPlayersText.text = "Player " + (controlsAssigned.Count + 1) + " press any button to join.";

        if (menu == null) return;
        switch (controlsAssigned.Count)
        {
            case 1:
                menu.menuVariableHandler.onePlayer.ChangeCameraPosition();
                menu.menuVariableHandler.onePlayer.ChangeCameraRotation();
                break;
            case 2:
                menu.menuVariableHandler.twoPlayers.ChangeCameraPosition();
                menu.menuVariableHandler.twoPlayers.ChangeCameraRotation();
                break;
            case 3:
                menu.menuVariableHandler.threePlayers.ChangeCameraPosition();
                menu.menuVariableHandler.threePlayers.ChangeCameraRotation();
                break;
            case 4:
                menu.menuVariableHandler.fourPlayers.ChangeCameraPosition();
                menu.menuVariableHandler.fourPlayers.ChangeCameraRotation();
                break;
            default:
                break;
        }
    }

    void CheckForInput()
    {
        Player playerOne = ReInput.players.GetPlayer(0);
        Player playerTwo = ReInput.players.GetPlayer(1);
        Player playerThree = ReInput.players.GetPlayer(2);
        Player playerFour = ReInput.players.GetPlayer(3);

        if (playerTwo.GetButtonDown("Back"))
        {
            print("Unassigning Controls for Player " + playerTwo.descriptiveName);
            controlsAssigned.Remove(controlsAssigned[1]);
            UnAssignController(playerTwo);
        }

        if (playerThree.GetButtonDown("Back"))
        {
            print("Unassigning Controls for Player " + playerThree.descriptiveName);
            controlsAssigned.Remove(controlsAssigned[2]);
            UnAssignController(playerThree);
        }

        if (playerFour.GetButtonDown("Back"))
        {
            print("Unassigning Controls for Player " + playerFour.descriptiveName);
            controlsAssigned.Remove(controlsAssigned[3]);
            UnAssignController(playerFour);
        }
    }

    void UnAssignController(Player player)
    {
        UpdatePlayerCount();
        player.controllers.RemoveController(player.controllers.Joysticks[0]);
        //player.controllers.ClearLastActiveControllerChangedDelegates();

           // player.controllers.RemoveController(player.controllers.GetController(ControllerType.Joystick, player.id));
       // player.controllers.hasKeyboard = false;
       // player.controllers.hasMouse = false;
    }
}