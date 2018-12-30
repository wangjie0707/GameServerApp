//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器广播其他角色移动消息
/// </summary>
public struct WorldMap_OtherRoleMoveProto : IProto
{
    public ushort ProtoCode { get { return 13009; } }

    public int RoleId; //角色编号
    public float TargetPosX; //目标坐标X
    public float TargetPosY; //目标坐标Y
    public float TargetPosZ; //目标坐标Z
    public long ServerTime; //客户端发包时的服务器时间
    public int NeedTime; //客户端移动所需时间

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleId);
            ms.WriteFloat(TargetPosX);
            ms.WriteFloat(TargetPosY);
            ms.WriteFloat(TargetPosZ);
            ms.WriteLong(ServerTime);
            ms.WriteInt(NeedTime);
            return ms.ToArray();
        }
    }

    public static WorldMap_OtherRoleMoveProto GetProto(byte[] buffer)
    {
        WorldMap_OtherRoleMoveProto proto = new WorldMap_OtherRoleMoveProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.RoleId = ms.ReadInt();
            proto.TargetPosX = ms.ReadFloat();
            proto.TargetPosY = ms.ReadFloat();
            proto.TargetPosZ = ms.ReadFloat();
            proto.ServerTime = ms.ReadLong();
            proto.NeedTime = ms.ReadInt();
        }
        return proto;
    }
}