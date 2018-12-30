using GameServerApp.Common;
using GameServerApp.Entity;
using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameLevel_VictoryProto;

namespace GameServerApp.Controller
{
    class RoleController : Singleton<RoleController>, IDisposable
    {

        public void Init()
        {
            //添加相关协议的监听

            #region 系统相关
            //客户端发送本地时间消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.System_SendLocalTime, OnSendLocalTime);
            #endregion

            #region 登陆相关
            //客户端发送登陆区服的消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_LogOnGameServer, OnLogOnGameServer);

            //客户端发送创建角色消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_CreateRole, OnCreateRole);

            //客户端发送进入游戏消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_EnterGame, OnEnterGame);

            //客户端发送删除角色消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_DeleteRole, OnDeleteRole);
            #endregion

            #region 游戏关卡相关

            //客户端发送进入游戏关卡消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.GameLevel_Enter, OnGameLevelEnter);

            //客户端发送战斗胜利消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.GameLevel_Victory, OnGameLevelVictory);

            //客户端发送战斗失败消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.GameLevel_Fail, OnGameLevelFail);

            //客户端发送复活消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.GameLevel_Resurgence, OnGameLevelResurgence);
            #endregion

            #region 世界地图相关
            //客户端发送进入世界地图消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.WorldMap_RoleEnter, OnWorldMapEnter);

            //客户端发送世界地图上角色坐标
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.WorldMap_Pos, OnWorldMapPos);
            #endregion

            #region 任务相关
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.Task_SearchTask, OnTaskSearchTask);
            #endregion

            #region 商城背包
            //客户端发送购买商城物品消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.Shop_BuyProduct, OnShopBuyProduct);

            ////客户端发送查询背包项消息
            //EventDispatcher.Instance.AddEventListener(ProtoCodeDef.Backpack_Search, OnBackpackSearch);

            ////客户端发送查询装备详情消息
            //EventDispatcher.Instance.AddEventListener(ProtoCodeDef.Goods_SearchEquipDetail, OnSearchEquipDetail);

            ////客户端发送查询装备详情消息
            //EventDispatcher.Instance.AddEventListener(ProtoCodeDef.Goods_SellToSys, OnSellToSys);

            ////客户端发送使用道具消息
            //EventDispatcher.Instance.AddEventListener(ProtoCodeDef.Goods_UseItem, OnUseItem);

            ////客户端发送穿戴装备消息
            //EventDispatcher.Instance.AddEventListener(ProtoCodeDef.Goods_EquipPut, OnEquipPut);
            #endregion

        }

        public override void Dispose()
        {
            //移除相关协议的监听

            #region 系统相关
            //客户端发送登录区服消息
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.System_SendLocalTime, OnSendLocalTime);
            #endregion

            #region 登陆相关
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_LogOnGameServer, OnLogOnGameServer);
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_CreateRole, OnCreateRole);
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_EnterGame, OnEnterGame);
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_DeleteRole, OnDeleteRole);
            #endregion

            #region 游戏关卡相关

            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.GameLevel_Enter, OnGameLevelEnter);
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.GameLevel_Victory, OnGameLevelVictory);
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.GameLevel_Fail, OnGameLevelFail);
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.GameLevel_Resurgence, OnGameLevelResurgence);
            #endregion

            #region 世界地图相关
            //客户端发送进入世界地图消息
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.WorldMap_RoleEnter, OnWorldMapEnter);

            //客户端发送世界地图上角色坐标
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.WorldMap_Pos, OnWorldMapPos);
            #endregion

            #region 任务相关
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.Task_SearchTask, OnTaskSearchTask);
            #endregion

            #region 商城背包
            //客户端发送购买商城物品消息
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.Shop_BuyProduct, OnShopBuyProduct);

            ////客户端发送查询背包项消息
            //EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.Backpack_Search, OnBackpackSearch);

            ////客户端发送查询装备详情消息
            //EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.Goods_SearchEquipDetail, OnSearchEquipDetail);

            ////客户端发送查询装备详情消息
            //EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.Goods_SellToSys, OnSellToSys);

            ////客户端发送使用道具消息
            //EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.Goods_UseItem, OnUseItem);

            ////客户端发送穿戴装备消息
            //EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.Goods_EquipPut, OnEquipPut);
            #endregion

        }

        //=====================协议监听 调用方法=================================
        #region 系统相关
        /// <summary>
        /// 客户端发送本地时间
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnSendLocalTime(Role role, byte[] buffer)
        {
            System_SendLocalTimeProto proto = System_SendLocalTimeProto.GetProto(buffer);
            float localTime = proto.LocalTime;

            SystemServerTimeReturn(role, localTime);
            System_GameServerConfigReturn(role);
        }

        /// <summary>
        /// 服务器返回服务器时间
        /// </summary>
        /// <param name="role"></param>
        /// <param name="localTime"></param>
        private void SystemServerTimeReturn(Role role, float localTime)
        {
            System_ServerTimeReturnProto proto = new System_ServerTimeReturnProto();
            proto.LocalTime = localTime;
            proto.ServerTime = DateTime.Now.ToUniversalTime().Ticks / 10000;

            Console.WriteLine("给客户端发的时间=" + proto.ServerTime);

            //给客户端发消息
            role._clientSocket.SendMsg(proto.ToArray());
        }

        /// <summary>
        /// 服务器返回开关列表
        /// </summary>
        /// <param name="role"></param>
        private void System_GameServerConfigReturn(Role role)
        {
            System_GameServerConfigReturnProto proto = new System_GameServerConfigReturnProto();

            List<GameServerConfigEntity> lst = GameServerConfigCacheModel.Instance.GetList();
            if (lst != null)
            {
                proto.ConfigCount = lst.Count;
                proto.ServerConfigList = new List<System_GameServerConfigReturnProto.ConfigItem>();
                for (int i = 0; i < lst.Count; i++)
                {
                    proto.ServerConfigList.Add(new System_GameServerConfigReturnProto.ConfigItem()
                    {
                        ConfigCode = lst[i].ConfigCode,
                        IsOpen = lst[i].IsOpen == 1,
                        Param = lst[i].Param
                    });
                    Console.WriteLine(lst[i].ConfigCode + "!" + lst[i].IsOpen);
                }
            }

            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #region 登陆相关
        #region 客户端发送登陆区服消息
        /// <summary>
        /// 客户端发送登陆区服消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnLogOnGameServer(Role role, byte[] buffer)
        {
            RoleOperation_LogOnGameServerProto proto = RoleOperation_LogOnGameServerProto.GetProto(buffer);
            //玩家账号
            int accountId = proto.AccountId;
            role.AccountId = accountId;
            LogOnGameServerReturn(role, accountId);
        }
        #endregion

        #region 客户端发送创建角色消息
        /// <summary>
        /// 客户端发送创建角色消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnCreateRole(Role role, byte[] buffer)
        {
            RoleOperation_CreateRoleProto proto = RoleOperation_CreateRoleProto.GetProto(buffer);
            //把角色信息添加到数据库

            RoleEntity entity = new RoleEntity();
            entity.JobId = proto.JobId;
            entity.NickName = proto.RoleNickName;
            entity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
            entity.AccountId = role.AccountId;
            entity.LastInWorldMapId = 1;
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;
            entity.Level = 1;

            //给角色战斗相关的属性赋值
            //职业 等级 
            JobEntity jobEntity = JobDBModel.Instance.Get(entity.JobId);

            //职业等级数据
            JobLevelEntity jobLevelEntity = JobLevelDBModel.Instance.Get(entity.Level);

            entity.CurrHP = entity.MaxHP = jobLevelEntity.HP;
            entity.CurrMP = entity.MaxMP = jobLevelEntity.MP;

            entity.Attack = (int)Math.Round(jobEntity.Attack * jobLevelEntity.Attack * 0.01f);
            entity.Defense = (int)Math.Round(jobEntity.Defense * jobLevelEntity.Defense * 0.01f);
            entity.Hit = (int)Math.Round(jobEntity.Hit * jobLevelEntity.Hit * 0.01f);
            entity.Dodge = (int)Math.Round(jobEntity.Dodge * jobLevelEntity.Dodge * 0.01f);
            entity.Cri = (int)Math.Round(jobEntity.Cri * jobLevelEntity.Cri * 0.01f);
            entity.Res = (int)Math.Round(jobEntity.Res * jobLevelEntity.Res * 0.01f);
            entity.Fighting = entity.Attack * 4 + entity.Defense * 4 + entity.Hit * 2 + entity.Dodge * 2 + entity.Cri + entity.Res;

            //1、查询昵称是否存在
            int count = RoleCacheModel.Instance.GetCount(string.Format("[NickName]='{0}'", proto.RoleNickName));
            MFReturnValue<object> retValue = null;
            if (count == 0)
            {
                retValue = RoleCacheModel.Instance.Create(entity);
                int roleId = retValue.GetOutputValue<int>("Id");
                role.RoleId = roleId;
                role.NickName = entity.NickName;
                role.Level = entity.Level;
                role.JobId = entity.JobId;
                role.MaxHP = entity.MaxHP;
                role.CurrHP = entity.CurrHP;
                role.MaxMP = entity.MaxHP;
                role.CurrMP = entity.CurrMP;
                role.Attack = entity.Attack;
                role.Defense = entity.Defense;
                role.Hit = entity.Hit;
                role.Dodge = entity.Dodge;
                role.Cri = entity.Cri;
                role.Res = entity.Res;
                role.Fighting = entity.Fighting;
            }
            else
            {
                retValue = new MFReturnValue<object>();
                retValue.HasError = true;
                retValue.ReturnCode = 1000;
            }
            OnCreateRoleReturn(role, retValue);
        }
        #endregion

        #region 客户端发送进入游戏消息
        /// <summary>
        /// 客户端发送进入游戏消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnEnterGame(Role role, byte[] buffer)
        {
            RoleOperation_EnterGameProto proto = RoleOperation_EnterGameProto.GetProto(buffer);
            role.RoleId = proto.RoleId;
            role.ChannelId = proto.ChannelId;
            OnEnterGameReturn(role);
        }

        #endregion

        #region 客户端发送删除角色消息
        /// <summary>
        /// 客户端发送删除角色消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnDeleteRole(Role role, byte[] buffer)
        {
            //删除角色

            RoleOperation_DeleteRoleProto proto = RoleOperation_DeleteRoleProto.GetProto(buffer);
            MFReturnValue<object> retValue = RoleCacheModel.Instance.Delete(proto.RoleId);
            OnDeleteRoleReturn(role, retValue);
        }
        #endregion
        #endregion

        #region 游戏关卡相关

        #region OnGameLevelEnter 客户端发送进入游戏关卡消息
        /// <summary>
        /// 客户端发送进入游戏关卡消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnGameLevelEnter(Role role, byte[] buffer)
        {
            //根据需要 验证体力 等信息
            GameLevel_EnterProto proto = GameLevel_EnterProto.GetProto(buffer);

            //添加攻打关卡记录
            Log_GameLevelEntity m_Log_GameLevel = new Log_GameLevelEntity();
            m_Log_GameLevel.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
            m_Log_GameLevel.RoleId = role.RoleId;
            m_Log_GameLevel.GameLevelId = proto.GameLevelId;
            m_Log_GameLevel.Grade = proto.Grade;
            m_Log_GameLevel.Action = 0;
            m_Log_GameLevel.CreateTime = DateTime.Now;
            Log_GameLevelDBModel.Instance.Create(m_Log_GameLevel);

            ////角色进入游戏关卡后，要离开世界地图
            //WorldMapSceneMgr.Instance.RoleLeave(role.RoleId, role.PrevWorldMapId);

            GameLevel_EnterReturnProto retProto = new GameLevel_EnterReturnProto();
            retProto.IsSuccess = true;
            role._clientSocket.SendMsg(retProto.ToArray());
        }
        #endregion

        #region OnGameLevelVictory 客户端发送战斗胜利消息
        /// <summary>
        /// 客户端发送战斗胜利消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnGameLevelVictory(Role role, byte[] buffer)
        {
            GameLevel_VictoryProto proto = GameLevel_VictoryProto.GetProto(buffer);

            //1.添加或者修改玩家过关详情
            int count = Role_PassGameLevelDetailCacheModel.Instance.GetCount(string.Format("[RoleId]={0} AND [GameLevelId]={1}", role.RoleId, proto.GameLevelId));
            if (count > 0)
            {
                //修改
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["@RoleId"] = role.RoleId;
                parameters["@GameLevelId"] = proto.GameLevelId;
                parameters["@Star"] = proto.Star;

                Role_PassGameLevelDetailCacheModel.Instance.Update("[Star]=@Star", "[RoleId]=@RoleId AND [GameLevelId]=@GameLevelId", parameters);
            }
            else
            {
                //添加
                Role_PassGameLevelDetailEntity entity = new Role_PassGameLevelDetailEntity();
                entity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                entity.RoleId = role.RoleId;
                entity.GameLevelId = proto.GameLevelId;
                entity.Grade = proto.Grade;
                entity.Star = proto.Star;
                entity.IsMopUp = 0;
                entity.CreateTime = DateTime.Now;

                Role_PassGameLevelDetailCacheModel.Instance.Create(entity);
            }

            //2.添加攻打关卡记录
            {
                Log_GameLevelEntity m_Log_GameLevel = new Log_GameLevelEntity();
                m_Log_GameLevel.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                m_Log_GameLevel.RoleId = role.RoleId;
                m_Log_GameLevel.GameLevelId = proto.GameLevelId;
                m_Log_GameLevel.Grade = proto.Grade;
                m_Log_GameLevel.Action = 1;
                m_Log_GameLevel.CreateTime = DateTime.Now;
                Log_GameLevelDBModel.Instance.Create(m_Log_GameLevel);
            }

            //3.添加杀怪记录
            List<MonsterItem> killMonsterList = proto.KillMonsterList;
            for (int i = 0; i < killMonsterList.Count; i++)
            {
                Log_KillMonsterEntity m_Log_KillMonster = new Log_KillMonsterEntity();
                m_Log_KillMonster.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                m_Log_KillMonster.RoleId = role.RoleId;
                m_Log_KillMonster.PlayType = 0;
                m_Log_KillMonster.PlaySceneId = proto.GameLevelId;
                m_Log_KillMonster.Grade = proto.Grade;
                m_Log_KillMonster.SpriteId = killMonsterList[i].MonsterId;
                m_Log_KillMonster.SpriteCount = killMonsterList[i].MonsterCount;
                m_Log_KillMonster.CreateTime = DateTime.Now;

                Log_KillMonsterDBModel.Instance.Create(m_Log_KillMonster);
            }

            int goodsCount = proto.GoodsTotalCount;

            //4.添加获得物品记录
            List<GoodsItem> getGoodsList = proto.GetGoodsList;

            ////添加到背包的物品集合
            //List<Role_BackpackAddItemEntity> lst = new List<Role_BackpackAddItemEntity>();

            for (int i = 0; i < getGoodsList.Count; i++)
            {
                Log_ReceiveGoodsEntity m_Log_ReceiveGoods = new Log_ReceiveGoodsEntity();
                m_Log_ReceiveGoods.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                m_Log_ReceiveGoods.RoleId = role.RoleId;
                m_Log_ReceiveGoods.PlayType = 0;
                m_Log_ReceiveGoods.PlaySceneId = proto.GameLevelId;
                m_Log_ReceiveGoods.Grade = proto.Grade;
                m_Log_ReceiveGoods.GoodsType = getGoodsList[i].GoodsType;
                m_Log_ReceiveGoods.GoodsId = getGoodsList[i].GoodsId;
                m_Log_ReceiveGoods.GoodsCount = getGoodsList[i].GoodsCount;
                m_Log_ReceiveGoods.CreateTime = DateTime.Now;

                Log_ReceiveGoodsDBModel.Instance.Create(m_Log_ReceiveGoods);

                //Role_BackpackAddItemEntity entity = null;

                ////先查找集合中 是否已经有这个物品
                //for (int j = 0; j < lst.Count; j++)
                //{
                //    if (lst[j].GoodsId == getGoodsList[i].GoodsId && lst[j].GoodsType == (GoodsType)getGoodsList[i].GoodsType)
                //    {
                //        entity = lst[j];
                //        break;
                //    }
                //}

                //if (entity != null)
                //{
                //    //如果有这个物品 则把物品的数量增加
                //    entity.GoodsCount += getGoodsList[i].GoodsCount;
                //}
                //else
                //{
                //    //如果没有这个物品 则实例化新对象并赋值
                //    entity = new Role_BackpackAddItemEntity()
                //    {
                //        GoodsType = (GoodsType)getGoodsList[i].GoodsType,
                //        GoodsId = getGoodsList[i].GoodsId,
                //        GoodsCount = getGoodsList[i].GoodsCount
                //    };

                //    //然后加入列表
                //    lst.Add(entity);
                //}
            }

            //List<Role_BackpackItemChangeEntity> changeList = null;
            ////同时把这些物品 加入玩家的背包中
            //Role_BackpackCacheModel.Instance.Add(role.RoleId, GoodsInType.DropOut, lst, ref changeList);

            //5.给玩家添加经验和金币 设置最后通关的游戏关卡Id
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["@Id"] = role.RoleId;
                parameters["@Exp"] = proto.Exp;
                parameters["@Gold"] = proto.Gold;
                parameters["@LastPassGameLevelId"] = proto.GameLevelId;

                RoleCacheModel.Instance.Update("[Exp]=[Exp]+@Exp, [Gold]=[Gold]+@Gold, [LastPassGameLevelId]=@LastPassGameLevelId",
                    "[Id]=@Id", parameters);
            }

            GameLevel_VictoryReturnProto retProto = new GameLevel_VictoryReturnProto();
            retProto.IsSuccess = true;
            role._clientSocket.SendMsg(retProto.ToArray());

            ////给玩家发送物品更新消息
            //OnGoodsChangeReturn(role, changeList);
        }
        #endregion

        #region OnGameLevelFail 客户端发送战斗失败消息
        /// <summary>
        /// 客户端发送战斗失败消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnGameLevelFail(Role role, byte[] buffer)
        {
            //根据需要 验证体力 等信息
            GameLevel_FailProto proto = GameLevel_FailProto.GetProto(buffer);

            //添加攻打关卡记录
            Log_GameLevelEntity m_Log_GameLevel = new Log_GameLevelEntity();
            m_Log_GameLevel.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
            m_Log_GameLevel.RoleId = role.RoleId;
            m_Log_GameLevel.GameLevelId = proto.GameLevelId;
            m_Log_GameLevel.Grade = proto.Grade;
            m_Log_GameLevel.Action = 2;
            m_Log_GameLevel.CreateTime = DateTime.Now;
            Log_GameLevelDBModel.Instance.Create(m_Log_GameLevel);

            GameLevel_FailReturnProto retProto = new GameLevel_FailReturnProto();
            retProto.IsSuccess = true;
            role._clientSocket.SendMsg(retProto.ToArray());
        }
        #endregion

        #region OnGameLevelResurgence 客户端发送复活消息
        /// <summary>
        /// 客户端发送复活消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnGameLevelResurgence(Role role, byte[] buffer)
        {
            GameLevel_ResurgenceReturnProto retProto = new GameLevel_ResurgenceReturnProto();
            retProto.IsSuccess = true;
            role._clientSocket.SendMsg(retProto.ToArray());
        }
        #endregion

        #endregion

        #region 世界地图相关

        #region OnWorldMapEnter 客户端发送进入世界地图消息
        /// <summary>
        /// 客户端发送进入世界地图消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnWorldMapEnter(Role role, byte[] buffer)
        {
            WorldMap_RoleEnterProto proto = WorldMap_RoleEnterProto.GetProto(buffer);
            role.LastInWorldMapId = proto.WorldMapSceneId;

            if (proto.WorldMapSceneId == 2)
            {
                role.CurrHP = role.MaxHP;
                role.CurrMP = role.MaxMP;
            }

            OnWorldMapEnterReturn(role);
        }
        #endregion

        #region OnWorldMapEnterReturn 服务器返回角色进入世界地图
        /// <summary>
        /// 服务器返回角色进入世界地图
        /// </summary>
        /// <param name="role"></param>
        private void OnWorldMapEnterReturn(Role role)
        {
            WorldMap_RoleEnterReturnProto proto = new WorldMap_RoleEnterReturnProto();
            proto.IsSuccess = true;
            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #region OnWorldMapPos 客户端发送世界地图上角色坐标
        /// <summary>
        /// 客户端发送世界地图上角色坐标
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnWorldMapPos(Role role, byte[] buffer)
        {
            WorldMap_PosProto proto = WorldMap_PosProto.GetProto(buffer);
            role.LastInWorldMapPos = string.Format("{0}_{1}_{2}_{3}", proto.x, proto.y, proto.z, proto.yAngle);
        }
        #endregion

        #endregion

        #region 任务相关

        #region OnTaskSearchTask 任务查询

        private void OnTaskSearchTask(Role role, byte[] buffer)
        {
            //测试环境 直接给客户端发送伪数据
            Task_SearchTaskReturnProto proto = new Task_SearchTaskReturnProto();

            proto.CurrTaskItemList = new List<Task_SearchTaskReturnProto.TaskItem>();

            proto.CurrTaskItemList.Add(new Task_SearchTaskReturnProto.TaskItem() { Id = 1001, Name = "任务1号", Status = 0, Content = "从服务端来，AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" });
            proto.CurrTaskItemList.Add(new Task_SearchTaskReturnProto.TaskItem() { Id = 1002, Name = "任务2号", Status = 1, Content = "从服务端来，BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB" });
            proto.CurrTaskItemList.Add(new Task_SearchTaskReturnProto.TaskItem() { Id = 1003, Name = "任务3号", Status = 2, Content = "从服务端来，CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC" });
            proto.CurrTaskItemList.Add(new Task_SearchTaskReturnProto.TaskItem() { Id = 1004, Name = "任务4号", Status = 2, Content = "从服务端来，DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD" });
            proto.CurrTaskItemList.Add(new Task_SearchTaskReturnProto.TaskItem() { Id = 1005, Name = "任务5号", Status = 1, Content = "从服务端来，EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE" });
            proto.CurrTaskItemList.Add(new Task_SearchTaskReturnProto.TaskItem() { Id = 1006, Name = "任务6号", Status = 0, Content = "从服务端来，FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF" });
            proto.CurrTaskItemList.Add(new Task_SearchTaskReturnProto.TaskItem() { Id = 1007, Name = "任务7号", Status = 1, Content = "从服务端来，GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG" });
            proto.CurrTaskItemList.Add(new Task_SearchTaskReturnProto.TaskItem() { Id = 1008, Name = "任务8号", Status = 0, Content = "从服务端来，HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH" });

            proto.TaskCount = proto.CurrTaskItemList.Count;
            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #endregion

        #region 商城相关
        /// <summary>
        /// 客户端发送购买商城物品消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnShopBuyProduct(Role role, byte[] buffer)
        {
            Shop_BuyProductProto proto = Shop_BuyProductProto.GetProto(buffer);
            OnShopBuyProductReturn(role, proto.ProductId);
        }

        /// <summary>
        /// 服务器返回购买商城物品消息
        /// </summary>
        /// <param name="role"></param>
        private void OnShopBuyProductReturn(Role role, int productId)
        {
            bool hasError = false;
            int msgCode = 102009;
            int oldMoney = role.Money; //原始元宝

            //1.查询玩家要购买的商品是否存在 如果商品不存在 提示 购买失败 您选择的商品不存在
            //这个productId 就是Shop表中的Id 所以这里就是查询的Shop表
            ShopEntity entity = ShopDBModel.Instance.Get(productId);
            if (entity == null)
            {
                hasError = true;
                msgCode = 102007;
            }

            //2.查询玩家拥有的元宝 是否足够支持这个商品 如果不够 提示 购买失败 您的元宝不足 请先充值
            if (!hasError)
            {
                if (entity.Price > role.Money) //如果这个商品价格 超过玩家拥有的元宝
                {
                    hasError = true;
                    msgCode = 102008;
                }
            }

            //3.扣除玩家身上的元宝 并添加购买日志
            if (!hasError)
            {
                //减掉玩家对象上的元宝
                role.Money -= entity.Price;

                //减掉数据库中 玩家的元宝
                Dictionary<string, object> param = new Dictionary<string, object>();
                param["@Id"] = role.RoleId;
                param["@Money"] = role.Money;
                RoleCacheModel.Instance.Update("[Money]= @Money", "Id=@Id", param); //更新数据库

                //添加购买日志
                Log_ShopBuyProductEntity logEntity = new Log_ShopBuyProductEntity();
                logEntity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                logEntity.RoleId = role.RoleId;
                logEntity.ShopId = productId;
                logEntity.GoodsType = (byte)entity.GoodsType;
                logEntity.GoodsId = entity.GoodsId;
                logEntity.GoodsCount = 1;
                logEntity.CreateTime = DateTime.Now;
                logEntity.UpdateTime = DateTime.Now;

                Log_ShopBuyProductCacheModel.Instance.Create(logEntity);
            }

            List<Role_BackpackItemChangeEntity> changeList = null;

            //4.给玩家的背包中添加购买到的物品
            if (!hasError)
            {
                Role_BackpackCacheModel.Instance.Add(role.RoleId, GoodsInType.ShopBuy, new Role_BackpackAddItemEntity()
                {
                    GoodsType = (GoodsType)entity.GoodsType,
                    GoodsId = entity.GoodsId,
                    GoodsCount = 1
                }, ref changeList);
            }

            Shop_BuyProductReturnProto proto = new Shop_BuyProductReturnProto();
            proto.IsSuccess = !hasError;
            proto.MsgCode = msgCode;
            role._clientSocket.SendMsg(proto.ToArray());

            //5.给玩家发送元宝更新消息
            if (!hasError)
            {
                OnMondeyChangeReturn(role, oldMoney, role.Money, ChangeType.Reduce, MoneyAddType.None, MoneyReduceType.BuyShopProduct, (GoodsType)entity.GoodsType, entity.GoodsId);
            }

            //6.给玩家发送物品更新消息
            if (!hasError)
            {
                OnGoodsChangeReturn(role, changeList);
            }
        }
        #endregion

        //=================================引用================================

        #region 根据玩家账号查询玩家存在的角色
        /// <summary>
        /// 根据玩家账号 查询玩家存在的角色
        /// </summary>
        /// <param name="accountId"></param>
        private void LogOnGameServerReturn(Role role, int accountId)
        {
            RoleOperation_LogOnGameServerReturnProto proto = new RoleOperation_LogOnGameServerReturnProto();

            List<RoleEntity> lst = RoleCacheModel.Instance.GetList(condition: string.Format("[AccountId]={0}", accountId));
            if (lst != null && lst.Count > 0)
            {
                proto.RoleCount = lst.Count;
                proto.RoleList = new List<RoleOperation_LogOnGameServerReturnProto.RoleItem>();

                for (int i = 0; i < lst.Count; i++)
                {
                    proto.RoleList.Add(new RoleOperation_LogOnGameServerReturnProto.RoleItem()
                    {
                        RoleId = lst[i].Id.Value,
                        RoleNickName = lst[i].NickName,
                        RoleLevel = lst[i].Level,
                        RoleJob = (byte)lst[i].JobId
                    });
                }
            }
            else
            {
                proto.RoleCount = 0;
            }
            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #region 服务器返回创建角色消息
        /// <summary>
        /// 服务器返回创建角色消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="retValue"></param>
        private void OnCreateRoleReturn(Role role, MFReturnValue<object> retValue)
        {
            RoleOperation_CreateRoleReturnProto proto = new RoleOperation_CreateRoleReturnProto();

            if (!retValue.HasError)
            {
                proto.IsSuccess = true;
            }
            else
            {
                proto.IsSuccess = false;
                proto.MsgCode = 1000;
            }
            role._clientSocket.SendMsg(proto.ToArray());

            if (proto.IsSuccess)
            {
                OnSelectRoleInfoReturn(role);
                OnSkillReturn(role);
                OnRechargeProductReturn(role); //给客户端发送 充值产品信息
            }
        }
        #endregion

        #region 服务器返回角色信息
        /// <summary>
        /// 服务器返回角色信息
        /// </summary>
        /// <param name="role"></param>
        private void OnSelectRoleInfoReturn(Role role)
        {
            RoleOperation_SelectRoleInfoReturnProto proto = new RoleOperation_SelectRoleInfoReturnProto();

            RoleEntity entity = RoleCacheModel.Instance.GetEntity(role.RoleId);

            if (entity != null)
            {
                proto.IsSuccess = true;
                proto.RoldId = entity.Id.Value;
                proto.RoleNickName = entity.NickName;
                proto.Level = entity.Level;
                proto.JobId = (byte)entity.JobId;
                proto.TotalRechargeMoney = entity.TotalRechargeMoney;
                proto.Money = entity.Money;
                proto.Gold = entity.Gold;
                proto.Exp = entity.Exp;
                proto.MaxHP = entity.MaxHP;
                proto.MaxMP = entity.MaxMP;
                proto.CurrHP = entity.CurrHP;
                proto.CurrMP = entity.CurrMP;
                proto.Attack = entity.Attack;
                proto.Defense = entity.Defense;
                proto.Hit = entity.Hit;
                proto.Dodge = entity.Dodge;
                proto.Cri = entity.Cri;
                proto.Res = entity.Res;
                proto.Fighting = entity.Fighting;
                proto.LastInWorldMapId = entity.LastInWorldMapId;
                proto.LastInWorldMapPos = entity.LastInWorldMapPos;

                role.NickName = entity.NickName;
                role.JobId = entity.JobId;
                role.Level = entity.Level;
                role.MaxHP = entity.MaxHP;
                role.CurrHP = entity.CurrHP;
                role.MaxMP = entity.MaxHP;
                role.CurrMP = entity.CurrMP;
                role.Attack = entity.Attack;
                role.Defense = entity.Defense;
                role.Hit = entity.Hit;
                role.Dodge = entity.Dodge;
                role.Cri = entity.Cri;
                role.Res = entity.Res;
                role.Fighting = entity.Fighting;

                role.PrevWorldMapId = entity.LastInWorldMapId;
                role.LastInWorldMapId = entity.LastInWorldMapId;
                role.LastInWorldMapPos = entity.LastInWorldMapPos;
            }
            else
            {
                proto.IsSuccess = false;
                proto.MsgCode = 1000;
            }
            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #region 服务器返回角色学会的技能信息
        /// <summary>
        /// 服务器返回角色学会的技能信息
        /// </summary>
        /// <param name="role"></param>
        private void OnSkillReturn(Role role)
        {
            RoleData_SkillReturnProto proto = new RoleData_SkillReturnProto();

            List<RoleSkillEntity> roleSkillEntities = RoleSkillDBModel.Instance.GetList(condition: "[RoleId]=" + role.RoleId);
            if (roleSkillEntities != null)
            {
                proto.SkillCount = (byte)roleSkillEntities.Count;
                proto.CurrSkillDataList = new List<RoleData_SkillReturnProto.SkillData>();

                for (int i = 0; i < roleSkillEntities.Count; i++)
                {
                    proto.CurrSkillDataList.Add(new RoleData_SkillReturnProto.SkillData()
                    {
                        SkillId = roleSkillEntities[i].SkillId,
                        SkillLevel = roleSkillEntities[i].SkillLevel,
                        SlotsNo = roleSkillEntities[i].SlotsNo
                    });
                }
            }

            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #region 充值相关

        /// <summary>
        /// 服务器返回充值产品信息
        /// </summary>
        /// <param name="role"></param>
        void OnRechargeProductReturn(Role role)
        {
            RoleData_RechargeProductReturnProto proto = new RoleData_RechargeProductReturnProto();

            //1.先根据玩家帐号 查询出玩家的购买记录
            List<RechargeRecordEntity> lstRecord = RechargeRecordCacheModel.Instance.GetList(condition: string.Format("[RoleId]={0}", role.RoleId));


            //2.根据渠道号 换算成渠道类型 然后查询出充值产品列表
            byte channelType = 0;
            //role.ChannelId
            List<RechargeProductEntity> lst = RechargeProductCacheModel.Instance.GetList(condition: string.Format("[ChannelType]={0}", channelType));

            proto.RechargeProductCount = lst.Count;
            proto.CurrItemList = new List<RoleData_RechargeProductReturnProto.RechargeProductItem>();
            for (int i = 0; i < lst.Count; i++)
            {
                RoleData_RechargeProductReturnProto.RechargeProductItem item = new RoleData_RechargeProductReturnProto.RechargeProductItem();

                item.RechargeProductId = lst[i].ProductId;
                item.ProductDesc = lst[i].ProductDesc;

                //根据充值产品编号 查询充值记录
                RechargeRecordEntity entity = GetRechargeRecord(lstRecord, lst[i].ProductId);

                switch (lst[i].ProductType)
                {
                    case 1:
                        //月卡 判断是否买过
                        if (entity == null)
                        {
                            item.CanBuy = 1;
                        }
                        else
                        {
                            int remainDay = 30 - (DateTime.Now - entity.UpdateTime).Days; //剩余天数
                            if (remainDay <= 0)
                            {
                                //购买超过30天了 可以购买
                                item.CanBuy = 1;
                            }
                            else
                            {
                                item.CanBuy = 0;
                                item.RemainDay = remainDay - 1;
                            }
                        }
                        break;
                    case 2:
                        //促销礼包 如果没有买过促销礼包 则可以购买
                        if (entity == null)
                        {
                            item.CanBuy = 1;
                        }
                        else
                        {
                            item.CanBuy = 0;
                        }
                        break;
                    case 3:
                        //普通计费点
                        item.CanBuy = 1;
                        if (entity == null)
                        {
                            item.DoubleFlag = 1; //首充双倍标记
                        }
                        else
                        {
                            item.DoubleFlag = 0;
                        }
                        break;
                }

                proto.CurrItemList.Add(item);
            }

            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #region GetRechargeRecord 根据充值产品编号获取充值产品记录信息
        /// <summary>
        /// 根据充值产品编号获取充值产品记录信息
        /// </summary>
        /// <param name="lstRecord"></param>
        /// <param name="rechargeProductId"></param>
        /// <returns></returns>
        private RechargeRecordEntity GetRechargeRecord(List<RechargeRecordEntity> lstRecord, int rechargeProductId)
        {
            if (lstRecord == null || lstRecord.Count == 0) return null;
            for (int i = 0; i < lstRecord.Count; i++)
            {
                if (lstRecord[i].ProductId == rechargeProductId)
                {
                    return lstRecord[i];
                }
            }
            return null;
        }
        #endregion

        #region 服务器返回进入游戏消息
        /// <summary>
        /// 服务器返回进入游戏消息
        /// </summary>
        /// <param name="role"></param>
        private void OnEnterGameReturn(Role role)
        {
            RoleOperation_EnterGameReturnProto proto = new RoleOperation_EnterGameReturnProto();
            proto.IsSuccess = true;
            role._clientSocket.SendMsg(proto.ToArray());

            OnSelectRoleInfoReturn(role);

            OnSkillReturn(role);

            OnRechargeProductReturn(role); //给客户端发送 充值产品信息
        }
        #endregion

        #region 服务器返回删除角色消息
        /// <summary>
        /// 服务器返回删除角色消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="retValue"></param>
        private void OnDeleteRoleReturn(Role role, MFReturnValue<object> retValue)
        {
            RoleOperation_DeleteRoleReturnProto proto = new RoleOperation_DeleteRoleReturnProto();
            if (!retValue.HasError)
            {
                proto.IsSuccess = true;
            }
            else
            {
                proto.IsSuccess = false;
                proto.MsgCode = 1000;
            }
            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #region 各种更新消息
        /// <summary>
        /// 发送元宝更新消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="oldMoney">更新前元宝</param>
        /// <param name="currMoney">更新后当前元宝</param>
        /// <param name="changeType">更新方式 0=增加 1=减少</param>
        /// <param name="addType">增加方式 1=充值 2=使用元宝票 3=系统奖励 4=GM奖励或补偿</param>
        /// <param name="reduceType">减少方式 1=购买商城物品 2=兑换成元宝票 3=原地复活</param>
        /// <param name="goodsType">物品类型 0=装备 1=道具 2=材料</param>
        /// <param name="goodsId">物品编号</param>
        private void OnMondeyChangeReturn(Role role, int oldMoney, int currMoney, ChangeType changeType, MoneyAddType addType, MoneyReduceType reduceType, GoodsType goodsType, int goodsId)
        {
            RoleData_MondeyChangeReturnProto proto = new RoleData_MondeyChangeReturnProto();
            proto.OldMoney = oldMoney;
            proto.CurrMoney = currMoney;
            proto.ChangeType = (byte)changeType;
            proto.AddType = (byte)addType;
            proto.ReduceType = (byte)reduceType;
            proto.GoodsType = (byte)goodsType;
            proto.GoodsId = goodsId;

            role._clientSocket.SendMsg(proto.ToArray());
        }

        /// <summary>
        /// 服务器返回金币更新消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="oldGold"></param>
        /// <param name="currGold"></param>
        /// <param name="changeType"></param>
        /// <param name="goldAddType"></param>
        /// <param name="goldReduceType"></param>
        /// <param name="goodsType"></param>
        /// <param name="goodsId"></param>
        private void OnGoldChangeReturn(Role role, int oldGold, int currGold, ChangeType changeType,
            GoldAddType goldAddType, GoldReduceType goldReduceType, GoodsType goodsType, int goodsId)
        {
            RoleData_GoldChangeReturnProto proto = new RoleData_GoldChangeReturnProto();
            proto.OldGold = oldGold;
            proto.CurrGold = currGold;
            proto.ChangeType = (byte)changeType;
            proto.AddType = (byte)goldAddType;
            proto.ReduceType = (byte)goldReduceType;
            proto.GoodsType = (byte)goodsType;
            proto.GoodsId = goodsId;

            role._clientSocket.SendMsg(proto.ToArray());
        }

        /// <summary>
        /// 服务器发送背包项更新消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="lst"></param>
        private void OnGoodsChangeReturn(Role role, List<Role_BackpackItemChangeEntity> lst)
        {
            //把更新消息 发给客户端
            Backpack_GoodsChangeReturnProto proto = new Backpack_GoodsChangeReturnProto();

            proto.BackpackItemChangeCount = lst.Count;
            proto.ItemList = new List<Backpack_GoodsChangeReturnProto.ChangeItem>();

            //Console.WriteLine("服务器发送背包项更新消息==>>" + proto.BackpackItemChangeCount.ToString());

            for (int i = 0; i < lst.Count; i++)
            {
                Role_BackpackItemChangeEntity entity = lst[i];

                proto.ItemList.Add(new Backpack_GoodsChangeReturnProto.ChangeItem()
                {
                    BackpackId = entity.BackpackId,
                    ChangeType = (byte)entity.Type,
                    GoodsType = (byte)entity.GoodsType,
                    GoodsId = entity.GoodsId,
                    GoodsCount = entity.GoodsCount,
                    GoodsServerId = entity.GoodsServerId
                });
            }

            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion
    }
}

