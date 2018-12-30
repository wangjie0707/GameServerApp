//===================================================
//开 发 者：WY 
//创建时间：2018-12-24 22:44:31
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送登录区服消息
/// </summary>
public struct RoleOperation_LogOnGameServerProto : IProto
{
    public ushort ProtoCode { get { return 10001; } }
    public string ProtoEnName { get { return "RoleOperation_LogOnGameServer"; } }

    public int AccountId; //账户ID

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(AccountId);
            return ms.ToArray();
        }
    }

    public static RoleOperation_LogOnGameServerProto GetProto(byte[] buffer)
    {
        RoleOperation_LogOnGameServerProto proto = new RoleOperation_LogOnGameServerProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.AccountId = ms.ReadInt();
        }
        return proto;
    }
}