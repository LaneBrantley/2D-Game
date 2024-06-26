using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private GameObject[] characters;
    private int _charIndex;
    public int CharIndex {
        get { return _charIndex; } set { _charIndex = value;}
    }
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    public void PlayGame() {
        
    }
    private void OnEnable() {
        SceneManager.sceneLoaded += onLevelFinishedLoading;
    }
    private void OnDisable() {
        SceneManager.sceneLoaded -= onLevelFinishedLoading;
    } 
    void onLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
            if (scene.name == "Level1" || scene.name == "Level2" || scene.name == "Level3") {
                Instantiate(characters[CharIndex]);
            }
        }
    }
