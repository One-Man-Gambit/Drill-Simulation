using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Courtformal : MonoBehaviour
{
    public static Courtformal instance;
    int numberaccount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            numberaccount++;

            Debug.Log(numberaccount);

            if (numberaccount >= 24)
            {
                SceneManager.LoadScene("ThanksPage");
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();
        }

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
