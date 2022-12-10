using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour {

	// Use this for initialization
	void BateDireita () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 2);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddTorque(100.0f);
        Invoke("ApagaBloco", 2.0f);
    }

    void BateEsquerda()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(10, 2);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddTorque(-100.0f);
        Invoke("ApagaBloco", 2.0f);
    }

    void ApagaBloco()
    {
        Destroy(this.gameObject);
    }
}
