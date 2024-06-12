using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private CanvasGroup _exitBgCanvasGroup;
    [SerializeField]
    private CanvasGroup _caughtBgCanvasGroup;
    [SerializeField]
    private AudioSource _caughtAudio;
    [SerializeField]
    private AudioSource _escapeAudio;

    private const float FADE_DURATION = 1.0f;
    private const float DISPLAY_IMAGE_DURATION = 1.0f;

    private PlayerMover _playerMover;

    private void Start()
    {
        _playerMover = _player.GetComponent<PlayerMover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _player && !_playerMover.IsOnPlanning)
        {
            StartCoroutine(EndLevel(_exitBgCanvasGroup, () => SceneManager.LoadScene(SceneManager.GetActiveScene().name), _escapeAudio));
        }
    }

    public void CaughtPlayer()
    {
        StartCoroutine(EndLevel(_caughtBgCanvasGroup, () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }, _caughtAudio));
    }

    private IEnumerator EndLevel(CanvasGroup bgCanvasGroup, Action onEndLevel, AudioSource audio)
    {
        audio.Play();
        float timer = 0;

        while(timer < FADE_DURATION + DISPLAY_IMAGE_DURATION)
        {
            timer += Time.deltaTime;
            bgCanvasGroup.alpha = timer / FADE_DURATION;
            yield return new WaitForEndOfFrame();
        }

        onEndLevel?.Invoke();
    }
}
