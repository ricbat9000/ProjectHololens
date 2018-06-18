using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTras : MonoBehaviour
{

    bool visible = true;
    public bool f = false, flag = false;
    public int cont = 0, j;

    public List<UnityEngine.Transform> lista = new List<UnityEngine.Transform>();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect()
    {
        Transform[] lista = new Transform[1 + this.lista.Count];
        GameObject prensa = GameObject.Find("Prensa");
        //Prensa prensaScript = prensa.GetComponent<Prensa>();
        Array.Clear(lista, 0, lista.Length);
        lista = this.lista.ToArray();

        GameObject cubeFrente = GameObject.Find("CubeFrente");
        ButtonFrente cubeFrenteScript = cubeFrente.GetComponent<ButtonFrente>();
        cubeFrenteScript.f = false;
        cubeFrenteScript.flag = false;

        for (j=0; f==false && j<lista.Length; j++)
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
                if (lista.Length != 0)
                {
                    cont = lista.Length - 1;
                    flag = true;
                }
                else
                {
                    cont = 0;
                }
            }
            else
            {
                cont = j - 2;
                flag = true;
            }
        }
            

        if (lista.Length > 0)
        {
            if (cont == lista.Length-1)
            {
                lista[0].gameObject.SetActive(false);
            }
            if (cont >= 0 && cont != lista.Length - 1)
            {
                lista[cont + 1].gameObject.SetActive(false);
            }

            lista[cont].gameObject.SetActive(true);

            if (cont == 0)
            {
                cont = lista.Length;
            }
            
            cont--;
        }
    }
}
