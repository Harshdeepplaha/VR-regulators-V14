using System.Collections;
using UnityEngine;

/// <summary>
/// The SimulationController class is responsible for managing the simulation end states.
/// </summary>
public class SimulationController : MonoBehaviour
{
    private SceneController sceneController;
    private UserPrefs userPrefs;
    private SimulationLanguageUpdater simulationLanguageUpdater;

    private float elapsedTime;
    private float tantrumTimeAtZero = 0.0f;
    private float tantrumTimeAboveEighty = 0.0f;

    private float gameTimeLimit = 300.0f; // 5 minutes
    private float tantrumZeroTimeLimit = 7.0f;
    private float tantrumEightyTimeLimit = 15.0f;

    public AudioSource audioSource;
    public AudioClip EndingVoice_adult_English;
    public AudioClip EndingVoice_kid_English;
    public AudioClip EndingVoice_adult_Spanish;
    public AudioClip EndingVoice_kid_Spanish;
    void Start()
    {
        // Find the SceneController and UserPrefs objects and get their scripts
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        userPrefs = GameObject.Find("UserPrefs").GetComponent<UserPrefs>();
        simulationLanguageUpdater = GameObject.Find("SimulationLanguageUpdater").GetComponent<SimulationLanguageUpdater>();

        // Track the elapsed time
        elapsedTime = 0.0f;
    }


    void Update()
    {
        if (elapsedTime == 0.0f)
        {
            // We only want to run this once
            simulationLanguageUpdater.UpdateSimulationText();
        }

        // Increment the elapsed time
        if (tatrumchildbehavior.simluationOnGoing)
            elapsedTime += Time.deltaTime;
        Debug.Log("tantrumTimeAtZero is" + tantrumTimeAtZero);
        Debug.Log("elapse time is: " + elapsedTime);
        if (tantrumTimeAtZero > tantrumZeroTimeLimit)
        {
            resetTimer();
            /* sceneController.UnloadScene(Enums.SceneNames.ChildScene);
             if (userPrefs.IsEnglishSpeaker())
                 sceneController.LoadScene(Enums.SceneNames.EndSceneWin);
             else sceneController.LoadScene(Enums.SceneNames.EndSceneWinSpanish);*/
            if (userPrefs.IsEnglishSpeaker())
                StartCoroutine(playAudioandEndGameWin(EndingVoice_adult_English));
            else StartCoroutine(playAudioandEndGameWin(EndingVoice_adult_Spanish));
        }
        else if (tantrumTimeAboveEighty > tantrumEightyTimeLimit || elapsedTime > gameTimeLimit)
        {
            resetTimer();
            /*sceneController.UnloadScene(Enums.SceneNames.ChildScene);
            if (userPrefs.IsEnglishSpeaker())
                sceneController.LoadScene(Enums.SceneNames.EndSceneLose);
            else sceneController.LoadScene(Enums.SceneNames.EndSceneLoseSpanish);*/
            if (userPrefs.IsEnglishSpeaker())
                StartCoroutine(playAudioandEndGameLose(EndingVoice_kid_English));
            else StartCoroutine(playAudioandEndGameLose(EndingVoice_kid_Spanish));
        }

    }

    public void incrementTantrumTimeAtZero()
    {
        tantrumTimeAtZero += Time.deltaTime;
    }

    public void incrementTantrumTimeAboveEighty()
    {
        tantrumTimeAboveEighty += Time.deltaTime;
    }

    public void resetTimer()
    {
        elapsedTime = 0.0f;
        tantrumTimeAtZero = 0.0f;
        tantrumTimeAboveEighty = 0.0f;
        tatrumchildbehavior.simluationOnGoing = false;
    }

    IEnumerator playAudioandEndGameWin(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length + 1f);
        sceneController.UnloadScene(Enums.SceneNames.ChildScene);
        if (userPrefs.IsEnglishSpeaker())
            sceneController.LoadScene(Enums.SceneNames.EndSceneWin);
        else sceneController.LoadScene(Enums.SceneNames.EndSceneWinSpanish);
    }

    IEnumerator playAudioandEndGameLose(AudioClip clip)
    {
        tatrumchildbehavior.gameLost = true; // to mute kid's audio source 
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length + 1f);
        tatrumchildbehavior.gameLost = false;
        sceneController.UnloadScene(Enums.SceneNames.ChildScene);
        if (userPrefs.IsEnglishSpeaker())
            sceneController.LoadScene(Enums.SceneNames.EndSceneLose);
        else sceneController.LoadScene(Enums.SceneNames.EndSceneLoseSpanish);
    }
}