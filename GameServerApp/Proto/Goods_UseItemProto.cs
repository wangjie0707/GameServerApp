//===================================================
//开 发 者：WY 
//创建时间：2018-12-30 22:27:53
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送使用道具消息
/// </summary>
public struct Goods_UseItemProto : IProto
{
    public ushort ProtoCode { get { return 16010; } }
    public string ProtoEnName { get { return "Goods_UseItem"; } }

    public int BackpackItemId; //背包项编号
    public int GoodsId; //物品编号

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(BackpackItemId);
            ms.WriteInt(GoodsId);
            return ms.ToArray();
        }
    }

    public static Goods_UseItemProto GetProto(byte[] buffer)
    {
        Goods_UseItemProto proto = new Goods_UseItemProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.BackpackItemId = ms.ReadInt();
            proto.GoodsId = ms.ReadInt();
        }
        return proto;
    }
}