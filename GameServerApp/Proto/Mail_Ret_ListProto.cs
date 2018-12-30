//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 从服务器返回道具列表
/// </summary>
public struct Mail_Ret_ListProto : IProto
{
    public ushort ProtoCode { get { return 2001; } }

    public int ItemCount; //道具数量
    public List<ProductItem> ItemList; //道具集合

    /// <summary>
    /// 道具集合
    /// </summary>
    public struct ProductItem
    {
        public int ItemId; //道具编号
        public string ItemName; //道具名称
    }

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(ItemCount);
            for (int i = 0; i < ItemCount; i++)
            {
                ms.WriteInt(ItemList[i].ItemId);
                ms.WriteUTF8String(ItemList[i].ItemName);
            }
            return ms.ToArray();
        }
    }

    public static Mail_Ret_ListProto GetProto(byte[] buffer)
    {
        Mail_Ret_ListProto proto = new Mail_Ret_ListProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.ItemCount = ms.ReadInt();
            proto.ItemList = new List<ProductItem>();
            for (int i = 0; i < proto.ItemCount; i++)
            {
                ProductItem _Item = new ProductItem();
                _Item.ItemId = ms.ReadInt();
                _Item.ItemName = ms.ReadUTF8String();
                proto.ItemList.Add(_Item);
            }
        }
        return proto;
    }
}