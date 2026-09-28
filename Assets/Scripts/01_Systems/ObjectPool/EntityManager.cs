
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Pool;
using Alchemy.Inspector;

public class EntityManager : MonoBehaviour
{
    [Serializable]
    public class PoolingData
    {
        public GameObject prefab;
        public int defaultCapacity;
        public int maxSize;
    }

    [SerializeReference] List<PoolingData> poolingDatas;
    private List<ObjectPool<Entity>> objectPools;
    private Dictionary<GameObject, int> poolIndexes;

    //임시
    public EntityData player;

    private void Awake()
    {
        objectPools = new List<ObjectPool<Entity>>();
        poolIndexes = new Dictionary<GameObject, int>();
        for (int i = 0; i < poolingDatas.Count; i++)
        {
            GameObject prefab = poolingDatas[i].prefab;

            ObjectPool<Entity> objectPool = new ObjectPool<Entity>(
                createFunc: () => { return CreateEntity(prefab); },
                actionOnGet: OnGetEntity,
                actionOnRelease: OnReleaseEntity,
                actionOnDestroy: OnDestroyEntity,
                defaultCapacity: poolingDatas[i].defaultCapacity,
                maxSize: poolingDatas[i].maxSize
            );

            objectPools.Add(objectPool);
            poolIndexes[prefab] = i;
        }
    }

    [Button]
    public Entity SpawnEntity(EntityData entityData, Vector3 position)
    {
        int index = poolIndexes[entityData.prefab];
        Entity entity = objectPools[index].Get();

        entity.transform.position = position;
        entity.transform.rotation = Quaternion.identity;
        entity.Initialize(entityData);
        entity.gameObject.SetActive(true);

        return entity;
    }

    [Button]
    public void SpawnPlayer()
    {
        SpawnEntity(player, Vector3.zero);
    }

    private Entity CreateEntity(GameObject prefab)
    {
        GameObject gameObject = Instantiate(prefab);
        if (!gameObject.TryGetComponent<Entity>(out Entity entity))
            Debug.Log("프리팹에 엔티티 컴포넌트가 없는데스웅;;");
        return entity;
    }

    private void OnGetEntity(Entity entity)
    {
        entity.gameObject.SetActive(false);
    }

    private void OnReleaseEntity(Entity entity)
    {
        entity.gameObject.SetActive(false);
    }

    private void OnDestroyEntity(Entity entity)
    {
        Destroy(entity.gameObject);
    }
}
