using UnityEngine;

namespace GenerateWorld
{
    public interface Room
    {
        int getShapeY();

        int getShapeX();

        Vector3[] GenerateWalls(Vector3 initPos);

        Vector3[] GenerateFloor(Vector3 initPos);

    }
}
