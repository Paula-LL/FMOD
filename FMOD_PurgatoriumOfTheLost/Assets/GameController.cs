using UnityEngine;
using UnityEngine.UI; // Per treballar amb UI Slider
using FMODUnity;
using FMOD.Studio;

public class GameController : MonoBehaviour
{
    [Header("FMOD Event Reference")]
    [SerializeField] private EventReference musicEvent; // Event de la cançó amb paràmetres
    [SerializeField] private EventReference musicInsEvent;

    [Header("UI Sliders")]
    [SerializeField] private Slider ChorusSlider;      
    [SerializeField] private Slider MidVolumeSlider;     

    private EventInstance musicInstance;
    private EventInstance musicInsInstance;

    public bool pausarMusica;
    [SerializeField] private GameObject canvasPrincipal;
    [SerializeField] private GameObject canvasPausa;

    [Header("VCA Sliders")]
    [SerializeField] private Slider VCAGeneral;
    [SerializeField] private Slider VCAMusic;
    [SerializeField] private Slider VCASFX;

    private VCA generalVCA;
    private VCA musicVCA;
    private VCA sfxVCA;
    void Start()
    {
        // Crear instància de l'event
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInsInstance = RuntimeManager.CreateInstance(musicInsEvent);
        musicInstance.start();


        // Assignar listeners als sliders
        ChorusSlider.onValueChanged.AddListener(OnChorusChanged);
        MidVolumeSlider.onValueChanged.AddListener(OnMidVolumeChanged);
        VCAGeneral.onValueChanged.AddListener(VCAGeneralOnValueChanged);
        VCAMusic.onValueChanged.AddListener(VCAMusicOnValueChanged);
        VCASFX.onValueChanged.AddListener(VCASFXOnValueChanged);

        // Assignar VCAs
        generalVCA = RuntimeManager.GetVCA("vca:/General VCA");
        musicVCA = RuntimeManager.GetVCA("vca:/Music VCA");
        sfxVCA = RuntimeManager.GetVCA("vca:/SFX VCA");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            activarPausa();
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            canvasPausa.SetActive(false);
            canvasPrincipal.SetActive(true);
            musicInstance.setPaused(false);
        }
    }

    private void OnChorusChanged(float value)
    {
        musicInstance.setParameterByName("ChorusIntensity", value);
    }

    private void OnMidVolumeChanged(float value)
    {  
        musicInstance.setParameterByName("MidVolume", value);
    }
    private void VCAGeneralOnValueChanged(float value)
    {
        generalVCA.setVolume(value);    
    }
    private void VCAMusicOnValueChanged(float value)
    {
        musicVCA.setVolume(value);  
    }
    private void VCASFXOnValueChanged(float value)
    {
        sfxVCA.setVolume(value);    
    }
    public void PausePlayMusic()
    {
        // Toggle the flag
        pausarMusica = !pausarMusica;

        if (pausarMusica)
        {
            musicInstance.setPaused(true);   // Pause
        }
        else
        {
            musicInstance.setPaused(false);  // Resume
        }
    }
    public void PlayMultiIntrumental()
    {
        musicInsInstance.start();
    }

    public void activarPausa()
    {
        musicInstance.setPaused(true);
        musicInsInstance.setPaused(true);
        canvasPrincipal.SetActive(false);
        canvasPausa.SetActive(true);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}