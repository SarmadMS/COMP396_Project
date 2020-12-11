using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911_Arms2 : MonoBehaviour
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
    [SerializeField] GameObject laser;
    [SerializeField] Transform gunFix;

    private float lt;
    private float rt;
    private float dPad_v;
    private float dPad_h;
    private int rand;
    private bool isFired = false;
    private bool isEmpty = false;
    private bool isReloading = false;
    private bool isAiming = false;

    private Animator anim;
    private Player2Controller playerController;


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
    public AudioSource gunAudioSource;
    public AudioSource mainCameraAudioSource;

    [System.Serializable]
    public class soundClips
    {
        public AudioClip shootSound;
        public AudioClip takeOutSound;
        //public AudioClip holsterSound;
        public AudioClip reloadSoundOutOfAmmo;
        public AudioClip reloadSoundAmmoLeft;
        //public AudioClip aimSound;

        // ----- Low Poly FPS Pack Free Version -----

        public AudioClip p2_1_round;
        public AudioClip p2_2_rounds;
        public AudioClip p2_3_rounds;
        public AudioClip p2_4_rounds;
        public AudioClip p2_5_rounds;
        public AudioClip p2_6_rounds;
        public AudioClip p2_7_rounds;
        public AudioClip p2_8_rounds;
        public AudioClip p2_Need_to_reload;
        public AudioClip p2_Lets_go;
        public AudioClip p2_Wait;
        public AudioClip p2_All_clear;
        public AudioClip p2_Over_there;
        public AudioClip p2_I_need_help_over_here;

        public AudioClip p2_Just_watch_my_6;      // Just watch my 6!
        public AudioClip p2_Lets_just_get_this;   // Let's just get this over with!
        public AudioClip p2_Oh_please;            // Oh please.You thought that was difficult!
        public AudioClip p2_Rot_in_hell;          // Rot in hell!
        public AudioClip p2_Youve_got;            // You've got to be kidding me!

        public AudioClip p2_Im_good;
        public AudioClip p2_Im_okay;
        public AudioClip p2_Not_great;
        public AudioClip p2_Its_bad;
        public AudioClip p2_I_dont_think;

        // Friendly fire lines:
        // Shoot at me again, and the next bullet's yours!
        // Am I invisible or something!
        // 2 can play at that game!
    }
    public soundClips SoundClips;



    // Start is called before the first frame update
    void Start()
    {
        gunAudioSource.clip = SoundClips.takeOutSound;
        gunAudioSource.Play();

        anim = GetComponent<Animator>();
        playerController = GameObject.Find("Dungeon Operative 2").transform.Find("Camera").GetComponent<Player2Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        lt = Input.GetAxis("P2 Aim");
        rt = Input.GetAxis("P2 Fire");
        dPad_v = Input.GetAxis("P2 D-pad Vertical");
        dPad_h = Input.GetAxis("P2 D-pad Horizontal");


        //If randomize muzzleflash is true, genereate random int values
        if (randomMuzzleflash == true)
        {
            randomMuzzleflashValue = Random.Range(minRandomValue, maxRandomValue);
        }

        // Fire
        if (rt == 1 && isFired == false && currentAmmo >= 2 && isReloading == false && isAiming == false)
        {
            gunAudioSource.clip = SoundClips.shootSound;
            gunAudioSource.Play();
            currentAmmo--;
            isFired = true;
            anim.SetTrigger("Not Aiming Fire");
        }
        else if (rt == 1 && isFired == false && currentAmmo == 1 && isReloading == false && isAiming == false)
        {
            gunAudioSource.clip = SoundClips.shootSound;
            gunAudioSource.Play();
            currentAmmo--;
            isFired = true;
            isEmpty = true;
            anim.SetTrigger("Not Aiming Fire Empty");
        }
        else if (rt == 1 && isFired == false && currentAmmo >= 2 && isReloading == false && isAiming == true)
        {
            gunAudioSource.clip = SoundClips.shootSound;
            gunAudioSource.Play();
            currentAmmo--;
            isFired = true;
            anim.SetTrigger("Aim Fire");
        }
        else if (rt == 1 && isFired == false && currentAmmo == 1 && isReloading == false && isAiming == true)
        {
            gunAudioSource.clip = SoundClips.shootSound;
            gunAudioSource.Play();
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

        // Laser
        if (Input.GetButtonUp("P2 Laser"))
        {
            if (laser.activeSelf == true)
            {
                laser.SetActive(false);
            }
            else
            {
                laser.SetActive(true);
            }
        }

        // Over there
        if (Input.GetButtonUp("P2 Over there"))
        {
            mainCameraAudioSource.clip = SoundClips.p2_Over_there;
            mainCameraAudioSource.Play();
        }

        // Health
        if (Input.GetButtonUp("P2 Health"))
        {
            // 100 - 81->I'm good
            if (playerController.GetHealth() > 80)
            {
                mainCameraAudioSource.clip = SoundClips.p2_Im_good;
                mainCameraAudioSource.Play();
            }
            // 80 - 61 ->  I'm okay
            else if (playerController.GetHealth() > 60 && playerController.GetHealth() <= 80)
            {
                mainCameraAudioSource.clip = SoundClips.p2_Im_okay;
                mainCameraAudioSource.Play();
            }
            // 60 - 41 -> Not great
            else if (playerController.GetHealth() > 40 && playerController.GetHealth() <= 60)
            {
                mainCameraAudioSource.clip = SoundClips.p2_Not_great;
                mainCameraAudioSource.Play();
            }
            // 40 - 21 -> It's bad
            else if (playerController.GetHealth() > 20 && playerController.GetHealth() <= 40)
            {
                mainCameraAudioSource.clip = SoundClips.p2_Its_bad;
                mainCameraAudioSource.Play();
            }
            // 20 - 0 -> I don't think I'm gonna make it
            else if (playerController.GetHealth() > 0 && playerController.GetHealth() <= 20)
            {
                mainCameraAudioSource.clip = SoundClips.p2_I_dont_think;
                mainCameraAudioSource.Play();
            }
        }

        // D=pad Vertical
        if (dPad_v == 1)
        {
            mainCameraAudioSource.clip = SoundClips.p2_Lets_go;
            mainCameraAudioSource.Play();
        }
        else if (dPad_v == -1)
        {
            mainCameraAudioSource.clip = SoundClips.p2_Wait;
            mainCameraAudioSource.Play();
        }

        // D=pad Horizontal
        if (dPad_h == -1)
        {
            mainCameraAudioSource.clip = SoundClips.p2_All_clear;
            mainCameraAudioSource.Play();
        }
        else if (dPad_h == 1)
        {
            if (currentAmmo == 8)
            {
                mainCameraAudioSource.clip = SoundClips.p2_8_rounds;
                mainCameraAudioSource.Play();
            }
            else if (currentAmmo == 7)
            {
                mainCameraAudioSource.clip = SoundClips.p2_7_rounds;
                mainCameraAudioSource.Play();
            }
            else if (currentAmmo == 6)
            {
                mainCameraAudioSource.clip = SoundClips.p2_6_rounds;
                mainCameraAudioSource.Play();
            }
            else if (currentAmmo == 5)
            {
                mainCameraAudioSource.clip = SoundClips.p2_5_rounds;
                mainCameraAudioSource.Play();
            }
            else if (currentAmmo == 4)
            {
                mainCameraAudioSource.clip = SoundClips.p2_4_rounds;
                mainCameraAudioSource.Play();
            }
            else if (currentAmmo == 3)
            {
                mainCameraAudioSource.clip = SoundClips.p2_3_rounds;
                mainCameraAudioSource.Play();
            }
            else if (currentAmmo == 2)
            {
                mainCameraAudioSource.clip = SoundClips.p2_2_rounds;
                mainCameraAudioSource.Play();
            }
            else if (currentAmmo == 1)
            {
                mainCameraAudioSource.clip = SoundClips.p2_1_round;
                mainCameraAudioSource.Play();
            }
            else if (currentAmmo == 0)
            {
                mainCameraAudioSource.clip = SoundClips.p2_Need_to_reload;
                mainCameraAudioSource.Play();
            }
        }

        // Character lines
        if (Input.GetButtonUp("P2 Character lines"))
        {
            rand = Random.Range(1, 6);

            if (rand == 1)
            {
                mainCameraAudioSource.clip = SoundClips.p2_Oh_please;
                mainCameraAudioSource.Play();
            }
            else if (rand == 2)
            {
                mainCameraAudioSource.clip = SoundClips.p2_Youve_got;
                mainCameraAudioSource.Play();
            }
            else if (rand == 3)
            {
                mainCameraAudioSource.clip = SoundClips.p2_Lets_just_get_this;
                mainCameraAudioSource.Play();
            }
            else if (rand == 4)
            {
                mainCameraAudioSource.clip = SoundClips.p2_Just_watch_my_6;
                mainCameraAudioSource.Play();
            }
            else if (rand == 5)
            {
                mainCameraAudioSource.clip = SoundClips.p2_Rot_in_hell;
                mainCameraAudioSource.Play();
            }
        }

        // Reload
        if (Input.GetButtonUp("P2 Reload") && isAiming == false)
        {
            GunFix();

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
        else if (Input.GetButtonUp("P2 Reload") && isAiming == true)
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
            gunAudioSource.clip = SoundClips.reloadSoundAmmoLeft;
            gunAudioSource.Play();
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
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(transform.eulerAngles.x + Random.Range(-5, 6), transform.eulerAngles.y + Random.Range(-5, 6), 0));
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

    void GunFix()
    {
        transform.position = transform.position;
        transform.rotation = gunFix.rotation;
    }
}
