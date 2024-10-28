using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;  

public class BotonPausa : MonoBehaviour
{

    [SerializeField] private TMP_Text MonedasDoradas;

    [SerializeField] private TMP_Text MonedasPlateadas;

    [SerializeField] private TMP_Text MonedasBronce;


    [SerializeField] private TMP_Text TextoDorado;

    [SerializeField] private TMP_Text TextoPlata;

    [SerializeField] private TMP_Text TextoBronce;


    [SerializeField] private TMP_Text TextoIndicativo;

    [SerializeField] private GameObject botonPausa;

    [SerializeField] private GameObject menuPausa;

    [SerializeField] private GameObject menuMuerte;



    [SerializeField] private RawImage imagenMonedaOro;

    [SerializeField] private RawImage imagenPlata;

    [SerializeField] private RawImage imagenBronce;




    private bool juegoPausado = false;

    private Movimiento2D Movimiento2d;


    
    public void ActivarVictoria(){
        TextoIndicativo.text= (" YOU WIN ");
        ActivarMuerte();
    }



    public void ActivarMuerte(){
        TextoDorado.text=(("x")+FindAnyObjectByType<Movimiento2D>().contadorOro).ToString();
        TextoPlata.text= (("X")+FindAnyObjectByType<Movimiento2D>().contadorPlata).ToString();
        TextoBronce.text = (("X")+FindAnyObjectByType<Movimiento2D>().contadorBronce).ToString();
        menuMuerte.SetActive(true);
        botonPausa.SetActive(false);
        MonedasDoradas.text = ("").ToString();
        MonedasPlateadas.text = ("").ToString();
        MonedasBronce.text = ("").ToString();
        imagenMonedaOro.enabled = false;
        imagenPlata.enabled = false;
        imagenBronce.enabled = false;
    }




    private void Update(){
        

        

        MonedasDoradas.text = (("Monedas Doradas x")+FindAnyObjectByType<Movimiento2D>().contadorOro).ToString();
        MonedasPlateadas.text = (("Monedas Plata x")+FindAnyObjectByType<Movimiento2D>().contadorPlata).ToString();
        MonedasBronce.text = (("Monedas Bronce x")+FindAnyObjectByType<Movimiento2D>().contadorBronce).ToString();



        if(Input.GetKeyDown(KeyCode.Escape)){

            if(juegoPausado){
                Reanudar();
            }else{
                Pausa();
            }
        }
    }

    public void Pausa(){
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar(){
        juegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Cerrar(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}