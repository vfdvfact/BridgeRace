using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools : Singleton<Pools>
{
    Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();
    [SerializeField] Transform brickParent;
    [SerializeField] Transform botParent;
    private void Awake()
    {
        GameUnit[] gameUnits = Resources.LoadAll<GameUnit>("Pool/Brick");
        for (int i = 0; i < gameUnits.Length; i++)
        {
            Preload(gameUnits[i], brickParent);
        }
        gameUnits = Resources.LoadAll<GameUnit>("Pool/Bot");
        for (int i = 0; i < gameUnits.Length; i++)
        {
            Preload(gameUnits[i], botParent);
        }
    }
    public void Preload(GameUnit prefab, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("PREFAB IS EMPTY!!!");
            return;
        }
        if (!poolInstance.ContainsKey(prefab.poolType) || poolInstance[prefab.poolType] == null)
        {
            Pool p = new Pool();
            p.PreLoad(prefab, parent);
            poolInstance[prefab.poolType] = p;
        }
    }
    public T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD!!!");
            return null;
        }
        return poolInstance[poolType].Spawn(pos, rot) as T;
    }
    public void Despawn(GameUnit unit)
    {
        if (!poolInstance.ContainsKey(unit.poolType))
        {
            Debug.LogError(unit.poolType + "IS NOT PRELOAD!!!");
        }
        poolInstance[unit.poolType].Despawn(unit);
    }
    public void Clear()
    {
        for (int i = 0; i < poolInstance.Count; i++)
        {
            poolInstance[(PoolType)i].Clear();
        }
    }
}
public enum PoolType
{
    Brick,Bot,
}
public class Pool
{
    Transform parent;
    GameUnit prefab;
    Queue<GameUnit> inactives = new Queue<GameUnit>();
    List<GameUnit> actives = new List<GameUnit>();
    int count=0;
    public void PreLoad(GameUnit prefab, Transform parent)
    {
        this.parent = parent;
        this.prefab = prefab;
    }
    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit;
        if (inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = inactives.Dequeue();
        }
        unit.TF.SetPositionAndRotation(pos, rot);
        actives.Add(unit);
        unit.gameObject.SetActive(true);
        return unit;
    }
    public void Despawn(GameUnit unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
    }
    public void Clear()
    {
        count = actives.Count;
        for (int i = 0; i < count; i++)
        {
            Despawn(actives[0]);
        }
        count = 0;
    }
}