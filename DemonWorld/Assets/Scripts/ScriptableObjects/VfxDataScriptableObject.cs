using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData",menuName = "Data/NewVfxData")]
public class VfxDataScriptableObject : ScriptableObject
{
    public List<VfxData> vfxDataList;
    public VfxData GetVfxData(VfxType vfxType) => vfxDataList.Find(vfxdata => vfxdata.vfxType == vfxType);

}
[System.Serializable]
public struct VfxData
{
    public VfxView vfxPrefab;
    public VfxType vfxType;
}

public enum VfxType
{
    GoblinDamage,GoblinDeath
}
