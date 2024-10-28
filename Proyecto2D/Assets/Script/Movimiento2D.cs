using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movimiento2D : MonoBehaviour
{

    private Rigidbody2D rb2D;

    [Header("Movimiento2D")]

    private float MovimientoHorizontal = 0f;

    [SerializeField] private float VelocidadMovimiento = 400f;

    [SerializeField] private float SuavizadorMovimiento = 0.3f;

    private Vector3 velocidad = Vector3.zero;

    private bool mirandoDerecha = true;


    [Header("Salto")]

    [SerializeField] private float fuerzaSalto;

    [SerializeField] private LayerMask queEsSuelo;
     
    [SerializeField] private Transform controladorSuelo;

    [SerializeField] private Vector3 dimensionesCaja;

    [SerializeField] private bool Ensuelo;

    private bool salto = false;

    [Header("Contadores")]

    public int contadorBronce = 0;
    public int contadorPlata = 0;
    public int contadorOro = 0;
    

    [Header ("Sonidos")]
    [SerializeField] private AudioClip Audiocolectar;
    [SerializeField] private AudioClip AudioMorir;
    [SerializeField] private AudioClip AudioSalto;
    [SerializeField] private AudioClip AudioVictoria;
    [SerializeField] private AudioClip AudioFondo;

    [Header ("TP")]
    [SerializeField] private Transform portalEntrada; 
    [SerializeField] private Transform portalSalida;


    [Header("Animacion")]

    private Animator animator;


    [Header("Eventos")]

    public UnityEvent MuerteJugador;


    void Start()
    {
        ControladorSonido.Instance.ReproducirMusicaFondo(AudioFondo);
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoHorizontal = Input.GetAxisRaw("Horizontal") * VelocidadMovimiento; 

        animator.SetFloat("Horizontal", Mathf.Abs(MovimientoHorizontal));

        if(Input.GetButtonDown("Jump")){
            salto = true;
        }
    }



    private void FixedUpdate()
    {
        Ensuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("Ensuelo", Ensuelo);
        Mover(MovimientoHorizontal * Time.fixedDeltaTime, salto);
        salto = false;
    }



    private void Mover(float mover, bool saltar){
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, SuavizadorMovimiento);
        if(mover > 0 && !mirandoDerecha){
            Girar();
        }else if(mover < 0 && mirandoDerecha){
            Girar();
        }

        if(Ensuelo && saltar){
            ControladorSonido.Instance.EjecutarSonido(AudioSalto);
            Ensuelo = false;
            rb2D.AddForce(new Vector2(0f,fuerzaSalto));
        }
    }



    private void Girar(){
        mirandoDerecha =!mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }


    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }

   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        

        if (collision.gameObject.CompareTag("Enemigo") || collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Muerte") )
        {
            Muerte();

        }else if (collision.gameObject.CompareTag("Finish"))
        {   
            Victoria();
           
        }else if (collision.gameObject.CompareTag("Moneda"))
        {   
            Destroy(collision.gameObject); 
            ControladorSonido.Instance.EjecutarSonido(Audiocolectar);
            contadorBronce++;
            Debug.Log("Monedas de Bronce: " + contadorBronce);
           
        }
        else if (collision.gameObject.CompareTag("MonedaPlateada"))
        {
            Destroy(collision.gameObject);
            ControladorSonido.Instance.EjecutarSonido(Audiocolectar);
            contadorPlata++;
            Debug.Log("Monedas de Plata: " + contadorPlata);
            
        }
        else if (collision.gameObject.CompareTag("MonedaDorada"))
        {
            Destroy(collision.gameObject);
            ControladorSonido.Instance.EjecutarSonido(Audiocolectar);
            contadorOro++;
            Debug.Log("Monedas de Oro: " + contadorOro);
           
        }
    }

    private void Muerte()
    {   
        ControladorSonido.Instance.DetenerMusicaFondo();
        ControladorSonido.Instance.EjecutarSonido(AudioMorir);
        FindAnyObjectByType<BotonPausa>().ActivarMuerte();
        Destroy(gameObject); 
    }



    private void Victoria(){
        ControladorSonido.Instance.DetenerMusicaFondo();
        ControladorSonido.Instance.EjecutarSonido(AudioVictoria);
        FindAnyObjectByType<BotonPausa>().ActivarVictoria();
        Destroy(gameObject); 
        
    }
}
