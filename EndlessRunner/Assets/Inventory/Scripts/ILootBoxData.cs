using System;

namespace Inventory.Scripts
{
    public interface ILootBoxData
    {
        ILootBoxConfig Config { get; }
        DateTime OpeningStartTime { get; }
    }
}
