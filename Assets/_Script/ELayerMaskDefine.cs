using System;
using UnityEngine;

[Flags]
public enum ELayerMaskDefine : Int32
{
    Nothing = 0,
    Default = 1 << 0,
    TransparentFX = 1 << 1,
    IgnoreRaycast = 1 << 2,
    TempFlag1 = 1 << 3,
    Water = 1 << 4,
    UI = 1 << 5,
    TempFlag2 = 1 << 6,
    TempFlag3 = 1 << 7,
    TempFlag4 = 1 << 8,
    TempFlag5 = 1 << 9,
    TempFlag6 = 1 << 10,
    TempFlag7 = 1 << 11,
    TempFlag8 = 1 << 12,
    TempFlag9 = 1 << 13,
    TempFlagA = 1 << 14,
    TempFlagB = 1 << 15,
    TempFlagC = 1 << 16,
    TempFlagD = 1 << 17,
    TempFlagE = 1 << 18,
    TempFlagF = 1 << 19,
    TempFlagG = 1 << 20,
    TempFlagH = 1 << 21,
    TempFlagI = 1 << 22,
    TempFlagJ = 1 << 23,
    TempFlagK = 1 << 24,
    TempFlagL = 1 << 25,
    TempFlagM = 1 << 26,
    TempFlagN = 1 << 27,
    TempFlagO = 1 << 28,
    TempFlagP = 1 << 29,
    TempFlagQ = 1 << 30,
    TempFlagR = 1 << 31
}
