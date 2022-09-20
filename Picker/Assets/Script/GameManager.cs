using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;

[Serializable]

public class BallAreaTecnicalOperations
{
    public Animator BallAreaLift;
    public TextMeshProUGUI numberText;
    public int DutyBall;
    public GameObject[] Balls;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pickerObject;
    [SerializeField] private GameObject[] pickerPalets;
    [SerializeField] private GameObject[] bonusBalls;
    private bool paletExist;
    [SerializeField] private GameObject ballControlObject; 
    public bool isPickerMoving;

    [SerializeField] private GameObject[] GeneralPanels;
    [SerializeField] private TextMeshProUGUI winLevelText;
    [SerializeField] private TextMeshProUGUI loseLevelText;

    private int scoredBallCount;
    private int totalCheckPointCount;
    private int currentCheckPointIndex;
    
    [SerializeField] private List<BallAreaTecnicalOperations> ballAreaTecnicalOperations = new List<BallAreaTecnicalOperations>();
    void Start()
    {
        isPickerMoving = true;
        for (int i = 0; i < ballAreaTecnicalOperations.Count; i++)
        {
            ballAreaTecnicalOperations[i].numberText.text = scoredBallCount + " / " + ballAreaTecnicalOperations[i].DutyBall;
        }
        totalCheckPointCount = ballAreaTecnicalOperations.Count - 1;
    }


    void Update()
    {
        if (isPickerMoving)
        {
            pickerObject.transform.position += 5f * Time.deltaTime * pickerObject.transform.forward;

            if (Time.timeScale != 0)
            {

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    pickerObject.transform.position = Vector3.Lerp(pickerObject.transform.position,new Vector3
                        (pickerObject.transform.position.x - .1f,pickerObject.transform.position.y,pickerObject.transform.position.z),.05f);
                }    
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    pickerObject.transform.position = Vector3.Lerp(pickerObject.transform.position,new Vector3
                        (pickerObject.transform.position.x + .1f,pickerObject.transform.position.y,pickerObject.transform.position.z),.05f);
                } 
                
            }
            
            
        }
    }

    public void ReachedBorder()
    {
        if (paletExist)
        {
            pickerPalets[0].SetActive(false);
            pickerPalets[1].SetActive(false);
        }
        
        isPickerMoving = false;
        Invoke("StageControl",2f);
        Collider[] HitColl = Physics.OverlapBox(ballControlObject.transform.position,ballControlObject.transform.localScale / 2,
            Quaternion.identity);
        int i = 0;
        while (i < HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0,0,.8f),ForceMode.Impulse);
            i++;
        }
    }

    public void CountBalls()
    {
        scoredBallCount++;
        ballAreaTecnicalOperations[currentCheckPointIndex].numberText.text = scoredBallCount + " / " + ballAreaTecnicalOperations[currentCheckPointIndex].DutyBall;
    }

    void StageControl()
    {
        if (scoredBallCount >= ballAreaTecnicalOperations[currentCheckPointIndex].DutyBall)
        {
            ballAreaTecnicalOperations[currentCheckPointIndex].BallAreaLift.Play("Lift");
            foreach (var item in ballAreaTecnicalOperations[currentCheckPointIndex].Balls)
            {
                item.SetActive(false);
            }

            if (currentCheckPointIndex == totalCheckPointCount)
            {
                Debug.Log("game finished");
                Time.timeScale = 0;
                GeneralPanels[2].SetActive(true);
                winLevelText.text = "LEVEL : " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
                
            }
            else
            {
                currentCheckPointIndex++;
                scoredBallCount = 0;
                if (paletExist)
                {
                    pickerPalets[0].SetActive(true);
                    pickerPalets[1].SetActive(true);
                }
            }
        }else
        {
            Debug.Log("DUR");
            GeneralPanels[1].SetActive(true);
            loseLevelText.text = "LEVEL : " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
        }
    }

    public void PaletReveal()
    {
        paletExist = true;
        pickerPalets[0].SetActive(true);
        pickerPalets[1].SetActive(true);
    }

    public void AddBonusBalls(int bonusBallIndex)
    {
        bonusBalls[bonusBallIndex].SetActive(true);
    }
    
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ballControlObject.transform.position,ballControlObject.transform.localScale);
    }*/
    
    public void ButtonOperations(string value)
    {
        switch (value)
        {
            case "Stop":
                GeneralPanels[0].SetActive(true);
                Time.timeScale = 0;
                break;
            case "Continue":
                GeneralPanels[0].SetActive(false);
                Time.timeScale = 1;
                break;
            case "Retry":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "NextLevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
                Time.timeScale = 1;
                break;
            case "Exit":
                Application.Quit();
                break;
        }
    }
}
