using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911_Arms : MonoBehaviour
{    
    [SerializeField] int currentAmmo = 8;
    [SerializeField] int magazineSize = 8;
    [SerializeField] GameObject magazinePrefab;
    [SerializeField] Transform magazineSpawnPoint;
    [SerializeField] GameObject casingPrefab;
    [SerializeField] Transform casingSpawnPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletForce = 400;
    [SerializeField] Transform bulletRotationX;
    [SerializeField] Transform bulletRotationY;

    private float lt;
    private float rt;
    private bool isFired = false;
    private bool isEmpty = false;
    private bool isReloading = false;
    private bool isAiming = false;

    private Animator anim;

    // ----- Low Poly FPS Pack Free Version -----
    [Header("Muzzleflash Settings")]
    public bool randomMuzzleflash = false;
    //min should always bee 1
    private int minRandomValue = 1;

    [Range(2, 25)]
    public int maxRandomValue = 5;

    private int randomMuzzleflashValue;

    public bool enableMuzzleflash = true;
    public ParticleSystem muzzleParticles;
    public bool enableSparks = true;
    public ParticleSystem sparkParticles;
    public int minSparkEmission = 1;
    public int maxSparkEmission = 7;

    [Header("Muzzleflash Light Settings")]
    public Light muzzleflashLight;
    public float lightDuration = 0.02f;

    [Header("Audio Source")]
    //Main audio source
    public AudioSource mainAudioSource;

    [System.Serializable]
    public class soundClips
    {
        public AudioClip shootSound;
        public AudioClip takeOutSound;
        //public AudioClip holsterSound;
        public AudioClip reloadSoundOutOfAmmo;
        public AudioClip reloadSoundAmmoLeft;
        //public AudioClip aimSound;
    }
    public soundClips SoundClips;
    // ----- Low Poly FPS Pack Free Version -----


    // Start is called before the first frame update
    void Start()
    {
        mainAudioSource.clip = SoundClips.takeOutSound;
        mainAudioSource.Play();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lt = Input.GetAxis("P1 Aim");
        rt = Input.GetAxis("P1 Fire");

        //If randomize muzzleflash is true, genereate random int values
        if (randomMuzzleflash == true)
        {
            randomMuzzleflashValue = Random.Range(minRandomValue, maxRandomValue);
        }

        // Fire
        if (rt == 1 && isFired == false && currentAmmo >= 2 && isReloading == false && isAiming == false)
        {
            mainAudioSource.clip = SoundClips.shootSound;
            mainAudioSource.Play();
            currentAmmo--;
            isFired = true;
            anim.SetTrigger("Not Aiming Fire");
        }
        else if (rt == 1 && isFired == false && currentAmmo == 1 && isReloading == false && isAiming == false)
        {
            mainAudioSource.clip = SoundClips.shootSound;
            mainAudioSource.Play();
            currentAmmo--;
            isFired = true;
            isEmpty = true;
            anim.SetTrigger("Not Aiming Fire Empty");
        }
        else if (rt == 1 && isFired == false && currentAmmo >= 2 && isReloading == false && isAiming == true)
        {
            mainAudioSource.clip = SoundClips.shootSound;
            mainAudioSource.Play();
            currentAmmo--;
            isFired = true;
            anim.SetTrigger("Aim Fire");
        }
        else if (rt == 1 && isFired == false && currentAmmo == 1 && isReloading == false && isAiming == true)
        {
            mainAudioSource.clip = SoundClips.shootSound;
            mainAudioSource.Play();
            currentAmmo--;
            isFired = true;
            isEmpty = true;
            anim.SetTrigger("Aim Fire Empty");
        }

        if (rt <= 0.5)
        {
            isFired = false;
        }

        // Aim
        if (lt == 1 && isAiming == false)
        {
            isAiming = true;
            if (currentAmmo > 0)
            {
                anim.SetTrigger("Aim In");
            }
            else if (currentAmmo == 0)
            {
                anim.SetTrigger("Aim In Empty");
            }
        }
        else if (lt == 0 && isAiming == true)
        {
            isAiming = false;
            if (currentAmmo > 0)
            {
                anim.SetTrigger("Aim Out");
            }
            else if (currentAmmo == 0)
            {
                anim.SetTrigger("Aim Out Empty");
            }
        }


        // Reload
        if (Input.GetButtonUp("P1 Reload") && isAiming == false)
        {
            currentAmmo = magazineSize;
            if (isEmpty == false)
            {
                anim.SetTrigger("Reload");
            }
            else
            {
                isEmpty = false;
                anim.SetTrigger("Reload Empty");
            }
        }
        else if (Input.GetButtonUp("P1 Reload") && isAiming == true)
        {
            currentAmmo = magazineSize;
            if (isEmpty == false)
            {
                anim.SetTrigger("Aim Out");
                anim.SetTrigger("Reload");
                anim.SetTrigger("Aim In");
            }
            else
            {
                isEmpty = false;
                anim.SetTrigger("Aim Out Empty");
                anim.SetTrigger("Reload Empty");
                anim.SetTrigger("Aim In");
            }
        }
    }

    void Reloading()
    {
        if (isReloading == false)
        {
            mainAudioSource.clip = SoundClips.reloadSoundAmmoLeft;
            mainAudioSource.Play();
            isReloading = true;
        }
        else
        {
            isReloading = false;
        }
    }

    void DropMagazine()
    {
        Instantiate(magazinePrefab, magazineSpawnPoint.position, magazineSpawnPoint.rotation);
    }

    void SpawnCasing()
    {
        Instantiate(casingPrefab, casingSpawnPoint.position, casingSpawnPoint.rotation);
    }

    void SpawnBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletForce;
        MuzzleVFX();
    }

    void NotAimSpawnBullet()
    {             
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(transform.eulerAngles.x + Random.Range(-5, 5), transform.eulerAngles.y + Random.Range(-5, 5), 0));
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletForce; 
        MuzzleVFX();
    }

    void MuzzleVFX()
    {
        muzzleflashLight.enabled = true;
        Invoke("MuzzleFlashLightOff", lightDuration);

        muzzleParticles.Emit(1);

        sparkParticles.Emit(Random.Range(1, 6));
    }

    void MuzzleFlashLightOff()
    {
        muzzleflashLight.enabled = false;
    } 
}
