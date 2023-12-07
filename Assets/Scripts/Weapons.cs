
public interface WeaponBehavior
{
    void Shoot();
}
public class SwordWaepon : WeaponBehavior
{
    public void Shoot()
    {

    }
}
public class PlayerGunWeapon : WeaponBehavior
{
    public void Shoot()
    {
        PlayerGun.Instance.Shoot();
        
    }
}

public class ArmorUpWeapon : WeaponBehavior
{
    public void Shoot()
    {
        Player.ArmorUp(15);
        
    }
}

public class HealWeapon : WeaponBehavior
{
    public void Shoot()
    {
       Player.Heal(15);
         
    }
}