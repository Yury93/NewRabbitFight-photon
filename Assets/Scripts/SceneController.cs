using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private string nameSceneMenu;
    [SerializeField] private string nameSceneLobby;
    [SerializeField] private string nameSceneSingleGame;
    
    public void ExitMenu()
    {
        Photon.Pun.PhotonNetwork.Disconnect();
        SceneManager.LoadScene(nameSceneMenu);
    }
    public void SceneLobby()
    {
        SceneManager.LoadScene(nameSceneLobby);
    }
    public void SceneSingleGame()
    {
        SceneManager.LoadScene(nameSceneSingleGame);
    }

}
