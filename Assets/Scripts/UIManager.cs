using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator healthBar;
    public UnityEngine.Transform ability;
    public UnityEngine.Transform icon;
    public UnityEngine.Transform seconds;
    public UnityEngine.Transform buttonToPress;
    public PlayerHealthManager playerHealth;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        ability = transform.Find("Ability");
        icon = ability.Find("Icon");
        seconds = ability.Find("Seconds");
        buttonToPress = ability.Find("ButtonToPress");

        //healthBar = GetComponent<Animator>();
        if (GameObject.FindGameObjectsWithTag("UI").Length > 1)
        {
            Object.Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);

        }
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetFloat("Health", playerHealth.playerCurrentHealth);
        if (playerController.canDash)
        {
            seconds.gameObject.SetActive(true);
            buttonToPress.gameObject.SetActive(true);
            icon.gameObject.SetActive(true);
            if (playerController.dashCooldownCount > 0)
            {
                buttonToPress.gameObject.SetActive(false);
                if (playerController.dashCooldownCount > 0 && playerController.dashCooldownCount < 1)
                {
                    seconds.GetComponent<TextMeshProUGUI>().text = "1";
                }
                else if (playerController.dashCooldownCount > 1 && playerController.dashCooldownCount < 2)
                {
                    seconds.GetComponent<TextMeshProUGUI>().text = "2";
                }
                else if (playerController.dashCooldownCount > 2 && playerController.dashCooldownCount < 3)
                {
                    seconds.GetComponent<TextMeshProUGUI>().text = "3";
                }

            }
            else
            {
                buttonToPress.gameObject.SetActive(true);
                seconds.GetComponent<TextMeshProUGUI>().text = "0";
            }
        }
        else
        {
            seconds.gameObject.SetActive(false);
            buttonToPress.gameObject.SetActive(false);
            icon.gameObject.SetActive(false);
        }
        

    }
}
