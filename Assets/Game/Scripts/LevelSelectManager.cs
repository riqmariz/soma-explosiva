using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject selectGameObject;

    [SerializeField] 
    private Button playButton;

    [SerializeField] 
    private float selectDurationAnim=1f;

    [SerializeField] 
    private Ease easeAnim = Ease.Linear;

    [SerializeField] 
    private RotateMode rotateMode = RotateMode.FastBeyond360;

    [SerializeField] 
    private Material availableMaterial;
    
    public Material AvailableMaterial => availableMaterial;

    [SerializeField]
    private Material notAvailableMaterial;

    public Material NotAvailableMaterial => notAvailableMaterial;

    private PlayerData _playerData;
    private SaveSystemManager _saveSystemManager;
    private List<LevelNode> _levelNodes;
    
    private Sequence _sequence;
    private void Awake()
    {
        _saveSystemManager = SaveSystemManager.GetInstance();
        _playerData = _saveSystemManager.GetPlayerData();
        _levelNodes = FindObjectsOfType<LevelNode>().OrderBy(x => x.Index).ToList();
        for (int i = 0; i < _levelNodes.Count; i++)
        {
            _levelNodes[i].SetLevelSelectManager(this);
            _levelNodes[i].Available = i <= _playerData.lastLevelAvailable;
        }
        Selected(_levelNodes[_playerData.lastLevelAvailable]);
    }
    public void Selected(LevelNode levelNode)
    {
        if (_sequence == null)
        {
            _sequence = DOTween.Sequence();
        }

        GameManager.GetInstance().currentLevelSceneNameIndex = levelNode.Index;
        var position =_levelNodes[levelNode.Index].transform.position;
        var rotation = selectGameObject.transform.rotation.eulerAngles;
        _sequence.Append(
            this.selectGameObject.transform
                .DOMove(new Vector3(position.x,position.y,selectGameObject.transform.position.z), selectDurationAnim)
                .SetEase(easeAnim)
        );
        _sequence.Join(
            selectGameObject.transform.DOLocalRotate(new Vector3(rotation.x+90,rotation.y,rotation.z), selectDurationAnim,rotateMode)
                .SetEase(easeAnim)
        );
        _sequence.OnComplete(() => _sequence = null);
        _sequence.Play();

        playButton.interactable = levelNode.Index <= _playerData.lastLevelAvailable;
    }

    public void Play()
    {
        var index = GameManager.GetInstance().currentLevelSceneNameIndex;
        SceneLoader.LoadUsingLoadingScene(_levelNodes[index].SceneLevelName);
    }
}