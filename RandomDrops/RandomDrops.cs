using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace RandomDrops {
    [ApiVersion(2, 1)]
    public class RandomDrops : TerrariaPlugin {
        public override string Name => "RandomDrops";
        public override Version Version => new Version(1, 0, 0);
        public override string Author => "Soofa";
        public override string Description => "NPCs drop random items.";
        public RandomDrops(Main game) : base(game) {
        }

        public override void Initialize() {
            ServerApi.Hooks.NpcKilled.Register(this, OnNpcKilled);
            ServerApi.Hooks.NpcLootDrop.Register(this, OnNpcLootDrop);
        }

        
        private void OnNpcKilled(NpcKilledEventArgs args) {
            int itemID = WorldGen.genRand.Next(1, 5079);
            int maxStack = TShock.Utils.GetItemById(itemID).maxStack;
            int stack = WorldGen.genRand.Next((int)Math.Ceiling(maxStack * 0.01), (int)Math.Ceiling(maxStack * 0.1));

            Item.NewItem(null, (int)args.npc.position.X, (int)args.npc.position.Y, 1, 1, itemID, stack);
        }

        private void OnNpcLootDrop(NpcLootDropEventArgs args) {
            args.Handled = true;
        }
        

        protected override void Dispose(bool disposing) {
            if (disposing) {
                ServerApi.Hooks.NpcKilled.Deregister(this, OnNpcKilled);
                ServerApi.Hooks.NpcLootDrop.Deregister(this, OnNpcLootDrop);
            }
            base.Dispose(disposing);
        }
    }
}