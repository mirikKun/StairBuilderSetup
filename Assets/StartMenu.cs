using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
   [SerializeField] private string sceneName = "SampleScene";
   [SerializeField] private GameObject mainMenu;
   [SerializeField] private GameObject aboutMenu;
   public void Exit()
   {
      Application.Quit();
   }

   public void StartButton()
   {
      SceneManager.LoadScene(sceneName);
   }

   public void ToAboutMenu()
   {
      aboutMenu.SetActive(true);
      mainMenu.SetActive(false);
   }
   public void ToMainMenu()
   {
      mainMenu.SetActive(true);
      aboutMenu.SetActive(false);
   }
}
