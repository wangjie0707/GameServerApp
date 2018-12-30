//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回战斗失败消息
/// </summary>
public struct GameLevel_FailReturnProto : IProto
{
    public ushort ProtoCode { get { return 12006; } }

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

    public static GameLevel_FailReturnProto GetProto(byte[] buffer)
    {
        GameLevel_FailReturnProto proto = new GameLevel_FailReturnProto();
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