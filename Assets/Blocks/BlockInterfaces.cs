using UnityEngine;

/// <summary>
/// ブロックのTick更新を行うためには，このインターフェイスを実装します．
/// </summary>
public interface ITickable
{
	void OnTick(Vector3Int location);
}

/// <summary>
/// プレイヤーがブロックを右クリックしたときに特定の処理を行うためには，このインターフェイスを実装します．
/// </summary>
public interface IInteractable
{
	void OnInteract(Vector3Int location, BaseBehaviour interacter);
}

/// <summary>
/// ブロックを設置したときにイベントを発生させます．
/// </summary>
public interface IPlacedEventListener
{
	void OnPlaced();
}