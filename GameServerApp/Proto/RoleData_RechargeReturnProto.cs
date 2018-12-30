//===================================================
//开 发 者：WY 
//创建时间：2018-12-26 20:40:06
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回角色充值信息
/// </summary>
public struct RoleData_RechargeReturnProto : IProto
{
    public ushort ProtoCode { get { return 11002; } }
    public string ProtoEnName { get { return "RoleData_RechargeReturn"; } }

    public bool IsSuccess; //是否成功
    public int RechargeProductId; //充值产品Id
    public byte RechargeProductType; //充值产品类型
    public int Money; //充值后元宝
    public int RemainDay; //剩余天数
    public int ErrorCode; //错误码

    public byte[] ToArray()
    {
        using (mmo_memotyStream ms = new mmo_memotyStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(IsSuccess)
            {
                ms.WriteInt(RechargeProductId);
                ms.WriteByte(RechargeProductType);
                ms.WriteInt(Money);
                ms.WriteInt(RemainDay);
            }
            else
            {
                ms.WriteInt(ErrorCode);
            }
            return ms.ToArray();
        }
    }

    public static RoleData_RechargeReturnProto GetProto(byte[] buffer)
    {
        RoleData_RechargeReturnProto proto = new RoleData_RechargeReturnProto();
        using (mmo_memotyStream ms = new mmo_memotyStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(proto.IsSuccess)
            {
                proto.RechargeProductId = ms.ReadInt();
                proto.RechargeProductType = (byte)ms.ReadByte();
                proto.Money = ms.ReadInt();
                proto.RemainDay = ms.ReadInt();
            }
            else
            {
                proto.ErrorCode = ms.ReadInt();
            }
        }
        return proto;
    }
}