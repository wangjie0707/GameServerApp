//===================================================
//开 发 者：WY 
//创建时间：2018-12-24 22:44:31
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送进入游戏消息
/// </summary>
public struct RoleOperation_EnterGameProto : IProto
{
    public ushort ProtoCode { get { return 10007; } }
    public string ProtoEnName { get { return "RoleOperation_EnterGame"; } }

    public int RoleId; //角色编号
    public int ChannelId; //渠道号

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleId);
            ms.WriteInt(ChannelId);
            return ms.ToArray();
        }
    }

    public static RoleOperation_EnterGameProto GetProto(byte[] buffer)
    {
        RoleOperation_EnterGameProto proto = new RoleOperation_EnterGameProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.RoleId = ms.ReadInt();
            proto.ChannelId = ms.ReadInt();
        }
        return proto;
    }
}