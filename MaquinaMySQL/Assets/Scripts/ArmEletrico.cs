using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ArmEletrico : MonoBehaviour
{
    private bool flag = false;
    private GameObject obj;
    private GameObject textoui;
    private Text instruction;
    private GameObject grid;
    private int cont = 1;
    private int x = 1;

    // Use this for initialization
    void Start()
    {
        obj = GameObject.Find("ArmEletrico");    //Objeto da maquina selecionado
        obj.SetActive(false);
        StartCoroutine(WaitForRequest());
    }

    //Input para gestos
    void OnSelect()
    {
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitForRequest()
    {
        grid = GameObject.Find("Panel");         //Grid onde vai ficar o texto
        instruction = obj.GetComponent<Text>();
        instruction.text = "\tArmario Eletrico\n";

        string url = "http://maintenance4.estig.ipb.pt/Hololens/cano.php";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            // check for errors
            if (www.error == null)
            {
                string[] res = parseRes(www.downloadHandler.text);
                for (int i = 0; i < res.Length; i++)
                {
                    CreateText(grid.transform, 0, 0, res[i], 10, Color.white);
                    var test = GameObject.Find("ArmEletrico" + i);
                    cont++;
                    instruction.text += res[i];
                    instruction.text += "\n";
                }
                Debug.Log("Text : " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("WWW Error: " + www.error);
            }
        }
    }

    string[] parseRes(String texto)
    {
        char[] charsToTrim = { ']', ' ', '[' };
        texto = texto.Replace(@"""", @"");
        texto = texto.Trim(charsToTrim);
        string[] res = texto.Split(',');
        return res;
    }

    GameObject CreateText(Transform canvas_transform, float x, float y, string text_to_print, int font_size, Color text_color)
    {
        GameObject UItextGO = new GameObject("ArmEletrico" + cont);
        UItextGO.transform.SetParent(canvas_transform);

        RectTransform trans = UItextGO.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(x, y);

        Text text = UItextGO.AddComponent<Text>();
        text.text = text_to_print;
        text.fontSize = font_size;
        text.color = text_color;

        return UItextGO;
    }
}
