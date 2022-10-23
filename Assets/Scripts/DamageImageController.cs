using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageImageController : MonoBehaviour
{
    Image image;
    [SerializeField] float fadeOutTime;
    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);

    }
    public void DamageFeedback()
    {
        image.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        StartCoroutine(DisapearFeedback());
    }

    private IEnumerator DisapearFeedback()
    {
        yield return new WaitForSeconds(0.1f);
        float alpha = 0.5f;
        while (alpha > 0.0)
        {
            alpha -= Time.deltaTime / fadeOutTime;
            image.color = new Color(1.0f, 0.0f, 0.0f, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
}
