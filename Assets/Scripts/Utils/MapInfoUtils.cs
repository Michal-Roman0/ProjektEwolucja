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
            return (maxDifficulty + difficultyBias - GetTileRawDifficulty(xPos, yPos)) / (float)maxDifficulty;
        }
        
        private static int GetTileRawDifficulty(float xPos, float yPos)
        {
            Vector2Int tilePosition = new Vector2Int(Mathf.FloorToInt(xPos), Mathf.FloorToInt(yPos));
            MapTile mapTile = Tilemap_Controller.instance.GetMapTile(tilePosition);

            if (mapTile is null)
            {
                throw new Exception($"There is no mapTile at x:{xPos}, y:{yPos}");
            }
            
            return mapTile.GetValue(MapType.Difficulty);
        }
    }
}