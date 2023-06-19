using System;
using UnityEngine;


namespace Utils
{
    public static class MapInfoUtils
    {
        public static float GetTileDifficulty(float xPos, float yPos)
        {
            int maxDifficulty = 100;
            int difficultyBias = 10;

            float diff = GetTileRawDifficulty(xPos, yPos);

            if(diff == -1)
                return -1;
            else
                return (maxDifficulty + difficultyBias - GetTileRawDifficulty(xPos, yPos)) / (float)maxDifficulty;
        }
        
        private static int GetTileRawDifficulty(float xPos, float yPos)
        {
            Vector2Int tilePosition = new Vector2Int(Mathf.FloorToInt(xPos), Mathf.FloorToInt(yPos));
            MapTile mapTile = Tilemap_Controller.instance.GetMapTile(tilePosition);

            if (mapTile is null)
            {
                Debug.Log($"There is no mapTile at x:{xPos}, y:{yPos}");
                return -1;
            }
            
            return mapTile.GetValue(MapType.Difficulty);
        }
    }
}