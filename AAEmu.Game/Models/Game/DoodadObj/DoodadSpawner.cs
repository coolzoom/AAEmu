using System;
using AAEmu.Game.Core.Managers.Id;
using AAEmu.Game.Core.Managers.UnitManagers;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Models.Game.World;
using NLog;

namespace AAEmu.Game.Models.Game.DoodadObj
{
    public class DoodadSpawner : Spawner<Doodad>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        public float Scale { get; set; }
        
        public Doodad Last { get; set; }

        public override Doodad Spawn(uint objId)
        {
            var doodad = DoodadManager.Instance.Create(objId, UnitId, null); // TODO look
            doodad.Spawner = this;
            doodad.Position = Position.Clone();
            doodad.OwnerType = DoodadOwnerType.System;
            if (Scale > 0)
                doodad.SetScale(Scale);
            if (doodad.Position == null)
            {
                _log.Error("Can't spawn doodad {1} from spawn {0}", Id, UnitId);
                return null;
            }

            doodad.Spawn();
            Last = doodad;
            return doodad;
        }

        public override void Despawn(Doodad doodad)
        {
            doodad.Delete();
            if (doodad.Respawn == DateTime.MinValue)
                ObjectIdManager.Instance.ReleaseId(doodad.ObjId);
            Last = null;
        }

        public void DecreaseCount(Doodad doodad)
        {
            if (RespawnTime > 0)
            {
                doodad.Respawn = DateTime.Now.AddSeconds(RespawnTime);
                SpawnManager.Instance.AddRespawn(doodad);
            }
            else
                Last = null;

            doodad.Delete();
        }
    }
}
