//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回进入世界地图场景消息
/// </summary>
public struct WorldMap_RoleEnterReturnProto : IProto
{
    public ushort ProtoCode { get { return 13002; } }

    public bool IsSuccess; //是否成功
    public short MessageId; //错误编号

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(!IsSuccess)
            {
                ms.WriteShort(MessageId);
            }
            return ms.ToArray();
        }
    }

    public static WorldMap_RoleEnterReturnProto GetProto(byte[] buffer)
    {
        WorldMap_RoleEnterReturnProto proto = new WorldMap_RoleEnterReturnProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
                proto.MessageId = ms.ReadShort();
            }
        }
        return proto;
    }
}