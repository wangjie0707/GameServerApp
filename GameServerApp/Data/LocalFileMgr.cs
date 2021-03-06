﻿using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 本地文件管理
/// </summary>
public class LocalFileMgr
{

    /// <summary>
    /// 把本地文件读取到byte数组
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static byte[] GetBuffer(string path)
    {
        byte[] buffer = null;

        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }
}
