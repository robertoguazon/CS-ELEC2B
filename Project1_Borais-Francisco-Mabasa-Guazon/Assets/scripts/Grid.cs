using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    //grid properties
    [SerializeField] private GameObject cell;
    [SerializeField] private float offsetX = 0;
    [SerializeField] private float offsetY = 0;

    private SpriteRenderer _cellSpriteRenderer;
    private float _cellWidth;
    private float _cellHeight;


    public int x = 0;
    public int y = 0;
    public int rows = 9;
    public int cols = 9;

    private GameObject[,] _cells;
    private float _gridWidth;
    private float _gridHeight;

	// Use this for initialization
	void Start () {
        if (cell == null) {
            Debug.Log("GameObject cell: null");
        } else {
            _cellSpriteRenderer = cell.GetComponent<SpriteRenderer>();
            _cellWidth = _cellSpriteRenderer.sprite.bounds.size.x;
            _cellHeight = _cellSpriteRenderer.sprite.bounds.size.y;

            _cells = new GameObject[rows, cols];

            _gridWidth = cols * _cellWidth + offsetX;
            _gridHeight = rows * _cellHeight + offsetY;
            Debug.Log("cell width: " + _cellWidth + ", cell height: " + _cellHeight);
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject newCell = Instantiate(cell) as GameObject;
                _cells[row, col] = newCell;
                newCell.transform.parent = this.transform;
                newCell.transform.position =  new Vector3(col * _cellWidth + offsetX, row * _cellHeight + offsetY, 0);
            }
        }

        //fix grid position to center
        this.transform.position = new Vector3(this.transform.position.x - _gridWidth / 2 + _cellWidth / 2, - _gridHeight / 2 + _cellHeight / 2);
    }

    public int GetSize() {
        return rows * cols;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public Sprite GetSpriteAt(int row, int col) {
        GameObject cell = _cells[row, col];
        if (cell.transform.childCount > 0) {
            return _cells[row, col].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }

        return null;
    }

    public int GetRows() {
        return rows;
    }               
    
    public int GetCols() {
        return cols;
    }         

    public GameObject GetCellAt(int row, int col) {
        return _cells[row, col];
    }
}
