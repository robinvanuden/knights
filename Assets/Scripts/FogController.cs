using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogController : MonoBehaviour {
    private Tilemap _tilemap;

    [Header("Animation")]
    [Tooltip("Time (in seconds) for one direction of the fade. The transparency will change every X seconds.")]
    [SerializeField]
    private float secondsPerStep = 2f;

    [Range(0f, 1f)] [SerializeField] public float minAlpha = 0.5f;
    [Range(0f, 1f)] [SerializeField] public float maxAlpha = 0.99f;

    [Tooltip("Start animating automatically when this component becomes enabled.")] [SerializeField]
    public bool playOnEnable = true;

    [Tooltip("Use unscaled time (ignores Time.timeScale).")] [SerializeField]
    private bool useUnscaledTime;

    private Coroutine _animRoutine;

    private void Awake() {
        if (_tilemap == null) _tilemap = GetComponent<Tilemap>();
    }

    private void OnEnable() {
        if (playOnEnable) Play();
    }

    private void OnDisable() {
        Stop();
    }

    private void Play() {
        if (_animRoutine != null) return;
        if (_tilemap == null) return;
        _animRoutine = StartCoroutine(AnimateTransparency());
    }

    private void Stop() {
        if (_animRoutine == null) return;
        StopCoroutine(_animRoutine);
        _animRoutine = null;
    }

    private IEnumerator AnimateTransparency() {
        // Guard against invalid configuration
        var aMin = Mathf.Clamp01(minAlpha);
        var aMax = Mathf.Clamp01(maxAlpha);
        if (Mathf.Approximately(aMin, aMax)) {
            SetAlpha(aMin);
            yield break;
        }

        var from = aMin;
        var to = aMax;

        while (enabled && _tilemap) {
            var duration = Mathf.Max(0.0001f, secondsPerStep);
            var t = 0f;
            while (t < 1f && enabled && _tilemap) {
                var dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                t += dt / duration;
                // Smoothstep easing for a nicer feel
                var eased = t * t * (3f - 2f * t);
                var alpha = Mathf.Lerp(from, to, Mathf.Clamp01(eased));
                SetAlpha(alpha);
                yield return null;
            }

            // Swap directions (ping-pong)
            (from, to) = (to, from);
        }
    }

    private void SetAlpha(float a) {
        _tilemap.color = new Color(_tilemap.color.r, _tilemap.color.g, _tilemap.color.b, a);
    }

    /// <summary>
    /// Fades the tilemap alpha to 0, then removes the tilemap GameObject.
    /// </summary>
    /// <param name="duration">Time in seconds to fade from current alpha to 0.</param>
    public void AnimateClear(float duration = 0.5f) {
        if (_tilemap == null) return;

        // Stop the ping-pong animation first
        Stop();

        // Ensure a reasonable duration
        var fadeDuration = Mathf.Max(0.0001f, duration);
        StartCoroutine(FadeOutAndRemove(fadeDuration));
    }

    private IEnumerator FadeOutAndRemove(float duration) {
        if (_tilemap == null) yield break;

        var startColor = _tilemap.color;
        var startA = startColor.a;
        var t = 0f;

        while (t < 1f && _tilemap) {
            var dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            t += dt / duration;
            var eased = t * t * (3f - 2f * t); // smoothstep
            var a = Mathf.Lerp(startA, 0f, Mathf.Clamp01(eased));
            SetAlpha(a);
            yield return null;
        }

        if (!_tilemap) yield break;
        SetAlpha(0f);
        // Remove the tilemap GameObject at the end of the fade
        var go = _tilemap.gameObject;
        _tilemap = null;
        if (go) Destroy(go);
    }

    public void ClearFog() {
        Stop();
        AnimateClear(2f);
    }
}