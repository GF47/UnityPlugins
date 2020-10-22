﻿/***************************************************************
 * @File Name       : ABDownLoader
 * @Author          : GF47
 * @Description     : ab包下载器
 * @Date            : 2017/7/31/星期一 15:50:39
 * @Edit            : none
 **************************************************************/

using System;
using System.IO;
using UnityEngine;

namespace GF47RunTime.AssetBundles
{
    public class ABDownloader : CustomYieldInstruction
    {
        public int Progress
        {
            get
            {
                if (_isDone) { return 100; }
                if (_downloader == null) { return 0; }
                return _downloader.Percent;
            }
        }
        public override bool keepWaiting { get { return !_isDone; } }
        private bool _isDone;


        private HttpDownloader _downloader;
        private int _retryNumber;

        private readonly string _url;
        private readonly string _nativePath;

        public ABDownloader(string abPath)
        {
            _url = ABConfig.SERVER_URL + "/" + ABConfig.PLATFORM + "/" + abPath;
            _nativePath = ABConfig.AssetBundle_Root_Hotfix + "/" + abPath;

            Start();
        }

        private void Start()
        {
            if (File.Exists(_nativePath))
            {
                File.Delete(_nativePath);
            }

            _downloader = new HttpDownloader(_url, _nativePath, FinishedCallback, ErrorCallback);
            _downloader.Start();
        }

        private void ErrorCallback(Exception e)
        {
            if (_retryNumber < 2)
            {
                _retryNumber++;
                Start();
            }
            else
            {
                _isDone = true;
                if (File.Exists(_nativePath))
                {
                    File.Delete(_nativePath);
                }
            }
        }

        private void FinishedCallback(bool b)
        {
            _isDone = b;
        }
    }
}
