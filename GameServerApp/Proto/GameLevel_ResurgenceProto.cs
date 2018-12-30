//===================================================
//创建时间：2018-12-24 22:11:19
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送复活消息
/// </summary>
public struct GameLevel_ResurgenceProto : IProto
{
    public ushort ProtoCode { get { return 12007; } }

    public int GameLevelId; //游戏关卡Id
    public byte Grade; //难度等级
    public byte Type; //复活类型

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(GameLevelId);
            ms.WriteByte(Grade);
            ms.WriteByte(Type);
            return ms.ToArray();
        }
    }

    public static GameLevel_ResurgenceProto GetProto(byte[] buffer)
    {
        GameLevel_ResurgenceProto proto = new GameLevel_ResurgenceProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.GameLevelId = ms.ReadInt();
            proto.Grade = (byte)ms.ReadByte();
            proto.Type = (byte)ms.ReadByte();
        }
        return proto;
    }
}