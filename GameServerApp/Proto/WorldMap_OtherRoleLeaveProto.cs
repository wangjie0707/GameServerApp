//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器广播其他角色离开场景消息
/// </summary>
public struct WorldMap_OtherRoleLeaveProto : IProto
{
    public ushort ProtoCode { get { return 13006; } }

    public int RoleId; //角色编号

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleId);
            return ms.ToArray();
        }
    }

    public static WorldMap_OtherRoleLeaveProto GetProto(byte[] buffer)
    {
        WorldMap_OtherRoleLeaveProto proto = new WorldMap_OtherRoleLeaveProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.RoleId = ms.ReadInt();
        }
        return proto;
    }
}