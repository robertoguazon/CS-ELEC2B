using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    //checkers
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
        _canvas.enabled = false;
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
        _canvas.enabled = true;
        _scoreboardAnimator.SetTrigger("Appear");

        //start checking animation
        _loading.SetActive(true);
        yield return new WaitForSeconds(seconds);

        //check using checker
        _rectangleChecker.CheckGrid(_grid.GetComponent<Grid>());

        float rectangleRed = _rectangleChecker.GetRedScore();
        float rectangleBlue = _rectangleChecker.GetBlueScore();

        //place texts
        _redRectangleText.text = "" + rectangleRed;
        _blueRectangleText.text = "" + rectangleBlue;

        //if tie calculate triangle
        if (rectangleBlue == rectangleRed) {
            _triangleChecker.CheckGrid(_grid.GetComponent<Grid>());
            _redTriangleText.text = "" + _triangleChecker.GetRedScore();
            _blueTriangleText.text = "" + _triangleChecker.GetBlueScore();
        } else {
            _redTriangleText.text = "?";
            _blueTriangleText.text = "?";
        }

        //stop checking animation
        _loading.SetActive(false);        
    }
}
