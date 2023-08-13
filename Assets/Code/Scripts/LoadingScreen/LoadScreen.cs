using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    public Slider loadSlider;
    public float loadingTime = 3f;
    private float _loadProgress = 0;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Timer());
    }
    
    /// <summary>
    /// Logic for the timer
    /// </summary>
    private IEnumerator Timer()
    {
        yield return new WaitUntil(() => GameDataManager.Instance.Loaded);
        float time = loadingTime;
        while (time >= 0)
        {
            time -= .1f;
            _loadProgress = (loadingTime-time)/loadingTime;
            t = 0;
            yield return new WaitForSeconds(.1f);
        }
        gameObject.SetActive(false);
    }
    
    private float t = 0;
    /// <summary>
    /// Updates the slider ui when progress is made
    /// </summary>
    void Update()
    {
        loadSlider.value = Mathf.Lerp(loadSlider.value, _loadProgress, t);
        t += 0.75f * Time.deltaTime;
    }
}
