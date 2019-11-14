using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scenecontrol2 : MonoBehaviour
{

    private dbfrutas objdb2;

    public string[] rutas2;
    public string[] anim_gen = { "", "", "", "" };

    public GameObject carta2;
    public GameObject game2;
    public GameObject scenecontrol;



    public const int griRows = 2;
    public const int griCols = 4;
    public const float offsetX = 3f;
    public const float offeseY = 3f;

    public AudioClip carta;
    AudioSource sonido;

    //////copiar al siguiente nivel
    public GameObject aux;
    public SonidoFrutaDos sss;
    //////

    [SerializeField]
    //private carta originalCard;
    public cartaDos originalCard;
    [SerializeField]
    //private Sprite[] images;
    public Sprite[] images;

    //ramdon bd
    public GameObject param;
    public Parametros prm;
    private dbfrutas objdb;
    public Sprite fruta;
    private string[] rutas;
    // 


    public int score = 0;

    private void Start()
    {
        startparametros();


        ///bd
        //param = GameObject.Find("Parametros");
        // prm = param.GetComponent<Parametros>();
        rutas = aleatorios();
        Debug.Log("aqui");
        Debug.Log("cantidad " + rutas.Length);
        for (int i = 0; i < rutas.Length; i++)
        {
            fruta = Resources.Load<Sprite>(rutas[i]);
            images[i] = fruta;

            Debug.Log("FRUTA FINAL Final" + fruta);

        }



        /////copiar al siguiente nivel
        aux = GameObject.Find("Auxiliar");
        sss = new SonidoFrutaDos();
        /////


        Vector3 startPos = originalCard.transform.position;
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3};
        //randon del array
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < griCols; i++)
        {
            for (int j = 0; j < griRows; j++)
            {
                cartaDos card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;

                }
                else
                {

                    card = Instantiate(originalCard) as cartaDos;

                }

                int index = j * griCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offeseY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }

        }

    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {

            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;

        }

        return newArray;

    }

    private cartaDos _firstReveaLed;
    private cartaDos _sconReveaLed;

    private int _score = 0;

    [SerializeField]
    private TextMesh scoreLabel = null;

    public bool canReveal
    {
        get { return _sconReveaLed = null; }

    }

    public void CardRevealed(cartaDos card)
    {
        if
        (_firstReveaLed == null)
        {

            if (_firstReveaLed = card)
            {
              //  sonido.clip = carta;
              //  sonido.Play();

            };




        }
        else
        {

            _sconReveaLed = card;
            StartCoroutine(CheckedMatch());

        }
    }
    private IEnumerator CheckedMatch()
    {
        if (_firstReveaLed.id == _sconReveaLed.id)
        {

            ////// copiar al siguiente nivel sonido
            string im = _sconReveaLed.GetComponent<SpriteRenderer>().sprite.ToString();
            string[] et = im.Split(' ');
            string n_fru = et[0];
            //Debug.Log(n_fru);//para mostrar el nombre de la fruta
            sss = aux.GetComponent<SonidoFrutaDos>();
            sss.nombrar_fruta(n_fru);
            ///////



            _score++;
            scoreLabel.text = "Puntaje: " + _score;
            if (_score == 4)
            {
                //tiempo de espera para cambio de escena ----> copiar al siguiente nivel
                yield return new WaitForSeconds(2.0f);
                SceneManager.LoadScene("ganarSegundoNivel");
            }

        }
        else
        {

            yield return new WaitForSeconds(0.2f); //tiempo que espera voltear carta cuando esta mal

            Debug.Log(_firstReveaLed.id);

            _firstReveaLed.Unreveal();
            _sconReveaLed.Unreveal();
        }

        _firstReveaLed = null;
        _sconReveaLed = null;

    }

    void cardCoparion(List<int> c)
    {

    }

    public void startparametros()
    {
        carta2 = GameObject.Find("carta");
        game2 = GameObject.Find("game");
        scenecontrol = GameObject.Find("SceneControl");

        gameObject.transform.SetAsFirstSibling();
        Debug.Log("EN PARAMETROS");
        int index = 0;
        bool rep = true;

        objdb2 = new dbfrutas();

        var totalLocal = objdb2.sqlite_totalRegistros();
        rutas = new string[totalLocal];

        for (int i = 0; i < rutas.Length; i++)
        {
            string sel = "SELECT * FROM frutas WHERE id = '";
            rutas[i] = objdb2.sqlite_consulta(sel + (i + 1) + "'");
        }

        foreach (string i in rutas)
        {
            Debug.Log("rutas en parámetros " + i);
        }
        Debug.Log("total antes de" + anim_gen.Length);

        for (int i = 0; i < anim_gen.Length; i++)
        {
            rep = true;
            while (rep == true)
            {
                rep = false;
                System.Random rand = new System.Random();
                index = rand.Next(rutas.Length);
                anim_gen[i] = rutas[index];
                if (i == 0)
                {
                    if (anim_gen[0] == anim_gen[1] || anim_gen[0] == anim_gen[2] || anim_gen[0] == anim_gen[3])
                    {
                        rep = true;
                    }
                }
                if (i == 1)
                {
                    if (anim_gen[1] == anim_gen[0] || anim_gen[1] == anim_gen[2] || anim_gen[1] == anim_gen[3])
                    {
                        rep = true;
                    }
                }
                if (i == 2)
                {
                    if (anim_gen[2] == anim_gen[0] || anim_gen[2] == anim_gen[1] || anim_gen[2] == anim_gen[3])
                    {
                        rep = true;
                    }
                }
                if (i == 3)
                {
                    if (anim_gen[3] == anim_gen[0] || anim_gen[3] == anim_gen[1] || anim_gen[3] == anim_gen[2])
                    {
                        rep = true;
                    }
                }
            }
        }

        foreach (string i in anim_gen)
        {

            Debug.Log("FRUTA FINAL Final" + i);

        }

        Debug.Log("total" + anim_gen.Length);




        carta2.SetActive(true);
        game2.SetActive(true);
        //cenecontrol.SetActive(true);
    }



    public string[] aleatorios()
    {
        return anim_gen;
    }

}

