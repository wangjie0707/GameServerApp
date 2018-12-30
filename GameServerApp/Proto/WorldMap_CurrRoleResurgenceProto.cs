//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送角色复活消息
/// </summary>
public struct WorldMap_CurrRoleResurgenceProto : IProto
{
    public ushort ProtoCode { get { return 13015; } }

    public int Type; //复活类型

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(Type);
            return ms.ToArray();
        }
    }

    public static WorldMap_CurrRoleResurgenceProto GetProto(byte[] buffer)
    {
        WorldMap_CurrRoleResurgenceProto proto = new WorldMap_CurrRoleResurgenceProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.Type = ms.ReadInt();
        }
        return proto;
    }
}