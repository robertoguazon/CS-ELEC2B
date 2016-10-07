using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    [SerializeField]
    private Sprite blankSprite;
    [SerializeField]
    private Color blankColor;
    [SerializeField]
    private Color highlightColor;

    private SpriteRenderer _spriteRenderer;

	void Start () {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (blankSprite == null) {
            Debug.Log("Sprite blankSprite: null");
        } else {
            _spriteRenderer.sprite = blankSprite;
        }

        if (blankColor == null)
        {
            Debug.Log("Color blankColor: null");
        } else {
            _spriteRenderer.color = new Color(blankColor.r, blankColor.g, blankColor.b);
        }

        if (highlightColor == null) {
            Debug.Log("Color highlightColor: null");
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown() {
        //access the method from parent
        Grid grid = GetComponentInParent<Grid>();
        GameController.PlaceChip(this.gameObject);
    }

    void OnMouseEnter() {
        transform.localScale = new Vector3(1.3f,1.3f,1.3f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

        _spriteRenderer.color = new Color(highlightColor.r,highlightColor.g,highlightColor.b);
    }

    void OnMouseExit() {
        transform.localScale = new Vector3(1f,1f,1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

        _spriteRenderer.color = new Color(blankColor.r, blankColor.g, blankColor.b);
    }

}
