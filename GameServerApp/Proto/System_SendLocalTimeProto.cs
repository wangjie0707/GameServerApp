﻿//===================================================
//开 发 者：WY 
//创建时间：2018-12-24 22:44:31
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送本地时间
/// </summary>
public struct System_SendLocalTimeProto : IProto
{
    public ushort ProtoCode { get { return 14001; } }
    public string ProtoEnName { get { return "System_SendLocalTime"; } }

    public float LocalTime; //本地时间(毫秒)

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteFloat(LocalTime);
            return ms.ToArray();
        }
    }

    public static System_SendLocalTimeProto GetProto(byte[] buffer)
    {
        System_SendLocalTimeProto proto = new System_SendLocalTimeProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.LocalTime = ms.ReadFloat();
        }
        return proto;
    }
}