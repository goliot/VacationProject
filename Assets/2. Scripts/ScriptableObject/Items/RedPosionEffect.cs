using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffects/RedPotion")]
public class RedPosionEffect : ItemEffect
{
    public override void ExecuteEffect()
    {
        Debug.Log("��������");
        // ���� ȿ�� ����
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.playerData.health += Mathf.Clamp(10, 0, player.playerData.maxHealth);
    }

    public override void CancelEffect()
    {
        throw new System.NotImplementedException();
    }
}
