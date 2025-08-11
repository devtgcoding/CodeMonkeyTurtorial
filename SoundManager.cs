using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECT_VOLUME = "soundEffectsVolume";
    public static SoundManager Instance {  get; private set; }
    [SerializeField] private AudioClipRefsSo AudioClipRefsSo;

    private float volume = 1.0f;
    private void Awake()
    {
        Instance = this;
        volume =  PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, 1f);

    }

    private void Start()
    {
        DeliveryManager.instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickSomething += Player_OnPickSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyTrash += TrashCounter_OnAnyTrash;
    }

    private void TrashCounter_OnAnyTrash(object sender, System.EventArgs e)
    {
       TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(AudioClipRefsSo.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(AudioClipRefsSo.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickSomething(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSo.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(AudioClipRefsSo.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
       DeliverCounter deliveryCounter = DeliverCounter.Instance;
       PlaySound(AudioClipRefsSo.deliveryFailed, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliverCounter deliveryCounter = DeliverCounter.Instance;
        PlaySound(AudioClipRefsSo.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volume);       
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiper = 1f)
    {
       
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiper * volume);
    }

    public void PlayFootStepsSound(Vector3 position,float volume)
    {
        PlaySound(AudioClipRefsSo.footstep, position, volume);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() { 
        return volume;
    }
}
