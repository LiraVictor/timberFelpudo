using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barra : MonoBehaviour {

	float escalaBarra;
	bool terminou;
	bool comecou;
	public GameObject cameraCena;

	// Use this for initialization
	void Start () {

		escalaBarra = this.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (comecou)
		{
            if(escalaBarra > 0.0f)
			{
                escalaBarra = escalaBarra - 0.15f * Time.deltaTime;
                this.transform.localScale = new Vector2(escalaBarra, 1.0f);
			}
			else
			{
				if (!terminou)
				{
					terminou = true;
					cameraCena.SendMessage("FimDeJogo");
				}
			}
        }
	}

	void Comecou()
	{
		comecou = true;
	}

    void AumentaBarra()
    {
        escalaBarra = escalaBarra + 0.035f;
        if (escalaBarra > 1.0f)
		{
			escalaBarra = 1.0f;
		}
    }
}
