using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Отрисовка стандартных полей

        LevelGenerator generator = (LevelGenerator)target;
        if (GUILayout.Button("Open Next Room")) // Кнопка для генерации следующей комнаты
        {
            generator.OpenNextRoom();
        }
    }
}