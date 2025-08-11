using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax;
    private void Awake()
    {
        player =  GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f) {
            footstepTimer = footstepTimerMax;
            if (player.IsWaking())
            {
                float volume = 0.3f;
                SoundManager.Instance.PlayFootStepsSound(player.transform.position, volume);
            }

          
        }
    }
}
