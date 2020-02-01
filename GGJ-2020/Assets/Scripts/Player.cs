using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player behaviour attached to use weapons and movement
/// </summary>
public class Player : MonoBehaviour
{
    private Weapon mWeapon = null;
    private Ray ray = new Ray();
    private RaycastHit rayHit = new RaycastHit();

    private void Awake()
    {
        mWeapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        //While touching the screen look at the touching position in the world and shoot using the refresh in the weapon
        if (Input.GetMouseButton(0))
        {
            if (Camera.main != null) ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out rayHit, 100)) 
                transform.LookAt(new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z), transform.up);
            mWeapon.Shoot();
        }
    }
}
