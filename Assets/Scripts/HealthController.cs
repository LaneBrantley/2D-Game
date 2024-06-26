using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private static HealthController instance;
    public Player player;
    [SerializeField]
    private Image[] hearts;

    // AudioSource to play the background music
    private AudioSource audioSource;

    // Audio clips for different levels
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        FindPlayerAndUpdateHealth();
        PlayMusicForCurrentLevel();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        FindPlayerAndUpdateHealth();
        PlayMusicForCurrentLevel();
    }

    private void FindPlayerAndUpdateHealth()
    {
        player = FindObjectOfType<Player>();
        UpdateHealth();
    }

    private void Update()
    {
        if (player != null)
        {
            UpdateHealth();
            if (player.life == 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.life)
            {
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].color = Color.white;
            }
        }
    }

    private void PlayMusicForCurrentLevel()
    {
        if (audioSource == null)
        {
            return;
        }

        AudioClip clipToPlay = null;

        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                clipToPlay = level1Music;
                break;
            case "Level2":
                clipToPlay = level2Music;
                break;
            case "Level3":
                clipToPlay = level3Music;
                break;
            default:
                break;

        }

        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HomeButton()
    {
        audioSource.clip = null;
        SceneManager.LoadScene("MainMenu");
        
    }
}
