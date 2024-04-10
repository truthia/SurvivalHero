using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public WeaponSO weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected float currrentPiece;

    [HideInInspector] public Transform shooter;

    // Start is called before the first frame update
    protected virtual void OnEnable()
    {
        Invoke(nameof(DisableWeapon), destroyAfterSeconds);
    
    }
    protected virtual void Start()
    {

    }

    public float GetCurrentDamage()
    {
        if (shooter != null)
        {
            return currentDamage * shooter.GetComponent<HeroStats>().CurrentMight;
        }
        else return currentDamage;
    }
    void DisableWeapon()
    {
        gameObject.SetActive(false);
    }
    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
        transform.forward = dir;
        Quaternion rotation = transform.rotation; // Lấy giá trị rotation hiện tại

        rotation.x = 0f; // Thiết lập giá trị rotation.x thành 0

        transform.rotation = rotation;
    }
    public virtual void SetUpStats()
    {
        HeroStats stats = shooter.GetComponent<HeroStats>();
        currentDamage = stats.CurrentDMG * weaponData.DamageMulti*stats.CurrentMight;
        currrentPiece = weaponData.Pirece;
    }
}
