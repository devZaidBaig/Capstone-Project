using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavepoint : MonoBehaviour {

    public GameObject Player;
    public float speed = 1.0F;
    public bool move = false;
    private float startTime;
    private float journeyLength;

    public List<GameObject> wavePointsToBeActivated;
    public List<GameObject> wavePointsToBeDeactivated;

    // Use this for initialization

    // Update is called once per frame
    public void MovePosition () {
        startTime = Time.time;
        journeyLength = Vector3.Distance(Player.transform.position, gameObject.transform.position);
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
            Player.transform.position = Vector3.Lerp(Player.transform.position, gameObject.transform.position, fracJourney);
        }

        if(Player.transform.position == gameObject.transform.position)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<SphereCollider>().enabled = true;
            move = false;
            gameObject.SetActive(false);

            for(int i = 0; i < wavePointsToBeActivated.Count; i++)
            {
                wavePointsToBeActivated[i].SetActive(true);
            }

            for (int i = 0; i < wavePointsToBeDeactivated.Count; i++)
            {
                wavePointsToBeDeactivated[i].SetActive(false);
            }
        }
    }

}
