using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	public void ResetGame() {
        SceneManager.LoadScene(GameController.GetGameLevel());
    }

    public void playGame() {
        SceneManager.LoadScene("Game");
    }

    public void exitGame() {
        Application.Quit();
    }

    public void GoBackMenu() {
        SceneManager.LoadScene("Menu");
    }

}
