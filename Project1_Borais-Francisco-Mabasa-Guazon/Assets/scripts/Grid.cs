using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    //grid properties
    [SerializeField] private GameObject cell;
    [SerializeField] private float offsetX = 0;
    [SerializeField] private float offsetY = 0;

    private SpriteRenderer _cellSpriteRenderer;
    private Vector3 _cellSize;


    public int x = 0;
    public int y = 0;
    public int rows = 9;
    public int cols = 9;

    private GameObject[,] _cells;
    private float _gridWidth;
    private float _gridHeight;

    //EXPERIMENTAL
    //experimental fitting grid to screen
    private Vector2 _cellSizeShould;
    private Vector2 _scaleShould = new Vector2(1,1);
    private float _cellScreenOffset = 0f;

    private static float _worldHeight;
    private static float _worldWidth;

    public static float getWorldWidth() {
        return _worldWidth;
    }

    public static float getWorldHeight() {
        return _worldHeight;
    }
    //END OF EXPERIMENTAL


    void Awake() {
        if (cell == null)
        {
            Debug.Log("GameObject cell: null");
        }
        else
        {
            _cellSpriteRenderer = cell.GetComponent<SpriteRenderer>();
            _cellSize = _cellSpriteRenderer.sprite.bounds.size;


            //EXPERIMENTAL
            //get the supposed to be height and width to fit screen
            _worldHeight = Camera.main.orthographicSize * 2.0f;
            _worldWidth = _worldHeight * Screen.width / Screen.height;

            float size = (_worldHeight > _worldWidth) 
                ? ((_worldWidth + _cellScreenOffset) / cols) 
                : ((_worldHeight + _cellScreenOffset) / rows);

            _cellSizeShould = new Vector3(size,size); //should be same

            _scaleShould.x = _cellSizeShould.x / _cellSize.x;
            _scaleShould.y = _cellSizeShould.y / _cellSize.y;
            Debug.Log("world width: " + _worldWidth);
            Debug.Log("world height: " + _worldHeight);
            Debug.Log("Cell size: " + _cellSize);
            Debug.Log("Cell size should: " + _cellSizeShould);    
            Debug.Log("scaleX: " + _scaleShould.x);
            Debug.Log("scaleY: " + _scaleShould.y);
            //END OF EXPERIMENTAL
         

            _cells = new GameObject[rows, cols];

            _gridWidth = cols * _cellSizeShould.x + offsetX;
            _gridHeight = rows * _cellSizeShould.y + offsetY;
            Debug.Log("cell width: " + _cellSize.x + ", cell height: " + _cellSize.y);
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject newCell = Instantiate(cell) as GameObject;
                _cells[row, col] = newCell;

                
                //EXPERIMENTAL
                newCell.transform.localScale = new Vector3(_scaleShould.x, _scaleShould.y, transform.localScale.z);
                _cellSize.x = _cellSizeShould.x;
                _cellSize.y = _cellSizeShould.y;
                //END OF EXPERIMENTAL
               


                newCell.transform.parent = this.transform;
                float x = col * _cellSize.x + offsetX;
                float y = row * _cellSize.y + offsetY;
                newCell.transform.position = new Vector3(x, y, 0);
                Debug.Log("x: " + x + ", y: " + y);

                Cell nCell = newCell.GetComponent<Cell>();
                nCell.Row = row;
                nCell.Col = col;

            }
        }
        /* EXPERIMENTAL - comment out
        //fix grid position to center
        this.transform.position = new Vector3(
            this.transform.position.x - _gridWidth / 2 + _cellSize.x / 2,
            -_worldHeight / 2 + _gridHeight / 2);
        */

        //EXPERIMENTAL
        Debug.Log("Grid width: " + _gridWidth);
        Debug.Log("Grid position before centering: " + this.transform.position);

        this.transform.position = new Vector3(
            transform.position.x - _gridWidth / 2 + _cellSizeShould.x / 2,
            transform.position.y - _gridHeight / 2 + _cellSizeShould.y / 2);


        Debug.Log("Grid position after centering: " + this.transform.position);
        //END OF EXPERIMENTAL

    }

    // Use this for initialization
    void Start () {
        
    }

    public int GetSize() {
        return rows * cols;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public Sprite GetChipSpriteAt(int row, int col) {
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
