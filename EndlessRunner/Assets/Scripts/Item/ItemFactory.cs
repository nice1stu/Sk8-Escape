using System;
using Inventory;
using Stat;
using Random = UnityEngine.Random;

namespace Item
{
    [Serializable]
    public class ItemFactory : IItemFactory
    {
        public IInventoryData Inventory { get; }

        public ItemFactory(IInventoryData inventory)
        {
            Inventory = inventory;
        }
        public ItemData CreateItem(IItemConfig itemConfig)
        {
            var itemBonusStats = new Stats();
            for(int i = 0; i < itemConfig.BonusStats; i++){
                // random number
                var random = Random.Range(1, 7);
                switch (random)
                {
                    case 1:
                        itemBonusStats.Stability++;
                        break;
                    case 2:
                        itemBonusStats.CoffinTimeAdded++;
                        break;
                    case 3:
                        itemBonusStats.ScoreMultiplier++;
                        break;
                    case 4:
                        itemBonusStats.Vision++;
                        break;
                    case 5:
                        itemBonusStats.GrindLeniency++;
                        break;
                    case 6:
                        itemBonusStats.GrindMiniGameBallSize++;
                        break;
                }
            }
            var item = new ItemData(itemBonusStats, itemConfig);
            Inventory.AddItem(item);
            return item;
        }
    }
}
