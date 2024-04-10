using System.Collections.Generic;
using UnityEngine;

public class Stitcher
{
    public GameObject Stitch(GameObject sourceClothing, GameObject targetAvatar)
    {
        GameObject instanceClothing = GameObject.Instantiate(sourceClothing);
        TransformCatalog boneCatalog = new TransformCatalog(targetAvatar.transform);
        SkinnedMeshRenderer[] skinnedMeshRenderers = instanceClothing.GetComponentsInChildren<SkinnedMeshRenderer>();
        GameObject targetClothing = AddChild(instanceClothing, targetAvatar.transform);
        foreach (SkinnedMeshRenderer sourceRenderer in skinnedMeshRenderers)
        {
            SkinnedMeshRenderer targetRenderer = AddSkinnedMeshRenderer(sourceRenderer, targetClothing);
            targetRenderer.bones = TranslateTransforms(sourceRenderer.bones, boneCatalog);
        }
        GameObject.Destroy(instanceClothing);
        return targetClothing;
    }

    private GameObject AddChild(GameObject source, Transform parent)
    {
        GameObject target = new GameObject(source.name);
        target.transform.parent = parent;
        target.transform.localPosition = source.transform.localPosition;
        target.transform.localRotation = source.transform.localRotation;
        target.transform.localScale = source.transform.localScale;
        return target;
    }

    private SkinnedMeshRenderer AddSkinnedMeshRenderer(SkinnedMeshRenderer source, GameObject parent)
    {
        SkinnedMeshRenderer target = parent.AddComponent<SkinnedMeshRenderer>();
        target.sharedMesh = source.sharedMesh;
        target.materials = source.materials;
        return target;
    }

    private Transform[] TranslateTransforms(Transform[] sources, TransformCatalog transformCatalog)
    {
        Transform[] targets = new Transform[sources.Length];
        for (int index = 0; index < sources.Length; index++)
            targets[index] = DictionaryExtensions.Find(transformCatalog, sources[index].name);
        return targets;
    }


    #region TransformCatalog
    private class TransformCatalog : Dictionary<string, Transform>
    {
        #region Constructors
        public TransformCatalog(Transform transform)
        {
            Catalog(transform);
        }
        #endregion

        #region Catalog
        private void Catalog(Transform transform)
        {
            if (ContainsKey(transform.name))
            {
                Remove(transform.name);
                Add(transform.name, transform);
            }
            else
                Add(transform.name, transform);
            foreach (Transform child in transform)
                Catalog(child);
        }
        #endregion
    }
    #endregion


    #region DictionaryExtensions
    private class DictionaryExtensions
    {
        public static TValue Find<TKey, TValue>(Dictionary<TKey, TValue> source, TKey key)
        {
            TValue value;
            source.TryGetValue(key, out value);
            return value;
        }
    }
    #endregion

}