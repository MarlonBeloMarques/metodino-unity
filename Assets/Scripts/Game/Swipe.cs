using UnityEngine;


public class Swipe : MonoBehaviour
{
    private bool tap, swipeUp, swipeDown; // atributos tap = apertou, e os lados para arrastar.
    private bool isDraging = false; // arrastando inicia falso
    private Vector2 startTouch, swipeDelta; // starttouch = inicia toque, swipedelta = area do deslize.

    private void Update()
    {
        
        tap = swipeUp = swipeDown = false; //tap recebe todo tipo de toque na tela, iniciando como falso.

        #region Standalone Inputs 
        // entradas automatas

        if(Input.GetMouseButtonDown(0)) // obter o botão do mouse para baixo, ao apertar, ele ativa
        {
            tap = true; // apertou vai ser verdadeiro
            isDraging = true; // arrastando vai ser verdadeiro
            startTouch = Input.mousePosition; // começar a tocar recebe entrada do mouse posicao
        }
        else if(Input.GetMouseButtonUp(0)) // se nao, obter o botão do mouse, ao soltar ele ativa
        {
            isDraging = false; // arrastando recebe falso
            Reset(); // resetae... começar a tocar e a area do toque recebem 0, arrastando recebe falso
        }
        #endregion

        #region Mobile Inputs
        //regiao das entradas para celular 

        if(Input.touches.Length>0) // touches = toques , length = comprimento... se entrada.toque.comprimento for maior que 0
        {
            if(Input.touches[0].phase == TouchPhase.Began) // phase = estagio, began = começasse...se entrada.toque.estagio for igual a começar
            {
                isDraging = true; // arrastando é verdadeiro
                tap = true; // toque é verdadeiro
                startTouch = Input.touches[0].position; // começar toque recebe a entrada. toque.posição
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) // ended = terminou... se nao entrada.toque.estagio for igual ao terminou ou igual a cancelado
            {
                isDraging = false; // arrastando é falso
                Reset(); // resetar... começar a tocar e a area do toque recebem 0, arrastando recebe 0
            }
        }
        #endregion

        // Calcular a Distancia
        swipeDelta = Vector2.zero; // a area do deslize inicia em zero
        if(isDraging) // se arrastar for verdadeiro
        {
            if(Input.touches.Length > 0) // se entrada.toque.comprimento for maior q 0
            {
                swipeDelta = Input.touches[0].position - startTouch; // entao a area do deslize recebe a entrada.toque.posição - começar toque
            }
            else if(Input.GetMouseButton(0)) // se nao, entrada.pegar.mouse.botao
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch; // a area do deslize vai receber a posicao x e y da entrada.mouse.posicao - começar toque
            }
        }


        //nós cruzamos a zona morta?
        if(swipeDelta.magnitude > 60) // se a area do deslize for maior que 60
        {
            // qual direção?
            float x = swipeDelta.x; // x recebe o x da area do deslize
            float y = swipeDelta.y; // y recebe o y da area do deslize
            if(Mathf.Abs(x) < Mathf.Abs(y)) // se x for maior que y
            {
                // cima ou baixo
                if (y < 0) // se y for menor que 0, esta para baixo
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true; // se nao, cima
                }
            }
            Reset();
        }


    }

    private void Reset() // responsavel por resetar
    {
        startTouch = swipeDelta = Vector2.zero; // começar a tocar e a area do toque recebem 0 
        isDraging = false; // arrastando recebe falso
    }

    //  metodos que so pode ser olhado e retorna os atributos... tap = toque na tela, swipedelta = area de toque, swipeleft = esquerda, swiperight = direita, swipeup = cima, swipedown = baixo.
    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }


}

