using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStatic", menuName = "ScriptableObjects/QuestStatic", order = 1)]
public class QuestStatic : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;
    [Tooltip("Lines npc says after entire quest is completed and turned in")]
    public List<string> finishedLines;

    [Header("Prerequisites")]
    public int playerLevelRequired;

    [Header("Quest Steps")]
    public GameObject[] questStepPrefabs;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
