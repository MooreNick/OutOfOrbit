using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStatic", menuName = "ScriptableObjects/QuestStatic", order = 1)]
public class QuestStatic : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Dialogue")]
    public List<string> questDialogueLines;

    [Header("Prerequisites")]
    public QuestStatic[] questPrerequisites;

    [Header("Quest Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int goldReward;
    public int experienceGained;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
