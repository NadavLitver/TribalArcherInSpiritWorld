using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public SoundAudioClip[] clips;
    public float DeactivateAudioObjectsTime;

    public ObjectPool m_pool;
    public enum Sound
    {
      PlayerJump,
      PlayerLand,
      PlayerHit,
      PlayerDead,
      PlayerWalk,
      PlayerLeap,
      OwlDead,
      OwlIdle,
      OwlHit,
      OwlAttack,
      PlayerSprint,
      HealthOrbReceived,
      BowDraw,
      BowReleaseFull,
      BowHit,
      StunShotHit,
      PlayerWalk2,
      PlayerWalk3,
      PlayerWalk4,
      PlayerSprint2,
      PlayerSprint3,
      PlayerSprint4,
      PlayerEnter,
      OwlProjectileHit,
      StatueDead,
      StatueAttack,
      StatueAim,
      SuicideCruising,
      SuicideDetect,
      SuicideDead,
      SuicideExplosions,
      LightingBoltArrowHit,
      StatueHit,
      QuestStart,
      QuestFinish,
      LightingBoltArrowHit2,
      RelicPickup,
      ScatterRelease


    }
    public static SoundManager instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public static void Play(Sound sound)
    {

        GameObject soundGO = _instance.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        _instance.StartCoroutine(_instance.DestroyAudioObjects(soundGO));

    }
    public static void Play(Sound sound, Vector3 worldPos)
    {
        GameObject soundGO = _instance.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        soundGO.transform.position = worldPos;
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        _instance.StartCoroutine(_instance.DestroyAudioObjects(soundGO));


    }
    public static void Play(Sound sound, Vector3 worldPos, float Volume)
    {
        GameObject soundGO = _instance.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        soundGO.transform.position = worldPos;
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.volume = Mathf.Clamp01(Volume);
        audioSource.PlayOneShot(GetAudioClip(sound));
        _instance.StartCoroutine(_instance.DestroyAudioObjects(soundGO));


    }
    public static void Play(Sound sound, Vector3 worldPos, float Volume, float pitch)
    {
        GameObject soundGO = _instance.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        soundGO.transform.position = worldPos;
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.volume = Mathf.Clamp01(Volume);
        audioSource.pitch = Mathf.Clamp(pitch, -3, 3);
        audioSource.PlayOneShot(GetAudioClip(sound));
        _instance.StartCoroutine(_instance.DestroyAudioObjects(soundGO));


    }
    public static void Play(Sound sound, Vector3 worldPos, float Volume, float pitch, float reverb)
    {

        GameObject soundGO = _instance.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        soundGO.transform.position = worldPos;
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.reverbZoneMix = Mathf.Clamp(reverb, 0, 1.1f);
        audioSource.volume = Mathf.Clamp01(Volume);
        audioSource.pitch = Mathf.Clamp(pitch, -3, 3);
        audioSource.PlayOneShot(GetAudioClip(sound));
        _instance.StartCoroutine(_instance.DestroyAudioObjects(soundGO));

    }
    public static void Play(Sound sound, Vector3 worldPos, float Volume, float pitch, float reverb,float _spatialBlend)
    {

        GameObject soundGO = _instance.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        soundGO.transform.position = worldPos;
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.reverbZoneMix = Mathf.Clamp(reverb, 0, 1.1f);
        audioSource.volume = Mathf.Clamp01(Volume);
        audioSource.pitch = Mathf.Clamp(pitch, -3, 3);
        audioSource.spatialBlend = _spatialBlend;
        audioSource.PlayOneShot(GetAudioClip(sound));
        _instance.StartCoroutine(_instance.DestroyAudioObjects(soundGO));

    }
    public static void Play(Sound sound, AudioSource source)
    {
       
        source.PlayOneShot(GetAudioClip(sound));

    }
    public static void Play(Sound sound, AudioSource source, float Volume)
    {

        source.volume = Mathf.Clamp01(Volume);
        source.PlayOneShot(GetAudioClip(sound));

    }
    public static void Play(Sound sound, AudioSource source, float Volume, float pitch)
    {
        source.pitch = Mathf.Clamp(pitch, -3, 3);
        source.volume = Mathf.Clamp01(Volume);
        source.PlayOneShot(GetAudioClip(sound));

    }
    public static void Play(Sound sound, AudioSource source, float Volume, float pitch, float reverb)
    {
        source.reverbZoneMix = Mathf.Clamp(reverb, 0, 1.1f);
        source.pitch = Mathf.Clamp(pitch, -3, 3);
        source.volume = Mathf.Clamp01(Volume);
        source.PlayOneShot(GetAudioClip(sound));

    }
    public static void Play(Sound sound, AudioSource source, float Volume, float pitch, float reverb, float _SpatialBlend)
    {
        source.spatialBlend = Mathf.Clamp(_SpatialBlend, -1, 1);
        source.reverbZoneMix = Mathf.Clamp(reverb, 0, 1.1f);
        source.pitch = Mathf.Clamp(pitch, -3, 3);
        source.volume = Mathf.Clamp01(Volume);
        source.PlayOneShot(GetAudioClip(sound));

    }
    /// <summary>
    /// Use this method when you want a random pitch
    /// </summary>
    /// <param name="pitchRange">Set the X of the vector to the minimum of the pitch range (can be negative) And the Y to the Max Range.</param>
    public static void Play(Sound sound, AudioSource source, float Volume, float reverb, Vector3 pitchRange)
    {

        source.reverbZoneMix = Mathf.Clamp(reverb, 0, 1.1f);
        source.pitch = Randomizer.ReturnRandomFloat(new Vector2(Mathf.Clamp(pitchRange.x, -3, 3), Mathf.Clamp(pitchRange.y, -3, 3)));
        source.volume = Mathf.Clamp01(Volume);
        source.PlayOneShot(GetAudioClip(sound));

    }

    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip clip in instance.clips)
        {
            if (clip.m_Sound == sound)
            {
                return clip.m_AudioClip;
            }
        }
        return null;
    }
    private IEnumerator DestroyAudioObjects(GameObject go)
    {
        yield return new WaitForSeconds(DeactivateAudioObjectsTime);
        go.SetActive(false);

    }

}
[Serializable]
public class SoundAudioClip
{
    public SoundManager.Sound m_Sound;
    public AudioClip m_AudioClip;
}