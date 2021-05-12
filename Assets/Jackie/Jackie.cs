using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Jackie : MonoBehaviour
{
    public float triggerDistance = 5f;
    public Transform head;

    public AudioClip helloClip;
    public AudioClip taskClip;
    public AudioClip endClip;

    public TextMeshProUGUI taskText;
    public GameObject blueScreen;

    Transform player;
    AudioSource audioSource;
    Vector3 originalPos;
    Coroutine driveCo;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerCharacter.Instance.transform;
        audioSource = GetComponent<AudioSource>();
        originalPos = transform.position;

        taskText.text = "|!| Find Jaki";
        StartCoroutine(AllInteractions());
    }

    // Update is called once per frame
    void Update()
    {
        head.LookAt(player);
    }
    IEnumerator AllInteractions()
    {
        yield return StartCoroutine(Interact(SayHello()));
        yield return StartCoroutine(Interact(StartTask()));
        yield return StartCoroutine(Interact(EndTask()));
        Time.timeScale = 0;
        PlayerCharacter.Instance.GetComponentInChildren<MouseLook>().enabled = false;
        PlayerCharacter.Instance.GetComponentInChildren<RayShooter>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        blueScreen.SetActive(true);
    }
    IEnumerator Interact(IEnumerator coroutine)
    {
        yield return new WaitUntil(() => PlayerIsClose());
        yield return StartCoroutine(coroutine);
    }

    IEnumerator SayHello()
    {
        audioSource.clip = helloClip;
        audioSource.Play();
        yield return new WaitForSeconds(8f);
    }
    IEnumerator StartTask()
    {
        audioSource.clip = taskClip;
        audioSource.Play();
        yield return new WaitForSeconds(5f);
        driveCo = StartCoroutine(DriveAway());
        string task = "innocent people killed.";
        PoliceController.Instance.policeCalls = 0;
        while(PoliceController.Instance.policeCalls < 10)
        {
            taskText.text = "|!| " + PoliceController.Instance.policeCalls + "/10 " + task;
            yield return null;
        }
        StopCoroutine(driveCo);
        taskText.text = "|!| Come back to Jaki";
        transform.position = originalPos;
    }
    IEnumerator EndTask()
    {
        audioSource.clip = endClip;
        audioSource.Play();
        yield return new WaitForSeconds(4f);
    }
    IEnumerator DriveAway()
    {
        Vector3 destination = transform.position + transform.forward * 200;
        while (Vector3.Distance(transform.position, destination) > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, 0.1f);
            yield return null;
        }
    }
    bool PlayerIsClose()
    {
        return Vector3.Distance(player.position, transform.position) < triggerDistance;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
    public void CloseApp()
    {
        Application.Quit();
    }
}