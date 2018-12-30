//===================================================
//开 发 者：WY 
//创建时间：2018-12-24 22:44:31
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端查询角色信息
/// </summary>
public struct RoleOperation_SelectRoleInfoProto : IProto
{
    public ushort ProtoCode { get { return 10009; } }
    public string ProtoEnName { get { return "RoleOperation_SelectRoleInfo"; } }

    public int RoldId; //角色编号

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoldId);
            return ms.ToArray();
        }
    }

    public static RoleOperation_SelectRoleInfoProto GetProto(byte[] buffer)
    {
        RoleOperation_SelectRoleInfoProto proto = new RoleOperation_SelectRoleInfoProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.RoldId = ms.ReadInt();
        }
        return proto;
    }
}