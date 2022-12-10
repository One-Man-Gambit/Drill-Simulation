using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenseChanges : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VMD()
    {
        SceneManager.LoadScene("VisualMouseDynamic");
    }

    public void VMC()
    {
        SceneManager.LoadScene("VisualMouseContinous");

    }

    public void VMN()
    {
        SceneManager.LoadScene("VisualMouseNosound");
    }

    public void VKD()
    {
        SceneManager.LoadScene("VisualKeyboardDynamic");
    }

    public void VKC()
    {
        SceneManager.LoadScene("VisualKeyboardContinous");
    }

    public void VKN()
    {
        SceneManager.LoadScene("VisualKeyboardNosound");
    }

    public void NVMD()
    {
        SceneManager.LoadScene("NVMouseDynamic");
    }

    public void NVMC()
    {
        SceneManager.LoadScene("NVMouseContinous");
    }

    public void NVMN()
    {
        SceneManager.LoadScene("NVMouseNosound");
    }

    public void NVKD()
    {
        SceneManager.LoadScene("NVKeyboardDynamic");
    }

    public void NVKC()
    {
        SceneManager.LoadScene("NVKeyboardContinous");
    }

    public void NVKN()
    {
        SceneManager.LoadScene("NVKeyboardNosound");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Training()
    {
        SceneManager.LoadScene("IntroOne");
    }

    public void NewSession()
    {
        SceneManager.LoadScene("VisualMouseDynamic");
    }


    public void goBacktoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void goIntroPage()
    {
        SceneManager.LoadScene("IntroPage");
    }
}
