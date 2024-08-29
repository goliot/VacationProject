using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffects/RedPotion")]
public class RedPosionEffect : ItemEffect
{
    public override void ExecuteRole()
    {
        Debug.Log("레드포션");
        // 포션 효과 적용
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.playerData.health += Mathf.Clamp(10, 0, player.playerData.maxHealth);
    }
}
