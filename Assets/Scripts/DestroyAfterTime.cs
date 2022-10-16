using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float fadeOutTime;
    MeshRenderer meshRenderer;
    Coroutine c;
    private void OnEnable()
    {
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        //if(c!=null)
        //StopCoroutine(c); --> esto solo se pone cuando la Pool es estática, sino no hace falta
        //esto se pone por si, imaginemos que tenemos toda la pool usada, si volvemos a activar un objeto de la pool
        //paramos la coroutina que está siendo usada por una decal y volvemos a iniciarla para no quitar rendimiento
        c = StartCoroutine(destroyAfterTime(time));

    }

    IEnumerator destroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        float alpha = 1.0f;
        while (alpha > 0.0)
        {
            alpha -= Time.deltaTime / fadeOutTime;
            meshRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            yield return new WaitForEndOfFrame();
        }
        //Destroy(gameObject);
        gameObject.SetActive(false);
        //antes se destruian porque no habia pool, pero con pool se tienen que, simplemente desactivar
    }
}
