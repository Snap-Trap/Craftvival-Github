using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Block Data", menuName = "Data/Block Data")]
public class BlockDataSO : ScriptableObject
{
    public float textureSizeX, textureSizeY;
    public List<TextureData> textureDataList;
}

[Serializable]
public class TextureData
{
    public BlockType blockType;
    public Vector2Int up, down, side;
    public bool isSolid = true;
    public bool generatesCollider = true;
}
