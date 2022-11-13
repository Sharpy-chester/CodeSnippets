using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SectionManager : MonoBehaviour {

    [SerializeField] List<GameObject> sections;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject ballObjForSpawn;
    public int sectionID = 1;
    [SerializeField] Text scoreText;
    public int score = 0;
    [SerializeField] GameObject gameOverScreen;
    bool ballIsActive = true;
    Vector3 mCameraPos;
    Vector3 thisSecPos;
    [SerializeField] Text deathText;
    [SerializeField] float ballPointXOffset;
    [SerializeField] float ballFallYOffset;
    [SerializeField] float cameraZOffset;

    void Awake()
    {
        mCameraPos = Camera.main.transform.position;
    }

    void Update () {
        if (ballIsActive)
        {
            if (ball.transform.position.x >= ballObjForSpawn.transform.position.x)
            {
                int randomNum = Random.Range(0, sections.Count);
                ballObjForSpawn.transform.localPosition = new Vector3(ballObjForSpawn.transform.localPosition.x + ballPointXOffset + sections[randomNum].transform.position.x, ballObjForSpawn.transform.localPosition.y + sections[randomNum].transform.position.y, ballObjForSpawn.transform.localPosition.z + sections[randomNum].transform.position.z);
                GameObject section = Instantiate(sections[randomNum], ballObjForSpawn.transform.position, this.transform.rotation);
                Section sectionScript = section.GetComponent<Section>();
                thisSecPos = section.transform.position;
                sectionID++;
                sectionScript.sectionID = sectionID;
                score++;
                scoreText.text = "Score: " + score.ToString();
            }
            if (ball.transform.position.y <= thisSecPos.y - ballFallYOffset)
            {
                GameOver("Fell off the ramp");
            }
            mCameraPos = new Vector3(ball.transform.position.x, ball.transform.position.y + 2, this.transform.position.z + cameraZOffset);
            Camera.main.transform.position = mCameraPos;
        }
        
    }

    public void GameOver(string howDied)
    {
        ballIsActive = false;
        Destroy(ball);
        gameOverScreen.SetActive(true);
        deathText.text = howDied;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene");
        
    }

    
}
