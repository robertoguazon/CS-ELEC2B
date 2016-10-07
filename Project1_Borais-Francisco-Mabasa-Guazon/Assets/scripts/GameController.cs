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

    private static GameObject _redChip;
    private static GameObject _blueChip;

    private static GameObject _grid;
    private static Grid _gridScript;
    private static int _available;

    private static bool _playerRed = true; //if false then player blue

    public void Start() {
        _redChip = prefabRedChip;
        _blueChip = prefabBlueChip;
        _grid = Instantiate(prefabGrid) as GameObject;
        _gridScript = _grid.GetComponent<Grid>();
        _available = _gridScript.GetSize();
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
    }
}
