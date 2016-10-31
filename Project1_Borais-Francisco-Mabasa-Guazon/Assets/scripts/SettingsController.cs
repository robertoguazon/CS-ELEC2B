using UnityEngine;
using System.Collections;

public class SettingsController : MonoBehaviour {

    [SerializeField]
    private Sprite cursorSprite;
    [SerializeField]
    private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField]
    private Vector2 hotspot = Vector2.zero;
    

    private SettingsController _instance = null;
    private Texture2D _cursorTexture;

    public void Awake() {
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("No instance");
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
            Debug.Log("Has instance");
            return;
        }

        Debug.Log("Instance: " + _instance);
    }

    public void Start() {
        Debug.Log("Settings: setting cursor...");
        _cursorTexture = new Texture2D((int)cursorSprite.rect.width, (int)cursorSprite.rect.height);
        Color[] pixels = cursorSprite.texture.GetPixels(
            (int)cursorSprite.textureRect.x, (int)cursorSprite.textureRect.y,
            (int)cursorSprite.textureRect.width, (int)cursorSprite.textureRect.height);
        _cursorTexture.SetPixels(pixels);
        Cursor.SetCursor(_cursorTexture, hotspot, cursorMode);
    }


   
}
