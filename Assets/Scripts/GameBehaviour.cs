using UnityEngine;

public class GameBehaviour : MonoBehaviour {

	[SerializeField] private GameType type;
	
	public void SetGameType(GameType gameType) {
		this.type = gameType;
	}
}