//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送进入游戏关卡消息
/// </summary>
public struct GameLevel_EnterProto : IProto
{
    public ushort ProtoCode { get { return 12001; } }

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

    public static GameLevel_EnterProto GetProto(byte[] buffer)
    {
        GameLevel_EnterProto proto = new GameLevel_EnterProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.GameLevelId = ms.ReadInt();
            proto.Grade = (byte)ms.ReadByte();
        }
        return proto;
    }
}