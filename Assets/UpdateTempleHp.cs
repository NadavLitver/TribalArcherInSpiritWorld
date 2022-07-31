using UnityEngine;
using UnityEngine.UI;

public class UpdateTempleHp : MonoBehaviour
{
    Slider templeHPSlider;
    void Start()
    {
        templeHPSlider = GetComponent<Slider>();
        templeHPSlider.maxValue = EnemySpawnerManager.instance.templeBody.maxHealth;
        templeHPSlider.value = templeHPSlider.maxValue;
        EnemySpawnerManager.instance.templeBody.hitEvent.AddListener(updateTempleHpSlider);
    }

    private void updateTempleHpSlider()
    {
        templeHPSlider.value = EnemySpawnerManager.instance.templeBody.health;
    }
}
