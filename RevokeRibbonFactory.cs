using ContractConfigurator;
using ContractConfigurator.Behaviour;
using MyPlugin;
using System.Collections.Generic;

namespace CCFFPlugin
{
    using Kerbal = ContractConfigurator.Kerbal;
    public class RevokeRibbonFactory : BehaviourFactory
    {
        protected List<Kerbal> kerbals;
        protected List<string> ribbons;

        protected TriggeredBehaviour.State onState;
        protected List<string> parameter = new List<string>();

        static protected FinalFrontierAdapter ffAdapter = null;

        public override bool Load(ConfigNode configNode)
        {
            // Load base class
            bool valid = base.Load(configNode);

            valid &= ConfigNodeUtil.ParseValue<TriggeredBehaviour.State>(configNode, "onState", x => onState = x, this, TriggeredBehaviour.State.CONTRACT_SUCCESS);
            if (onState == TriggeredBehaviour.State.PARAMETER_COMPLETED || onState == TriggeredBehaviour.State.PARAMETER_FAILED)
            {
                valid &= ConfigNodeUtil.ParseValue<List<string>>(configNode, "parameter", x => parameter = x, this);
            }

            valid &= ConfigNodeUtil.ParseValue<List<Kerbal>>(configNode, "kerbal", x => kerbals = x, this);
            valid &= ConfigNodeUtil.ParseValue<List<string>>(configNode, "ribbon", x => ribbons = x, this);

            valid &= ConfigNodeUtil.ValidateUnexpectedValues(configNode, this);

            return valid;
        }

        public override ContractBehaviour Generate(ConfiguredContract contract)
        {
            return new RevokeRibbon(kerbals, ribbons, onState, parameter);
        }

        static public FinalFrontierAdapter getFFAdapter()
        {
            if (ffAdapter == null)
            {
                ffAdapter = new FinalFrontierAdapter();
                ffAdapter.Plugin();
            }
            return ffAdapter;
        }
    }
}
