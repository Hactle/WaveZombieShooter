using UnityEngine;

namespace Factory
{
    public interface IDropFactory
    {
        GameObject CreateDrop(Vector3 spawnPosition);
    }
}

