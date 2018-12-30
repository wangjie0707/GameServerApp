//===================================================
//开 发 者：WY 
//创建时间：2018-12-30 22:25:50
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送购买商城物品消息
/// </summary>
public struct Shop_BuyProductProto : IProto
{
    public ushort ProtoCode { get { return 16001; } }
    public string ProtoEnName { get { return "Shop_BuyProduct"; } }

    public int ProductId; //商品编号

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(ProductId);
            return ms.ToArray();
        }
    }

    public static Shop_BuyProductProto GetProto(byte[] buffer)
    {
        Shop_BuyProductProto proto = new Shop_BuyProductProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.ProductId = ms.ReadInt();
        }
        return proto;
    }
}