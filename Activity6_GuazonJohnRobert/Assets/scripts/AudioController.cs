using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    [SerializeField]
    private float musicVolume = 0.1f;
    [SerializeField]
    private float soundVolume = 1.0f;
    [SerializeField]
    private AudioClip musicMainMenu;
    [SerializeField]
    private AudioClip musicBackground;
    [SerializeField]
    private AudioClip musicGameLoose;
    [SerializeField]
    private AudioClip musicGameWin;

    private static AudioController _instance;
    private static float _musicVolume;
    private static float _soundVolume;

        
    private static AudioClip _musicMainMenu;
    private static AudioClip _musicBackground;
    private static AudioClip _musicGameLoose;
    private static AudioClip _musicGameWin;

    private static AudioSource _musicSource;

    void Awake()
    {
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


        _musicVolume = musicVolume;
        _soundVolume = soundVolume;

        _musicMainMenu = musicMainMenu;
        _musicBackground = musicBackground;
        _musicGameLoose = musicGameLoose;
        _musicGameWin = musicGameWin;

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true;
        _musicSource.volume = musicVolume;
    }

    public static void PlayMusicMainMenu()
    {
        if (_musicSource.clip != _musicMainMenu)
        {
            _musicSource.clip = _musicMainMenu;
        }
        _musicSource.Play();
    }

    public static void PlayMusicBackground() {
        if (_musicSource.clip != _musicBackground)
        {
            _musicSource.clip = _musicBackground;
        }
        _musicSource.Play();
    }

    public static void PlayMusicGameLoose()
    {
        if (_musicSource.clip != _musicGameLoose)
        {
            _musicSource.clip = _musicGameLoose;
        }
        _musicSource.Play();
    }

    public static void PlayMusicGameWin()
    {
        if (_musicSource.clip != _musicGameWin)
        {
            _musicSource.clip = _musicGameWin;
        }
        _musicSource.Play();
    }

}
