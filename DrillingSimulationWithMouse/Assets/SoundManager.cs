using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Source Reference")]
    public AudioSource source;
    public AudioSource depthSource;
    private AudioSource playSounds;

    [Header("Audio Files")]
    public AudioClip beginDrilling;
    public AudioClip onGoingDrilling;
    public AudioClip[] drillingEpisodes;

    [Header("States")]
    public bool IsDrilling = false;    

    private void Start() 
    {
        GameManager.GetInstance().OnFinished += StopAll;
    }

    public void BeginDrilling() 
    {
        if (!GameManager.GetInstance().GetSettings().AudioEnabled) return;

        IsDrilling = true;       
        source.clip = beginDrilling;
        source.Play();
    }

    public void OnExitFromWood()
    {
        if (!GameManager.GetInstance().GetSettings().AudioEnabled) return;

        if (IsDrilling) {
            depthSource.Stop();
        }                
    }

    public void StopDrilling() 
    {
        //if (!GameManager.GetInstance().GetSettings().AudioEnabled) return;

        source.Stop();
        depthSource.Stop();
        IsDrilling = false;
    }

    public void SetDepth(int index) 
    {
        if (!GameManager.GetInstance().GetSettings().AudioEnabled) return;
        if (!GameManager.GetInstance().GetSettings().AudioDynamic) return;

        depthSource.Stop();
        depthSource.clip = drillingEpisodes[index];
        depthSource.Play();
    }

    public void StopAll() 
    {
        depthSource.Stop();
        source.Stop();
    }
}
