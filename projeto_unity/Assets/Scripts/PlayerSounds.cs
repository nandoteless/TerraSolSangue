using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private EventReference _footsteps;
    private EventInstance footsteps;

    private void Awake()
    {
        if (!_footsteps.IsNull)
        {
            footsteps = RuntimeManager.CreateInstance(_footsteps);
        }
    }

    public void PlayFootsteps()
    {
        if (footsteps.handle != System.IntPtr.Zero)
        {
            footsteps.start();
        }
    }
}
