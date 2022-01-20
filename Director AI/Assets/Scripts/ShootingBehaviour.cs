using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootingBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _primaryGunTemplate = null;
    [SerializeField]
    private GameObject _secondaryGunTemplate = null;

    [SerializeField]
    private GameObject _primarySocket = null;
    [SerializeField]
    private GameObject _secondarySocket = null;

    private BasicWeapon _primaryGun = null;
    private BasicWeapon _secondaryGun = null;

    public BasicWeapon Weapon
    {
        get { return _primaryGun; }
    }

    void Awake()
    {
        //spawn guns
        if (_primaryGunTemplate != null && _primarySocket != null)
        {
            var gunObject = Instantiate(_primaryGunTemplate,
                _primarySocket.transform, true);
            gunObject.transform.localPosition = Vector3.zero;
            gunObject.transform.localRotation = Quaternion.identity;
            _primaryGun = gunObject.GetComponent<BasicWeapon>();
        }

        if (_secondaryGunTemplate != null && _secondarySocket != null)
        {
            var gunObject = Instantiate(_secondaryGunTemplate,
                _secondarySocket.transform, true);
            gunObject.transform.localPosition = Vector3.zero;
            gunObject.transform.localRotation = Quaternion.identity;
            _secondaryGun = gunObject.GetComponent<BasicWeapon>();
        }

    }

    public void PrimaryFire()
    {
        if (_primaryGun != null)
            _primaryGun.Fire();
    }

    public void SecondaryFire()
    {
        if (_secondaryGun != null)
            _secondaryGun.Fire();
    }

    public void Reload()
    {
        if (_primaryGun != null)
            _primaryGun.Reload();
        if (_secondaryGun != null)
            _secondaryGun.Reload();
    }
}
