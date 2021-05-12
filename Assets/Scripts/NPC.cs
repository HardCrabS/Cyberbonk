using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NPC : MonoBehaviour
{
    [SerializeField] float interactDistance = 3f;
    [SerializeField] float timeBetwReplicas = 5;
    [SerializeField] AudioClip[] facePlayerClips;

    AudioSource audioSource;

    float nextTimeToSay = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        CheckPlayerDistance();
    }
    private void CheckPlayerDistance()
    {
        Vector3 playerPos = PlayerCharacter.Instance.gameObject.transform.position;
        if (!audioSource.isPlaying && Vector3.Distance(playerPos, transform.position) < interactDistance
            && Time.time > nextTimeToSay)
        {
            audioSource.clip = facePlayerClips[Random.Range(0, facePlayerClips.Length)];
            audioSource.Play();
            nextTimeToSay = Time.time + timeBetwReplicas;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}