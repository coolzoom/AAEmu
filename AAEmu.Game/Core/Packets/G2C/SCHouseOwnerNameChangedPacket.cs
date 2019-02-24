using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;

namespace AAEmu.Game.Core.Packets.G2C
{
    public class SCHouseOwnerNameChangedPacket : GamePacket
    {
        private readonly ushort _tl;
        private readonly string _newName;
        
        public SCHouseOwnerNameChangedPacket(ushort tl, string newName) : base(0x0c3, 1)
        {
            _tl = tl;
            _newName = newName;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_tl);
            stream.Write(_newName);
            return stream;
        }
    }
}
