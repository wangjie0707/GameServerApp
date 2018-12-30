//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器广播角色复活消息
/// </summary>
public struct WorldMap_OtherRoleResurgenceProto : IProto
{
    public ushort ProtoCode { get { return 13016; } }

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

    public static WorldMap_OtherRoleResurgenceProto GetProto(byte[] buffer)
    {
        WorldMap_OtherRoleResurgenceProto proto = new WorldMap_OtherRoleResurgenceProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.RoleId = ms.ReadInt();
        }
        return proto;
    }
}