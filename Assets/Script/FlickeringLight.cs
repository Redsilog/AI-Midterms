using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public bool horrorFlicker = true;
    public float minOnTime = 0.05f;
    public float maxOnTime = 0.2f;
    public float minOffTime = 0.05f;
    public float maxOffTime = 0.3f;

    private Light _light;
    private float _timer;
    private bool _isOn;

    void Start()
    {
        _light = GetComponent<Light>();
        if (_light == null)
        {
            Debug.LogError("No Light component found on this GameObject.");
            enabled = false;
            return;
        }

        _isOn = true;
        _light.enabled = true;
        SetNextFlickerTime();
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _isOn = !_isOn;
            _light.enabled = _isOn;
            SetNextFlickerTime();
        }
    }

    void SetNextFlickerTime()
    {
        if (_isOn)
        {
            _timer = Random.Range(minOnTime, maxOnTime);
        }
        else
        {
            _timer = Random.Range(minOffTime, maxOffTime);
        }
    }
}
