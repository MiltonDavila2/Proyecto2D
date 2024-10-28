using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    [SerializeField] private Transform portalSalida; 
    [SerializeField] private AudioClip TP;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            ControladorSonido.Instance.EjecutarSonido(TP);
            other.transform.position = portalSalida.position;
        }
    }
}
