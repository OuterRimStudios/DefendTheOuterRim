using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public MenuVariableHandler menuVariableHandler;

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}

[System.Serializable]
public class MenuVariableHandler
{
    public Button campaignButton;
    public Button cBackButton;
    public TextMeshProUGUI cAddPlayersText;
    public Button endlessButton;
    public Button eBackButton;
    public TextMeshProUGUI eAddPlayersText;

    public ChangeCameraValues onePlayer;
    public ChangeCameraValues twoPlayers;
    public ChangeCameraValues threePlayers;
    public ChangeCameraValues fourPlayers;
}