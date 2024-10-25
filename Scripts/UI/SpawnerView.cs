using TMPro;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private SpawnerInfo _spawner;
    [SerializeField] private TextMeshProUGUI _infoQuantitiesCubes;
    [SerializeField] private TextMeshProUGUI _infoQuantitiesBombs;
    [SerializeField] private TextMeshProUGUI _infoQuantityAllItems;

    private string _name;
    private bool _nameValueAssigned;

    private void OnEnable()
    {
        _spawner.ObjectSpawned += UpdateCounts;
        _spawner.ObjectDespawned += UpdateCounts;
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned -= UpdateCounts;
        _spawner.ObjectDespawned -= UpdateCounts;
    }

    private void UpdateCounts(GameObject item)
    {
        AssignNameValue(item);
        
        if (item.name.Equals(_name))
        {
            ShowTextQuantityItem(item, _infoQuantitiesCubes);
        }
        else
        {
            ShowTextQuantityItem(item, _infoQuantitiesBombs);
        }
    }

    private void ShowTextQuantityItem(GameObject item, TextMeshProUGUI text)
    {
        text.text = $"{item.name}: " +
            $"\n{nameof(_spawner.QuantityObjectsCreated)} - {_spawner.QuantityObjectsCreated}" +
            $"\n{nameof(_spawner.QuantityObjectsScene)}  - {_spawner.QuantityObjectsScene}";

        _infoQuantityAllItems.text =
            $"\n{nameof(_spawner.QuantityObjectsAllTime)} - {_spawner.QuantityObjectsAllTime}";
    }

    private void AssignNameValue(GameObject item)
    {
        if(_nameValueAssigned == false)
        {
            _name = item.name;
            _nameValueAssigned = true;
        }
    }
}