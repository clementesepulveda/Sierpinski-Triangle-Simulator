using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class TriangleManager : MonoBehaviour
{
    public bool isPlaying;

    public GameObject[] corners;
    public GameObject randomPointPrefab;

    public float pointSize;
    public Color pointColor;
    public int initSpeed;

    public AudioSource boopSound;

    private GameObject canvas;
    private List<GameObject> randomPoints;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start() {

        randomPoints = new List<GameObject>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        currentPosition = Vector3.zero;
        isPlaying = false;
        initSpeed = 1;

        
        GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("Restart").GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update() {
        if ( isPlaying) {
            for(int i = 0; i < initSpeed; i++) {
                newPoint();
            }

        }
    }

    void newPoint() {
        int randomVertex = ChooseRandomVertex();
        MoveCurrentPosTowardsVertex(randomVertex);
        InstantiateInCurrentPosition();
    }

    int ChooseRandomVertex() {
        int randomNumber = Random.Range(0, corners.Length);
        return randomNumber;
    }

    void MoveCurrentPosTowardsVertex(int vertexIndex) {
        Vector3 vertexPosition = corners[vertexIndex].transform.localPosition ;
        currentPosition = (vertexPosition + currentPosition)/2;
    }

    void InstantiateInCurrentPosition() {
        GameObject randomPoint = Instantiate(randomPointPrefab, currentPosition, Quaternion.identity) as GameObject;
        randomPoint.transform.SetParent(canvas.transform, false);
        randomPoints.Add(randomPoint);
    }

    public void PlayAnimation() {
        boopSound.Play();
        isPlaying = true;

        GameObject.FindGameObjectWithTag("Play").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("Next").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("Restart").GetComponent<Button>().interactable = true;
    }

    public void NextPoint() {
        boopSound.Play();

        newPoint();
    }

    public void PauseAnimation() {
        boopSound.Play();
        isPlaying = false;

        GameObject.FindGameObjectWithTag("Play").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("Next").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("Restart").GetComponent<Button>().interactable = true;
    }

    public void ResetAnimation() {
        boopSound.Play();
        isPlaying = false;

        foreach( GameObject randomPoint in randomPoints ) {
            Destroy(randomPoint);
        }

        randomPoints.Clear();
        
        GameObject.FindGameObjectWithTag("Play").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("Next").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("Restart").GetComponent<Button>().interactable = false;
    }

    public void UpdateSpeed(System.Single newSpeed) {
        initSpeed = (int)newSpeed;
    }
}
