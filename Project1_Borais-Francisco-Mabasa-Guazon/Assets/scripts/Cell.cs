using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    [SerializeField]
    private Sprite blankSprite;
    [SerializeField]
    private Color blankColor;

    [SerializeField]
    private Color highlightRedColor = new Color(0.9f, 0.5f, 0.5f, 1);
    [SerializeField]
    private Color highlightBlueColor = new Color(0.61f, 0.61f, 0.96f, 1);

    public int Row { get; set; }
    public int Col { get; set; }

    private SpriteRenderer _spriteRenderer;
    private Color _defaultSpriteRendererColor;

    private bool _checked;

    void Start () {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSpriteRendererColor = this._spriteRenderer.color;

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

        _checked = false;
	}

    public bool isChecked() {
        return _checked;
    }

    public void check() {
        _checked = true;
    }

    void OnMouseDown() {
        //access the method from parent
        Grid grid = GetComponentInParent<Grid>();
        GameController.PlaceChip(this.gameObject);
    }

    void OnMouseEnter() {
       

        transform.localScale = new Vector3(1.3f,1.3f,1.3f);
        _spriteRenderer.sortingLayerName = "Floating Cell";
        if (this.transform.childCount > 0){
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Floating Chip";
        }

        if (GameController.IsPlayerRed()) {
            _spriteRenderer.color = new Color(highlightRedColor.r, highlightRedColor.g, highlightRedColor.b, highlightRedColor.a);
        } else {
            _spriteRenderer.color = new Color(highlightBlueColor.r, highlightBlueColor.g, highlightBlueColor.b, highlightBlueColor.a);
        }
    }

    void OnMouseExit() {
        transform.localScale = new Vector3(1f,1f,1f);
        _spriteRenderer.sortingLayerName = "Cell";
        if (this.transform.childCount > 0) {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Chip";
        }

        _spriteRenderer.color = new Color(blankColor.r, blankColor.g, blankColor.b);
    }

    public Sprite GetChipSprite() {
        return transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }

    public Sprite GetSprite() {
        return _spriteRenderer.sprite;
    }
}
