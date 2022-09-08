
using System;
using System.Collections.Generic;
using UnityEngine;

public class KhtPool : KhtSingleton<KhtPool>
{
    [Serializable]
    public class PrefabData
    {
        public GameObject prefab;
        public int initPoolSize = 0;
    }
    [SerializeField] private List<PrefabData> prefabDatas = null;

    private readonly Dictionary<int, Queue<GameObject>> _pools = new Dictionary<int, Queue<GameObject>>();
    private readonly Dictionary<int, int> _objectToPoolDict = new Dictionary<int, int>();

    private new void Awake()
    {
        base.Awake();

        foreach (var prefabData in prefabDatas)
        {
            _pools.Add(prefabData.prefab.GetInstanceID(), new Queue<GameObject>());
            for (int i = 0; i < prefabData.initPoolSize; i++)
            {
                GameObject retObject = Instantiate(prefabData.prefab, Instance.transform, true);
                Instance._objectToPoolDict.Add(retObject.GetInstanceID(), prefabData.prefab.GetInstanceID());
                Instance._pools[prefabData.prefab.GetInstanceID()].Enqueue(retObject);
                retObject.SetActive(false);
            }
        }
        prefabDatas = null;
    }

    public static GameObject GetObject(GameObject prefab)
    {
        if (!Instance)
        {
            return Instantiate(prefab);
        }

        int prefabId = prefab.GetInstanceID();
        if (!Instance._pools.ContainsKey(prefabId))
        {
            Instance._pools.Add(prefabId, new Queue<GameObject>());
        }

        if (Instance._pools[prefabId].Count > 0)
        {
            return Instance._pools[prefabId].Dequeue();
        }

        GameObject retObject = Instantiate(prefab);
        Instance._objectToPoolDict.Add(retObject.GetInstanceID(), prefabId);

        return retObject;
    }

    public static void ReturnObject(GameObject poolObject)
    {
        if (!Instance)
        {
            Destroy(poolObject);
            return;
        }

        int objectId = poolObject.GetInstanceID();

        if (!Instance._objectToPoolDict.TryGetValue(objectId, out int poolId))
        {
            Destroy(poolObject);
            return;
        }

        Instance._pools[poolId].Enqueue(poolObject);
        poolObject.transform.SetParent(Instance.transform);
        poolObject.SetActive(false);
    }
}
