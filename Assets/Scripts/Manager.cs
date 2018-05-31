using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public AnimationControl animationControl;
    public GameObject death;
    public int fileCount = 0;
    public bool Key = false;
    public GameObject FlashLight;
    public float FlashLightPower = 10f;
    public GameObject CreepyGuy;
    public List<GameObject> Junks;
    public Text power;
    public Text Files;
    public Text ObjectiveText;
    public GameObject ObjectivePanel;

    public List<Transform> spawnPoints;
    public List<GvrAudioSource> gvrAudioSources;

    float timeStarted;
    bool done = false;

    private void Start()
    {
        timeStarted = Time.time;
        SpawnCreepyGuy();
        StartCoroutine(Objective("Find all the Files and Escape from the Alien!"));
    }

    // Update is called once per frame
    void Update () {
        if (animationControl.attack)
        {
            death.SetActive(true);
            Color c = death.GetComponent<Image>().color;
            Color d = new Color(255f, 0f, 0f, 1f);
            death.GetComponent<Image>().color = Color.Lerp(c, d, 0.5f * Time.deltaTime);
            Debug.Log(death.GetComponent<Image>().color.a);
        }

        if(Time.time > FlashLightPower + timeStarted)
        {
            FlashLight.SetActive(false);
        }
        int p = (int)(100 - ((Time.time / (FlashLightPower + timeStarted)) * 100));
        if (p > 0)
        {
            power.text = "Power: " + p + " %";
        }
        else
        {
            p = 0;
            power.text = "Power: " + p + " %";
        }
	}

    void SpawnCreepyGuy()
    {
        StartCoroutine(spawnCreep(Random.Range(15f, 25f)));
    }

    public void obj(string s)
    {
        StartCoroutine(Objective(s));
    }

    IEnumerator spawnCreep(float time)
    {
        CreepyGuy.SetActive(true);
        CreepyGuy.GetComponent<GvrAudioSource>().Play();
        CreepyGuy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
        yield return new WaitForSeconds(time);
        CreepyGuy.SetActive(false);
        StartCoroutine(spawnCreep1());
    }

    IEnumerator spawnCreep1()
    {
        yield return new WaitForSeconds(Random.Range(25f, 40f));
        SpawnCreepyGuy();
    }

    IEnumerator Objective(string s)
    {
        ObjectivePanel.SetActive(true);
        ObjectiveText.text = s;
        yield return new WaitForSeconds(5f);
        ObjectivePanel.SetActive(false);
    }

    public void PickUpFile(GameObject obj)
    {
        fileCount++;
        Files.text = "Files: " + fileCount + " /5";       
        Destroy(obj);
    }

    public void PickUpBattery(GameObject g)
    {
        FlashLightPower += 50f;
        Destroy(g);
    }

    public void PlayJunkAudio()
    {
        if (!done)
        {
            for (int i = 0; i < Junks.Count; i++)
            {
                Junks[i].GetComponent<Rigidbody>().useGravity = true;
                Junks[i].GetComponent<Rigidbody>().AddForce(new Vector3(0f, 100f, -300f));
            }
            Junks[0].GetComponent<GvrAudioSource>().Play();
            done = true;
        }
    }

    public void PickUpKeycard()
    {
        Key = true;
        StartCoroutine(Objective("Now Escape!!!"));
    }

    public void randomSoundGenerator()
    {
        StartCoroutine(soundGenerator());
    }

    IEnumerator soundGenerator()
    {
        gvrAudioSources[Random.Range(0, gvrAudioSources.Count)].Play();
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        randomSoundGenerator();
    }
}
