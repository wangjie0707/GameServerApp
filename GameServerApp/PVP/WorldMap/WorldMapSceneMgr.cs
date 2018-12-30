using GameServerApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp.PVP.WorldMap
{

    /// <summary>
    /// 世界地图场景管理器
    /// </summary>
    class WorldMapSceneMgr : Singleton<WorldMapSceneMgr>, IDisposable
    {

        private Dictionary<int, WorldMapSceneController> m_WorldMapSceneControllerDic;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {

            InitWorldMapScene();

            //客户端发送角色已经进入世界地图场景的消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.WorldMap_RoleAlreadyEnter, OnWorldMapRoleAlreadyEnter);

            //客户端发送当前角色移动消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.WorldMap_CurrRoleMove, OnWorldMapCurrRoleMove);

            //客户端发送角色使用技能消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.WorldMap_CurrRoleUseSkill, OnWorldMapCurrRoleUseSkill);

            //客户端发送角色复活消息
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.WorldMap_CurrRoleResurgence, OnWorldMapCurrRoleResurgence);

        }

        #region InitWorldMapScene初始化世界地图场景控制器
        /// <summary>
        /// 初始化世界地图场景控制器
        /// </summary>
        private void InitWorldMapScene()
        {
            List<WorldMapEntity> lst = WorldMapDBModel.Instance.GetList();

            if (lst == null) return;

            m_WorldMapSceneControllerDic = new Dictionary<int, WorldMapSceneController>();

            for (int i = 0; i < lst.Count; i++)
            {
                WorldMapEntity entity = lst[i];
                Console.WriteLine("世界地图-{0}-初始化完毕", entity.Name);
                WorldMapSceneController ctrl = new WorldMapSceneController(entity.Id);
                m_WorldMapSceneControllerDic[entity.Id] = ctrl;
            }
        }


        /// <summary>
        /// 角色进入某个场景
        /// </summary>
        /// <param name="role"></param>
        /// <param name="worldMapSceneId"></param>
        public void RoleEnter(Role role, int worldMapSceneId)
        {
            if (!m_WorldMapSceneControllerDic.ContainsKey(worldMapSceneId)) return;

            m_WorldMapSceneControllerDic[worldMapSceneId].RoleEnter(role);
        }

        /// <summary>
        /// 角色离开某个场景
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="worldMapSceneId"
        public void RoleLeave(int roleId, int worldMapSceneId)
        {
            if (!m_WorldMapSceneControllerDic.ContainsKey(worldMapSceneId)) return;
            m_WorldMapSceneControllerDic[worldMapSceneId].RoleLeave(roleId);

            Console.WriteLine("roleId=" + roleId);
            Console.WriteLine("worldMapSceneId=" + worldMapSceneId);

            //通知同场景其他玩家 我离开了
            NotifyOtherRole_RoleLeave(worldMapSceneId, roleId);
        }

        /// <summary>
        /// 获取当前某个场景所有角色
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoleList(int worldMapSceneId)
        {
            if (!m_WorldMapSceneControllerDic.ContainsKey(worldMapSceneId)) return null;
            return m_WorldMapSceneControllerDic[worldMapSceneId].GetRoleList();
        }

        #endregion

        #region OnWorldMapRoleAlreadyEnter 客户端发送角色已经进入世界地图场景的消息
        private void OnWorldMapRoleAlreadyEnter(Role role, byte[] buffer)
        {
            WorldMap_RoleAlreadyEnterProto proto = WorldMap_RoleAlreadyEnterProto.GetProto(buffer);

            int sceneId = proto.TargetWorldMapSceneId;

            Console.WriteLine("角色要离开的场景=" + role.PrevWorldMapId);

            //1.离开上一个场景
            RoleLeave(role.RoleId, role.PrevWorldMapId);

            role.PrevWorldMapId = sceneId;
            role.LastInWorldMapId = sceneId;
            role.LastInWorldMapPos = string.Format("{0}_{1}_{2}_{3}", proto.RolePosX, proto.RolePosY, proto.RolePosZ, proto.RoleYAngle);

            //2、发送当前场景中的其他玩家
            SendCurrSceneInitRole(role, sceneId);

            //3.进入当前场景
            RoleEnter(role, sceneId);

            //4、通知同场景的其他玩家 告诉他们我来了
            NotifyOtherRole_RoleEnter(sceneId, role.RoleId);
        }
        #endregion

        #region SendCurrSceneInitRole 服务器发送当前场景初始化的角色
        /// <summary>
        /// 服务器发送当前场景初始化的角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="sceneId"></param>
        private void SendCurrSceneInitRole(Role role, int sceneId)
        {
            WorldMap_InitRoleProto proto = new WorldMap_InitRoleProto();

            List<Role> lst = GetRoleList(sceneId);

            proto.RoleCount = lst.Count;
            proto.ItemList = new List<WorldMap_InitRoleProto.RoleItem>();

            for (int i = 0; i < lst.Count; i++)
            {
                WorldMap_InitRoleProto.RoleItem item = new WorldMap_InitRoleProto.RoleItem();
                item.RoleId = lst[i].RoleId;

                item.RoleJobId = lst[i].JobId;
                item.RoleLevel = lst[i].Level;
                item.RoleNickName = lst[i].NickName;
                item.RoleMaxHP = lst[i].MaxHP;
                item.RoleCurrHP = lst[i].CurrHP;
                item.RoleMaxMP = lst[i].MaxMP;
                item.RoleCurrMP = lst[i].CurrMP;

                string[] arr = lst[i].LastInWorldMapPos.Split('_');
                item.RolePosX = float.Parse(arr[0]);
                item.RolePosY = float.Parse(arr[1]);
                item.RolePosZ = float.Parse(arr[2]);
                item.RoleYAngle = float.Parse(arr[3]);

                proto.ItemList.Add(item);
            }

            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #region NotifyOtherRole_RoleEnter 通知同场景的其他玩家告诉他们我来了
        /// <summary>
        /// 通知同场景的其他玩家 告诉他们我来了
        /// </summary>
        /// <param name="worldMapSceneId"></param>
        /// <param name="enterRoleId"></param>
        private void NotifyOtherRole_RoleEnter(int worldMapSceneId, int enterRoleId)
        {
            List<Role> lst = GetRoleList(worldMapSceneId);
            if (lst != null && lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].RoleId == enterRoleId) continue;
                    SendToOtherRole_RoleEnter(lst[i], enterRoleId);
                }
            }
        }

        /// <summary>
        /// 通知同场景的其他玩家 告诉他们我来了
        /// </summary>
        /// <param name="otherRole"></param>
        /// <param name="enterRoleId"></param>
        private void SendToOtherRole_RoleEnter(Role otherRole, int enterRoleId)
        {
            WorldMap_OtherRoleEnterProto
                proto = new WorldMap_OtherRoleEnterProto();
            proto.RoleId = enterRoleId;
            Role enterRole = RoleMgr.Instance.GetRole(enterRoleId);
            if (enterRole != null)
            {
                proto.RoleJobId = enterRole.JobId;
                proto.RoleLevel = enterRole.Level;
                proto.RoleNickName = enterRole.NickName;
                proto.RoleMaxHP = enterRole.MaxHP;
                proto.RoleCurrHP = enterRole.CurrHP;
                proto.RoleMaxMP = enterRole.MaxMP;
                proto.RoleCurrMP = enterRole.CurrMP;

                string[] arr = enterRole.LastInWorldMapPos.Split('_');
                proto.RolePosX = float.Parse(arr[0]);
                proto.RolePosY = float.Parse(arr[1]);
                proto.RolePosZ = float.Parse(arr[2]);
                proto.RoleYAngle = float.Parse(arr[3]);
            }

            otherRole._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #region NotifyOtherRole_RoleLeave 通知同场景的其他玩家 我离开了
        /// <summary>
        /// 通知同场景的其他玩家 我离开了
        /// </summary>
        /// <param name="worldMapSceneId"></param>
        /// <param name="levalRoleId"></param>
        private void NotifyOtherRole_RoleLeave(int worldMapSceneId, int levalRoleId)
        {
            List<Role> lst = GetRoleList(worldMapSceneId);
            if (lst != null && lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].RoleId == levalRoleId) continue;
                    SendToOtherRole_RoleLeave(lst[i], levalRoleId);
                }
            }
        }

        /// <summary>
        /// 通知同场景的其他玩家 我离开了
        /// </summary>
        /// <param name="otherRole"></param>
        /// <param name="levalRoleId"></param>
        private void SendToOtherRole_RoleLeave(Role otherRole, int levalRoleId)
        {
            WorldMap_OtherRoleLeaveProto proto = new WorldMap_OtherRoleLeaveProto();
            proto.RoleId = levalRoleId;
            otherRole._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        #region 角色移动
        /// <summary>
        /// 客户端发送当前角色移动消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnWorldMapCurrRoleMove(Role role, byte[] buffer)
        {
            WorldMap_CurrRoleMoveProto proto = WorldMap_CurrRoleMoveProto.GetProto(buffer);
            Console.WriteLine("我的服务器时间=" + proto.ServerTime);
            Console.WriteLine("服    务器时间=" + DateTime.Now.ToUniversalTime().Ticks / 10000);

            List<Role> lst = GetRoleList(role.LastInWorldMapId);
            if (lst != null && lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].RoleId == role.RoleId) continue;

                    WorldMap_OtherRoleMoveProto myProto = new WorldMap_OtherRoleMoveProto();

                    myProto.RoleId = role.RoleId;
                    myProto.TargetPosX = proto.TargetPosX;
                    myProto.TargetPosY = proto.TargetPosY;
                    myProto.TargetPosZ = proto.TargetPosZ;
                    myProto.ServerTime = proto.ServerTime;
                    myProto.NeedTime = proto.NeedTime;

                    lst[i]._clientSocket.SendMsg(myProto.ToArray());
                }
            }
        }
        #endregion

        /// <summary>
        /// 被攻击的角色列表
        /// </summary>
        private List<WorldMap_OtherRoleUseSkillProto.BeAttackItem> m_BeAttackItemList = new List<WorldMap_OtherRoleUseSkillProto.BeAttackItem>();

        /// <summary>
        /// 被攻击后死亡的角色列表
        /// </summary>
        private List<int> m_DieRoleList = new List<int>();

        /// <summary>
        /// 当前角色使用技能
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnWorldMapCurrRoleUseSkill(Role role, byte[] buffer)
        {
            m_BeAttackItemList.Clear();
            m_DieRoleList.Clear();

            WorldMap_CurrRoleUseSkillProto proto = WorldMap_CurrRoleUseSkillProto.GetProto(buffer);

            int attackRoleId = role.RoleId; //攻击者ID
            int skillId = proto.SkillId; //使用的技能编号
            int skillLevel = proto.SkillLevel; //使用的技能等级
            float rolePosX = proto.RolePosX;
            float rolePosY = proto.RolePosY;
            float rolePosZ = proto.RolePosZ;
            float roleYAngle = proto.RoleYAngle;

            int beAttackCount = proto.BeAttackCount; //被攻击者数量
            for (int i = 0; i < beAttackCount; i++)
            {
                int beAttackRoleId = proto.ItemList[i].BeAttackRoleId; //被攻击者编号

                bool isCri = false; //是否暴击
                int hurtValue = 0; //伤害值

                Role attackRole = RoleMgr.Instance.GetRole(attackRoleId); //攻击者
                //攻击者的MP要减少
                SkillLevelEntity skillLevelEntity = SkillLevelDBModel.Instance.GetEntityBySkillIdAndLevel(skillId, skillLevel);
                attackRole.CurrMP -= skillLevelEntity.SpendMP;
                attackRole.CurrMP = attackRole.CurrMP < 0 ? 0 : attackRole.CurrMP;

                Role beAttackRole = RoleMgr.Instance.GetRole(beAttackRoleId); //被攻击者

                //服务器端计算伤害
                CalculateHurtValue(attackRole, beAttackRole, skillId, skillLevel, ref isCri, ref hurtValue);

                WorldMap_OtherRoleUseSkillProto.BeAttackItem item = new WorldMap_OtherRoleUseSkillProto.BeAttackItem();
                item.BeAttackRoleId = beAttackRoleId; //被攻击者id
                item.IsCri = (byte)(isCri ? 1 : 0); //是否暴击
                item.ReduceHp = hurtValue; //伤害

                beAttackRole.CurrHP -= hurtValue; //服务器端的被攻击者 减少血量
                if (beAttackRole.CurrHP <= 0)
                {
                    //说明角色已经死亡
                    beAttackRole.CurrHP = 0;
                    m_DieRoleList.Add(beAttackRoleId);
                }

                m_BeAttackItemList.Add(item);
            } 

            //要把受伤消息 发给同场景的玩家 玩家A 要能看到 B和C在战斗 即便A只是旁观者
            //================告诉同场景的玩家 给他们发消息 包括使用技能的玩家 被攻击者 和 旁观者
            {
                List<Role> lst = GetRoleList(role.LastInWorldMapId);
                if (lst != null && lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        SendOtherRoleUseSkill(lst[i], attackRoleId, skillId, skillLevel, rolePosX, rolePosY, rolePosZ, roleYAngle, m_BeAttackItemList);
                    }
                }
            }
            //===========================

            //要把死亡消息 发给同场景的玩家 玩家A 要能看到 B和C在战斗 即便A只是旁观者
            {
                List<Role> lst = GetRoleList(role.LastInWorldMapId);
                if (lst != null && lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        SendOtherRoleDie(lst[i], attackRoleId, m_DieRoleList);
                    }
                }
            }
        }

        #region SendOtherRoleUseSkill 服务器广播 其他角色使用技能消息
        /// <summary>
        /// 服务器广播 其他角色使用技能消息
        /// </summary>
        /// <param name="role"></param>
        private void SendOtherRoleUseSkill(Role role,
            int attackRoleId, int skillId, int skillLevel, float rolePosX, float rolePosY, float rolePosZ, float roleYAngle,
            List<WorldMap_OtherRoleUseSkillProto.BeAttackItem> lst)
        {
            WorldMap_OtherRoleUseSkillProto proto = new WorldMap_OtherRoleUseSkillProto();
            proto.AttackRoleId = attackRoleId;
            proto.SkillId = skillId;
            proto.SkillLevel = skillLevel;
            proto.RolePosX = rolePosX;
            proto.RolePosY = rolePosY;
            proto.RolePosZ = rolePosZ;
            proto.RoleYAngle = roleYAngle;

            proto.BeAttackCount = lst.Count;
            proto.ItemList = lst;

            role._clientSocket.SendMsg(proto.ToArray());
        }
        #endregion

        /// <summary>
        /// 服务器发送角色死亡消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="lst"></param>
        private void SendOtherRoleDie(Role role, int attackRoleId, List<int> lst)
        {
            WorldMap_OtherRoleDieProto proto = new WorldMap_OtherRoleDieProto();
            proto.AttackRoleId = attackRoleId; //攻击者是谁
            proto.DieCount = lst.Count;
            proto.RoleIdList = lst; //哪些人死亡

            role._clientSocket.SendMsg(proto.ToArray());
        }

        #region CalculateHurtValue 服务器端计算伤害
        /// <summary>
        /// 服务器端计算伤害
        /// </summary>
        /// <param name="currRole"></param>
        /// <param name="enemy"></param>
        /// <param name="skillId"></param>
        /// <param name="skillLevel"></param>
        /// <param name="isCri"></param>
        /// <param name="hurtValue"></param>
        private void CalculateHurtValue(Role currRole, Role enemy, int skillId, int skillLevel, ref bool isCri, ref int hurtValue)
        {
            if (currRole == null || enemy == null) return;

            SkillEntity skillEntity = SkillDBModel.Instance.Get(skillId);
            SkillLevelEntity skillLevelEntity = SkillLevelDBModel.Instance.GetEntityBySkillIdAndLevel(skillId, skillLevel);
            if (skillEntity == null) return;

            //计算伤害
            //1.攻击数值 = 攻击方的综合战斗力 * （技能的伤害倍率 * 0.01f）
            float attackValue = currRole.Fighting * (skillLevelEntity.HurtValueRate * 0.01f);

            //2.基础伤害 = 攻击数值 * 攻击数值 / (攻击数值 + 被攻击方的防御)
            float baseHurt = attackValue * attackValue / (attackValue + enemy.Defense);

            //3.暴击概率 = 0.05f + (攻击方暴击/(攻击方暴击+防御方抗性)) * 0.1f;
            float cri = 0.05f + (currRole.Cri / (currRole.Cri + enemy.Res)) * 0.1f;

            //暴击概率 = 暴击概率>0.5f?0.5f:暴击概率
            cri = cri > 0.5f ? 0.5f : cri;


            //4.是否暴击 = 0-1的随机数 < =暴击概率
            isCri = new Random().NextDouble() <= cri;

            //5.暴击伤害倍率 = 有暴击?1.5f ：1f;
            float criHrut = isCri ? 1.5f : 1f;

            //6.随机数 = 0.9f-1.1f之间
            int random = new Random().Next(9000, 11000);
            float frandom = random * 0.0001f;

            //7.最终伤害 = 基础伤害 * 暴击伤害倍率 * 随机数
            hurtValue = (int)Math.Round(baseHurt * criHrut * frandom);
            hurtValue = hurtValue < 1 ? 1 : hurtValue;
        }
        #endregion

        /// <summary>
        /// 客户端发送角色复活消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnWorldMapCurrRoleResurgence(Role role, byte[] buffer)
        {
            WorldMap_CurrRoleResurgenceProto proto = new WorldMap_CurrRoleResurgenceProto();

            int type = proto.Type; //以后此处处理逻辑 比如扣除元宝等

            //修改要复活的角色HP和MP
            Role resurgenceRole = RoleMgr.Instance.GetRole(role.RoleId); //要复活者
            resurgenceRole.CurrHP = resurgenceRole.MaxHP;
            resurgenceRole.CurrMP = resurgenceRole.MaxMP;

            List<Role> lst = GetRoleList(role.LastInWorldMapId);
            if (lst != null && lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    SendRoleResurgence(lst[i], role.RoleId);
                }
            }
        }

        /// <summary>
        /// 服务器发送角色复活消息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="roleId"></param>
        private void SendRoleResurgence(Role role, int roleId)
        {
            WorldMap_OtherRoleResurgenceProto proto = new WorldMap_OtherRoleResurgenceProto();
            proto.RoleId = roleId;
            role._clientSocket.SendMsg(proto.ToArray());
        }

    }
}
