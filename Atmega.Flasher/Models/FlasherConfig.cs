using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Atmega.Flasher.Models {
    public class FlasherConfig : BaseConfig {

        private ProgrammerType _programmerType;
        private ObservableCollection<KeyValuePair<ProgrammerType, string>> _programmerTypes = new ObservableCollection<KeyValuePair<ProgrammerType, string>>();

        public FlasherAvrIspConfig AvrIsp { get; protected set; }

        public FlasherConfig() {
            AvrIsp = new FlasherAvrIspConfig();
        }

        public ProgrammerType ProgrammerType {
            get { return _programmerType; }
            set {
                if (_programmerType != value) {
                    _programmerType = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<KeyValuePair<ProgrammerType, string>> ProgrammerTypes {
            get {
                return _programmerTypes;
            }
        }

        public static FlasherConfig ReadFromConfig() {
            var res = new FlasherConfig {
                AvrIsp = FlasherAvrIspConfig.ReadFromConfig()
            };
            res.ProgrammerTypes.Clear();
            res.ProgrammerTypes.Add(new KeyValuePair<ProgrammerType, string>(ProgrammerType.AvrIsp, "Avr ISP"));
            res.ProgrammerTypes.Add(new KeyValuePair<ProgrammerType, string>(ProgrammerType.ComBitBang, "Com Bit Bang"));
            res.ProgrammerTypes.Add(new KeyValuePair<ProgrammerType, string>(ProgrammerType.Stub, "Stub"));
            res.ProgrammerType = res.GetConfigEnum(ProgrammerType.AvrIsp, "ProgrammerType");
            return res;
        }

        protected override string KeyPrefix {
            get { return null; }
        }

        public override void Save() {
            UpdateConfig(ProgrammerType.ToString(), "ProgrammerType");
            AvrIsp.Save();
        }
    }
}
