//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送查询任务消息
/// </summary>
public struct Task_SearchTaskProto : IProto
{
    public ushort ProtoCode { get { return 15001; } }


    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            return ms.ToArray();
        }
    }

    public static Task_SearchTaskProto GetProto(byte[] buffer)
    {
        Task_SearchTaskProto proto = new Task_SearchTaskProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
        }
        return proto;
    }
}