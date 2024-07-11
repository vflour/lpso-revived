using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image ProgressImage;
    [SerializeField] private float DefaultSpeed = 1f;
    [SerializeField] private UnityEvent<float> OnProgress;
    [SerializeField] private UnityEvent OnCompleted;

    private Coroutine AnimationCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (ProgressImage.type != Image.Type.Filled)
        {
            Debug.Log("Image type unsupported. Set to filled.");
            enabled = false;
        }
    }

    public void SetProgress(float progress)
    {
        SetProgress(progress, DefaultSpeed);
    }

    public void SetProgress(float progress, float speed)
    {
        if (progress < 0 || progress > 1) {
            Debug.Log($"Invalid progress value of {progress} was clamped.");
            progress = Mathf.Clamp01(progress);
        }
        if (progress != ProgressImage.fillAmount)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            AnimationCoroutine = StartCoroutine(AnimateProgress(progress, speed));
        }
    }

    private IEnumerator AnimateProgress(float progress, float speed)
    {
        float time = 0;
        float initialProgress = ProgressImage.fillAmount;

        while (time < 1)
        {
            ProgressImage.fillAmount = Mathf.Lerp(initialProgress, progress, time);
            time += Time.deltaTime * speed;

            OnProgress?.Invoke(ProgressImage.fillAmount);
            yield return null;
        }
        ProgressImage.fillAmount = progress;
        OnProgress?.Invoke(progress);
        OnCompleted?.Invoke();
    }
}
