using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
//using UnityEditorInternal;
using UnityEngine.SceneManagement;

public class SceneNumber : MonoBehaviour
{

    public static SceneNumber instance;

 
  
    // Start is called before the first frame update

  
    int trainingNumber = -1;
  
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }
       


        if (Input.GetKeyDown(KeyCode.Return) )
        {
            
            trainingNumber++;
            Debug.Log(trainingNumber);

            if (trainingNumber >= 0 && trainingNumber < 2)
            {
                SceneManager.LoadScene("TrainingOne");
               
            }
          else if (trainingNumber >= 2 && trainingNumber <4)
            {

                SceneManager.LoadScene("TrainingTwo");
                

            }

            else if (trainingNumber >= 4 && trainingNumber < 6)
            {

                SceneManager.LoadScene("TrainingThree");


            }

            else if (trainingNumber >= 6 && trainingNumber < 8)
            {

                SceneManager.LoadScene("TrainingFour");


            }
            else if (trainingNumber ==8)
            {

                SceneManager.LoadScene("IntroTwo");


            }

            else if (trainingNumber >= 9 && trainingNumber < 11)
            {

                SceneManager.LoadScene("TrainingFive");


            }

            else if (trainingNumber >= 11 && trainingNumber < 13)
            {

                SceneManager.LoadScene("TrainingSix");


            }

            else if (trainingNumber == 13)
            {

                SceneManager.LoadScene("IntroThree");


            }

            else if (trainingNumber == 14)
            {

                SceneManager.LoadScene("MainMenu");


            }




        }



    }

    public void coundScene()
    {
      
    }


private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
