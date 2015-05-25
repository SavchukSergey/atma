namespace Atmega.Flasher.AvrIsp {

    public struct AvrIspVersion {
        public int Hardware { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }
        
        public char Type { get; set; }
    }
}
