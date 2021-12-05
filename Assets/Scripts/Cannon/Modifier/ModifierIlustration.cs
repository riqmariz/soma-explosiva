using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ModifierIlustration : MonoBehaviour
{
    [SerializeField]
    private float radius = 1f;
    [SerializeField]
    private float rotationAnimationTime = .5f;
    [SerializeField]
    private ModifierManager manager;
    [SerializeField]
    private GameObject modifierIlustrationPrefab;
    [SerializeField]
    private float degreeOffset = 90f;
    [SerializeField]
    private float selectedScaleMult = 1.2f;

    private List<SingleModIlustration> ilustrations;
    private bool rotating = false;
    private int selectedModIndex = 0;
    private Vector3 initialScale = Vector3.one;
    private float nonSelectedAlpha = .6f;

    public enum Rotation 
    {
        Clockwise        = 0,
        counterclockwise = 1
    }

    private void Start()
    {
        InstantiateModifiersIlustration();
        manager.ModifierListUpdated.AddListener((list) => RotateIlustrations(1));
        initialScale = ilustrations[0].transform.localScale;
    }

    private Vector2 DegreesToVector(float degree) 
    {
        return (Vector2)(Quaternion.Euler(0, 0, degree) * Vector2.right);
    }

    private void InstantiateModifiersIlustration() 
    {
        var descriptions = manager.ModifiersDescriptions;
        var directions = GetDirectionsList(descriptions.Count);
        int i = 0;
        ilustrations = new List<SingleModIlustration>();

        foreach (Vector3 direction in directions) 
        {
            var currIlistration = Instantiate(
                modifierIlustrationPrefab,
                transform.position + (direction * radius),
                transform.rotation,
                transform);
            
            var ilustration = currIlistration.GetComponent<SingleModIlustration>();
            ilustrations.Add(ilustration);
            ilustration.SetDescription(descriptions[i]);
            ilustration.DoTransParency(nonSelectedAlpha, rotationAnimationTime);
            i++;
        }


        ilustrations[selectedModIndex].transform.DOScale(initialScale * selectedScaleMult, rotationAnimationTime);
        ilustrations[selectedModIndex].DoTransParency(1f,rotationAnimationTime);
    }

    public void RotateIlustrations(int r) => RotateIlustrations((Rotation)r);

    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    public void RotateIlustrations(Rotation r)
    {
        if (rotating)
            return;

        ilustrations[selectedModIndex].transform.DOScale(initialScale, rotationAnimationTime);
        ilustrations[selectedModIndex].DoTransParency(nonSelectedAlpha, rotationAnimationTime);

        rotating = true;
        Sequence s = DOTween.Sequence();

        if (r == Rotation.Clockwise)
        {
            selectedModIndex -= 1;
            for (int i = 0; i < ilustrations.Count; i++)
            {
                s.Join(ilustrations[i].transform.DOMove(ilustrations[(i + 1) % ilustrations.Count].transform.position, rotationAnimationTime));
            }
        }
        else if (r == Rotation.counterclockwise)
        {
            selectedModIndex += 1;
            for (int i = ilustrations.Count - 1; i >= 0; i--)
            {
                s.Join(ilustrations[i].transform.DOMove(ilustrations[mod(i - 1, ilustrations.Count)].transform.position, rotationAnimationTime));
            }
        }
        else
        {
            Debug.LogWarning("Unknown Rotation");
        }

        selectedModIndex = mod(selectedModIndex, ilustrations.Count);

        ilustrations[selectedModIndex].transform.DOScale(initialScale * selectedScaleMult, rotationAnimationTime);
        ilustrations[selectedModIndex].DoTransParency(1f,rotationAnimationTime);

        s.Play();
        s.OnComplete(() =>
        {
            rotating = false;
        });
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);

        if (manager) 
        {
            foreach(Vector3 dir in GetDirectionsList(manager.ModifiersDescriptions.Count))
                Gizmos.DrawWireSphere(transform.position + (dir * radius),.3f);
        }
    }

    private List<Vector3> GetDirectionsList(int listSize) 
    {
        List<Vector3> positions = new List<Vector3>();
        float stepSize = 360f / listSize;
        float currDegree = 0;

        for (int i = 0; i < listSize; i++, currDegree += stepSize) 
        {
            var posV2 = DegreesToVector(currDegree + degreeOffset);
            positions.Add(new Vector3(posV2.x,posV2.y));
        }
        return positions;
    }
}
