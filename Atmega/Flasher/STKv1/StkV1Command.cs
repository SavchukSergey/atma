namespace Atmega.Flasher.STKv1 {
    public enum StkV1Command : byte {
        GetSync = 0x30,
        GetSignOn = 0x31,
        SetParameterValue = 0x40,
        GetParameterValue = 0x41,
        SetDeviceParameters = 0x42,
        SetDeviceParametersExt = 0x45,
        EnterProgramMode = 0x50,
        LeaveProgramMode = 0x51,
        ChipErase = 0x52,
        LoadAddress = 0x55,
        ProgramFlashMemory = 0x60,
        ProgramDataMemory = 0x61,
        ProgramFuseBits = 0x62,
        ProgramLockBits = 0x63,
        ProgramPage = 0x64,
        ProgramFuseBitsExt = 0x65,
    }
}
