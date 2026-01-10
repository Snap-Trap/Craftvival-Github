using System;
using UnityEngine;
public static class Chunk
{
    public static void LoopThroughBlocks(ChunkData chunkData, Action<int, int, int> actionToPerform)
    {
        for (int index = 0; index < chunkData.blocks.Length; index++)
        {
            var position = GetPositionFromIndex(chunkData, index);
            actionToPerform(position.x, position.y, position.z);
        }
    }

    private static Vector3Int GetPositionFromIndex(ChunkData chunkData, int index)
    {
        int x = index % chunkData.chunkSize;
        int y = (index / chunkData.chunkSize) % chunkData.chunkHeight;
        int z = index / (chunkData.chunkSize *  chunkData.chunkHeight);
        return new Vector3Int(x, y, z);
    }

    private static bool InRange(ChunkData chunkData, int axisCoordinate)
    {
        if (axisCoordinate < 0 || axisCoordinate >= chunkData.chunkSize)
        {
            return false;
        }
        
        return true;
    }
    
    private static bool InRangeHeight(ChunkData chunkData, int yCoordinate)
    {
        if (yCoordinate < 0 || yCoordinate >= chunkData.chunkHeight)
        {
            return false;
        }
        
        return true;
    }

    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, Vector3Int chunkCoordinates)
    {
        return GetBlockFromChunkCoordinates(chunkData, chunkCoordinates.x, chunkCoordinates.y, chunkCoordinates.z);
    }

    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z)
    {
        if (InRange(chunkData, x) && InRangeHeight(chunkData, y) && InRange(chunkData, z))
        {
            int index = GetIndexFromPosition(chunkData, x, y, z);
            return chunkData.blocks[index];
        }
        
        throw new Exception("Invalid chunk coordinates");
    }

    public static void SetBlock(ChunkData chunkData, Vector3Int localPosition, BlockType block)
    {
        if (InRange(chunkData, localPosition.x) && InRangeHeight(chunkData, localPosition.y) && InRange(chunkData, localPosition.z))
        {
            int index = GetIndexFromPosition(chunkData, localPosition.x, localPosition.y, localPosition.z);
            chunkData.blocks[index] = block;
        }
        else
        {
            throw new Exception("World needs to ask for an appropiate chunk");
        }
    }

    private static int GetIndexFromPosition(ChunkData chunkData, int x, int y, int z)
    {
        return x + chunkData.chunkSize * y + chunkData.chunkHeight * chunkData.chunkHeight * z;
    }

    public static Vector3Int GetBlockInChunkCoordinates(ChunkData chunkData, Vector3Int pos)
    {
        return new Vector3Int
        {
            x = pos.x - chunkData.worldPosition.x,
            y = pos.y - chunkData.worldPosition.y,
            z = pos.z - chunkData.worldPosition.z
        };
    }

    public static MeshData GetChunkMeshData(ChunkData chunkData)
    {
        MeshData meshData = new MeshData(true);
        
        return meshData;
    }
}
