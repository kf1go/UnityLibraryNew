using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RuntimeRigSetup : MonoBehaviour
{
    public enum EPredefiendHandle
    {
        Custom,
        RHandle,
        LHandle
    }

    [SerializeField] private RigSetup[] setup;
    private MultiParentConstraint rigConstraint;

    private void Awake()
    {
        rigConstraint = GetComponent<MultiParentConstraint>();
        Debug.Assert(rigConstraint != null, this);
    }

    /*public void Setup(ViewmodelAnimator rigger)
    {
        ref MultiParentConstraintData multiParentConstraintData = ref rigConstraint.data;
        WeightedTransformArray weightedTransformArray = multiParentConstraintData.sourceObjects;
        for (int i = 0; i < setup.Length; i++)
        {
            RigSetup item = setup[i];
            EPredefiendHandle handleEnum = item.rigSetup;

            Transform rigTransform;

            if (handleEnum == EPredefiendHandle.Custom)
            {
                rigTransform = item.transform;
                Debug.Assert(rigTransform != null, this);
            }
            else
            {
                rigTransform = rigger.GetHandleTrasnform(handleEnum);
            }
            weightedTransformArray.SetTransform(i, rigTransform);
        }
        multiParentConstraintData.sourceObjects = weightedTransformArray;
    }*/
}
