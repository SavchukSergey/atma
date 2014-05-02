using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atmega.Asm.Opcodes.Branch.Status {
    public class BreqOpcode : BaseOffset7Opcode {
        
        public BreqOpcode()
            : base("111100lllllll001") {
        }

    }
}
