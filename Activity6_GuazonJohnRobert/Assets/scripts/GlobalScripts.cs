using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalScripts : MonoBehaviour {

    public void Start() {
        switch (SceneManager.GetActiveScene().name) {
            case "MainMenu":
                AudioController.PlayMusicMainMenu();
                break;
            case "Level1":
            case "Level2":
            case "Level3":
                AudioController.PlayMusicBackground();
                break;
            case "Lose":
                AudioController.PlayMusicGameLoose();
                break;
            case "Win":
                AudioController.PlayMusicGameWin();
                break;
        }
    }
    
}
