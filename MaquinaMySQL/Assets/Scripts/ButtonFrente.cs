using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFrente : MonoBehaviour {

    bool visible = true;
    public bool f = false, flag = false;
    public int cont = 0, j;

    public List<UnityEngine.Transform> lista = new List<UnityEngine.Transform>();

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnSelect()
    {
        Transform[] lista = new Transform[1+this.lista.Count];
        GameObject prensa = GameObject.Find("Prensa");
        //Prensa prensaScript = prensa.GetComponent<Prensa>();
        Array.Clear(lista, 0, lista.Length);
        Debug.Log("lista.Length: " + lista.Length+"   -   "+ this.lista.Count);
        lista = this.lista.ToArray();
        

        GameObject cubeTras = GameObject.Find("CubeTras");
        ButtonTras cubeTrasScript = cubeTras.GetComponent<ButtonTras>();
        cubeTrasScript.f=false;
        cubeTrasScript.flag = false;

        //Debug.Log("lista: " + this.lista.Count.ToString());

        for (j = 0; f == false && j < lista.Length; j++)
        {
            if (lista[j].gameObject.activeInHierarchy)
            {
                f = true;
            }
        }

        if (!flag)
        {
            if (!f)
            {
                cont = 0;
                flag = true;
            }
            else
            {
                cont = j;
                flag = true;            
            }            
        }


        if (lista.Length > 0)
        {
            if (cont == 0)
            {
                lista[lista.Length - 1].gameObject.SetActive(false);
            }
            if (cont > 0)
            {
                lista[cont - 1].gameObject.SetActive(false);
            }

            if (cont == lista.Length)
            {
                cont = 0;
            }

            lista[cont].gameObject.SetActive(true);
            cont++;
        }
    }    
}
