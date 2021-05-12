using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    public float damage = 30;
    public float shotsASecond = 1;

    private Camera _camera;
    private Gun gun;
    private CameraShake cameraShake;

    float nextShotTime = 0;

    void Start()
    {
        _camera = GetComponent<Camera>();
        gun = GetComponentInChildren<Gun>();
        cameraShake = GetComponent<CameraShake>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextShotTime)
        {
            Vector3 point = new Vector3(
            _camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                IDamagable target = hitObject.GetComponent<IDamagable>();
                if (target != null)
                {
                    target.ReactToHit(damage);
                }
            }
            gun.ShootBullet();
            StartCoroutine(cameraShake.Shake(0.05f, 0.1f));
            nextShotTime = Time.time + 1 / shotsASecond;
        }
    }
}