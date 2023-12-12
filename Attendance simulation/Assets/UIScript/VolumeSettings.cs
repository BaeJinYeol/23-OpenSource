using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public void SetVolume()
    {
        SoundManager.Instance.SetBgVolume();
    }
}
