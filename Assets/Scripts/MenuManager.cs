using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject Player;
    public GameObject canvas;
    public Text Canvastext;
    public GameObject wavepoint;
    public GameObject MenuCanvas;
    public AudioSource ButtonClick;

    bool check = false;
    string textToDisplay;
	
	// Update is called once per frame
	void Update () {
        if(Player.transform.position == wavepoint.transform.position && check == false)
        {
            wavepoint.SetActive(true);
            canvas.SetActive(true);
            Canvastext.text = "Trigger/touch to pickup files and batteries";
            StartCoroutine(changeText(7f));
            check = true;
        }

        if (wavepoint.GetComponent<Wavepoint>().move)
        {
            canvas.SetActive(false);
            Canvastext.text = "";
        }

        if(Player.transform.position == wavepoint.transform.position)
        {
            MenuCanvas.SetActive(false);
        }
        else
        {
            MenuCanvas.SetActive(true);
        }
	}

    public void tutorial1()
    {
        ButtonClick.Play();
        check = false;
        canvas.SetActive(true);
        wavepoint.SetActive(true);
        Canvastext.text = "Look Behind and Click the sphere to Move";
        StartCoroutine(changeText(7f));
    }

    public void ChangeScene(string play)
    {
        ButtonClick.Play();
        SceneManager.LoadSceneAsync(play);
    }

    public void PickUpItems(GameObject disable)
    {
        canvas.SetActive(true);
        Canvastext.text = "Good job!";
        StartCoroutine(changeText(2f));
        disable.GetComponent<BoxCollider>().enabled = false;
        disable.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(EnableComponents(disable));
    }

    IEnumerator changeText(float time)
    {
        yield return new WaitForSeconds(time);
        canvas.SetActive(false);
    }

    IEnumerator EnableComponents(GameObject disable)
    {
        yield return new WaitForSeconds(10f);
       
            disable.GetComponent<BoxCollider>().enabled = true;
            disable.GetComponent<MeshRenderer>().enabled = true;
    }
}
