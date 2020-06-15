using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Dino : MonoBehaviour {
        private Rigidbody2D rig;
        private Animator anima;

        public bool crouching;
        public static bool EstanoChao;
        public GameObject PontoColisao;
        public LayerMask plat;
        public float raio;
        public float forcaP;

        public GameObject Pe_F;
        public GameObject Pe_T;

        public float tempC;
        public float temp;

        public static bool EstaVivo;
        public static bool Morreu;

        public AudioSource Audio;
        public AudioClip jump;
        public AudioClip crouch;
        public AudioClip die;

        public static int pontuacao;
        public UnityEngine.UI.Text txtponto;

        public float currentime;

        public BoxCollider2D cabeca;
        public BoxCollider2D corpo;

        public SwipeTest Swipe;

        //
        private int oneJump = 0;
        private int oneCrouch = 0;

        public GameObject AnimaTutoJump;
        public GameObject AnimaTutoCrouch;

        public bool Verif_Metros;

        public float Limite_Contagem;

        // Use this for initialization
        void Start() {
            rig = GetComponent<Rigidbody2D>();
            anima = GetComponent<Animator>();
            PlayerPrefs.SetInt("pontuacao", pontuacao);
            currentime = 0;

            EstaVivo = true;
        }

        // Update is called once per frame
        void Update() {

 
            // metros corridos
            txtponto.text = pontuacao.ToString();

            Diminuir_Limite_Contagem();

            if (Verif_Metros)
            {
                currentime += Time.deltaTime;

                if (currentime >= Limite_Contagem)
                {
                    currentime = 0;
                    pontuacao += 1;
                }
            }

            if (!PressIniciar.active_tuto)
            {
                if (IniciarMenu.activeTuto_2)
                {
                    currentime += Time.deltaTime;

                    if (currentime >= 0.60)
                    {
                        currentime = 0;
                        pontuacao += 1;
                    }
                }
            }

            //


            Animations();

            if (EstaVivo)
            {
                if (IniciarMenu.Iniciou)
                {
                    if (!Controle_Tutorial.tutorial)
                    {
                        Crouch();
                        Jump();
                    }
                }
            }
            else
            {
                Morreu = true;
            }

            //

            if(PressIniciar.active_tuto)
            {
                if(IniciarMenu.activeTuto_2)
                {
                    AnimaTutoJump.SetActive(true);
                }
            }


            if(Controle_Tutorial.tutorial)
            {
                Tutorial_Dino_Control_Jump();

                if(!PressIniciar.active_tuto)
                {
                    if(!IniciarMenu.activeTuto_2)
                    {
                        AnimaTutoCrouch.SetActive(true);
                        Tutorial_DIno_Control_Crouch();
                    }
                }
            }

            //

            EstanoChao = Physics2D.OverlapCircle(PontoColisao.transform.position, raio, plat);
        }

        public void OnTriggerEnter2D(Collider2D outro)
        {
            if (outro.tag == "Pedra" || outro.tag == "Pedra_Ponta")
            {
                EstaVivo = false;

                cabeca.enabled = false;
                corpo.enabled = false;

                Audio.PlayOneShot(die);

                if(EstanoChao  && !Swipe.GetComponent<SwipeTest>().pular && rig.velocity.y == 0)
                {
                    rig.AddForce(new Vector2(0, 500));
                }
                else if( rig.velocity.y <= 0)
                {
                    rig.AddForce(new Vector2(0, 1000));
                }

                PlayerPrefs.SetInt("pontuacao", pontuacao);
                if (pontuacao > PlayerPrefs.GetInt("Record"))
                {
                    PlayerPrefs.SetInt("Record", pontuacao);
                }
            }
        }

        private void Crouch()
        {
            if (Swipe.GetComponent<SwipeTest>().abaixar && !crouching && EstanoChao)
            {
                Swipe.GetComponent<SwipeTest>().abaixar = false;

                crouching = true;

                Audio.PlayOneShot(crouch);

                cabeca.transform.position = new Vector3(transform.position.x, transform.position.y - (-0.2f),transform.position.z);
                cabeca.isTrigger = true;
            }

            if (crouching)
            {
                temp += Time.deltaTime;
                if (temp >= tempC)
                {
                    temp = 0;
                    crouching = false;
                    
                    cabeca.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
                }
            }
        }

        

        private void Jump()
        {
            if (Swipe.GetComponent<SwipeTest>().pular && EstanoChao)
            {
                temp = 0;
                Swipe.GetComponent<SwipeTest>().pular = false;

                Audio.PlayOneShot(jump);
                rig.AddForce(new Vector2(0, forcaP));
                if (crouching)
                {
                    cabeca.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
                    crouching = false;
                }
            }


            Pe_F.SetActive(EstanoChao);
            Pe_T.SetActive(EstanoChao);

        }

        private void Animations()
        {
            anima.SetBool("Dino_Crouch", crouching);
            anima.SetBool("Dino_Jump", !EstanoChao);
            anima.SetFloat("VelVert", rig.velocity.y);

            if (Morreu)
            {
                anima.SetBool("Dino_Die",true);
                Pe_F.SetActive(false);
                Pe_T.SetActive(false);

            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(PontoColisao.transform.position, raio);
        }

        public void Tutorial_Dino_Control_Jump()
        {
            if (Swipe.GetComponent<SwipeTest>().pular && EstanoChao && oneJump==0)
            {
                StartCoroutine("Desativa_TutoJump");
              
                oneJump = 1;
                Swipe.GetComponent<SwipeTest>().pular = false;

                Audio.PlayOneShot(jump);
                rig.AddForce(new Vector2(0, forcaP));
                if (crouching)
                {
                    crouching = false;
                }
                
            }

            Pe_F.SetActive(EstanoChao);
            Pe_T.SetActive(EstanoChao);

        }

        public void Tutorial_DIno_Control_Crouch()
        {
            if (Swipe.GetComponent<SwipeTest>().abaixar && !crouching && EstanoChao && oneCrouch==0)
            {
                StartCoroutine("Desativa_TutoCrouch");

                oneCrouch = 1;

                Swipe.GetComponent<SwipeTest>().abaixar = false;

                
                crouching = true;
                Audio.PlayOneShot(crouch);
            }

            if (crouching)
            {
                temp += Time.deltaTime;
                if (temp >= tempC)
                {
                    crouching = false;
                    temp = 0;

                    cabeca.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
                }
            }
        }

        IEnumerator Desativa_TutoJump()
        {
            yield return new WaitForSeconds(0.4f);
            PressIniciar.active_tuto = false;
            IniciarMenu.activeTuto_2 = false;
            AnimaTutoJump.SetActive(false);
        }



        IEnumerator Desativa_TutoCrouch()
        {
            yield return new WaitForSeconds(0.4f);
            Controle_Tutorial.tutorial = false;
            AnimaTutoCrouch.SetActive(false);
            Verif_Metros = true;
        }

        public void Diminuir_Limite_Contagem()
        {
            if (pontuacao > 250)
            {
                Limite_Contagem = 0.20f;
            }
            else if(pontuacao > 150)
            {
                Limite_Contagem = 0.30f;
            }
            else if (pontuacao > 100)
            {
                Limite_Contagem = 0.40f;
            }
            else if (pontuacao > 50)
            {
                Limite_Contagem = 0.50f;
            }
            else
            {
                Limite_Contagem = 0.60f;
            }
        }
    }


}
