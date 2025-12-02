using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TrocarCenaDepoisDoVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nomeDaProximaCena;

    void Start()
    {
        videoPlayer.loopPointReached += QuandoVideoTerminar;
    }

    void QuandoVideoTerminar(VideoPlayer vp)
    {
        SceneManager.LoadScene(nomeDaProximaCena);
    }
}