using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    public void PlayGame() {
        int selectedCharacter = 
            int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        GameManager.instance.CharIndex = selectedCharacter;
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();


        SceneManager.LoadScene("Level1");
        
    }
} //class
