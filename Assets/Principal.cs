using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Principal : MonoBehaviour {

	public GameObject jogadorFelpudo;
    public GameObject felpudoIdle;
    public GameObject felpudoBate;

    public GameObject barril;
    public GameObject inimigoEsq;
    public GameObject inimigoDir;

	public Text pontuacao;

	public GameObject barra;

	public AudioClip somBate;
    public AudioClip somPerde;

    float escalaJogadorHorizontal;
	private List<GameObject> listaBlocos;
	bool ladoPersonagem;
	int score;

	bool comecou;
	bool acabou;

    void Start () {
		escalaJogadorHorizontal = transform.localScale.x;

		felpudoBate.SetActive(false);
        listaBlocos = new List<GameObject>();
        CriaBarrisInicio();

		pontuacao.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 + 100);
		pontuacao.text = "Toque Para Iniciar!";
		pontuacao.fontSize = 25;
	}
	
	// Update is called once per frame
	void Update () {

		if (!acabou)
		{
            if (Input.GetButtonDown("Fire1"))
            {
				if (!comecou)
				{
					comecou = true;
					barra.SendMessage("Comecou");
				}
				GetComponent<AudioSource>().PlayOneShot(somBate);

                if (Input.mousePosition.x > Screen.width / 2)
                {
                    bateDireita();
                }
                else
                {
                    bateEsquerda();
                }
                listaBlocos.RemoveAt(0);
                ReposicionaBlocos();
                ConfereJogada();

            }
        }
	}

	void bateDireita()
	{
		ladoPersonagem = true;
        felpudoIdle.SetActive(false);
        felpudoBate.SetActive(true);
        jogadorFelpudo.transform.position = new Vector2(1.1f, jogadorFelpudo.transform.position.y);
		jogadorFelpudo.transform.localScale = new Vector2(-escalaJogadorHorizontal, jogadorFelpudo.transform.localScale.y);
		Invoke("voltaAnimacao", 0.25f);
		listaBlocos[0].SendMessage("BateDireita");
	}
    void bateEsquerda()
    {
		ladoPersonagem = false;
        felpudoIdle.SetActive(false);
        felpudoBate.SetActive(true);
        jogadorFelpudo.transform.position = new Vector2(-1.1f, jogadorFelpudo.transform.position.y);
        jogadorFelpudo.transform.localScale = new Vector2(escalaJogadorHorizontal, jogadorFelpudo.transform.localScale.y);
        Invoke("voltaAnimacao", 0.25f);
        listaBlocos[0].SendMessage("BateEsquerda");
    }

	void VoltaAnimacao()
	{
        felpudoIdle.SetActive(true);
        felpudoBate.SetActive(false);
    }

	GameObject CriaNovoBarril(Vector2 posicao)
	{
		GameObject novoBarril;

		if(Random.value > 0.5f || (listaBlocos.Count <= 2))
		{
			novoBarril = Instantiate(barril);
		}
		else
		{
			if(Random.value > 0.5f)
			{
                novoBarril = Instantiate(inimigoDir);
			}
			else
			{
                novoBarril = Instantiate(inimigoEsq);
            }
        }
		novoBarril.transform.position = posicao;
		return novoBarril;
	}

	void CriaBarrisInicio()
	{
		for(int i = 0; i <= 7; i++)
		{
            GameObject objetoBarril = CriaNovoBarril(new Vector2(0, -2.1f + (i * 0.99f)));
			listaBlocos.Add(objetoBarril);
        }
	}

	void ReposicionaBlocos()
	{
		GameObject objetoBarril = CriaNovoBarril(new Vector2(0, -2.1f + (8 * 0.99f)));
        listaBlocos.Add(objetoBarril);
        for (int i = 0; i <= 7; i++)
		{
			listaBlocos[i].transform.position = new Vector2(listaBlocos[i].transform.position.x, listaBlocos[i].transform.position.y - 0.99f);
		}
    }

	void ConfereJogada()
	{
		if (listaBlocos[0].gameObject.CompareTag("inimigo"))
		{
			if ((listaBlocos[0].name == "inimigoEsq(Clone)" && !ladoPersonagem) || (listaBlocos[0].name == "inimigoDir(Clone)" && ladoPersonagem))
			{
				FimDeJogo();

            }
			else
			{
                MarcaPonto();
            }
		}
		else
		{
            MarcaPonto();
        }
	}

	void MarcaPonto()
	{
		score++;
		pontuacao.text = score.ToString();
		pontuacao.fontSize = 100;
		pontuacao.color = new Color(0.95f, 1.0f, 0.35f);
		barra.SendMessage("AumentaBarra");
	}

	void FimDeJogo()
	{
		acabou = true;
		felpudoBate.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);
        felpudoIdle.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);

		jogadorFelpudo.GetComponent<Rigidbody2D>().isKinematic = false;
        

		if (ladoPersonagem)
		{
            jogadorFelpudo.GetComponent<Rigidbody2D>().AddTorque(100.0f);
            jogadorFelpudo.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, 3.0f);
        }
		else
		{
            jogadorFelpudo.GetComponent<Rigidbody2D>().AddTorque(-100.0f);
			jogadorFelpudo.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, 3.0f);

        }
        GetComponent<AudioSource>().PlayOneShot(somPerde);
        Invoke("RecarregaCena", 2);
    }

	void RecarregaCena()
	{
		Application.LoadLevel("MinhaCena");
	}
}
