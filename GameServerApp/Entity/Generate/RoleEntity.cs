using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mmcoy.Framework.AbstractBase;

/// <summary>
/// 
/// </summary>
[Serializable]
public partial class RoleEntity : MFAbstractEntity
{
    #region 重写基类属性
    /// <summary>
    /// 主键
    /// </summary>
    public override int? PKValue
    {
        get
        {
            return this.Id;
        }
        set
        {
            this.Id = value;
        }
    }
    #endregion

    #region 实体属性

    /// <summary>
    /// 编号
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public EnumEntityStatus Status { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int JobId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public byte Sex { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int TotalRechargeMoney { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Money { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Gold { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Exp { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int MaxHP { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int MaxMP { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int CurrHP { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int CurrMP { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Attack { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Defense { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Hit { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Dodge { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Cri { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Res { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Fighting { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int LastPassGameLevelId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int LastInWorldMapId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string LastInWorldMapPos { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Equip_Weapon { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Equip_Pants { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Equip_Clothes { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Equip_Belt { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Equip_Cuff { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Equip_Necklace { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Equip_Shoe { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Equip_Ring { get; set; }

    #endregion
}
