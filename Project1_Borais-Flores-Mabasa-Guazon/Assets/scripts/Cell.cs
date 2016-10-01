using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    [SerializeField] private Sprite blankSprite;

    private SpriteRenderer _spriteRenderer;

	void Start () {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (blankSprite == null) {
            Debug.Log("Sprite blankSprite: null");
        } else {
            _spriteRenderer.sprite = blankSprite;
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
