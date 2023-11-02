using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI bankText;
    public TextMeshProUGUI timeText;
    private int timeLeft;
    private bool isCountingDown = true;
    public List<Button> buttonList = new();
    private RawImage[] rawImages;
    private RawImage tickImage;
    private int score = 0;

    public GameObject pausePanel;
    private bool isPaused = false;
    public TextMeshProUGUI pauseText;
    public GameObject resumeButton;
    public TextMeshProUGUI timeOverText;

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

    private int blueCost = 10;
    private int redCost = 10;
    private int greenCost = 10;
    private int yellowCost = 4000;


    private int shovelCost;
    public int currentDamage = 1;
    public String currentShovel;

    private Dictionary<String, bool> shovelDictionary = new();
    private Dictionary<string, string> nameDictionary = new();

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
    }

    void Start()
    {
        pausePanel.SetActive(false);
        resumeButton.SetActive(false);
        pauseText.enabled = false;
        timeOverText.enabled = false;
        timeLeft = 5;
        StartCoroutine(startCountown());
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

    private void setBankText(int score)
    {
        bankText.SetText("Bank: " + score + "$");
    }

    private void setTimeText(int arg)
    {
        timeText.SetText("Time left: " + arg + "s");
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator startCountown()
    {
        while (timeLeft > 0)
        {
            isCountingDown = true;
            timeLeft--;
            setTimeText(timeLeft);
            yield return new WaitForSeconds(1);
        }

        if (timeLeft == 0)
        {
            isCountingDown = false;
            setTimeText(0);
            ShovelController sc = FindObjectOfType<ShovelController>();
            sc.setPlayingStatus(false);
            Debug.Log("Time has run out");
            timeOverText.enabled = true;
            pausePanel.SetActive(true);
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

    private void pauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        resumeButton.SetActive(true);
        pauseText.enabled = true;
        isPaused = true;
        
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        resumeButton.SetActive(false);
        pauseText.enabled = false;
        isPaused = false;
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
            }
            else
            {
                Debug.Log("Not enough money for " + shovelName.Substring(7) + " shovel");
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
        }
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