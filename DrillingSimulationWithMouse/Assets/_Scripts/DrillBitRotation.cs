using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBitRotation : MonoBehaviour
{
    public bool shouldTurn = false;
    public bool isInWood = false;

    public Transform point;

    public delegate void OnInteractDelegate();
    public OnInteractDelegate OnRemoval;

    // Update is called once per frame
    private void Update()
    {
        if (shouldTurn == true)
        {
            transform.Rotate(Vector3.forward, 50000 * Time.deltaTime);
        }        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Wood") 
        {   
            Debug.Log("Entered into Wood");
            isInWood = true;
        }

        if (other.gameObject.tag == "5cm")
        {
            if (GameManager.GetInstance().IsTraining) {
                // 5 cm reached
            }
        }

        if (other.gameObject.tag == "10cm") 
        {
            if (GameManager.GetInstance().IsTraining) {
                // 10 cm reached
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Wood")
        {
            Debug.Log("Removed from Wood");
            isInWood = false;
            OnRemoval?.Invoke();
        }
    }
}
