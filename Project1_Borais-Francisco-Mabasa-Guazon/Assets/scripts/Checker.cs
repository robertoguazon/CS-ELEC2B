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

    //more efficient than CheckGrid()
    public void CheckPlacedCells(Grid grid, List<GameObject> placedCells) {
 
        int orig = placedCells.Count;
        for (int i = 0; i < placedCells.Count; i++) {
            GameObject cellObject = placedCells[i];
            Cell cell = cellObject.GetComponent<Cell>();
            //Debug.Log("Checking cell at (row,col): " + cell.Row + "," + cell.Col);
            Sprite chipSprite = cell.GetChipSprite();

            if (!cell.isChecked()) {
                if (CheckCell(grid, cell))
                {
                    if (chipSprite == spriteRedChip) _playerRed++;
                    else if (chipSprite == spriteBlueChip) _playerBlue++;
                }
            }
           
        }
    }

    //more efficient
    public bool CheckCell(Grid grid, Cell cell) {
        _currentChipSprite = cell.GetChipSprite();
        List<Vector3> vertices = new List<Vector3>();
        List<Cell> cellsToCheck = new List<Cell>(); // new implementation of checking

        cellsToCheck.Add(cell);

        for (int checkerRow = 0; (checkerRow < checkerRows.Length); checkerRow++)
        {

            if ((checkerRow + cell.Row) >= grid.GetRows()) return false;
            for (int checkerCol = 0; (checkerCol < checkerRows[checkerRow]); checkerCol++)
            {

                if ((checkerCol + cell.Col) >= grid.GetCols()) return false;
                // Debug.Log("checker row,col (: " + checkerRow + ", " + checkerCol + ")");


                GameObject cellObject = grid.GetCellAt(cell.Row + checkerRow, cell.Col + checkerCol);
                Cell checkCell = cellObject.GetComponent<Cell>();
                if (checkCell.isChecked()) return false;

                // Debug.Log("Final(" + (cell.Row + checkerRow) + ", " + (cell.Col + checkerCol) + ")");
                if (_currentChipSprite != grid.GetChipSpriteAt(checkerRow + cell.Row, checkerCol + cell.Col))
                {
                    return false;
                }

                vertices.Add(cellObject.transform.position);
                cellsToCheck.Add(checkCell);
            }
        }

     
          Color color = Color.black;

          if (cell.GetChipSprite() == spriteRedChip)
          {
              color = new Color(1f, 0.3f, 0.3f);
          }
          else if (cell.GetChipSprite() == spriteBlueChip)
          {
              color = new Color(0.3f, 0.3f, 1f);
          }

          for (int i = 0; i < vertices.Count; i++)
          {
              DrawLine(vertices[i], vertices[(i + 1) % vertices.Count], color);
          }


        //checking important
        foreach (Cell cellToCheck in cellsToCheck) {
              cellToCheck.check();
          }
         

        return true;
    }

    public bool CheckCell(Grid grid, int row, int col)
    {

        _currentChipSprite = grid.GetChipSpriteAt(row, col);
        List<Vector3> vertices = new List<Vector3>();

        for (int checkerRow = 0; (checkerRow < checkerRows.Length); checkerRow++)
        {

            if ((checkerRow + row) >= grid.GetRows()) return false;
            for (int checkerCol = 0; (checkerCol < checkerRows[checkerRow]); checkerCol++)
            {

                if ((checkerCol + col) >= grid.GetCols()) return false;
                //Debug.Log("checker row,col (: " + checkerRow + ", " + checkerCol + ")");

                //Debug.Log("Final(" + (row + checkerRow) + ", " + (col + checkerCol) + ")");
                if (_currentChipSprite != grid.GetChipSpriteAt(checkerRow + row, checkerCol + col))
                {
                    return false;
                }

                vertices.Add(grid.GetCellAt(row + checkerRow, col + checkerCol).transform.position);
            }
        }

        Color color = Color.black;

        if (grid.GetChipSpriteAt(row, col) == spriteRedChip)
        {
            color = new Color(1f, 0.3f, 0.3f);
        }
        else if (grid.GetChipSpriteAt(row, col) == spriteBlueChip)
        {
            color = new Color(0.3f, 0.3f, 1f);
        }

        for (int i = 0; i < vertices.Count; i++)
        {
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
        lr.material = new Material(Shader.Find("Particles/Additive")); //INCLUDE THIS IN UNITY SHADERS ALWAYS TODO
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    //less efficient than placed cells where only the cells that have been used are checked
    //but at worse case they are the same
    public void CheckGrid(Grid grid)
    {
        _gridRows = grid.GetRows();
        _gridCols = grid.GetCols();

        Debug.Log("grid rows,cols (" + _gridRows + ", " + _gridCols);

        for (int row = 0; row < _gridRows; row++ /*row += checkerRows.Length*/)
        {
            for (int col = 0; col < _gridCols; col++ /*col += checkerRows[row]*/)
            {

                Debug.Log("row, col(" + row + ", " + col + ")");
                if (CheckCell(grid, row, col))
                {
                    //add points if match
                    if (grid.GetChipSpriteAt(row, col) == spriteRedChip)
                    {
                        _playerRed++;
                    }
                    else if (grid.GetChipSpriteAt(row, col) == spriteBlueChip)
                    {
                        _playerBlue++;
                    }
                }

            }
        }

        Debug.Log("Finished checking grid");
    }
}
