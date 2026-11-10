using System.Collections;
using UnityEngine;

namespace Creatures.Mobs
{
    public class ExplosionWaveEffect : MonoBehaviour
    {
        [Header("Appearance")]
        [SerializeField] private Color color = new Color(1f, 0.05f, 0.05f, 0.45f);
        [SerializeField] private float lineWidth = 0.12f;
        [SerializeField] private int segments = 48;

        [Header("Animation")]
        [SerializeField] private float startRadius = 0.05f;
        [SerializeField] private float duration = 0.4f;

        public void Play(Vector3 center, float targetRadius)
        {
            if (targetRadius <= 0f || duration <= 0f)
                return;

            var sourceRenderer = GetComponent<SpriteRenderer>();
            var waveObject = new GameObject("Explosion Wave")
            {
                transform =
                {
                    position = center
                }
            };

            var wave = waveObject.AddComponent<ExplosionWaveEffect>();
            wave.StartWave(
                targetRadius,
                color,
                lineWidth,
                segments,
                startRadius,
                duration,
                sourceRenderer != null ? sourceRenderer.sortingLayerID : 0,
                sourceRenderer != null ? sourceRenderer.sortingOrder + 1 : 1);
        }

        private void StartWave(
            float targetRadius,
            Color waveColor,
            float waveWidth,
            int waveSegments,
            float waveStartRadius,
            float waveDuration,
            int sortingLayerId,
            int sortingOrder)
        {
            var shader = Shader.Find("Sprites/Default");
            if (shader == null)
            {
                Destroy(gameObject);
                return;
            }

            var material = new Material(shader);
            var line = gameObject.AddComponent<LineRenderer>();
            line.useWorldSpace = false;
            line.loop = true;
            line.positionCount = waveSegments;
            line.widthMultiplier = waveWidth;
            line.numCornerVertices = 2;
            line.material = material;
            line.sortingLayerID = sortingLayerId;
            line.sortingOrder = sortingOrder;

            StartCoroutine(Animate(
                line,
                material,
                targetRadius,
                waveColor,
                waveSegments,
                waveStartRadius,
                waveDuration));
        }

        private IEnumerator Animate(
            LineRenderer line,
            Material material,
            float targetRadius,
            Color waveColor,
            int waveSegments,
            float waveStartRadius,
            float waveDuration)
        {
            var elapsed = 0f;

            while (elapsed < waveDuration)
            {
                var progress = Mathf.Clamp01(elapsed / waveDuration);
                var easedProgress = 1f - Mathf.Pow(1f - progress, 3f);
                var radius = Mathf.Lerp(waveStartRadius, targetRadius, easedProgress);
                var currentColor = waveColor;
                currentColor.a *= 1f - progress;

                line.startColor = currentColor;
                line.endColor = currentColor;
                SetCirclePositions(line, waveSegments, radius);

                elapsed += Time.deltaTime;
                yield return null;
            }

            Destroy(material);
            Destroy(gameObject);
        }

        private static void SetCirclePositions(LineRenderer line, int waveSegments, float radius)
        {
            var angleStep = Mathf.PI * 2f / waveSegments;

            for (var i = 0; i < waveSegments; i++)
            {
                var angle = angleStep * i;
                line.SetPosition(i, new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius);
            }
        }

        private void OnValidate()
        {
            lineWidth = Mathf.Max(0.01f, lineWidth);
            segments = Mathf.Max(12, segments);
            startRadius = Mathf.Max(0f, startRadius);
            duration = Mathf.Max(0.01f, duration);
        }
    }
}
