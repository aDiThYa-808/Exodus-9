using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Weapon : MonoBehaviour
{
    //camera
    public Camera playerCamera;

    //shooting
    public bool readyToShoot, isShooting;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //burst
    public int bulletsPerBurst = 3;
    public int currentBurst;

    //bullet spread
    public float spreadIntensity;

    //bullet
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    public float BulletVelocity = 100;
    public float BulletLifeTime = 3f;

    //muzzle flash
    public GameObject MuzzleFlash;

    //animator
    private Animator anim;

    //shooting mode
    public enum ShootingMode
    {
        single,
        burst,
        auto
    }

    //ammos
    public int gun_ammo = 450;

    public TextMeshProUGUI ammoText;



    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        currentBurst = bulletsPerBurst;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (currentShootingMode == ShootingMode.auto)
        {
            //hold right mouse
            isShooting = CrossPlatformInputManager.GetButton("Shoot");
        }

        if (currentShootingMode == ShootingMode.single || currentShootingMode == ShootingMode.burst)
        {
            //click right mouse
            isShooting = CrossPlatformInputManager.GetButtonDown("Shoot");
        }
        if (readyToShoot && isShooting && gun_ammo > 0)
        {
            currentBurst = bulletsPerBurst;

            FireWeapon();
        }

    }

    private void FireWeapon()
    {


        //instantiate bullet and set the direction in which the bullet will travel
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity);
        Vector3 shootingDirection = getDirectionAndSpread().normalized;
        bullet.transform.forward = shootingDirection;

        //play muzzleflash
        MuzzleFlash.GetComponent<ParticleSystem>().Play();

        //play recoil animation
        anim.SetTrigger("recoil");

        //playing bullet sound
        SoundManager.Instance.PlayerFireSound();

        //add force to the bullet in the shooting direction
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * BulletVelocity, ForceMode.Impulse);

        //destroy bullet after its lifeTime
        StartCoroutine(DestroyBullet(bullet, BulletLifeTime));

        //check if we are done shooting
        if (allowReset)
        {
            Invoke("resetShot", shootingDelay);
            allowReset = false;
        }

        //burst mode: if bullets left is > 1 then decrement currentBurst and invoke fireweapon method
        if (currentShootingMode == ShootingMode.burst && currentBurst > 1)
        {
            currentBurst--;
            Invoke("FireWeapon", shootingDelay);
        }
        gun_ammo--;
        ammoText.text = gun_ammo.ToString();

    }

    private void resetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 getDirectionAndSpread()
    {
        //shoot a ray from the middle of the screen
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            //hit something
            targetPoint = hit.point;
        }

        else
        {
            //shoot air
            targetPoint = ray.GetPoint(100);
        }

        //bullets direction
        Vector3 direction = targetPoint - BulletSpawn.position;

        //spread
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}

