using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFader : MonoBehaviour
{
    private bool mFaded = true;
    public float Duration = 0.4f;
    public int cur = 0;
    public void Fade()
    {
        //cur++;
        var canvGroup = GetComponent<CanvasGroup>();          
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));
        mFaded = !mFaded;
  

    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;
        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
            yield return null;
        }
    }
}
