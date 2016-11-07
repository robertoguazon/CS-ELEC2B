using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtonLoadLevel : MonoBehaviour {

	public void loadLevel(string leveltoLoad)
	{
        switch (leveltoLoad) {
            case "Level1":
            case "Level2":
            case "Level3":
                AudioController.PlayMusicBackground();
                break;
            case "LevelSelection":
                AudioController.PlayMusicMainMenu();
                break;
            case "Lose":
                AudioController.PlayMusicGameLoose();
                break;
            case "Win":
                AudioController.PlayMusicGameWin();
                break;
        }
        SceneManager.LoadScene(leveltoLoad);
	}
}
