using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KeePass.Plugins;

namespace Cockroach
{
    public sealed class CockroachExt:Plugin
    {
        private IPluginHost m_host = null;
        private CockroachGen m_gen = null;

        string updateurl = "http://192.168.1.100/version/cockroach_version.txt"; //检测更新
        public override string UpdateUrl
        {
            get
            {
                return updateurl;
            }
        }

        public override bool Initialize(IPluginHost host)
        {
            if (host == null) return false;
            m_host = host;
            m_gen = new CockroachGen();
            m_host.PwGeneratorPool.Add(m_gen);
            return true;            
        }

        public override void Terminate()
        {
            if (m_host != null)
            {
                m_host.PwGeneratorPool.Remove(m_gen.Uuid);
                m_gen = null;
                m_host = null;
            }
        }
    }
}
