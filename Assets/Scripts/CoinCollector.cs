using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int CollectedCoins { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        Coin coin = other.GetComponent<Coin>();

        if (coin != null)
        {
            int value = coin.Collect();
            CollectedCoins += value;
        }
    }

    public void ResetCoins()
    {
        CollectedCoins = 0;
    }
}