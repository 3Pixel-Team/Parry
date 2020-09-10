using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public enum SpikeState 
    {
        Active,
        Inactive,
    }

    [SerializeField]
    private SpikeState state = SpikeState.Inactive;
    [SerializeField]
    private float activeTime;
    [SerializeField]
    private float inactiveTime;
    [SerializeField]
    private float yOffset;
    [SerializeField]
    private float timer = 0.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        switch (state) 
        {
            case SpikeState.Active:
                if (timer >= activeTime) 
                {
                    state = SpikeState.Inactive;
                    gameObject.transform.Translate(new Vector2(0.0f, -yOffset));
                    timer = 0.0f;
                }
            break;
            case SpikeState.Inactive:
                if (timer >= inactiveTime) 
                {
                    state = SpikeState.Active;
                    gameObject.transform.Translate(new Vector2(0.0f, yOffset));
                    timer = 0.0f;
                }
            break;
        }
    }

    // OnTriggerEnter2D damages the entity entering the collider (if possible)
    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (state != SpikeState.Active)
        {
            return;
        }
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null) 
        {
            damageable.TakeDamage(10);
        }
    }
}
