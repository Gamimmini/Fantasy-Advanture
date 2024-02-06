using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MusicManagers : MonoBehaviour
{
    [SerializeField] AudioSource music;
    //private const string volumeDataPath = "Assets/MusicData/volumeData.json";
    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        LoadAudioSource();
        
        volumeSlider.interactable = false;
        volumeSlider.value = music.volume;
    }

    private void Awake()
    {
        LoadVolume();
    }
    public void LoadAudioSource()
    {
        music = GetComponent<AudioSource>();
    }
    public void OnMusic()
    {
        music.volume = 0.5f;
        OnVolumeSliderChanged();
    }

    public void OffMusic()
    {
        music.volume = 0f;
        OnVolumeSliderChanged();
    }
    public void DecreaseVolume()
    {
        music.volume = Mathf.Clamp01(music.volume - 0.1f);
        OnVolumeSliderChanged();
    }
    public void IncreaseVolume()
    {
        music.volume = Mathf.Clamp01(music.volume + 0.1f);
        OnVolumeSliderChanged();
    }
    public void OnVolumeSliderChanged()
    {
        if (volumeSlider != null)
        {
            //music.volume = volumeSlider.value;
            volumeSlider.value = music.volume;
            SaveVolume();
        }
    }

    public void SaveVolume()
    {
        VolumeData volumeData = new VolumeData();
        volumeData.volume = music.volume;

        string volumeJson = JsonUtility.ToJson(volumeData, true);
        File.WriteAllText(Application.dataPath + "/volumeData.json", volumeJson);
    }


    public void LoadVolume()
    {
        string filePath = Application.dataPath + "/volumeData.json";
        if (File.Exists(filePath))
        {
            string volumeJson = File.ReadAllText(filePath);
            VolumeData volumeData = JsonUtility.FromJson<VolumeData>(volumeJson);

            music.volume = volumeData.volume;
        }
    }

}
