using UnityEngine;
using System.Collections;
using UnityEngine.UI; // include UI namespace so can reference UI elements

public class GameManager : MonoBehaviour {

	// static reference to game manager so can be called from other scripts directly (not just through gameobject component)
	public static GameManager gm;

	// private variables
	GameObject _player;
	Vector3 _spawnLocation;

	// set things up here
	void Awake () {
		// setup reference to game manager
		if (gm == null)
			gm = GetComponent<GameManager>();

		// setup all the variables, the UI, and provide errors if things not setup properly.
		setupDefaults();
	}

	// game loop
	void Update() {
				Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
	}

	// setup all the variables, the UI, and provide errors if things not setup properly.
	void setupDefaults() {
		// setup reference to player
		if (_player == null)
			_player = GameObject.FindGameObjectWithTag ("Player");
		
		if (_player == null)
			Debug.LogError ("Player not found in Game Manager");
		
		// get initial _spawnLocation based on initial position of player
		_spawnLocation = _player.transform.position;
	}

}
