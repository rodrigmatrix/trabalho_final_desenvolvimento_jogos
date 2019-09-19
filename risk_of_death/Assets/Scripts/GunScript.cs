
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    float start;
    public float damage = 10f;
    public float range = 100f;
    public float speed = 20;
    public float impactForce = 30f;
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Text scoreText;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        start = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }
    void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            muzzleFlash.Play();
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                score += enemy.takeDamage(damage);
                scoreText.text = ("Score: " + score).ToString();
            }
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 0.2f);
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

        }
    }
}
