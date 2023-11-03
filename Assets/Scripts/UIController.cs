using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UIElements.Image;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI bankText;
    public TextMeshProUGUI timeText;
    private int timeLeft = 12;
    private bool isCountingDown = true;
    public List<Button> buttonList = new();
    private RawImage[] rawImages;
    private RawImage tickImage;
    private int score = 0;

    public GameObject pausePanel;
    private bool isPaused = false;
    public TextMeshProUGUI pauseText;
    public GameObject resumeButton;
    public GameObject playAgainButton;
    public TextMeshProUGUI timeOverText;

    private Animator timeLeftAnimator;
    

    private Color equipedColor = new Color(0.66f, 1f, 0.6f);
    private Color notOwnedColor = new Color(0.63f, 0.63f, 0.63f);

    public bool blueOwned = false;
    public bool redOwned = false;
    public bool greenOwned = false;
    public bool yellowOwned = false;

    private int blueDMG = 1;
    private int redDMG = 2;
    private int greenDMG = 3;
    private int yellowDMG = 4;

    private int blueCost;
    private int redCost;
    private int greenCost;
    private int yellowCost;

    private AudioSource audioSource = new();

    private int shovelCost;
    public int currentDamage = 1;
    public String currentShovel;

    private Dictionary<String, bool> shovelDictionary = new();
    private Dictionary<string, string> nameDictionary = new();
    private Dictionary<String, AudioClip> audioClips = new();

    public void sendDurationToTimeBar()
    {
        //FindObjectOfType<TimeBar>().setDuration(timeLeft);
    }
    
    private void setShovelCost()
    {
        blueCost = FindObjectOfType<ButtonScript>().setBlueCost();
        redCost = FindObjectOfType<ButtonScript>().setRedCost();
        greenCost = FindObjectOfType<ButtonScript>().setGreenCost();
        yellowCost = FindObjectOfType<ButtonScript>().setYellowCost();
    }

    private void setUpDictionary()
    {
        shovelDictionary["Button_Blue"] = blueOwned;
        shovelDictionary["Button_Red"] = redOwned;
        shovelDictionary["Button_Green"] = greenOwned;
        shovelDictionary["Button_Yellow"] = yellowOwned;

        nameDictionary["Button_Blue"] = "Blue";
        nameDictionary["Button_Red"] = "Red";
        nameDictionary["Button_Green"] = "Green";
        nameDictionary["Button_Yellow"] = "Yellow";

        audioClips["Select"] = Resources.Load<AudioClip>("Audio/DM-CGS-28");
        audioClips["Failed"] = Resources.Load<AudioClip>("Audio/DM-CGS-34");
        audioClips["Jump1"] = Resources.Load<AudioClip>("Audio/DM-CGS-21");
        audioClips["Jump2"] = Resources.Load<AudioClip>("Audio/DM-CGS-32");
        audioClips["Complete"] = Resources.Load<AudioClip>("Audio/DM-CGS-18");
    }

    public void reloadPlayScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 1f;
        setShovelCost();
        pausePanel.GetComponent<FadePausePanel>().setResumeColor(0.0f);
        resumeButton.SetActive(false);
        playAgainButton.SetActive(false);
        pauseText.enabled = false;
        timeOverText.enabled = false;
        timeLeftAnimator = GetComponentInChildren<Animator>();
        
        int timeLeft = 5;
        FindObjectOfType<TimeBar>().setDuration(timeLeft);
        
        StartCoroutine(startCountownFrom(timeLeft + 1));

        setUpDictionary();
        setBankText(score);
        setAllButtonTextColorToGray();

        foreach (Button button in buttonList)
        {
            rawImages = button.GetComponentsInChildren<RawImage>();
            foreach (RawImage image in rawImages)
            {
                if (image.CompareTag("Tick"))
                {
                    tickImage = image;
                }
            }

            tickImage.enabled = false;
        }
    }

    public float getDuration()
    {
        return timeLeft;
    }
    
    private void setBankText(int score)
    {
        bankText.SetText("Bank: " + score + "$");
    }

    private void setTimeText(int arg)
    {
        timeText.SetText("Time left: " + arg + "s");
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator startCountownFrom(int timeLeft)
    {
        while (timeLeft > 1)
        {
            isCountingDown = true;
            timeLeft--;
            setTimeText(timeLeft);
            yield return new WaitForSeconds(1);
        }

        if (timeLeft == 1)
        {
            isCountingDown = false;
            setTimeText(0);
            ShovelController sc = FindObjectOfType<ShovelController>();
            sc.setPlayingStatus(false);
            pausePanel.GetComponent<FadePausePanel>().setPauseColor();
            Debug.Log("Time has run out");
            timeLeftAnimator.Play("TimeLeftAnimation");
            timeOverText.enabled = true;
            playAgainButton.SetActive(true);
            audioSource.volume = 0.4f;
            playSound("Complete");
            yield return null;
        }
    }


    private void togglePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isCountingDown)
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void pauseGame()
    {
        isPaused = true;
        //pausePanel.SetActive(true);
        resumeButton.SetActive(true);
        pauseText.enabled = true;
        pausePanel.GetComponent<FadePausePanel>().setPauseColor();
        Time.timeScale = 0f;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void resumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        //pausePanel.SetActive(false);
        resumeButton.SetActive(false);
        pauseText.enabled = false;
        pausePanel.GetComponent<FadePausePanel>().setResumeColor();
    }

    void Update()
    {
        togglePauseMenu();
    }

    private void setAllButtonTextColorToGray()
    {
        TextMeshProUGUI[] texts;
        foreach (Button button in buttonList)
        {
            texts = button.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in texts)
            {
                text.color = notOwnedColor;
            }
        }
    }


    private void showEquiped()
    {
        foreach (Button button in buttonList)
        {
            // Contains every text object in a button
            var texts = button.GetComponentsInChildren<TextMeshProUGUI>();

            ShovelController sc = FindObjectOfType<ShovelController>();
            if (button.CompareTag(currentShovel))
            {
                foreach (var text in texts)
                {
                    if (text.CompareTag("ShovelTitle"))
                    {
                        text.color = equipedColor;
                    }
                    else
                    {
                        text.color = Color.white;
                    }
                }

                sc.setShovelMaterial(nameDictionary[button.tag]);
            }
            else if (shovelDictionary.ContainsKey(button.tag) && shovelDictionary[button.tag])
            {
                foreach (var text in texts)
                {
                    text.color = Color.white;
                }
            }
        }
    }

    private void setDamageOf(String shovelName)
    {
        switch (shovelName)
        {
            case "Button_Blue":
                currentDamage = blueDMG;
                break;
            case "Button_Red":
                currentDamage = redDMG;
                break;
            case "Button_Green":
                currentDamage = greenDMG;
                break;
            case "Button_Yellow":
                currentDamage = yellowDMG;
                break;
        }
    }


    private int getShovelCostOf(String shovelName)
    {
        switch (shovelName)
        {
            case "Button_Blue":
                return blueCost;
            case "Button_Red":
                return redCost;
            case "Button_Green":
                return blueCost;
            case "Button_Yellow":
                return yellowCost;
            default:
                return 0;
        }
    }

    private void buyShovel(ref bool shovelTypeOwned, String shovelName)
    {
        if (!shovelTypeOwned)
        {
            Debug.Log(shovelName.Substring(7) + " shovel not owned. Attempting to buy...");
            if (score >= getShovelCostOf(shovelName))
            {
                shovelTypeOwned = true;
                shovelDictionary[shovelName] = true;
                score -= getShovelCostOf(shovelName);
                setBankText(score);
                setDamageOf(shovelName);
                sendDamageToLevelGenerator();
                currentShovel = shovelName;
                showEquiped();
                Debug.Log(shovelName.Substring(7) + " shovel bought");

                playSound("Select");
            }
            else
            {
                Debug.Log("Not enough money for " + shovelName.Substring(7) + " shovel");
                playSound("Failed");
            }
        }
        else
        {
            Debug.Log(shovelName.Substring(7) + " shovel already owned, equiping");
            setDamageOf(shovelName);
            sendDamageToLevelGenerator();
            currentShovel = shovelName;
            showEquiped();
            Debug.Log("Damage of " + currentDamage + " given to Level Generator");

            playSound("Select");
        }
    }


    public void playSound(String sound)
    {
        audioSource.clip = audioClips[sound];
        audioSource.Play();
    }

    public void sendDamageToLevelGenerator()
    {
        LevelGenerator2 lg = FindObjectOfType<LevelGenerator2>();
        if (lg != null)
        {
            lg.receiveDamageValueFromUI(currentDamage);
        }
    }

    public void redClick()
    {
        Debug.Log("Red button clicked");
        buyShovel(ref redOwned, "Button_Red");
        //receiveShovelCost(shovelCost);
    }

    public void blueClick()
    {
        Debug.Log("Blue button clicked");
        buyShovel(ref blueOwned, "Button_Blue");
    }

    public void yellowClick()
    {
        Debug.Log("Yellow button clicked");
        buyShovel(ref yellowOwned, "Button_Yellow");
    }

    public void greenClick()
    {
        Debug.Log("Green button clicked");
        buyShovel(ref greenOwned, "Button_Green");
    }

    public void receiveBlockValue(int value)
    {
        score += value;
        setBankText(score);
    }

    public void receiveShovelCost(int cost)
    {
        shovelCost = cost;
        Debug.Log("Shovel cost received from button: " + shovelCost);
    }
}