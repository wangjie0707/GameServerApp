//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送角色更新信息消息
/// </summary>
public struct WorldMap_CurrRoleUpdateInfoProto : IProto
{
    public ushort ProtoCode { get { return 13013; } }

    public int RoldId; //角色编号
    public string RoleNickName; //角色昵称

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoldId);
            ms.WriteUTF8String(RoleNickName);
            return ms.ToArray();
        }
    }

    public static WorldMap_CurrRoleUpdateInfoProto GetProto(byte[] buffer)
    {
        WorldMap_CurrRoleUpdateInfoProto proto = new WorldMap_CurrRoleUpdateInfoProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.RoldId = ms.ReadInt();
            proto.RoleNickName = ms.ReadUTF8String();
        }
        return proto;
    }
}