using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    [Header("Movimiento")]

    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float RangoMovimiento = 2f;

    private Vector3 direccion = Vector3.left;
    private float posicionInicialX;
    

    [Header("Muerte")]

    [SerializeField] private string tagCabeza = "Player";
     
    [SerializeField] private Transform controladorCabeza;

    [SerializeField] private Vector3 dimensionesCaja;

    [SerializeField] private bool Encima;

    [SerializeField] private GameObject objetoAlMorir;

    [Header("Animacion")]

    private Animator animator;

    [Header("Audio")]


    [SerializeField] private AudioClip SonidomuerteMonstruo;






    // Start is called before the first frame update
    void Start()
    {
        posicionInicialX = transform.position.x;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverEnemigo();
        animator.SetBool("Encima", Encima);
    }

    void FixedUpdate()
    {
        
        Collider2D colision = Physics2D.OverlapBox(controladorCabeza.position, dimensionesCaja, 0f);
        if (colision != null && colision.CompareTag(tagCabeza))
        {
            Encima = true;
            Debug.Log("El jugador está encima de la cabeza del enemigo");
            Muerte();
        }
        else
        {
            Encima = false;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(controladorCabeza.position, dimensionesCaja);
    }

    private void MoverEnemigo()
    {
        
        transform.position += direccion * velocidad * Time.deltaTime;

        
        if (transform.position.x >= posicionInicialX + RangoMovimiento || transform.position.x <= posicionInicialX - RangoMovimiento)
        {
            
            direccion = -direccion; 

            
            if (direccion == Vector3.right)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); 
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f); 
            }
        }
    }

    private void Muerte()
    {

        ControladorSonido.Instance.EjecutarSonido(SonidomuerteMonstruo);
        
        if (objetoAlMorir != null)
        {
            Instantiate(objetoAlMorir, transform.position, Quaternion.identity);
        }
        
        
        Destroy(gameObject);
    }

}

