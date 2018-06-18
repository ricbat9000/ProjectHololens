using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Networking;

public class TextUI : MonoBehaviour
{
    private Text instruction;
    private bool flag;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitForRequest());
        var obj = GameObject.Find("ServidorWarn");
        instruction = obj.GetComponent<Text>();
        if (flag)
        {
            instruction.text = "Sem comunicação com o servidor, contacte o administrador.";
        }
        else
        {
            instruction.text = "";
            obj.SetActive(false);
        } 
    }

    IEnumerator WaitForRequest()
    {
        string url = "http://maintenance4.estig.ipb.pt";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            // check for errors
            if (www.error == null)
            {
                flag = false;
                Debug.Log("Text : " + www.downloadHandler.text);
            }
            else
            {
                flag = true;
                Debug.Log("WWW Error: " + www.error);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
