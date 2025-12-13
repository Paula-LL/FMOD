using UnityEngine;
using UnityEngine.UI; // Per treballar amb UI Slider
using FMODUnity;
using FMOD.Studio;

public class GameController : MonoBehaviour
{
    [Header("FMOD Event Reference")]
    [SerializeField] private EventReference musicEvent; // Event de la cançó amb paràmetres

    [Header("UI Sliders")]
    [SerializeField] private Slider ChorusSlider;       // Slider per controlar el Low-Pass
    [SerializeField] private Slider MidVolumeSlider;       // Slider per controlar el Reverb

    private EventInstance musicInstance;

    void Start()
    {
        // Crear instància de l'event
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
       

        // Assignar listeners als sliders
        ChorusSlider.onValueChanged.AddListener(OnChorusChanged);
        MidVolumeSlider.onValueChanged.AddListener(OnMidVolumeChanged);
    }

    private void OnChorusChanged(float value)
    {
        // Modifica el paràmetre continuous "FilterAmount"
        musicInstance.setParameterByName("ChorusIntensity", value);
    }

    private void OnMidVolumeChanged(float value)
    {
        // Modifica el paràmetre continuous "ReverbIntensity"
        musicInstance.setParameterByName("MidVolume", value);
    }

}