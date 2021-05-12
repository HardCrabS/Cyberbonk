using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float shotsPerSec = 2;
    [SerializeField] private GameObject fireballPrefab;

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    private GameObject _fireball;
    private bool _alive;
    float nextTimeToShoot = 0;

    void Start()
    {
        _alive = true;
    }
    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (fireballPrefab != null && Time.time > nextTimeToShoot)
                    {
                        StartCoroutine(ShootFireballDelayed());
                        nextTimeToShoot = Time.time + 1 / shotsPerSec;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }
    IEnumerator ShootFireballDelayed()
    {
        yield return new WaitForSeconds(Random.Range(0, 2f));

        _fireball = Instantiate(fireballPrefab) as GameObject;
        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        _fireball.transform.rotation = transform.rotation;
        Destroy(_fireball, 10);
    }
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}