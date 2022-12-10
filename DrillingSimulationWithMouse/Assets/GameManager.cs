using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Trial 
{
    public GameSettings.CameraView ViewMode;
    public float TargetDepth;
    public bool HandEnabled;
    public bool AudioEnabled;
    public bool AudioDynamic;

    public float Result;

    public static Trial Record(GameSettings settings, float attempt) 
    {
        Trial trial = new Trial();
        trial.ViewMode = settings.ViewMode;
        trial.TargetDepth = settings.TargetDepth;
        trial.HandEnabled = settings.HandEnabled;
        trial.AudioEnabled = settings.AudioEnabled;
        trial.AudioDynamic = settings.AudioDynamic;
        trial.Result = attempt;
        return trial;
    }
}

public enum PlateauFormat
{
    Consecutive,
    AverageOf
}

public enum GameState 
{
    Menu,
    Playing
}

public class GameManager : MonoBehaviour
{
    // =============== SINGLETON ====================
    private static GameManager instance;

    private void Awake() {
        
        // Set up Singleton
        if (instance != null) {
            Destroy(gameObject);            
        } else {
            instance = this;
        }
    }

    public static GameManager GetInstance() {         
        return instance;
    }
    // =============== SINGLETON ====================

    public delegate void OnTrialCompleteDelegate();
    public OnTrialCompleteDelegate OnSubmitRecord;
    public OnTrialCompleteDelegate OnFinished;
    public bool TrialComplete;

    public GameState state = GameState.Menu;

    [Header("Testing Values")]
    public PlateauFormat TestingFormat;
    public float AcceptableRange = 0.1f; // 10%

    // Only to be used with Consecutive Format: as testing should be infinite in this format
    public int ConsecutiveRequirement = 5;
    // Only to be used with AverageOf Format: will take a set number of trials and then average them out
    public int NumberOfTrials;
    [SerializeField] private List<float> trialResults;
    public GameSettings trialSettings;

    [Header("Scene Transition")]
    public int NextSceneIndex;

    [Header("UI")]
    public Text objectiveInfoDisplay = null;
    public GameObject startPanelRef = null;
    public GameObject resultsPanelRef = null;
    public Text resultsDisplay = null;
    public GameObject continueButton = null;
    public GameObject restartButton = null;
    public Text resultTimeDisplay = null;
    public GameObject consecutivePanelRef = null;
    public TMP_Text consecutiveDisplay = null;

    [Header("Trial Settings")]
    private int trialIndex = 0;
    private bool trialsComplete = false;    

    [Header("Trial Object References")]
    public GameObject depthRef;
    public GameObject topCamera;
    public GameObject sideCamera;
    public GameObject frontCamera;
    public GameObject angledCamera;

    [Header("Object & Position References")]
    public GameObject drillObjectReference;
    public GameObject handObjectReference;
    public SkinnedMeshRenderer handRenderer;
    //public Transform handStartPoint;
    //public Transform drillStartPoint;
    public Transform handResetPoint;

    [Header("Trial Conditions")]
    public bool HandEnabled = true;
    public bool AudioEnabled = true;    // 3 trials no audio, 3 trials constant audio
    public bool AudioDynmaic = true;    // 3 trials dynamic audio
    public bool IsTraining = true;

    public List<GameSettings> Settings = new List<GameSettings>();
    

    // Store a set of trials for review at end of simulation
    public List<Trial> trials = new List<Trial>();

    // Trial Timer
    private float trialTime = 0.0f;
    private bool keepTime = false;

    public void Begin() 
    {
        SetGameState(GameState.Playing);
        startPanelRef.SetActive(false);

        trialTime = 0.0f;
        keepTime = true;


        if (TestingFormat == PlateauFormat.Consecutive) 
        {
            consecutivePanelRef.SetActive(true);
        }
    }

    public void SetGameState(GameState newState) 
    {
        state = newState;
        if (state == GameState.Menu) 
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        } else 
        {
            // Begin by hiding our cursor from and locking it to the center of the screen so it doesn't go out of the window.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public GameSettings.CameraView GetViewMode() 
    {
        
        //return Settings[trialIndex].ViewMode;
        return trialSettings.ViewMode;
    }

    public GameSettings GetSettings() 
    {
        //return Settings[trialIndex];
        return trialSettings;
    }

    private void Start() 
    {
        Reset();
    }
    
    private void Update() 
    {
        if (keepTime) 
        {
            trialTime += Time.deltaTime;
        }
    }

    private void LoadResultScreen(bool success) 
    {
        SetGameState(GameState.Menu);
        resultsPanelRef.SetActive(true);

        continueButton.SetActive(success);
        restartButton.SetActive(!success);
    
        resultTimeDisplay.text = "Results\t\tTime Spent: " + trialTime.ToString("F2");

        string output = "";
        int trialIndex = 0;
        for (int i = 0; i < trialResults.Count; i++) 
        {
            float accuracy = trialResults[i] - trialSettings.TargetDepth;
            string accuracyResult = "";
            if (Mathf.Abs(accuracy) < 0.1f) {
                accuracyResult = "PERFECT";
            } else {
                accuracyResult = ((accuracy < 0) ? "Short " : "Deep ") + accuracy.ToString("F2") + "cm";
            } 

            output += "Trial " + (i+1) + "\tTarget: " + trialSettings.TargetDepth + "cm" + "\tResult: " + trialResults[i].ToString("F2") + "cm" + "\t( " + accuracyResult + " )\n";   
        }        

        resultsDisplay.text = output;
    }

    private void Reset() 
    {        
        // Adjust Camera View
        switch (trialSettings.ViewMode) 
        {
            default:
            case GameSettings.CameraView.Top:
                topCamera.SetActive(true);
                sideCamera.SetActive(false);
                angledCamera.SetActive(false);
                frontCamera.SetActive(false);
                break;
            case GameSettings.CameraView.Side:
                sideCamera.SetActive(true);
                topCamera.SetActive(false);   
                angledCamera.SetActive(false);
                frontCamera.SetActive(false);             
                break;
            case GameSettings.CameraView.Front:
                frontCamera.SetActive(true);
                angledCamera.SetActive(true);
                topCamera.SetActive(false);
                sideCamera.SetActive(false);
                break;
            case GameSettings.CameraView.Angled:
                angledCamera.SetActive(true);
                frontCamera.SetActive(false);
                topCamera.SetActive(false);
                sideCamera.SetActive(false);
                break;
        }

        // Adjust Depth Tracker
        if (trialSettings.IsTraining) {
            //depthRef.SetActive(true);

            float ratio = 0.35f / 5;
            float conversion = trialSettings.TargetDepth * ratio;

            //depthRef.transform.position = new Vector3(depthRef.transform.position.x, depthRef.transform.position.y, conversion);
        }

        // Toggle Hand Mesh
        handRenderer.enabled = trialSettings.HandEnabled;

        // Reset Hand and Drill Position
        if (trialSettings.HandEnabled) {
            handRenderer.enabled = true;
            handObjectReference.transform.position = handResetPoint.position;
            //drillObjectReference.transform.position = handStartPoint.position;
            //handObjectReference.transform.position = 
            //    (Settings[index].ViewMode == GameSettings.CameraView.Side) ? 
            //        handStartPoint.position : handStartPoint.position;

            handObjectReference.GetComponent<MouseController>().BeginWithDrill(drillObjectReference);
        } else 
        {
            handRenderer.enabled = false;
            //drillObjectReference.transform.position = handStartPoint.position;
            handObjectReference.transform.position = handResetPoint.position;
            handObjectReference.GetComponent<MouseController>().BeginWithDrill(drillObjectReference);

        }
           

        // Change Objective Display Text
        objectiveInfoDisplay.text = "Drill to a depth of " + trialSettings.TargetDepth +"cm";
    }

    public void GetConvertedDepth(float current) 
    {
        float ratio = 0.35f / 5;
        float conversion = current / ratio;        
        conversion -= 2;    // For some reason, our depth is adding 2 to measurements, so we subtract 2 here

        Debug.Log(conversion);
    }

    public void OnRecordSubmission(float result)
    {
        // Get accuracy in cm
        float ratio = 0.35f / 5;
        float conversion = result / ratio;        
        conversion -= 2;    // For some reason, our depth is adding 2 to measurements, so we subtract 2 here
        Debug.Log("Depth: " + conversion);
        if (TestingFormat == PlateauFormat.AverageOf) 
        {   
            // Submit the record
            trialResults.Add(conversion);

            if (trialResults.Count >= 10) 
            {
                // Calculate total of all submissions
                float total = 0;
                for (int i = 0; i < trialResults.Count; i++) 
                {
                    total += trialResults[i];
                }

                // Determine the average and if it is within an acceptable range.
                float average = total / trialResults.Count;
                Debug.Log("Trial Average: " + average);
                if (average >= trialSettings.TargetDepth - (trialSettings.TargetDepth * AcceptableRange) && 
                    average <= trialSettings.TargetDepth + (trialSettings.TargetDepth * AcceptableRange))
                {
                    // If average is within acceptable range, continue to next trial (scene)
                    Debug.Log("Trial was completed successfully");
                    // Call the OnTrialComplete delegate to notify all other observers
                    OnSubmitRecord?.Invoke();
                    LoadResultScreen(true);
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    keepTime = false;
                }

                else 
                {
                    // If average is not within acceptable range, reset the trial.
                    Debug.Log("Trial was failed.  Resetting trial.");
                    LoadResultScreen(false);
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    //RestartTrial();
                }
                
            }            
        } else 
        {
            if (conversion >= trialSettings.TargetDepth - (trialSettings.TargetDepth * AcceptableRange) && 
                conversion <= trialSettings.TargetDepth + (trialSettings.TargetDepth * AcceptableRange))
            {
                // Add to list if it's within the acceptable range
                trialResults.Add(conversion);

                string output = "";
                for (int i = 0; i < ConsecutiveRequirement; i++) 
                {
                    Debug.Log("i: " + i + " | trial count: " + trialResults.Count);
                    if (i >= trialResults.Count) {
                        output += "- ";
                    }
                    else {
                        output += "✔ ";
                    }
                }

                consecutiveDisplay.text = output;

                if (trialResults.Count >= ConsecutiveRequirement) 
                {
                    Debug.Log("Trial was completed successfully");
                    LoadResultScreen(true);
                    keepTime = false;
                }                
            }

            else 
            {
                // Reset the trial results list back to zero and prompt the user with a "try again" message.
                Debug.Log("Resetting the trial");
                trialResults.Clear();
                trialResults = new List<float>();

                consecutiveDisplay.text = "- - - - -";
            }
        }
        
        OnSubmitRecord?.Invoke();
        
        // Reset to the Trial Starting Defaults
        Reset();
    }

    public void TransitionScene() 
    {
        int index = SceneManager.GetActiveScene().buildIndex;  
        SceneManager.UnloadSceneAsync(index);
        SceneManager.LoadSceneAsync(NextSceneIndex);
    }

    public void RestartTrial() 
    {
        int index = SceneManager.GetActiveScene().buildIndex;  
        SceneManager.UnloadSceneAsync(index);
        SceneManager.LoadSceneAsync(index);
    }

    public void QuitGame() 
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
