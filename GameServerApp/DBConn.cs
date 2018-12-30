using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DBConn
{
    private static string m_DBGameServer;
    /// <summary>
    /// 去 app.config文件里取数据
    /// </summary>
    public static string DBGameServer {
        get {
            if (string.IsNullOrEmpty(m_DBGameServer))
            {
                m_DBGameServer = System.Configuration.ConfigurationManager.ConnectionStrings["DBGameServer"].ConnectionString;
            }
            return m_DBGameServer;
        } 
    }
}
