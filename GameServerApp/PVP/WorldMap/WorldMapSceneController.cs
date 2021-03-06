﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp.PVP.WorldMap
{
    /// <summary>
    /// 世界地图场景控制器
    /// </summary>
    class WorldMapSceneController
    {
        #region 世界地图编号
        /// <summary>
        /// 世界地图编号
        /// </summary>
        private int m_WorldMapSceneId;

        /// <summary>
        /// 世界地图编号
        /// </summary>
        public int WorldMapSceneId { get { return m_WorldMapSceneId; } }
        #endregion

        private Dictionary<int, Role> m_RoleDic;
        private List<Role> m_RoleList;


        public WorldMapSceneController(int worldMapSceneId) {
            m_WorldMapSceneId = worldMapSceneId;
            m_RoleDic = new Dictionary<int, Role>();
            m_RoleList = new List<Role>();
        }

        /// <summary>
        /// 角色进入场景
        /// </summary>
        /// <param name="role"></param>
        public void RoleEnter(Role role) {
            m_RoleDic[role.RoleId] = role;
        }

        /// <summary>
        /// 角色离开场景
        /// </summary>
        /// <param name="roleId"></param>
        public void RoleLeave(int roleId) {
            if (m_RoleDic.ContainsKey(roleId))
            {
                m_RoleDic.Remove(roleId);
            }
        }

        
        /// <summary>
        /// 获取当前场景所有角色
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoleList() {
            m_RoleList.Clear();

            foreach (var item in m_RoleDic)
            {
                m_RoleList.Add(item.Value);
            }
            return m_RoleList;
        }

    }
}
