using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class SoundsControl : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Audio Source Reference")]
    public AudioSource source;
    private AudioSource playSounds;

    [Header("Audio Files")]
    public AudioClip beginDrilling;
    public AudioClip onGoingDrilling;
    public AudioClip[] drillingEpisodes;

    [Header("Stats")]
    public bool IsDrilling = false;

    
    public GameObject soundConditions;
    public GameObject noSoundCondition;
    public GameObject ControlMethod;

    public void BeginDrilling() 
    {
        source.clip = beginDrilling;
        source.Play();

        IsDrilling = true;
    }

    public void StopDrilling() 
    {
        source.Stop();
        IsDrilling = false;
    }

    private void Start() 
    {
        playSounds = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (ControlMethod.active == false && noSoundCondition.active == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                playSounds = GetComponent<AudioSource>();

                playSounds.PlayOneShot(beginDrilling, 0.5f);

            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playSounds.Stop();

        }


    }

            void OnMouseDown()
    {

        if (noSoundCondition.active == false)
        {
            playSounds = GetComponent<AudioSource>();

            playSounds.PlayOneShot(beginDrilling, 0.5f);
        }

    }


     void OnMouseUp()
    {
        if (noSoundCondition.active == false )
        {

         
            playSounds.Stop();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (soundConditions.active != true || noSoundCondition.active != false) {
            return;
        }

       

        if (other.gameObject.name == "WoodBoard1")
        {                        
            source.PlayOneShot(drillingEpisodes[0], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard2")
        {
            source.PlayOneShot(drillingEpisodes[1], 0.6f);
        }

        if (other.gameObject.name == "WoodBoard3")
        {
            source.PlayOneShot(drillingEpisodes[2], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard4")
        {

            source.PlayOneShot(drillingEpisodes[3], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard5")
        {
            

            source.PlayOneShot(drillingEpisodes[4], 0.6f); 
        }


        if (other.gameObject.name == "WoodBoard6")
        {
            
            source.PlayOneShot(drillingEpisodes[5], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard7")
        {

            
            source.PlayOneShot(drillingEpisodes[6], 0.6f);
        }

        if (other.gameObject.name == "WoodBoard8")
        {
            

            source.PlayOneShot(drillingEpisodes[7], 0.6f);
        }
        if (other.gameObject.name == "WoodBoard9")
        {

            
            source.PlayOneShot(drillingEpisodes[8], 0.6f);
        }
        if (other.gameObject.name == "WoodBoard10")
        {

            
            source.PlayOneShot(drillingEpisodes[9], 0.6f);
        }
        
        if (other.gameObject.name == "WoodBoard11")
        {
            

            source.PlayOneShot(drillingEpisodes[10], 0.6f);


        }


        if (other.gameObject.name == "WoodBoard12")
        {
            

            source.PlayOneShot(drillingEpisodes[11], 0.6f);
        }

        if (other.gameObject.name == "WoodBoard13")
        {

            
            source.PlayOneShot(drillingEpisodes[12], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard14")
        {

            
            source.PlayOneShot(drillingEpisodes[13], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard15")
        {
            

            source.PlayOneShot(drillingEpisodes[14], 0.6f); 
        }


        if (other.gameObject.name == "WoodBoard16")
        {
            
            source.PlayOneShot(drillingEpisodes[15], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard17")
        {

        
            source.PlayOneShot(drillingEpisodes[16], 0.6f);
        }

        if (other.gameObject.name == "WoodBoard18")
        {
            

            source.PlayOneShot(drillingEpisodes[17], 0.6f);
        }
        if (other.gameObject.name == "WoodBoard19")
        {

            
            source.PlayOneShot(drillingEpisodes[18], 0.6f);
        }
        if (other.gameObject.name == "WoodBoard20")
        {

            
            source.PlayOneShot(drillingEpisodes[19], 0.6f);
        }
        if (other.gameObject.name == "WoodBoard21")
        {


            source.PlayOneShot(drillingEpisodes[20], 0.6f);


        }


        if (other.gameObject.name == "WoodBoard22")
        {


            source.PlayOneShot(drillingEpisodes[21], 0.6f);
        }

        if (other.gameObject.name == "WoodBoard23")
        {


            source.PlayOneShot(drillingEpisodes[22], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard24")
        {


            source.PlayOneShot(drillingEpisodes[23], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard25")
        {


            source.PlayOneShot(drillingEpisodes[24], 0.6f); 
        }


        if (other.gameObject.name == "WoodBoard26")
        {

            source.PlayOneShot(drillingEpisodes[25], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard27")
        {


            source.PlayOneShot(drillingEpisodes[26], 0.6f);
        }

        if (other.gameObject.name == "WoodBoard28")
        {


            source.PlayOneShot(drillingEpisodes[27], 0.6f);
        }
        if (other.gameObject.name == "WoodBoard29")
        {


            source.PlayOneShot(drillingEpisodes[28], 0.6f);
        }
        if (other.gameObject.name == "WoodBoard30")
        {


            source.PlayOneShot(drillingEpisodes[29], 0.6f);
        }
        if (other.gameObject.name == "WoodBoard31")
        {


            source.PlayOneShot(drillingEpisodes[30], 0.6f);


        }


        if (other.gameObject.name == "WoodBoard32")
        {


            source.PlayOneShot(drillingEpisodes[31], 0.6f);
        }

        if (other.gameObject.name == "WoodBoard33")
        {


            source.PlayOneShot(drillingEpisodes[32], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard34")
        {


            source.PlayOneShot(drillingEpisodes[33], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard35")
        {


            source.PlayOneShot(drillingEpisodes[34], 0.6f); 
        }


        if (other.gameObject.name == "WoodBoard36")
        {

            source.PlayOneShot(drillingEpisodes[35], 0.6f);
        }


        if (other.gameObject.name == "WoodBoard37")
        {


            source.PlayOneShot(drillingEpisodes[36], 0.6f);
        }

        if (other.gameObject.name == "WoodBoard38")
        {


            source.PlayOneShot(drillingEpisodes[37], 0.6f);
        }
        if (other.gameObject.name == "WoodBoard39")
        {


            source.PlayOneShot(drillingEpisodes[38], 0.6f);
        }
        if (other.gameObject.name == "WoodBoard40")
        {


            source.PlayOneShot(drillingEpisodes[39], 0.65f);
        }
        if (other.gameObject.name == "WoodBoard41")
        {            
            source.PlayOneShot(drillingEpisodes[40], 0.8f);
        }
    }
}