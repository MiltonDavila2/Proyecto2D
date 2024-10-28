using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamarACancion : MonoBehaviour
{
    [SerializeField] private AudioClip IntroMenu;

    void Start(){
        ControladorSonido.Instance.ReproducirMusicaFondo(IntroMenu);
    }
}
