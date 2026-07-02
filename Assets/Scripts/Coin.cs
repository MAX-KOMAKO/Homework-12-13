using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value = 1;
    [SerializeField] private float _rotationSpeed = 90f;

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }

    public int Collect()
    {
        gameObject.SetActive(false);

        return _value;
    }
}