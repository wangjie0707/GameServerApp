﻿//===================================================
//开 发 者：WY 
//创建时间：2018-12-30 22:27:49
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回出售物品给系统消息
/// </summary>
public struct Goods_SellToSysReturnProto : IProto
{
    public ushort ProtoCode { get { return 16009; } }
    public string ProtoEnName { get { return "Goods_SellToSysReturn"; } }

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
            }
            ms.WriteInt(MsgCode);
            return ms.ToArray();
        }
    }

    public static Goods_SellToSysReturnProto GetProto(byte[] buffer)
    {
        Goods_SellToSysReturnProto proto = new Goods_SellToSysReturnProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
            }
            proto.MsgCode = ms.ReadInt();
        }
        return proto;
    }
}