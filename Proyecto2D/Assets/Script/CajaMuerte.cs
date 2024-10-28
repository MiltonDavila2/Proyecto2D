using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaMuerte : MonoBehaviour
{
    [SerializeField] Vector3 Dimensiones;


    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Dimensiones);
    }



}
