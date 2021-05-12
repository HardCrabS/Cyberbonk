using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    public int _health = 100;
    public Slider healthSlider;
    public AudioClip deathClip;

    public static PlayerCharacter Instance { get; private set; }

    bool isDead = false;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }
    public void Hurt(int damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, 100);
        StartCoroutine(FillHealthBar());
        if(_health <= 0 && !isDead)
        {
            isDead = true;
            GetComponent<Animation>().Play();
            var auS = GetComponent<AudioSource>();
            auS.clip = deathClip;
            auS.Play();
            StartCoroutine(RestartSceneDelayed());
        }
    }

    IEnumerator FillHealthBar()
    {
        Image fillImage = healthSlider.GetComponentsInChildren<Image>()[1];
        float targetValue = _health / 100f;
        float delta = targetValue - healthSlider.value;

        Color fillColor = delta >= 0 ? Color.blue : Color.red;
        fillImage.color = fillColor;

        int steps = 40;
        float step = delta / steps;
        for(int i = 0; i < steps; i++)
        {
            healthSlider.value += step;
            yield return null;
        }
        healthSlider.value = targetValue;
        fillImage.color = Color.white;
    }

    IEnumerator RestartSceneDelayed()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}