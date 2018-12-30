//===================================================
//开 发 者：WY 
//创建时间：2018-12-30 22:27:17
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送查询背包项消息
/// </summary>
public struct Backpack_SearchProto : IProto
{
    public ushort ProtoCode { get { return 16004; } }
    public string ProtoEnName { get { return "Backpack_Search"; } }


    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            return ms.ToArray();
        }
    }

    public static Backpack_SearchProto GetProto(byte[] buffer)
    {
        Backpack_SearchProto proto = new Backpack_SearchProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
        }
        return proto;
    }
}