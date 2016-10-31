﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    //chips
    [SerializeField]
    private GameObject prefabRedChip;
    [SerializeField]
    private GameObject prefabBlueChip;
    [SerializeField]
    private GameObject prefabGrid;
    [SerializeField]
    private GameObject rectangleChecker;
    [SerializeField]
    private GameObject triangleChecker;
    [SerializeField]
    private GameObject prefabLoading;

    //ui s
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Image scoreboard;
    [SerializeField]
    private Text redRectangleText;
    [SerializeField]
    private Text redTriangleText;
    [SerializeField]
    private Text blueRectangleText;
    [SerializeField]
    private Text blueTriangleText;
    [SerializeField]
    private Animator scoreboardAnimator;
    [SerializeField]
    private Animator resetButtonAnimator;

    private static GameObject _redChip;
    private static GameObject _blueChip;

    private static GameObject _grid;
    private static Grid _gridScript;
    private static int _available;

    private static bool _playerRed = true; //if false then player blue

    private static GameObject _loading;

    private static GameController _gameController;

    //score board
    private static Canvas _canvas;
    private static Image _scoreboard;
    private static Text _redRectangleText;
    private static Text _redTriangleText;
    private static Text _blueRectangleText;
    private static Text _blueTriangleText;
    private static Animator _scoreboardAnimator;

    //reset button
    private static Animator _resetButtonAnimator;


    //checkers
    private static Checker _rectangleChecker;
    private static Checker _triangleChecker;

    //sprites
    private static Sprite _redSprite;
    private static Sprite _blueSprite;
    private static Sprite _playerSprite;

    //sprites
    private static int _turn;
    private static int _startAdjacentTurn;

    private static List<GameObject> _availableCells;
    private static List<GameObject> _placedCells;

    public static readonly int[,] adjacents = {
        {-1,-1}, { 0,-1}, { 1,-1},
        {-1, 0},          { 1, 0},
        {-1, 1}, { 0, 1}, { 1, 1 } 
    };


    public void Start() {
        _redChip = prefabRedChip;
        _blueChip = prefabBlueChip;
        _grid = Instantiate(prefabGrid) as GameObject;
        _gridScript = _grid.GetComponent<Grid>();
        _available = _gridScript.GetSize();

        _gameController = this;

        //sample
        _rectangleChecker = rectangleChecker.GetComponent<Checker>();
        _triangleChecker = triangleChecker.GetComponent<Checker>();

        _loading = Instantiate(prefabLoading) as GameObject;
        _loading.SetActive(false);
        _loading.transform.position = Vector3.zero;

        _canvas = canvas;
        _scoreboard = scoreboard;
        _redRectangleText = redRectangleText;
        _redTriangleText = redTriangleText;
        _blueRectangleText = blueRectangleText;
        _blueTriangleText = blueTriangleText;

        _scoreboardAnimator = scoreboardAnimator;
        _resetButtonAnimator = resetButtonAnimator;

        _canvas.enabled = false;

        _redSprite = _redChip.GetComponent<SpriteRenderer>().sprite;
        _blueSprite = _blueChip.GetComponent<SpriteRenderer>().sprite;
        _playerSprite = _redSprite;

        _turn = 1;
        _startAdjacentTurn = 3;

        _availableCells = new List<GameObject>();
        for (int row = 0; row < _gridScript.GetRows(); row++)
        {
            for (int col = 0; col < _gridScript.GetCols(); col++)
            {
                _availableCells.Add(_gridScript.GetCellAt(row, col));
            }
        }

        _placedCells = new List<GameObject>();
    }

    void Awake() {
      
    }

    private static bool HasChip(GameObject cell) {

        if (cell.transform.childCount > 0) {
            foreach (Transform child in cell.transform) {
                if (child.tag == "Chip") return true;
            }
        }

        return false;
    }

    private static bool HasAdjacent(GameObject cell, Sprite cellSprite) {

        Cell cellCode = cell.GetComponent<Cell>();
        int cellRow = cellCode.Row;
        int cellCol = cellCode.Col;
        
        for (int i = 0; i < adjacents.GetLength(0); i++) {
            if (CheckCellAdjacent(cellRow + adjacents[i, 1], cellCol + adjacents[i, 0], cellSprite)) {
                return true;
            }
        }

        return false;
    }

    private static bool CheckCellAdjacent(int row, int col, Sprite sprite) {

        if (row < 0 || col < 0 || row >= _gridScript.GetRows() || col >= _gridScript.GetCols() ) return false;

        Sprite targetSprite = _gridScript.GetChipSpriteAt(row, col);
        if (targetSprite != null) {
            if (targetSprite == sprite)
            {
                return true;
            }

        }

        return false;
    }

    public static void PlaceChip(GameObject cell)
    {
        
        
        if (HasChip(cell)) return; // exit if the cell already has chip
        //Debug.Log("No chip");
        if (_turn >= _startAdjacentTurn) {
            //Debug.Log("Check adjacent");
            if (!HasAdjacent(cell,_playerSprite)) return; // check if has adjacent then be able to place chip
            //Debug.Log("Has adjacent");
        }

        _turn++; //add turn after placing chip

        GameObject newChip;
        if (_playerRed)
        {
            newChip = Instantiate(_redChip) as GameObject;
        }
        else
        {
            newChip = Instantiate(_blueChip) as GameObject;
        }

        newChip.GetComponent<SpriteRenderer>().sortingLayerName = "Floating Chip";
        newChip.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, cell.transform.position.z - 1);
        newChip.transform.SetParent(cell.transform);
        newChip.transform.localScale = new Vector3(1f, 1f, 1f);

        //-1 available cell
        _available--;
        ComputeAvailableCells(cell);
        //Debug.Log("Available cells: " + _availableCells.Count);
        //Debug.Log(_available);

        //change turns
        ChangeTurn();

        //play audio
        Audio.PlayDropChip();

        //add to placed cells
        _placedCells.Add(cell);
    }

    public static bool IsAvailable (Sprite sprite) {

        foreach(GameObject cell in _availableCells) {
            if (HasAdjacent(cell, sprite)) {
                return true;
            }
        }

        return false;
    }

    public static void ComputeAvailableCells(GameObject cell) {

        _availableCells.Remove(cell);

        Debug.Log("_availableCells: " + _availableCells.Count);
    }

    private static void ChangeTurn()
    {
        _playerRed = !_playerRed;
        if (_playerRed)
        {
            _playerSprite = _redSprite;
        }
        else
        {
            _playerSprite = _blueSprite;
        }

        if (_available <= 0 || (_turn >= _startAdjacentTurn && !IsAvailable(_playerSprite)))
        {
            if (_available > 0) {
                _gameController.StartCoroutine(FillSpaces(1f)); //TODO
            }
            _gameController.StartCoroutine(CheckWinner(1.5f));
        }

        Debug.Log("Last turn: " + (_playerRed ? "Red" : "Blue"));
    }

    private static IEnumerator FillSpaces(float delay) {

        yield return new WaitForSeconds(delay);

        GameObject gameObjectChip;
        if (!_playerRed) //last wins
        {
            gameObjectChip = _redChip;
        }
        else
        {
            gameObjectChip = _blueChip;
        }


        foreach (GameObject cellGameObject in _availableCells) {
            GameObject chip = Instantiate(gameObjectChip) as GameObject;
            FillChip(cellGameObject,chip);
        }
        Audio.PlayDropChip();
        _availableCells.Clear(); //clear all cells
    }

    private static void FillChip(GameObject cell, GameObject chip) {


        chip.GetComponent<SpriteRenderer>().sortingLayerName = "Floating Chip";
        chip.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, cell.transform.position.z - 1);
        chip.transform.SetParent(cell.transform);
        chip.transform.localScale = new Vector3(1f, 1f, 1f);

        _available--;
        _placedCells.Add(cell);
    }


    private static IEnumerator CheckWinner(float seconds) {
        _canvas.enabled = true;
        _scoreboardAnimator.SetTrigger("Appear");
        _resetButtonAnimator.SetTrigger("Appear");

        //start checking animation
        _loading.SetActive(true);
        yield return new WaitForSeconds(seconds);

        //check using checker
        _rectangleChecker.CheckPlacedCells(_gridScript, _placedCells);

        float rectangleRed = _rectangleChecker.GetRedScore();
        float rectangleBlue = _rectangleChecker.GetBlueScore();

        //place texts
        _redRectangleText.text = "" + rectangleRed;
        _blueRectangleText.text = "" + rectangleBlue;

        //if tie calculate triangle
        if (rectangleBlue == rectangleRed) {
            _triangleChecker.CheckPlacedCells(_gridScript, _placedCells);
            _redTriangleText.text = "" + _triangleChecker.GetRedScore();
            _blueTriangleText.text = "" + _triangleChecker.GetBlueScore();
        } else {
            _redTriangleText.text = "?";
            _blueTriangleText.text = "?";
        }

        //stop checking animation
        _loading.SetActive(false);        
    }

    public static string GetGameLevel() {
        return SceneManager.GetActiveScene().name;
    }

    public static bool IsPlayerRed() {
        return _playerRed;
    }
}
