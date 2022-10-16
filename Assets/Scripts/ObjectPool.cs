using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] int numOfObjects;
    [SerializeField] GameObject objectToPool;
    List<GameObject> pooledObjects = new List<GameObject>();

    private void Awake()
    {
        for (int i=0;i<numOfObjects;i++)
        {
            addObjectToPool();
        }
    }

    public GameObject enableObject(Vector3 position, Quaternion rotation)
    {
        GameObject o = getAvailableObject();

        //haciendo este if hacemos que la pool de los decals sea dinámica, que cuando se mete un nuevo objeto
        //(es decir, se hace más grande), en ese momento pierde eficiencia pero luego, si lo tenemos que volver a usar,
        //la longitud de la lista ya será lo suficientemente grande como para poder usarlo sin necesidad de crecer de nuevo

        //FORMA ESTÁTICA
        //--------------------------------
        pooledObjects.Remove(o);
        pooledObjects.Add(o);

        //se elimina de la lista y se vuelve a meter para ponerlo en el útlimo lugar de la lista y así tener siempre en la primera posición la decal usada más antigua

        o.SetActive(true);
        o.transform.position = position;
        o.transform.rotation = rotation;
        return o;
    }

    public GameObject getAvailableObject()
    {
        for (int i=0; i<pooledObjects.Count;i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return pooledObjects[0];
    }

    private GameObject addObjectToPool()
    {
        GameObject o = Instantiate(objectToPool);
        o.SetActive(false);
        pooledObjects.Add(o);
        return o;
    }
}
