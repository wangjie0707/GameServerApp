using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mmcoy.Framework.AbstractBase;

/// <summary>
/// 
/// </summary>
[Serializable]
public partial class Role_Equip_GemEntity : MFAbstractEntity
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
    ///角色Id 
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    ///角色装备编号 
    /// </summary>
    public int RoleEquipId { get; set; }

    /// <summary>
    ///孔编号 
    /// </summary>
    public byte HoleNo { get; set; }

    /// <summary>
    ///宝石Id 对应材料表的Id 
    /// </summary>
    public int GemId { get; set; }

    /// <summary>
    ///宝石属性类型 
    /// </summary>
    public byte GemAttr { get; set; }

    /// <summary>
    ///宝石增加属性值 
    /// </summary>
    public byte GemAttrValue { get; set; }

    /// <summary>
    ///创建时间 
    /// </summary>
    public DateTime CreateTime { get; set; }

    #endregion
}
