using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Mmcoy.Framework.AbstractBase;

/// <summary>
/// DBModel
/// </summary>
public partial class Role_Equip_GemDBModel : MFAbstractSQLDBModel<Role_Equip_GemEntity>
{
    #region Role_Equip_GemDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private Role_Equip_GemDBModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static Role_Equip_GemDBModel instance = null;
    public static Role_Equip_GemDBModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new Role_Equip_GemDBModel();
                    }
                }
            }
            return instance;
        }
    }
    #endregion

    #region 实现基类的属性和方法

    #region ConnectionString 数据库连接字符串
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    protected override string ConnectionString
    {
        get { return DBConn.DBGameServer; }
    }
    #endregion

    #region TableName 表名
    /// <summary>
    /// 表名
    /// </summary>
    protected override string TableName
    {
        get { return "Role_Equip_Gem"; }
    }
    #endregion

    #region ColumnList 列名集合
    private IList<string> _ColumnList;
    /// <summary>
    /// 列名集合
    /// </summary>
    protected override IList<string> ColumnList
    {
        get
        {
            if (_ColumnList == null)
            {
                _ColumnList = new List<string> { "Id", "Status", "RoleId", "RoleEquipId", "HoleNo", "GemId", "GemAttr", "GemAttrValue", "CreateTime" };
            }
            return _ColumnList;
        }
    }
    #endregion

    #region ValueParas 转换参数
    /// <summary>
    /// 转换参数
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected override SqlParameter[] ValueParas(Role_Equip_GemEntity entity)
    {
        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Id", entity.Id) { DbType = DbType.Int32 },
                new SqlParameter("@Status", entity.Status) { DbType = DbType.Byte },
                new SqlParameter("@RoleId", entity.RoleId) { DbType = DbType.Int32 },
                new SqlParameter("@RoleEquipId", entity.RoleEquipId) { DbType = DbType.Int32 },
                new SqlParameter("@HoleNo", entity.HoleNo) { DbType = DbType.Byte },
                new SqlParameter("@GemId", entity.GemId) { DbType = DbType.Int32 },
                new SqlParameter("@GemAttr", entity.GemAttr) { DbType = DbType.Byte },
                new SqlParameter("@GemAttrValue", entity.GemAttrValue) { DbType = DbType.Byte },
                new SqlParameter("@CreateTime", entity.CreateTime) { DbType = DbType.DateTime },
                new SqlParameter("@RetMsg", SqlDbType.NVarChar, 255),
                new SqlParameter("@ReturnValue", SqlDbType.Int)
            };
        return parameters;
    }
    #endregion

    #region GetEntitySelfProperty 封装对象
    /// <summary>
    /// 封装对象
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    protected override Role_Equip_GemEntity GetEntitySelfProperty(IDataReader reader, DataTable table)
    {
        Role_Equip_GemEntity entity = new Role_Equip_GemEntity();
        foreach (DataRow row in table.Rows)
        {
            var colName = row.Field<string>(0);
            if (reader[colName] is DBNull)
                continue;
            switch (colName.ToLower())
            {
                case "id":
                    if (!(reader["Id"] is DBNull))
                        entity.Id = Convert.ToInt32(reader["Id"]);
                    break;
                case "status":
                    if (!(reader["Status"] is DBNull))
                        entity.Status = (EnumEntityStatus)Convert.ToInt32(reader["Status"]);
                    break;
                case "roleid":
                    if (!(reader["RoleId"] is DBNull))
                        entity.RoleId = Convert.ToInt32(reader["RoleId"]);
                    break;
                case "roleequipid":
                    if (!(reader["RoleEquipId"] is DBNull))
                        entity.RoleEquipId = Convert.ToInt32(reader["RoleEquipId"]);
                    break;
                case "holeno":
                    if (!(reader["HoleNo"] is DBNull))
                        entity.HoleNo = Convert.ToByte(reader["HoleNo"]);
                    break;
                case "gemid":
                    if (!(reader["GemId"] is DBNull))
                        entity.GemId = Convert.ToInt32(reader["GemId"]);
                    break;
                case "gemattr":
                    if (!(reader["GemAttr"] is DBNull))
                        entity.GemAttr = Convert.ToByte(reader["GemAttr"]);
                    break;
                case "gemattrvalue":
                    if (!(reader["GemAttrValue"] is DBNull))
                        entity.GemAttrValue = Convert.ToByte(reader["GemAttrValue"]);
                    break;
                case "createtime":
                    if (!(reader["CreateTime"] is DBNull))
                        entity.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    break;
            }
        }
        return entity;
    }
    #endregion

    #endregion
}
