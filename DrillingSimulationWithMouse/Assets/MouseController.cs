using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Components
    public Animator animator;
    public Camera cam;
    public GameObject mesh;
    public Rigidbody rigid;
    public SoundManager sounds;

    // States
    private bool DrillHeld = true;
    private bool DrillOn = false;
    private float DrillDepthLimit = 0.0f;   // Intended for when hole has been drilled but drill is not currently on.  Stops from going past drill depth

    // References
    public GameObject drillObjectRef = null;
    public Transform DrillHeldPoint;
    public DrillBitRotation bitRotationRef;

    private void Start() 
    {
        // Get a reference to the rigidbody component of the hand.
        rigid = GetComponent<Rigidbody>();        

        // Add our OnRemovedFromWood function to the drill bit OnRemoval delegate
        bitRotationRef.OnRemoval += OnRemoveDrillFromWood;

        // Pass some functionality to GameManager for when trials are completed
        GameManager.GetInstance().OnSubmitRecord += ReleaseDrill;
    }

    private void Update() 
    {   
        if (GameManager.GetInstance().state == GameState.Menu) return;
        if (GameManager.GetInstance().TrialComplete) return;

        HandMotion();
        UserInput();

        
    }    

    public void BeginWithDrill(GameObject drillRef) 
    {
        GrabDrill(drillRef);
    }

    private void HandMotion() 
    {   
        // Generate motion vector based on mouse motion
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        int view = (int)GameManager.GetInstance().GetViewMode();
        //Vector3 motion = new Vector3(((view == 0) ? 0 : horizontal), ((view == 0) ? vertical : 0), (view == 0) ? horizontal : vertical);
        Vector3 motion = new Vector3(0, 0, (view == 0) ? horizontal : vertical);
        
        motion *= Time.deltaTime * ((DrillOn) ? 2.5f : 10.0f);

        // Use rigidbody to move the hand to prevent clipping through colliders
        rigid.MovePosition(transform.position + motion);
        
        // Clamp the position to our screen space, so we don't go out of bounds
        Vector3 clampedPosition = transform.position;        
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -2.7f, 2.2f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0, 3);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -2f, ((bitRotationRef.isInWood && !DrillOn) ? DrillDepthLimit : 0));
        transform.position = clampedPosition;
    }

    private void UserInput() 
    {   
        // When we press the left mouse button down
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {              
            // If drill is not being held
            if (DrillHeld == false) 
            {
                // Check if drill is within a radius of our hand
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
                foreach (var hit in hitColliders) 
                {
                    // If the drill is found to be within the radius
                    if (hit.gameObject.tag == "Drill") 
                    {   
                        // Grab it
                        GrabDrill(hit.gameObject);
                    }
                }   
            } 
            
            // Otherwise, if the drill is already being held
            else 
            {
                UseDrill();
            } 
        }

        // When we release the left mouse button
        if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            // If the drill is currently being held
            if (DrillHeld) 
            {
                StopUsingDrill();
            } 
        }

        // When we press down the right mouse button
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {   
            // Only be able to release the drill if hand is enabled
            if (GameManager.GetInstance().GetSettings().HandEnabled) {
                // If the drill is currently being held
                if (DrillHeld) 
                {
                    // Release the drill.                
                    ReleaseDrill();                
                }
            }
            
        }
    }

    private void UseDrill() 
    {
        // Constrain movement to only the Z axis while using the drill
        rigid.constraints = GetConstraints(GameManager.GetInstance().GetViewMode(), true); 

        // Turn on the drill bit
        DrillOn = true;
        bitRotationRef.shouldTurn = true;

        // Play Drill Sound
        if (GameManager.GetInstance().GetSettings().AudioEnabled) {
            sounds.BeginDrilling();
        }
        
        // Animate our finger holding down the drill trigger
        animator.SetBool("Index", true);      
    }

    private void StopUsingDrill() 
    {        
        // Constrain movement to only along the X axis when nothing is held
        if (!bitRotationRef.isInWood) {
            rigid.constraints = GetConstraints(GameManager.GetInstance().GetViewMode(), false);
            Debug.Log("Drill Bit not in wood");
        } else {
            DrillDepthLimit = transform.position.z;

            Debug.Log("In Wood");
            // Calculate actual depth
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10)) {
                float handToWall = (hit.point - transform.position).magnitude;
                float handToBit = (bitRotationRef.point.position - transform.position).magnitude;
                float actualDepth = handToBit - handToWall;                  
                GameManager.GetInstance().OnRecordSubmission(actualDepth); 
            }
            
        }

        // Turn off the drill bit
        DrillOn = false;
        bitRotationRef.shouldTurn = false;

        Debug.Log("Stop Using Drill");
        sounds.StopDrilling();
        // Stop drilling sound
        if (GameManager.GetInstance().GetSettings().AudioEnabled) {
            sounds.StopDrilling();
            Debug.Log("Sounds Stopped");
        }

        // Animate our finger releasing the drill trigger
        animator.SetBool("Index", false);
    }

    private void OnRemoveDrillFromWood() 
    {
        rigid.constraints = GetConstraints(GameManager.GetInstance().GetViewMode(), false);
        
        if (GameManager.GetInstance().GetSettings().AudioEnabled) {
            sounds.OnExitFromWood();
        }
    }

    private void GrabDrill(GameObject drill) 
    {
        // Set DrillHeld state to true;
        //DrillHeld = true;
        animator.SetBool("Grab", DrillHeld);

        // Rotate our hand 90 degrees to line up with the drill
        //mesh.transform.Rotate(new Vector3(0.0f, 0.0f, 90f));

        // Store a reference to the drill object
        //drillObjectRef = drill;

        // Set the drill's parent to the hand, so we can move it with our hand
        //drillObjectRef.transform.SetParent(DrillHeldPoint);

        // Lastly, set the local (relative to the hand) position to the appropriate coordinates so it rests nicely in our hand.
        //drillObjectRef.transform.localPosition = new Vector3(0.215f, -1.43f, 0.125f);
        
    }

    private void ReleaseDrill() 
    {
        // Constrain movement to only along the X axis when nothing is held
        rigid.constraints = GetConstraints(GameManager.GetInstance().GetViewMode(), false);

        // Set DrillHeld State to false.
        //DrillHeld = false;
        animator.SetBool("Grab", DrillHeld);

        // Because we rotate the hand when grabbing the drill, we want to rotate it back.
        //mesh.transform.Rotate(new Vector3(0.0f, 0.0f, -90f));
        
        // Last thing we need to do is clear the parent status of the drill and remove our reference to the drill.
        //if (drillObjectRef != null) { 
        //    drillObjectRef.transform.parent = null;
        //    drillObjectRef = null;
        //}        
    }

    public RigidbodyConstraints GetConstraints(GameSettings.CameraView view, bool isUse) 
    {
        RigidbodyConstraints constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        
        if (isUse) {
            constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        }

        // if (view == GameSettings.CameraView.Top && isUse) {
        //     constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        // }

        // else if (view == GameSettings.CameraView.Top && !isUse) {
        //     constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        // }

        // else if (view == GameSettings.CameraView.Side && isUse) {
        //     constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        // }

        // else if (view == GameSettings.CameraView.Side && !isUse) {
        //     constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        // }

        // else if (view == GameSettings.CameraView.Front && isUse) {
        //     //constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        //     Debug.Log("Forward View Not Yet Implemented");
        // }

        // else if (view == GameSettings.CameraView.Front && !isUse) {
        //     //constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        //     Debug.Log("Forward View Not Yet Implemented");
        // }

        // else if (view == GameSettings.CameraView.Front && isUse) {
        //     //constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        //     Debug.Log("Angled View Not Yet Implemented");
        // }

        // else if (view == GameSettings.CameraView.Front && !isUse) {
        //     //constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        //     Debug.Log("Angled View Not Yet Implemented");
        // }

        return constraints;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }
}
