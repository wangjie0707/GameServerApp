//===================================================
//开 发 者：WY 
//创建时间：2018-12-30 22:26:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回购买商城物品消息
/// </summary>
public struct Shop_BuyProductReturnProto : IProto
{
    public ushort ProtoCode { get { return 16002; } }
    public string ProtoEnName { get { return "Shop_BuyProductReturn"; } }

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

    public static Shop_BuyProductReturnProto GetProto(byte[] buffer)
    {
        Shop_BuyProductReturnProto proto = new Shop_BuyProductReturnProto();
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