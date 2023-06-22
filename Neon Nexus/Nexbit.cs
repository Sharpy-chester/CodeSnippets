using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Player;


public class Nexbit : MonoBehaviour
{
    public int nexbits;
    float airtime = 0;
    [Range(0.1f, 1)]
    [SerializeField] float airtimeMultiplier;
    [SerializeField] float airtimeCutoff = 3f;
    float speed = 0;
    [Range(0.1f, 1)]
    [SerializeField] float speedMultiplier;

    [SerializeField] int baseHealthIncrease = 2;
    [SerializeField] float healthDecreaseTime = 5f;
    float currentHealthDecreaseTime = 0f;
    int airtimeScore = 0;
    [SerializeField] TextMeshProUGUI airtimeScoreTxt, wallrunScoreTxt, killScoreTxt, speedScoreTxt;
    int totalAirtimeScore, totalWallrunScore, totalKillScore, totalSpeedScore;
    bool reduceHealth = true;
    [SerializeField] float scoreTurnoffTime = 2f;
    int speedScore = 0;

    PlayerWallRun wallrun;
    float currentWallrunTime = 0;
    UIManager uiManager;

    GameManager gm;
    [SerializeField] TextMeshProUGUI nexbitTxt, speedTxt, airtimeTxt;
    [SerializeField] GameObject healthTxt;

    [SerializeField] PlayerJump pj;
    [SerializeField] Rigidbody playerRB;

    bool inAir = false;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        nexbits = PlayerPrefs.GetInt("Nexbits");
        if(nexbitTxt)
        {
            nexbitTxt.text = nexbits.ToString();
        }
        gm = FindObjectOfType<GameManager>();
        if(gm)
        {
            gm.enemyDeath += CalculateNexbits;
            gm.gameEnd += SendScores;
        }
        SceneManager.activeSceneChanged += SaveNexbits;
        if (pj)
        {
            pj.onNotGrounded += InAir;
            pj.onGrounded += NotInAir;
            wallrun = pj.GetComponent<PlayerWallRun>();
        }
        if(airtimeTxt)
        {
            airtimeTxt.text = 0.ToString();
        }
    }

    private void Update()
    {
        if(wallrun)
        {
            if (wallrun.isWallRunning && inAir)
            {
                if(!wallrunScoreTxt.gameObject.activeSelf)
                {
                    wallrunScoreTxt.gameObject.SetActive(true);
                }
                currentWallrunTime += Time.deltaTime;
                wallrunScoreTxt.text = "Wallrun Score: " + (int)(currentWallrunTime * 100);
            }
            else if (currentWallrunTime != 0)
            {
                if (currentWallrunTime > 3)
                {
                    IncreaseHealth((int)(currentWallrunTime / 3));
                }
                uiManager.AddScore((int)(currentWallrunTime * 100));
                totalWallrunScore += (int)(currentWallrunTime * 100);
                currentWallrunTime = 0;
                StartCoroutine(TurnScoreOff(scoreTurnoffTime, wallrunScoreTxt.gameObject));
            }
        }
        if (inAir)
        {
            airtime += Time.deltaTime;
            airtimeTxt.text = ((int)((airtime + 1) * airtimeMultiplier) * (int)(speed * speedMultiplier)).ToString();
            if(airtime >= airtimeCutoff)
            {
                if (!airtimeScoreTxt.gameObject.activeSelf)
                {
                    airtimeScoreTxt.gameObject.SetActive(true);
                }
                airtimeScore = (int)(airtime * 30);
                airtimeScoreTxt.text = "Airtime Score: " + airtimeScore;

                if (speed > 15)
                {
                    if (!speedScoreTxt.gameObject.activeSelf)
                    {
                        speedScoreTxt.gameObject.SetActive(true);
                    }
                    speedScore = (int)speed;
                    speedScoreTxt.text = "Speed Score: " + ((int)speed).ToString();
                }
                else
                {
                    if (speedScoreTxt.gameObject.activeSelf)
                    {
                        speedScoreTxt.gameObject.SetActive(false);
                    }
                    speedScore = 0;
                }
            }
            
            
        }
        else if (airtimeScoreTxt && airtimeScoreTxt.gameObject.activeSelf)
        {
            if(airtimeScore > 30)
            {
                IncreaseHealth((int)(airtimeScore / 30));
            }
            StartCoroutine(TurnScoreOff(scoreTurnoffTime, airtimeScoreTxt.gameObject));
            uiManager.AddScore(airtimeScore);
            totalAirtimeScore += airtimeScore;
            airtimeScore = 0;

            if (speed > 25)
            {
                IncreaseHealth((int)(speed / 25));
            }
            StartCoroutine(TurnScoreOff(scoreTurnoffTime, speedScoreTxt.gameObject));
            uiManager.AddScore(speedScore);
            totalSpeedScore += speedScore;
            speedScore = 0;

        }
        {

        }
        if(playerRB)
        {
            speed = playerRB.velocity.magnitude;
            speedTxt.text = "Speed: " + ((int)speed).ToString() + " KM/h";
            currentHealthDecreaseTime += Time.deltaTime;
            if (currentHealthDecreaseTime > healthDecreaseTime)
            {
                currentHealthDecreaseTime = 0;
                if (playerRB.TryGetComponent(out Health health))
                {
                    health.ReduceHealth(1);
                }
            }
        }
    }

    void IncreaseHealth(int amt)
    {
        if (playerRB.TryGetComponent(out Health health))
        {
            GameObject txt = Instantiate(healthTxt, nexbitTxt.transform.parent.Find("Health"));
            txt.GetComponent<TextMeshProUGUI>().text = "+" + amt;
            Destroy(txt, 2.5f);
            health.IncreaseHealth(amt);
        }
    }

    void InAir()
    {
        inAir = true;
        airtimeTxt.gameObject.SetActive(true);
    }

    void NotInAir()
    {
        inAir = false;
        airtime = 0;
        airtimeTxt.gameObject.SetActive(false);
    }

    void CalculateNexbits()
    {
        int calculatedScore = (int)((airtime + 1) * airtimeMultiplier) * (int)(speed * speedMultiplier);
        nexbits += calculatedScore;
        if (nexbitTxt)
        {
            nexbitTxt.text = nexbits.ToString();
        }
        killScoreTxt.gameObject.SetActive(true);
        killScoreTxt.text = "Enemy killed: " + calculatedScore * 10;
        uiManager.AddScore(calculatedScore * 10);
        totalKillScore += calculatedScore * 10;
        IncreaseHealth(2);
        StartCoroutine(TurnScoreOff(scoreTurnoffTime, killScoreTxt.gameObject));
    }

    IEnumerator TurnScoreOff(float time, GameObject go) //this will become an issue if the player kills multiple enemies at once, but oh well
    {
        yield return new WaitForSeconds(time);
        go.SetActive(false);
    }

    public void RemoveNexbits(int amt)
    {
        nexbits -= amt;
        if (nexbitTxt)
        {
            nexbitTxt.text = nexbits.ToString();
        }
        PlayerPrefs.SetInt("Nexbits", nexbits);
    }

    void SaveNexbits(Scene current, Scene next)
    {
        PlayerPrefs.SetInt("Nexbits", nexbits);
        SceneManager.activeSceneChanged -= SaveNexbits;
    }

    void SendScores()
    {
        gm.airtimeScore = totalAirtimeScore;
        gm.enemyScore = totalKillScore;
        gm.speedScore = totalSpeedScore;
        gm.wallrunScore = totalWallrunScore;
        gm.finalScore = totalWallrunScore + totalSpeedScore + totalKillScore + totalAirtimeScore;
    }
}