using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayListManager : MonoBehaviour
{
    [SerializeField] private SoMusic[] _soMusics;
    [SerializeField] AudioManager _audioManager;
    [SerializeField] private int _startMusicId;

    private int currentId;
    private void Awake() {
        _audioManager.OnAskForNewSong+= AudioManagerOnOnAskForNewSong;
        StaticEvent.OnAskForNextSong+= AudioManagerOnOnAskForNewSong;
    }

    private void OnDestroy()
    {
        _audioManager.OnAskForNewSong-= AudioManagerOnOnAskForNewSong;
        StaticEvent.OnAskForNextSong-= AudioManagerOnOnAskForNewSong;
    }


    private void Start() {
        if( _startMusicId >= _soMusics.Length) _startMusicId = 0;
        currentId = 0;
        PlayerSoundAtId(_startMusicId);
    }

    private void AudioManagerOnOnAskForNewSong(object sender, EventArgs e) {
        if (GameStateData.PlayMusicInRandom) {
            SoMusic selectecMusic = _soMusics[Random.Range(0, _soMusics.Length)];
            AudioManager.Instance.PlayMusic(selectecMusic.Song.GetSound());
            StaticEvent.DoOnSoMusicChange(selectecMusic);
        }
        else {
            if( currentId+1 >= _soMusics.Length) currentId = 0;
            else currentId++;
            PlayerSoundAtId(currentId);   
        }
    }

    private void PlayerSoundAtId(int id) {
        SoMusic selectecMusic = _soMusics[id];
        AudioManager.Instance.PlayMusic(selectecMusic.Song.GetSound());
        StaticEvent.DoOnSoMusicChange(selectecMusic);
    }
}