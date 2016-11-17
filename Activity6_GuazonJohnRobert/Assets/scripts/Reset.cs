using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefManager.ResetPlayer(3);
	}
}
