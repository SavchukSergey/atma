using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Atmega.Flasher.Models {
    public class FlasherConfig : BaseConfig {

        private ProgrammerType _programmerType;
        private readonly ObservableCollection<KeyValuePair<ProgrammerType, string>> _programmerTypes = new ObservableCollection<KeyValuePair<ProgrammerType, string>>();
        private readonly AvrIspConfig _avrIsp;
        private readonly ComBitBangConfig _comBitBang;

        public AvrIspConfig AvrIsp {
            get { return _avrIsp; }
        }

        public ComBitBangConfig ComBitBang {
            get { return _comBitBang; }
        }

        public FlasherConfig()
            : base(string.Empty) {
            _avrIsp = new AvrIspConfig("AvrIsp.");
            _comBitBang = new ComBitBangConfig("ComBitBang.");
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

        public override void ReadFromConfig() {
            _avrIsp.ReadFromConfig();
            _comBitBang.ReadFromConfig();

            ProgrammerTypes.Clear();
            ProgrammerTypes.Add(new KeyValuePair<ProgrammerType, string>(ProgrammerType.AvrIsp, "Avr ISP"));
            ProgrammerTypes.Add(new KeyValuePair<ProgrammerType, string>(ProgrammerType.ComBitBang, "Com Bit Bang"));
            ProgrammerTypes.Add(new KeyValuePair<ProgrammerType, string>(ProgrammerType.Stub, "Stub"));
            ProgrammerType = GetConfigEnum(ProgrammerType.AvrIsp, "ProgrammerType");
        }

        public static FlasherConfig Read() {
            var res = new FlasherConfig();
            res.ReadFromConfig();
            return res;
        }

        public override void Save() {
            UpdateConfig(ProgrammerType.ToString(), "ProgrammerType");
            _avrIsp.Save();
            _comBitBang.Save();
        }
    }
}
