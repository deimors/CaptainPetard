using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

public class ScorePresenter : MonoBehaviour
{
    [Inject]
    public IGameEvents GameEvents { private get; set; }
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.OfType<GameEvent, GameEvent.ScoreChanged>()
            .Subscribe(UpdateScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScore(GameEvent.ScoreChanged @event)
    {
        var textObject = this.gameObject.GetComponent<Text>();
        textObject.text = @event.Score.ToString();
    }
    
}
