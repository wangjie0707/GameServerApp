//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送战斗失败消息
/// </summary>
public struct GameLevel_FailProto : IProto
{
    public ushort ProtoCode { get { return 12005; } }

    public int GameLevelId; //游戏关卡Id
    public byte Grade; //难度等级

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(GameLevelId);
            ms.WriteByte(Grade);
            return ms.ToArray();
        }
    }

    public static GameLevel_FailProto GetProto(byte[] buffer)
    {
        GameLevel_FailProto proto = new GameLevel_FailProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.GameLevelId = ms.ReadInt();
            proto.Grade = (byte)ms.ReadByte();
        }
        return proto;
    }
}