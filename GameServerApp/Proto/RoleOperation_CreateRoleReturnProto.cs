﻿//===================================================
//开 发 者：WY 
//创建时间：2018-12-24 22:44:31
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回创建角色消息
/// </summary>
public struct RoleOperation_CreateRoleReturnProto : IProto
{
    public ushort ProtoCode { get { return 10004; } }
    public string ProtoEnName { get { return "RoleOperation_CreateRoleReturn"; } }

    public bool IsSuccess; //是否成功
    public int MsgCode; //消息码

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(!IsSuccess)
            {
                ms.WriteInt(MsgCode);
            }
            return ms.ToArray();
        }
    }

    public static RoleOperation_CreateRoleReturnProto GetProto(byte[] buffer)
    {
        RoleOperation_CreateRoleReturnProto proto = new RoleOperation_CreateRoleReturnProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
                proto.MsgCode = ms.ReadInt();
            }
        }
        return proto;
    }
}