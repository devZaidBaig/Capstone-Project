using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class congrats : MonoBehaviour {

    public GameObject manager;
    public GameObject Congratulations;

    float startTime, journeyLength,speed=1.0f;
    bool move = false;
    bool done = true;

    public void MovePosition()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(manager.transform.position, gameObject.transform.position);
        move = true;
    }

    void Update()
    {
        if (move)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            manager.transform.position = Vector3.Lerp(manager.transform.position, gameObject.transform.position, fracJourney);
        }

		if(manager.transform.position == gameObject.transform.position && done)
        {
            Congratulations.SetActive(true);
            StartCoroutine(loadlevel());
            done = false;
        }
	}

    IEnumerator loadlevel()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync("menu");
    }
}
