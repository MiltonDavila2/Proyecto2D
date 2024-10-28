using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSonido : MonoBehaviour
{
   public static ControladorSonido Instance;

   private AudioSource _audioSource;


   private void Awake(){

    if(Instance == null){

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }else{
        Destroy(gameObject);
    }
    _audioSource = GetComponent<AudioSource>();

        
   }

   public void EjecutarSonido(AudioClip sonido){
        _audioSource.PlayOneShot(sonido);
   }

   public void ReproducirMusicaFondo(AudioClip musicaClip){
       if(_audioSource.clip != musicaClip){
           _audioSource.clip = musicaClip;
           _audioSource.loop = true; 
           _audioSource.Play();
       }
   }


    public void DetenerMusicaFondo(){
       if (_audioSource.isPlaying){
           _audioSource.Stop(); 
       }
   }

    

}
