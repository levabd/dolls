﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class MoveTools
{
    [Header("Катетер в руке")]
    public Vector3 CatcheterInConductor;
    [Header("Катетер")]
    public Vector3 Catheter;
    [Header("CatheterVenflon Rotation")]
    public Vector3 CatheterVenflon;
    [Header("Проводник")]
    public Vector3 Conductor;
    [Header("Игла")]
    public Vector3 Needle;
    [Header("Игла(без шприца)")]
    public Vector3 NeedleElone;
    [Header("Шприц")]
    public Vector3 Syringe;
    [Header("Шприц для анестезии")]
    public Vector3 Syringe_anestesia;
    [Header("Стерильні серветки")]
    public Vector3 SterileTissue;
    [Header("BIG")]
    public Vector3 BIG;
    [Header("Игла с Троакаром после выстрела BIG")]
    public Vector3 NeedleBIG;
}
[System.Serializable]
public class AnimationTool
{
    [Header("Анестезия")]
    public Vector3 AnastesiaV2;    
    [Header("Удаление катетера")]
    public Vector3 CatcheterOut;
    [Header("Углубление катетера по проводнику вращательными движениями")]
    public Vector3 CatcheterRotateToConductor;
    [Header("Углубление катетера по проводнику прямым движением")]
    public Vector3 CatcheterToConductor;
    [Header("Подключение катетера к системе переливания жидкостей")]
    public Vector3 CatheterTransfunsion;
    [Header("ClampVeins Rotation")]
    public Vector3 ClampVeins;
    [Header("Вставка проводника")]
    public Vector3 ConductorInANeedle;
    [Header("ConductorOut Rotation")]
    public Vector3 ConductorOut;
    [Header("Удаление проводника")]
    public Vector3 Desinfection;
    [Header("GauzeBallsEnclose Rotation")]
    public Vector3 GauzeBallsEnclose;
    [Header("GauzeBallsRubUpDown Rotation")]
    public Vector3 GauzeBallsRubUpDown;
    [Header("Наклеивание пластыря")]
    public Vector3 HandWithPatch;
    [Header("NapkinPut Rotation")]
    public Vector3 NapkinPut;
    [Header("Удаление иглы")]
    public Vector3 NeedleOut;
    [Header("Анимация пальпации")]
    public Vector3 Paplation;
    [Header("ReleaseTheVein Rotation")]
    public Vector3 ReleaseTheVein;
    [Header("Shave Rotation")]
    public Vector3 Shave;
    [Header("StretchTheSkin Rotation")]
    public Vector3 StretchTheSkin;
    [Header("StretchTheSkinLeft Rotation")]
    public Vector3 StretchTheSkinLeft;
    [Header("Венфлон соеденить с сист. переливания жидкостей")]
    public Vector3 VenflonTransfusion;
    [Header("Венфлон Вытащить мадрен")]
    public Vector3 VenflonGetMandren;
    [Header("Венфлон ввести раствор")]
    public Vector3 NaclToVenflon;
    [Header("BIG активировать (стрелять)")]
    public Vector3 BIGFire;
    [Header("BIG снять предохранитель")]
    public Vector3 BIGRemoveTheFuse;
    [Header("Извлечь Троакар")]
    public Vector3 BIGNeedleOut;
    [Header("Наклеить пластырь на иглу от BIG")]
    public Vector3 PatchToBIGNeedle;
    [Header("Вывести физраствор в иглу big")]
    public Vector3 SyringeToBIGNeedle;
    [Header("Ввести канюлю в руку")]
    public Vector3 CannuleEnter;
    [Header("Удалить мандрен из канюли")]
    public Vector3 CannuleRemoveMandren;
    [Header("Потянуть канюлю на себя")]
    public Vector3 CannulePull;
    [Header("Ввести канюлю в артерию")]
    public Vector3 CannulePush;
    [Header("Промыть канюлю")]
    public Vector3 CannuleRinse;
    [Header("Пришить канюлю")]
    public Vector3 CannuleStitch;
    [Header("Соединить с датчиком")]
    public Vector3 InvasiveSensorConnect;
    [Header("Вывести монитор")]
    public Vector3 Monitor;
    [Header("Сделать надрез используя скальпель")]
    public Vector3 Cut;
    [Header("Вставить дренаж в надрез")]
    public Vector3 DrenajToIncision;
    [Header("Вытянуть метал. стилет и углубить дренаж")]
    public Vector3 DrenajOutConductor;
    [Header("анимация сшивания")]
    public Vector3 Stitch;
}

public class PrefabTransformController : MonoBehaviour
{
    [Header("Угол наклона префабов без анимации")]
    public MoveTools moveTools;
    [Header("Угол наклона префабов с анимацией")]
    public AnimationTool animationTool;

}
