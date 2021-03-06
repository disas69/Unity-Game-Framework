using DG.Tweening;
using Framework.UI.Structure.Base.Model;
using Framework.UI.Structure.Base.View;
using JetBrains.Annotations;
using Source.Data;
using Source.State;
using UnityEngine;
using TMPro;

namespace Source.UI.Pages
{
    public class SuccessPage : Page<PageModel>
    {
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private float _fadeInTime;
        [SerializeField] private CanvasGroup _overlay;

        public override void OnEnter()
        {
            base.OnEnter();

            if (_level != null)
            {
                _level.text = $"Level {GameData.LevelProgression - 1}";
            }

            _overlay.alpha = 0f;
            _overlay.blocksRaycasts = false;

            DOTween.Sequence()
                .AppendInterval(2.5f)
                .AppendCallback(GoToNextLevel)
                .Play();
        }

        [UsedImplicitly]
        public void GoToNextLevel()
        {
            _overlay.blocksRaycasts = true;

            DOTween.Sequence()
                .Append(_overlay.DOFade(1f, _fadeInTime))
                .AppendCallback(() => GameController.Instance.Session.Dispose())
                .AppendCallback(() => GameController.Instance.Session.WaitUntilUnloaded(() =>
                {
                    GameController.Instance.SetState(GameState.Start);
                }))
                .Play();
        }
    }
}