using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	public void ResetGame() {
        SceneManager.LoadScene(GameController.GetGameLevel());
    }
}
