//===================================================
//开 发 者：WY 
//创建时间：2018-12-24 22:44:31
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送创建角色消息
/// </summary>
public struct RoleOperation_CreateRoleProto : IProto
{
    public ushort ProtoCode { get { return 10003; } }
    public string ProtoEnName { get { return "RoleOperation_CreateRole"; } }

    public byte JobId; //职业ID
    public string RoleNickName; //角色名称

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteByte(JobId);
            ms.WriteUTF8String(RoleNickName);
            return ms.ToArray();
        }
    }

    public static RoleOperation_CreateRoleProto GetProto(byte[] buffer)
    {
        RoleOperation_CreateRoleProto proto = new RoleOperation_CreateRoleProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.JobId = (byte)ms.ReadByte();
            proto.RoleNickName = ms.ReadUTF8String();
        }
        return proto;
    }
}