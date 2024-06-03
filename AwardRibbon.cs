using ContractConfigurator;
using ContractConfigurator.Behaviour;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CCFFPlugin
{
    using static ContractConfigurator.Behaviour.ChangeKerbalType;
    using Kerbal = ContractConfigurator.Kerbal;
    public class AwardRibbon : TriggeredBehaviour
    {
        protected List<Kerbal> kerbals = new List<Kerbal>();
        protected List<string> ribbons = new List<string>();

        static AwardRibbon()
        {
        }

        public AwardRibbon()
        {
        }

        public AwardRibbon(List<Kerbal> kerbals, List<string> ribbons, TriggeredBehaviour.State onState, List<string> parameter)
            : base(onState, parameter)
        {
            this.kerbals = kerbals;
            this.ribbons = ribbons;
        }

        protected override void TriggerAction()
        {
            var ffAdapter = AwardRibbonFactory.getFFAdapter();
            if (ffAdapter == null || !ffAdapter.IsInstalled())
                return;

            var pcms = kerbals.Select(k => k.pcm).ToArray();
            foreach (var ribbon in ribbons)
            {
                ffAdapter.AwardRibbonToKerbals(ribbon, pcms);
            }
        }

        protected override void OnSave(ConfigNode configNode)
        {
            base.OnSave(configNode);

            foreach (var kerbal in kerbals)
            {
                configNode.AddValue("kerbal", kerbal.name);
            }
            foreach (var ribbon in ribbons)
            {
                configNode.AddValue("ribbon", ribbon);
            }
        }
        protected override void OnLoad(ConfigNode configNode)
        {
            base.OnLoad(configNode);

            kerbals = ConfigNodeUtil.ParseValue<List<Kerbal>>(configNode, "kerbal", new List<Kerbal>());
            ribbons = ConfigNodeUtil.ParseValue<List<string>>(configNode, "ribbon", new List<string>());
        }
    }
}
