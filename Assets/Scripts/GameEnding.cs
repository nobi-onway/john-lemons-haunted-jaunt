using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private CanvasGroup _exitBgCanvasGroup;

    private const float FADE_DURATION = 1.0f;
    private const float DISPLAY_IMAGE_DURATION = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _player)
        {
            StartCoroutine(EndLevel());
        }
    }

    private IEnumerator EndLevel()
    {
        float timer = 0;

        while(timer < FADE_DURATION + DISPLAY_IMAGE_DURATION)
        {
            timer += Time.deltaTime;
            _exitBgCanvasGroup.alpha = timer / FADE_DURATION;
            yield return new WaitForEndOfFrame();
        }

        Application.Quit();
    }
}
