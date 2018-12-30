using GameServerApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServerApp.Controller
{
    public class RechargeCtrl : Singleton<RechargeCtrl>, IDisposable
    {
        private byte[] m_ReceiveBuffer = new byte[1024];

        public void Recharge(Socket socket)
        {

            int len = socket.Receive(m_ReceiveBuffer);

            using (mmo_memotyStream stream = new mmo_memotyStream())
            {
                stream.Write(m_ReceiveBuffer, 0, len);

                byte[] buff = stream.ToArray();

                string str = System.Text.UTF8Encoding.UTF8.GetString(buff);

                Console.WriteLine("游戏服收到 {0}", str);
                //处理充值的逻辑

                string[] arr = str.Split('_');
                short channelId = arr[0].ToShort();
                int roleId = arr[1].ToInt();
                int rechargeProductId = arr[2].ToInt();

                Role role = RoleMgr.Instance.GetRole(roleId);
                if (role != null)
                {
                    role.Recharge(channelId, rechargeProductId); //游戏服 告诉这个这个角色 我要给你充值 充值的产品id=rechargeProductId
                }

                buff = null;
                socket.Disconnect(false);
                socket.Dispose();
            }
        }
    }
}