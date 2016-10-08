using UnityEngine;
using System.Collections;

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

    private static GameObject _redChip;
    private static GameObject _blueChip;

    private static GameObject _grid;
    private static Grid _gridScript;
    private static int _available;

    private static bool _playerRed = true; //if false then player blue

    private static GameController _gameController;

    //sample
    private static Checker _rectangleChecker;
    private static Checker _triangleChecker;

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
    }

    public static void PlaceChip(GameObject cell)
    {
        

        if (cell.transform.childCount > 0)
        {
            foreach (Transform child in cell.transform)
            {
                if (child.tag == "Chip") return;
            }
        }

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
        newChip.transform.parent = cell.transform;
        newChip.transform.localScale = new Vector3(1f, 1f, 1f);

        //-1 available cell
        _available--;
        Debug.Log(_available);

        //change turns
        ChangeTurn();

        //play audio
        Audio.PlayDropChip();
    }

    private static void ChangeTurn()
    {
        _playerRed = !_playerRed;
        if (_playerRed)
        {
            Debug.Log("Player: Red");
        }
        else
        {
            Debug.Log("Player: Blue");
        }

        if (_available <= 0)
        {
            _gameController.StartCoroutine(CheckWinner(1.5f));
        }
    }

    private static IEnumerator CheckWinner(float seconds) {

        //start checking animation
        //TODO

        yield return new WaitForSeconds(seconds);

        //stop checking animation
        //TODO

        _rectangleChecker.CheckGrid(_grid.GetComponent<Grid>());
        _triangleChecker.CheckGrid(_grid.GetComponent<Grid>());

        Debug.Log("Finished checking rectangles");
        Debug.Log("Winner: " + _rectangleChecker.GetWinner());

       Debug.Log("Finished checking triangles");
       Debug.Log("Winner: " + _triangleChecker.GetWinner());
    }
}
