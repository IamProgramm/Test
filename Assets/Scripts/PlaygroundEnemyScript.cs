using UnityEngine;

public class PlaygroundEnemyScript : MonoBehaviour
{
	public GameObject[] Targets;
    [SerializeField]
	public int UpdateTargets = 0;
	public int UpdateTargetsForTargets = 0;

	public void Start()
	{
		for (int i = 0; i < Targets.Length; i++)
		{
			UpdateTargets++;
			UpdateTargetsForTargets = UpdateTargets;
		}

	}
}