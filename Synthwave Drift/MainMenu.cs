using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] BtecCarController carController;
    [SerializeField] RoadGeneration roadGenerator;
    [SerializeField] UIManager ui;
    [SerializeField] Animator animator;
    [SerializeField] GameObject[] cars;
    int carNum = 0;
    public Text cassTxt;
    public Text highScore;
    public Text buyTxt;
    public GameManager manager;
    [SerializeField] Car[] carSO;

    void Awake()
    {
        carController.enabled = false;
        roadGenerator.enabled = false;
        ui.gameObject.SetActive(false);
        cassTxt.text = "Cassettes: " + manager.cassettes;
        highScore.text = "High Score: " + manager.HighScore;
        UpdateCurrentCarUI();
    }
    public void UpdateCurrentCarUI()
    {
        foreach (Car carso in carSO)
        {
            if (bool.TrueString == PlayerPrefs.GetString(carso.carName))
                carso.unlocked = true;
        }

        Car car = carSO[carNum];
        if (car.unlocked)
        {
            buyTxt.text = "Unlocked";
            buyTxt.fontSize = 64;
        }
        else
        {
            buyTxt.text = "Buy for " + car.cassNeeded + " cassettes";
            buyTxt.fontSize = 52;
        }
    }

    public void Play()
    {
        if (carSO[carNum].unlocked)
        {
            animator.SetBool("change", true);
            carController.enabled = true;
            roadGenerator.enabled = true;
            ui.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }


    public void NextCar()
    {
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }
        carNum++;
        if (carNum > cars.Length - 1)
            carNum = 0;
        cars[carNum].SetActive(true);
        UpdateCurrentCarUI();
    }
    public void LastCar()
    {
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }
        carNum--;
        if (carNum < 0)
            carNum = cars.Length - 1;
        cars[carNum].SetActive(true);
        UpdateCurrentCarUI();
    }
    public void buyCar()
    {
        if (manager.cassettes >= carSO[carNum].cassNeeded)
        {
            carSO[carNum].unlocked = true;
            manager.cassettes -= carSO[carNum].cassNeeded;
            PlayerPrefs.SetInt("cass", manager.cassettes);
            cassTxt.text = "Cassettes: " + manager.cassettes;
            UpdateCurrentCarUI();
            //right, let me explain myself here. There is no playerprefs set bool so ima cast this string into a bool. Is this dumb? Yes. Does it work? Also yes
            PlayerPrefs.SetString(carSO[carNum].carName, bool.TrueString);
        }
    }
}