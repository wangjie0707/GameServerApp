//===================================================
//开 发 者：WY 
//创建时间：2018-12-30 22:27:26
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送查询装备详情消息
/// </summary>
public struct Goods_SearchEquipDetailProto : IProto
{
    public ushort ProtoCode { get { return 16006; } }
    public string ProtoEnName { get { return "Goods_SearchEquipDetail"; } }

    public int GoodsServerId; //物品服务器端编号

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(GoodsServerId);
            return ms.ToArray();
        }
    }

    public static Goods_SearchEquipDetailProto GetProto(byte[] buffer)
    {
        Goods_SearchEquipDetailProto proto = new Goods_SearchEquipDetailProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.GoodsServerId = ms.ReadInt();
        }
        return proto;
    }
}