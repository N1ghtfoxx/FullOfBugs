using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WelcomeScreenManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup playButton;
    [SerializeField] private CanvasGroup quitButton;
    [SerializeField] private float fadeDuration = 1f; // Duration of the fade effect

    private void Awake()
    {
        // Hide buttons initially
        playButton.alpha = 0f;
        quitButton.alpha = 0f;

        // Ensure the buttons are not interactable until they are fully visible
        playButton.interactable = false;
        quitButton.interactable = false;
    }

    private void Start()
    {
        // Start fading in the buttons after a delay
        Invoke(nameof(ShowButtons), 1f); // Delay of 1 second before showing buttons
    }

    private void ShowButtons()
    {
        // Start fading in the buttons
        StartCoroutine(FadeIn(playButton));
        StartCoroutine(FadeIn(quitButton));
    }

    private IEnumerator FadeIn(CanvasGroup group)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            group.alpha = elapsedTime / fadeDuration; // Gradually increase alpha from 0 to 1
            yield return null; // Wait for the next frame
        }
        group.alpha = 1f; // Ensure it's fully visible at the end
        group.interactable = true; // Make the button interactable after fade-in
    }

    // called by the Play button's OnClick event
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("StartScreen"); // Load the StartScreen scene
    }

    // called by the Quit button's OnClick event
    public void OnQuitClicked()
    {
        Debug.Log("Quitting application...");
        Application.Quit(); // Quit the application (only works in a built application, not in the editor)
    

#if UNITY_EDITOR
        // this lets you "quit" while testing in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
