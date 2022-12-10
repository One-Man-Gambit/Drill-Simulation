using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSettings
{
    public enum CameraView {
        Side, Top, Front, Angled
    }

    public CameraView ViewMode = CameraView.Angled;
    public float TargetDepth = 0;

    public bool HandEnabled = true;
    public bool AudioEnabled = true;
    public bool AudioDynamic = true;
    public bool IsTraining = true;
}
