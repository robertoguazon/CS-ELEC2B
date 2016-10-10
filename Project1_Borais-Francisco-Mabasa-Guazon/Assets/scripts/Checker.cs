using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Checker : MonoBehaviour {

    [SerializeField]
    private Sprite spriteRedChip;
    [SerializeField]
    private Sprite spriteBlueChip;
    [SerializeField]
    private int[] checkerRows;

    private Sprite _currentChipSprite;

    private int _currentRow;
    private int _currentCol;

    private int _playerRed;
    private int _playerBlue;

    private int _gridRows;
    private int _gridCols;

    public int GetPlayerBlue() {
        return _playerBlue;
    }

    public int GetPlayerRed() {
        return _playerRed;
    }

    public void Start() {
        Array.Reverse(checkerRows);
    }

    public void CheckGrid(Grid grid) {
        _gridRows = grid.GetRows();
        _gridCols = grid.GetCols();

        Debug.Log("grid rows,cols (" + _gridRows + ", " + _gridCols);

        for (int row = 0; row < _gridRows; row++) {
            for (int col = 0; col < _gridCols; col++) {

                Debug.Log("row, col(" + row + ", " + col + ")");
                if (CheckCell(grid, row, col)) {
                    //add points if match
                    if (grid.GetSpriteAt(row, col) == spriteRedChip) {
                        _playerRed++;
                    } else if (grid.GetSpriteAt(row, col) == spriteBlueChip) {
                        _playerBlue++;
                    }
                }

            }
        }

        Debug.Log("Finished checking grid");
    }

    public bool CheckCell(Grid grid, int row, int col) {

        _currentChipSprite = grid.GetSpriteAt(row, col);
        List<Vector3> vertices = new List<Vector3>();
            
        for (int checkerRow = 0; (checkerRow < checkerRows.Length); checkerRow++) {

            if ((checkerRow + row) >= grid.GetRows()) return false;
            for (int checkerCol = 0; (checkerCol < checkerRows[checkerRow]); checkerCol++) {

                if ((checkerCol + col) >= grid.GetCols()) return false;
                Debug.Log("checker row,col (: " + checkerRow + ", " + checkerCol + ")");

                Debug.Log("Final(" + (row + checkerRow) + ", " + (col + checkerCol) + ")");
                if (_currentChipSprite != grid.GetSpriteAt(checkerRow + row, checkerCol + col)) {
                    return false;
                }

                vertices.Add(grid.GetCellAt(row + checkerRow, col + checkerCol).transform.position);

            } 
        }

        Color color = Color.black;

        if (grid.GetSpriteAt(row, col) == spriteRedChip)
        {
            color = new Color(1f,0.3f,0.3f);
        }
        else if (grid.GetSpriteAt(row, col) == spriteBlueChip)
        {
            color = new Color(0.3f, 0.3f, 1f);
        }

        for (int i = 0; i < vertices.Count; i++) {
            DrawLine(vertices[i], vertices[(i + 1) % vertices.Count], color);
        }

        return true;
    }

    public string GetWinner()
    {
        string winner = "";
        if (_playerRed > _playerBlue) {
            winner += "Player red wins with " + _playerRed + " | " + _playerBlue;
        } else if (_playerRed < _playerBlue) {
            winner += "Player blue wins with " + _playerBlue + " | " + _playerRed;
        } else {
            winner += "Tie tie tie... No one wins red,blue: (" +_playerRed + ", " + _playerBlue + ")";
        }

        return winner;
    }

    public int GetRedScore() {
        return _playerRed;
    }

    public int GetBlueScore() {
        return _playerBlue;
    }

    private void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = new Vector3(start.x, start.y, start.z - 10);
        myLine.AddComponent<LineRenderer>();
        myLine.GetComponent<LineRenderer>().sortingLayerName = "Checking";
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Additive"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
