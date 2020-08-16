using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaterialChanger : MonoBehaviour
{

    public AudioClip tickSound;
    private AudioSource audioSource;
    public Material selectedButtonMaterial;
    public Material FlickerMaterial;
    private Material buttonOriginalMaterial;
    private Renderer rendrer;

    public AudioSource AudioSource
    {
        get      
        {
            if(audioSource == null) audioSource = this.gameObject.GetComponent<AudioSource>();
            return audioSource;
        }

        set
        {
            audioSource = value;
        }
    }

    private void Start()
    {

        audioSource = this.gameObject.GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();

        }
        if (tickSound == null)
        {
            tickSound = Resources.Load<AudioClip>("tick");
        }
        audioSource.clip = tickSound;
        audioSource.Stop();
        rendrer = this.GetComponent<Renderer>();
        buttonOriginalMaterial = rendrer.material;

    }

    public void SetOnMaterial()
    {
        if (AudioSource != null)
        {
            audioSource.pitch = 1;
            audioSource.Stop();
            audioSource.Play();
        }

        rendrer.material = selectedButtonMaterial;
    }

    public void SetMaterial(Material mat)
    {
        audioSource.pitch = 1;
        audioSource.Stop();
        audioSource.Play();

        rendrer.material = mat;
    }


    public void SetOffMaterial()
    {
        if(AudioSource != null)
        {
            audioSource.pitch = 10;
            audioSource.Stop();
            audioSource.Play();
        }
        
        rendrer.material = buttonOriginalMaterial;
    }

    public void SetFlickerMateril()
    {
        if (AudioSource != null)
        {
            audioSource.pitch = 1;
            audioSource.Stop();
            audioSource.Play();
        }
        rendrer.material = FlickerMaterial;
    }

}
