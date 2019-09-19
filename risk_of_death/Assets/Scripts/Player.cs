using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Singleton

    public static Player instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    public GameObject player;
    public float health = 50f;
    public Text healthText;
    void Update()
    {
        healthText.text = ("Vida: " + health).ToString();
    }
    public void takeDamage(float ammount)
    {
        health -= ammount;
        if (health <= 0f)
        {
            health = 0f;
            Invoke("die", 1f);
        }
    }
    void die()
    {
        Debug.Log("morri");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
