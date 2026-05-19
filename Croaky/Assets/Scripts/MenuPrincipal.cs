using UnityEngine;
using UnityEngine.SceneManagement; // Required to change scenes
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer masterMixer; 
    private bool Silence = false;

    // This variable will store a reference to the panel
    public GameObject Instructions; 

    // Function to display the tutorial
    public void InInstructions()
    {
        Instructions.SetActive(true);
    }

    // Function to hide the tutorial
    public void OutInstructions()
    {
        Instructions.SetActive(false);
    }


    public void Changesounds()
    {
        Silence = !Silence;

        if (Silence)
        {
            // -80 decibels is complete silence in Unity
            masterMixer.SetFloat("MyExposedVolume", -80f);
        }
        else
        {
            // 0 decibels is the normal volume
            masterMixer.SetFloat("MyExposedVolume", 0f);
        }
    }

    public void Quality(int indice)
{
    // Change the graphics engine quality level
    QualitySettings.SetQualityLevel(indice);
    
    Debug.Log("Level Quality: " + indice);
}


    public void Play()
    {
        // Load your game scene. Make sure the name matches.
        SceneManager.LoadScene("Croaky");
    }

    public void Out()
    {
        Debug.Log("Exiting the game...");
        Application.Quit(); // This works in the exported game (.exe)
    }
}
