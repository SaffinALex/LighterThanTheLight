using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Ui_LevelInfo : MonoBehaviour
{
    [Header("Auto Design")]
    [SerializeField] protected GameObject planeContainer;
    [SerializeField] protected float sizeItem = 0.05f;
    [SerializeField] protected float spacingItem = 0.01f;
    [SerializeField] protected float padding = 0.05f;

    [Header("Design Prefabs")]
    public GameObject designBoss;
    public GameObject designHardBoss;

    public GameObject designSpecialEvent;

    public GameObject designRareItem;
    public GameObject designVeryRareItem;

    List<GameObject> allDesigns = new List<GameObject>();
    List<GameObject> allDesignVisible = new List<GameObject>();
    
    void Awake(){
        allDesigns.Add(designBoss);
        allDesigns.Add(designHardBoss);
        allDesigns.Add(designSpecialEvent);
        allDesigns.Add(designRareItem);
        allDesigns.Add(designVeryRareItem);
    }
    void Start(){
        AddCameraConstraint();
    }
    
    void Update(){
        if(GetComponent<LookAtConstraint>().sourceCount == 0 || GetComponent<LookAtConstraint>().GetSource(0).sourceTransform == null){
            AddCameraConstraint();
        }
    }

    void AddCameraConstraint(){
        ConstraintSource cs = new ConstraintSource();
        cs.sourceTransform = Camera.main.transform;
        cs.weight = 1;
        if (GetComponent<LookAtConstraint>().sourceCount > 0) GetComponent<LookAtConstraint>().RemoveSource(0);
        GetComponent<LookAtConstraint>().AddSource(cs);
    }

    public void SetInfo(List<int> specialsEvents){
        float newSizeContainer = ((padding * 2) + (spacingItem * (specialsEvents.Count - 1)) + (sizeItem * specialsEvents.Count)) / 2;
        planeContainer.transform.localScale = new Vector3(newSizeContainer, 1f, planeContainer.transform.localScale.z);
        ClearCurrentVisual();
        for(int i = 0; i < specialsEvents.Count; i++){
            if(specialsEvents[i] < allDesigns.Count && allDesigns[specialsEvents[i]]){
                GameObject visual = Instantiate(allDesigns[specialsEvents[i]], new Vector3(0,0,0), Quaternion.identity, transform);
                visual.transform.localPosition = new Vector3(- newSizeContainer * 5 + ((newSizeContainer * 10) / (specialsEvents.Count + 1)) * (i + 1),0.5f,-0.5f);
                
                visual.transform.localRotation = Quaternion.AngleAxis(90, Vector3.left);
                allDesignVisible.Add(visual);
            }
        }
    }

    void ClearCurrentVisual(){
        for(int i = 0; i < allDesignVisible.Count; i++){
            Destroy(allDesignVisible[i]);
        }
    }
}
