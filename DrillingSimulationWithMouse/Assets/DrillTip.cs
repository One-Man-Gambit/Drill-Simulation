using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillTip : MonoBehaviour
{
    public SoundManager sm;

    public void OnTriggerEnter(Collider other) 
    {
        for (int i = 0; i < sm.drillingEpisodes.Length; i++) 
        {
            if (other.gameObject.name == "WoodBoard"+(i+1).ToString()) {
                if (GameManager.GetInstance().GetSettings().AudioDynamic) {
                    sm.SetDepth(i);
                }                
            }
        }
    }
}
