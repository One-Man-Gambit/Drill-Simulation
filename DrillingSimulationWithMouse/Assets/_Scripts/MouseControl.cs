using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
//using UnityEditorInternal;
using UnityEngine.SceneManagement;
using TMPro;

public class MouseControl : MonoBehaviour
{


    private Vector3 mOffset;
    private float mZCoord;
    private float reduceMove = 0.01f; 
    private float yPos;
    private float dragSlowDown = 1;
    private float gap = 0;
    private float drillingDistance = 0;
    public GameObject mainCamera;
    public GameObject CameraTop;
    public GameObject ControlMethod;
    private float moveForward = 0;
    public Text text;
    public GameObject buttonShowUp;
    public GameObject nosoundoffer;
    public GameObject soundvalue;
    public GameObject trainingOpen;
    public GameObject lineOne;
    public GameObject lineTwo;
    private bool colided = false;










    private void Start()
    {

 

        buttonShowUp.active = false;

        // text = GameObject.Find("Canvas/Text").GetComponent<Text>();


        if (trainingOpen.active == true)
        {
          
            if (CameraTop.active == true)
            {

                if (lineOne.active == true && lineTwo.active == false)
                {
                    if (ControlMethod.active == true)
                    {
                        text.text = "Click and drag the middle of the drill until you reach 5cm (red line)";
                    }

                    if (ControlMethod.active == false)
                    {
                        text.text = "Press 'Up Arrow' to start 5 cm drilling (read line)";
                    }
                }
                
                if (lineOne.active == false && lineTwo.active == true)
                {
                    if (ControlMethod.active == true)
                    {
                        text.text = "Click and drag the middle of drill until you reach 10cm(red line)";
                    }

                    if (ControlMethod.active == false)
                    {
                        text.text = "Press 'Up Arrow' to start 10cm drilling (read line)";
                    }
                }

            }

            if (CameraTop.active == false)
            {
                if (lineOne.active == true && lineTwo.active == false)
                {
                    if (ControlMethod.active == true)
                    {
                        text.text = "Click and drag the middle of the drill until you reach 5cm (red line)";
                    }

                    if (ControlMethod.active == false)
                    {
                        text.text = "Press 'Up Arrow' to start 5 cm drilling.";
                    }
                }

                if (lineOne.active == false && lineTwo.active == true)
                {
                    if (ControlMethod.active == true)
                    {
                        text.text = "Click and drag the middle of drill until you reach 10cm(red line)";
                    }

                    if (ControlMethod.active == false)
                    {
                        text.text = "Press 'Up Arrow' to start 10 cm drilling.";
                    }
                }
            }

        }

        if (trainingOpen.active == false)
        {


            if (ControlMethod.active == true)
            {
                text.text = "Please Use Mouse Control by draging the drill.";
            }

            if (ControlMethod.active == false)
            {
                text.text = "Press 'Up-Arrow' to start. Press 'Space' to stop the drill.";
            }
        }
    }

    void OnMouseDown()
        {

            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            //Store offset between mouse and object's position
            mOffset = gameObject.transform.position - GetMouseWorldPos();

        //Cursor.visible = false;
        if (trainingOpen.active == false)
        {
            if (CameraTop.active == false)
            {
                text.text = "STARTING. Drill depth 12cm. Release mouse to stop.";


            }

            if (CameraTop.active == true)
            {
                text.text = "Drill depth of 12 cm. Release the mouse to stop";

            }
        }

    }



        private Vector3 GetMouseWorldPos()
        {
            //Mouse position about x and y
            Vector3 mousePoint = Input.mousePosition;


            //z Coordinate
            // try to add the cursor's movement on the Y axis to the drill's Z axis
            // reduceMove makes the movement on y axis slightly

            mousePoint.z = mZCoord;



            yPos = mousePoint.y * reduceMove;




            mousePoint.y = 0;
            mousePoint.x = 0;


            return Camera.main.ScreenToWorldPoint(mousePoint);


        }




        // Make the mouse contro with visual condition as a function








        void OnMouseDrag()
        {

        // add the movement on y axis to the z axis
        //the 0.5f means to reduce the movement of the beginning when the first time cursor chosen the object
        // when drill get into the board, try to slow down the drill's movement
        //gap need to be caculated because the movement is based on the y axis of mouse, therefore, when click the drill, there will have a gap inevitable


        // CameraTop.SetActive(true);
        if (trainingOpen.active == false)
        {
            if (CameraTop.active == false && ControlMethod.active == true)
            {

                transform.position = GetMouseWorldPos() + mOffset + new Vector3(0.0f, 0.0f, yPos * 0.13f - 0.44f);
            }
            if (CameraTop.active == true && ControlMethod.active == true)
            {

                transform.position = GetMouseWorldPos() + mOffset + new Vector3(0.0f, 0.0f, yPos *0.13f -0.2f);
            }
        }


        if (trainingOpen.active == true)
        {
            if (CameraTop.active == false && ControlMethod.active == true && colided == false)
            {

                transform.position = GetMouseWorldPos() + mOffset + new Vector3(0.0f, 0.0f, yPos * 0.13f - 0.44f);
            }
            if (CameraTop.active == true && ControlMethod.active == true && colided == false)
            {

                transform.position = GetMouseWorldPos() + mOffset + new Vector3(0.0f, 0.0f, yPos * 0.13f - 0.2f);
            }
        }


    }

        
    

    
    private void OnMouseUp()
    {
        // the most drilling position is 2.09, and the starting position is 1.25. if we assume the drill tip can drill 10 cm totoaly.
        // so we can seperate the position 1 to 10cm.

        drillingDistance = (gameObject.transform.position.z - 1.5f) * (15 / (2.3f - 1.5f));



      

        if (trainingOpen.active == false)
        {

            printCondition();




            // Time.timeScale = 0f;
            text.text = "It is stoped. Press Button To The Next Test.";
            buttonShowUp.active = true;

        }


    }





    private void OnTriggerEnter(Collider other)
    {


        //if (other.gameobject.name == "woodboard1" && cameratop.active == false && controlmethod.active == true)
        //{

        //    dragslowdown = 0.07f;
        //    gap = 6.06f;
        //}

        //if (other.gameobject.name == "woodboard1" && cameratop.active == true && controlmethod.active == true)
        //{

        //    dragslowdown = 0.07f;
        //    gap = 2.34f;
        //}
        //if (other.gameobject.name == "woodboard1" &&  controlmethod.active == false)
        //{
        //    moveforward = 0.7f;
        //}

        if (ControlMethod.active == true)
        {
            if (other.gameObject.name == "WoodBoard14" && trainingOpen.active == true && CameraTop.active == true && lineOne.active == true && lineTwo.active == false)
            {
                colided = true;
                text.text = "You have reached 5 cm. Press 'Enter' to next trial";
                
            }

            if (other.gameObject.name == "WoodBoard28" && trainingOpen.active == true && CameraTop.active == true && lineOne.active == false && lineTwo.active == true)
            {
                colided = true;
                text.text = "You have reached 10 cm. Press 'Enter' to next trial";
            }

        }


        if (ControlMethod.active == false)
        {
            if (other.gameObject.name == "WoodBoard14" && trainingOpen.active == true && CameraTop.active == true && lineOne.active == true && lineTwo.active == false)
            {
                moveForward = 0.0f;
                text.text = "You have reached 5 cm. Press 'Space' to stop the drill bit rotation.";
            }

            if (other.gameObject.name == "WoodBoard28" && trainingOpen.active == true && CameraTop.active == true && lineOne.active == false && lineTwo.active == true)
            {
                moveForward = 0.0f;
                text.text = "You have reached 10 cm. Press 'Space' to stop the drill bit rotation.";
            }

            if (other.gameObject.name == "WoodBoard14" && trainingOpen.active == true && CameraTop.active == false && lineOne.active == true && lineTwo.active == false && soundvalue.active == true && nosoundoffer.active == false)
            {
                text.text = "You have reached 5 cm. Press 'Enter' to next trial";
                moveForward = 0.0f;
            }

            if (other.gameObject.name == "WoodBoard28" && trainingOpen.active == true && CameraTop.active == false && lineOne.active == false && lineTwo.active == true && soundvalue.active == true && nosoundoffer.active == false)
            {
                text.text = "You have reached 10 cm. Press 'Enter' to next trial";
                moveForward = 0.0f;
            }
        }


    }



    void Update()
    {

        
            if (ControlMethod.active == false && CameraTop.active == false && trainingOpen.active == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                text.text = "STARTING. Drill depth 12cm. Press Space bar to stop.";
            }
        }


        if (ControlMethod.active == false && CameraTop.active == true && trainingOpen.active == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                text.text = "Drill depth 12cm.  Press Space bar to stop.";
            }
        }


        if (ControlMethod.active == false && CameraTop.active == false && trainingOpen.active == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                text.text = "It is starting to drill...";
            }
        }

        if (ControlMethod.active == false )
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {

                moveForward = 1.4f;

               
               
            }
           



            transform.Translate(Vector3.forward * Time.deltaTime * 0.2f * moveForward);
           
            if (Input.GetKeyDown(KeyCode.Space))
            {

                moveForward = 0.0f;


                if (trainingOpen.active == true && CameraTop.active ==true)
                {


                    text.text = "It is stoped. Press 'Enter' to the next trial.";

                   
                    
                }



                drillingDistance = (gameObject.transform.position.z - 1.5f) * (15 / (2.3f - 1.5f));

                if (trainingOpen.active == false)
                {
                    printCondition();

                    //Time.timeScale = 0f;
                    text.text = "Press Button To The Next Test.";
                    buttonShowUp.active = true;
                }
            }

            //if (transform.position.z >= 2.8f)
            //{
            //    transform.position = new Vector3(0, 1, 2.8f);

            //}
        }

        
        }

        public void printCondition()
    {
        string path = Application.dataPath + "/_DataCollection/DataCollection.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "This is a data collection.\n");
        }

        string testTime = "Test Time: " + System.DateTime.Now + "\n";
        if (drillingDistance < 0)
        {
            File.AppendAllText(path, "\n" + testTime + "You Did not Attach the Board!" + "\n");

        }
        if (CameraTop.active == true && ControlMethod.active == true && soundvalue.active == true && nosoundoffer.active == false && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: VMD. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == true && ControlMethod.active == true && soundvalue.active == false && nosoundoffer.active == true && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: VMN. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == false && ControlMethod.active == false && soundvalue.active == true && nosoundoffer.active == false && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: NVKD. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == false && ControlMethod.active == true && soundvalue.active == false && nosoundoffer.active == true && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: NVMN. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == true && ControlMethod.active == false && soundvalue.active == false && nosoundoffer.active == false && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: VKC. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == true && ControlMethod.active == false && soundvalue.active == true && nosoundoffer.active == false && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: VKD. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == true && ControlMethod.active == false && soundvalue.active == false && nosoundoffer.active == true && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: VKN. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == false && ControlMethod.active == false && soundvalue.active == false && nosoundoffer.active == true && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: NVKN. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == false && ControlMethod.active == true && soundvalue.active == false && nosoundoffer.active == false && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: NVMC. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == true && ControlMethod.active == true && soundvalue.active == false && nosoundoffer.active == false && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: VMC. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == false && ControlMethod.active == true && soundvalue.active == true && nosoundoffer.active == false && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: NVMD. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

        if (CameraTop.active == false && ControlMethod.active == false && soundvalue.active == false && nosoundoffer.active == false && drillingDistance >= 0)
        {

            File.AppendAllText(path, "\n" + testTime + "Condition is: NVKC. You Have Drilled " + drillingDistance.ToString("F2") + " cm." + "\n");
        }

    }

    }








  








//{

//    Rigidbody rb;

//    void Start()
//    {
//        // Store reference to attached Rigidbody
//        rb = GetComponent<Rigidbody>();
//    }

//    void OnMouseDrag()
//    {
//        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

//        // Move by Rigidbody rather than transform directly
//        rb.MovePosition(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen)));
//    }

//    void OnCollisionEnter(Collision col)
//    {
//        // Freeze on collision
//        rb.isKinematic = true;
//    }
//}