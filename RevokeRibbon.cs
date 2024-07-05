using ContractConfigurator;
using ContractConfigurator.Behaviour;
using System.Collections.Generic;
using System.Linq;

namespace CCFFPlugin
{
    using Kerbal = ContractConfigurator.Kerbal;
    public class RevokeRibbon : TriggeredBehaviour
    {
        protected List<Kerbal> kerbals = new List<Kerbal>();
        protected List<string> ribbons = new List<string>();

        static RevokeRibbon()
        {
        }

        public RevokeRibbon()
        {
        }

        public RevokeRibbon(List<Kerbal> kerbals, List<string> ribbons, TriggeredBehaviour.State onState, List<string> parameter)
            : base(onState, parameter)
        {
            this.kerbals = kerbals;
            this.ribbons = ribbons;
        }

        protected override void TriggerAction()
        {
            var ffAdapter = RevokeRibbonFactory.getFFAdapter();
            if (ffAdapter == null || !ffAdapter.IsInstalled())
                return;

            foreach (var ribbon in ribbons)
            {
                foreach (var pcm in kerbals.Select(k => k.pcm))
                    ffAdapter.RevokeRibbonFromKerbal(ribbon, pcm);
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
