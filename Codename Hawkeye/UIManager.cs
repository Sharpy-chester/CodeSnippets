/* 
Cameron made this script! Pretty much all it has right now is a public function that updates the health bar, but
damn Cam made it well. Amazing.

This should probably be called inside Health.cs in the DamageCreature public function. Its in update for the moment
for testing purposes.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Slider healthBar;
    [SerializeField] Health playerHealthScript;
    [SerializeField] GameManager manager;
    [SerializeField] CanvasGroup gameOverUI;
    [SerializeField] CanvasGroup gameWinUI;
    [SerializeField] Button respawnBtn;
    [SerializeField] Button winButton;
    [SerializeField] CanvasGroup gameLoseUI;
    [SerializeField] Button gameLoseBtn;
    public int speed = 2;
    [SerializeField]bool uifaded;
    [SerializeField] float fadeDuration;
    [SerializeField] GameObject[] hearts;
    [SerializeField] Sprite EmptyHeartSprite;
    [SerializeField] Sprite FullHeartSprite;
    bool fading = false;

    private void Update()
    {
        //this is in update for the moment but should be moved
        if (healthBar != null)
        {
            UpdateHealthBar();
        }
        if (playerHealthScript.GetCurrentHealth() < 1 && !uifaded && !fading && manager.lifes > 1)
        {
            FadeGameOverUI();
        }
        if(manager.lifes <= 1 && playerHealthScript.GetCurrentHealth() < 1 && !uifaded && !fading)
        {
            FadeGameLoseUI();
        }
    }

    private void Start()
    {
        playerHealthScript = player.GetComponent<Health>();
        manager.lifes = manager.maxLifes;
    }

    IEnumerator FadeUI(CanvasGroup fadedui, float start, float end, bool respawn)
    {
        print("fading");
        fading = true;
        Time.timeScale = 1;
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadedui.alpha = Mathf.Lerp(start, end, timer / fadeDuration);
            yield return null;
        }
        if (respawn)
        {
            print(respawn);
            manager.respawn(player);
            Time.timeScale = 0;
        }
        uifaded = !uifaded;
        fading = false;
    }

    public void UpdateHeartsUI(int lives)
    {

        for (int i = 2; i > lives - 1; i--)
        {
            hearts[i].GetComponent<Image>().sprite = EmptyHeartSprite;
        }
    }


    public void FadeGameOverUI()
    {
        if (manager.lifes > 0)
        {
            StartCoroutine(FadeUI(gameOverUI, gameOverUI.alpha, uifaded ? 0 : 1, !uifaded));
            gameOverUI.interactable = !gameOverUI.interactable;
            gameOverUI.blocksRaycasts = !gameOverUI.blocksRaycasts;
        }
    }

    public void FadeGameWinUI()
    {
        StartCoroutine(FadeUI(gameWinUI, gameWinUI.alpha, fadeDuration, false));
        gameWinUI.interactable = !gameWinUI.interactable;
        gameWinUI.blocksRaycasts = !gameWinUI.blocksRaycasts;
    }

    public void FadeGameLoseUI()
    {
        StartCoroutine(FadeUI(gameLoseUI, gameLoseUI.alpha, fadeDuration, false));
        gameLoseUI.interactable = !gameLoseUI.interactable;
        gameLoseUI.blocksRaycasts = !gameLoseUI.blocksRaycasts;
    }

    //updates the ui of the health bar by using info from the players health script
    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            if(healthBar.value != playerHealthScript.GetCurrentHealth())
            {
                healthBar.value = Mathf.Lerp(healthBar.value, playerHealthScript.GetCurrentHealth() / (float)playerHealthScript.GetMaxHealth(), speed * Time.deltaTime);
            }
            //healthBar.value = (float)playerHealthScript.GetCurrentHealth() / (float)playerHealthScript.GetMaxHealth();
            //print(playerHealthScript.GetCurrentHealth());
            //print(playerHealthScript.GetMaxHealth());
        }
    }

    public void Level1()
    {
        manager.levelNum = 1;
        SceneManager.LoadScene("Level 1");
        FindObjectOfType<AudioManager>().StopPlaying("Main Menu");
        FindObjectOfType<AudioManager>().Play("Level 1");
    }

    public void Level2()
    {
        manager.levelNum = 2;
        SceneManager.LoadScene("Level 2");
        FindObjectOfType<AudioManager>().StopPlaying("Main Menu");
        FindObjectOfType<AudioManager>().Play("Level 2");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel1()
    {
        manager.levelNum = 0;
        SceneManager.LoadScene("Introduction");
    }

    public void Quit()
    {
        Application.Quit();
    }



}