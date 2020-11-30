using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911_Arms : MonoBehaviour
{
    private Animator anim;

    [SerializeField] int currentAmmo = 8;
    [SerializeField] int magazineSize = 8;

    private float lt;
    private float rt;
    private bool isFired = false;
    private bool isEmpty = false;
    private bool isReloading = false;
    private bool isAiming = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lt = Input.GetAxis("P1 Aim");
        rt = Input.GetAxis("P1 Fire");

        // Fire
        if (rt == 1 && isFired == false && currentAmmo >= 2 && isReloading == false && isAiming == false)
        {
            currentAmmo--;
            isFired = true;
            anim.SetTrigger("Not Aiming Fire");
        }
        else if (rt == 1 && isFired == false && currentAmmo == 1 && isReloading == false && isAiming == false)
        {
            currentAmmo--;
            isFired = true;
            isEmpty = true;
            anim.SetTrigger("Not Aiming Fire Empty");
        }
        else if (rt == 1 && isFired == false && currentAmmo >= 2 && isReloading == false && isAiming == true)
        {
            currentAmmo--;
            isFired = true;
            anim.SetTrigger("Aim Fire");
        }
        else if (rt == 1 && isFired == false && currentAmmo == 1 && isReloading == false && isAiming == true)
        {
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
            isReloading = true;
        }
        else
        {
            isReloading = false;
        }
    }
}
