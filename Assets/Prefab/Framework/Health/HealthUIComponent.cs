using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIComponent : MonoBehaviour
{
    [SerializeField] HealthBar healthBarToSpawn;
    [SerializeField] Transform healthBarAttachPoints;
    [SerializeField] HealthComponent healthComponent;


    private void Start()
    {
        InGameUI inGameUI = FindAnyObjectByType<InGameUI>();
        HealthBar newHealthBar = Instantiate(healthBarToSpawn, inGameUI.transform);
        newHealthBar.Init(healthBarAttachPoints);
        healthComponent.onHealthChange += newHealthBar.SetHealthSliderValue;
        healthComponent.onHealthEmpty += newHealthBar.OnOwnerDead;
    }
}
