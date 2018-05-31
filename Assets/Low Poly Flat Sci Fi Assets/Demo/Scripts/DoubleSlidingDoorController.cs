using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//	This script handles automatic opening and closing sliding doors
//	It is fired by triggers and the door closes if found no character in the trigger area


//	Door status
public enum DoubleSlidingDoorStatus {
	Closed,
	Open,
	Animating
}

[RequireComponent(typeof(AudioSource))]
public class DoubleSlidingDoorController : MonoBehaviour {

	private DoubleSlidingDoorStatus status = DoubleSlidingDoorStatus.Closed;

	[SerializeField]
	private Transform halfDoorLeftTransform;	//	Left panel of the sliding door
	[SerializeField]
	public Transform halfDoorRightTransform;	//	Right panel of the sliding door

	[SerializeField]
	private float slideDistance	= 0.88f;		//	Sliding distance to open each panel the door

	private Vector3 leftDoorClosedPosition;
	private Vector3 leftDoorOpenPosition;

	private Vector3 rightDoorClosedPosition;
	private Vector3 rightDoorOpenPosition;

	[SerializeField]
	private float speed = 1f;					//	Spped for opening and closing the door

	private int objectsOnDoorArea	= 0;


	//	Sound Fx
	[SerializeField]
	private AudioClip doorOpeningSoundClip;
	[SerializeField]
	public AudioClip doorClosingSoundClip;

	private AudioSource audioSource;

    public ParticleSystem laserEffect;
    public Manager manager;
    public GameObject KeyCard;
    public List<Transform> KeyCardSpawnPoints;
    public GameObject wavepoint;
    bool once = true;


    // Use this for initialization
    void Start () {
		leftDoorClosedPosition	= new Vector3 (0f, 0f, 0f);
		leftDoorOpenPosition	= new Vector3 (0f, 0f, slideDistance);

		rightDoorClosedPosition	= new Vector3 (0f, 0f, 0f);
		rightDoorOpenPosition	= new Vector3 (0f, 0f, -slideDistance);

		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (status != DoubleSlidingDoorStatus.Animating) {
			if (status == DoubleSlidingDoorStatus.Open) {
				if (objectsOnDoorArea == 0) {
					StartCoroutine ("CloseDoors");
				}
			}
		}
	}

	public void OpenDoor() {
		
		if (status != DoubleSlidingDoorStatus.Animating) {
			if (status == DoubleSlidingDoorStatus.Closed) {
				StartCoroutine ("OpenDoors");
                laserEffect.Play();
                gameObject.GetComponent<BoxCollider>().enabled = false;
                if (once)
                {
                    once = false;
                    manager.obj("Click the panel/door to close the door");
                }
            }
		}
		objectsOnDoorArea++;
        Invoke("DoorClose", 10f);    
	}

    public void OpenDoorWithKey()
    {
        if (manager.Key) {
            if (status != DoubleSlidingDoorStatus.Animating)
            {
                if (status == DoubleSlidingDoorStatus.Closed)
                {
                    StartCoroutine("OpenDoors");
                    laserEffect.Play();
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    wavepoint.SetActive(true);
                }
            }
            objectsOnDoorArea++;
            Invoke("DoorClose", 10f);
        }
        else if(manager.fileCount == 5)
        {
            KeyCard.SetActive(true);
            KeyCard.transform.position = KeyCardSpawnPoints[Random.Range(0, KeyCardSpawnPoints.Count)].transform.position;
            manager.obj("You need a Keycard!");
        }
        else
        {
           manager.obj("Find all the Files!");
        }
    }

    void OnTriggerStay(Collider other) {
		
	}

	public void DoorClose() {
        //	Keep tracking of objects on the door
        gameObject.GetComponent<BoxCollider>().enabled = true;
        objectsOnDoorArea--;
	}

	IEnumerator OpenDoors () {

		if (doorOpeningSoundClip != null) {
			audioSource.PlayOneShot (doorOpeningSoundClip, 0.7F);
		}

		status = DoubleSlidingDoorStatus.Animating;

		float t = 0f;

		while (t < 1f) {
			t += Time.deltaTime * speed;
		
			halfDoorLeftTransform.localPosition = Vector3.Slerp(leftDoorClosedPosition, leftDoorOpenPosition, t);
			halfDoorRightTransform.localPosition = Vector3.Slerp(rightDoorClosedPosition, rightDoorOpenPosition, t);

			yield return null;
		}

		status = DoubleSlidingDoorStatus.Open;

	}

	IEnumerator CloseDoors () {

		if (doorClosingSoundClip != null) {
			audioSource.PlayOneShot(doorClosingSoundClip, 0.7F);
		}

		status = DoubleSlidingDoorStatus.Animating;

		float t = 0f;

		while (t < 1f) {
			t += Time.deltaTime * speed;

			halfDoorLeftTransform.localPosition = Vector3.Slerp(leftDoorOpenPosition, leftDoorClosedPosition, t);
			halfDoorRightTransform.localPosition = Vector3.Slerp(rightDoorOpenPosition, rightDoorClosedPosition, t);

			yield return null;
		}

		status = DoubleSlidingDoorStatus.Closed;

	}

	//	Forced door opening
	public bool DoOpenDoor () {

		if (status != DoubleSlidingDoorStatus.Animating) {
			if (status == DoubleSlidingDoorStatus.Closed) {
				StartCoroutine ("OpenDoors");
				return true;
			}
		}

		return false;
	}

	//	Forced door closing
	public bool DoCloseDoor () {

		if (status != DoubleSlidingDoorStatus.Animating) {
			if (status == DoubleSlidingDoorStatus.Open) {
				StartCoroutine ("CloseDoors");
				return true;
			}
		}

		return false;
	}
}
