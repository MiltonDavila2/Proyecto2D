using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BotonCanvas : MonoBehaviour
{
    public void Jugar(){
        SceneManager.LoadScene(1);
    }


    public void Cerra(){
        Debug.Log("Se cierra Jump and Conquer");
        Application.Quit();
    }
}
