using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Prensa : MonoBehaviour
{
    private bool flag = false, f2 = false;
    private GameObject obj;
    private GameObject textoui;
    private Text instruction;
    private GameObject grid;
    private int cont = 1;
    private int x = 1;
    public List<UnityEngine.Transform> children = new List<UnityEngine.Transform>();

    public ButtonTras cubeTrasScript;
    public ButtonFrente cubeFrenteScript;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitForRequest());
        obj = GameObject.Find("PrensaText");    //Objeto da maquina selecionado
        obj.SetActive(false);
    }

    //Input para gestos
    void OnSelect()
    {
        GameObject cubeTras = GameObject.Find("CubeTras");
        cubeTrasScript = cubeTras.GetComponent<ButtonTras>();
        GameObject cubeFrente = GameObject.Find("CubeFrente");
        cubeFrenteScript = cubeFrente.GetComponent<ButtonFrente>();
        if (flag)
        {
            obj.SetActive(false);
            flag = false;
        }
        else if (flag == false)
        {
            obj.SetActive(true);
            flag = true;
        }

        cubeFrenteScript.lista.Clear();
        cubeTrasScript.lista.Clear();
        if (cubeFrenteScript.cont!=0 && GameObject.Find("Prensa" + cubeFrenteScript.cont)!=null)
        {
            GameObject filho1 = GameObject.Find("Prensa" + cubeFrenteScript.cont);
            filho1.gameObject.SetActive(false);
        }
        if (cubeTrasScript.cont == cubeTrasScript.lista.Count)
        {
            if (cubeTrasScript.cont != 0 && GameObject.Find("Prensa" + cubeTrasScript.cont) != null)
            {
                GameObject filho2 = GameObject.Find("Prensa" + cubeTrasScript.cont);
                filho2.gameObject.SetActive(false);
            }
        }
        else
        {
            if (cubeTrasScript.cont != 0 && GameObject.Find("Prensa" + (cubeTrasScript.cont + 2)) != null)
            {
                GameObject filho2 = GameObject.Find("Prensa" + (cubeTrasScript.cont + 2));
                filho2.gameObject.SetActive(false);
            }
        }
        

        if (!f2)
        {
            var numPrensa = 1;
            foreach (Transform child in grid.transform)
            {
                if (child.name.Equals("Prensa" + numPrensa))
                {
                    numPrensa++;
                    cubeFrenteScript.lista.Add(child);
                    cubeTrasScript.lista.Add(child);
                }
            }
            f2 = true;
        }
        else
        {
            f2 = false;
        }
        

        Debug.Log("count lista: " + cubeFrenteScript.lista.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitForRequest()
    {
        textoui = GameObject.Find("PrensaText");    //Objeto da maquina selecionado
        grid = GameObject.Find("Panel");         //Panel onde vai ficar o componente texto

        instruction = textoui.GetComponent<Text>();
        instruction.text = "\t<color=#00ffffff>Prensa</color>\n";

        string url = "http://maintenance4.estig.ipb.pt/Hololens/prensa.php";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            // check for errors
            if (www.error == null)
            {
                string[] res = parseRes(www.downloadHandler.text);
                for (int i = 0; i < res.Length; i++)
                {
                    if (i < res.Length-1)
                    {
                        CreateText(grid.transform, i + 1 + "º - " + res[i]);                      
                        cont++;
                    }
                    else
                    {
                        CreateText(grid.transform, res[i]);
                        var test = GameObject.Find("Prensa" + i);
                        instruction.text += res[i] +"\n";
                    }                    
                }
            }
            else
            {
                Debug.Log("WWW Error: " + www.error);
            }
        }

        //Itera os gameobjects filhos do gameobject Panel e caso sejam relevantes ao objecto prensa estes são adicionados numa Lista
        
        
    }

    string[] parseRes(String texto)
    {
        string[] res = texto.Split('\n');
        return res;
    }

    GameObject CreateText(Transform canvas_transform, string text_to_print)
    {
        //Caracteristicas comuns
        int font_size = 10;
        float x = -250;
        float y = 150f;
        float z = 0f;
        float width = 300;
        float height = 100;
        Color text_color = Color.white;

        //Cria o gameobject que vai ficar com o texto e define o seu objeto pai
        GameObject UItextGO = new GameObject("Prensa" + cont);
        UItextGO.transform.SetParent(canvas_transform);

        //Adiciona o componente de texto ao objecto vazio criado em cima
        Text text = UItextGO.AddComponent<Text>();

        //Configura a posição do objecto, largura, altura, escala e a sua rotação
        text.rectTransform.anchoredPosition3D = new Vector3(x, y, z);
        text.rectTransform.sizeDelta = new Vector2(width, height);
        text.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        text.rectTransform.localRotation = Quaternion.Euler(0, -25, 0);

        //Adiciona o texto com cor, tamanho e estilo de fonte
        text.text = text_to_print;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = font_size;
        text.color = text_color;

        //Configura o componente para permitir overflow horizontal e vertical
        text.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap;
        text.verticalOverflow = UnityEngine.VerticalWrapMode.Overflow;

        //Faz com que o objeto seja iniciado com estado desativo
        UItextGO.SetActive(false);

        return UItextGO;
    }
}

