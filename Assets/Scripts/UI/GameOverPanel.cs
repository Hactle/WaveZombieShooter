using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EntityBehaviour;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        private CanvasGroup _gameOverPanel;

        private float _fadeDuration = 1f;

        private void Awake()
        {
            _gameOverPanel = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            EntityDieLogic.instance.OnPlayerDie += ShowGameOverPanel;

            _gameOverPanel.alpha = 0f;
            _gameOverPanel.interactable = false;
            _gameOverPanel.blocksRaycasts = false;
        }

        private void ShowGameOverPanel()
        {
            Debug.Log("Lose");
            StartCoroutine(FadeGameOverPanel());
        }

        private IEnumerator FadeGameOverPanel()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _fadeDuration)
            {
                _gameOverPanel.alpha = Mathf.Lerp(0, 1, elapsedTime / _fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _gameOverPanel.alpha = 1;
            _gameOverPanel.interactable = true;
            _gameOverPanel.blocksRaycasts = true;

            GameState.Instance.PauseGame();
        }

        private void OnDisable()
        {
            EntityDieLogic.instance.OnPlayerDie -= ShowGameOverPanel;
        }
    }
}
