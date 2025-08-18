using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class MusicaController : MonoBehaviour
{
    [SerializeField] private EventReference musicaEvent;

    private EventInstance musicaInstance;

    void Start()
    {
        // Cria a instância da música e começa a tocar
        musicaInstance = RuntimeManager.CreateInstance(musicaEvent);
        musicaInstance.start();

        // Opcional: se quiser que a música não seja destruída ao trocar de cena
        DontDestroyOnLoad(gameObject);
    }

    public void PararMusica()
    {
        // Para a música suavemente
        musicaInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicaInstance.release();
    }
}
