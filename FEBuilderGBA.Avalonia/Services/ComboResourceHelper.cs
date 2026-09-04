using System;
using System.Collections.Generic;

namespace FEBuilderGBA.Avalonia.Services
{
    /// <summary>
    /// Builds named list items from ROM tables for use in dropdown/combo controls.
    /// Each method returns a list of (id, displayName) tuples.
    /// </summary>
    public static class ComboResourceHelper
    {
        public static List<(uint id, string name)> MakeUnitList()
        {
            var result = new List<(uint, string)>();
            var rom = CoreState.ROM;
            if (rom?.RomInfo == null) return result;
            uint count = rom.RomInfo.unit_maxcount;
            if (count == 0) count = 0x100;
            for (uint i = 0; i < count; i++)
                result.Add((i, $"{U.ToHexString(i)} {NameResolver.GetUnitName(i)}"));
            return result;
        }

        public static List<(uint id, string name)> MakeClassList()
        {
            var result = new List<(uint, string)>();
            var rom = CoreState.ROM;
            if (rom?.RomInfo == null) return result;
            uint baseAddr = ResolvePointer(rom, rom.RomInfo.class_pointer);
            uint dataSize = rom.RomInfo.class_datasize;
            if (baseAddr == 0 || dataSize == 0) return result;
            uint count = rom.getBlockDataCount(baseAddr, dataSize, (int i, uint addr) =>
            {
                if (i == 0) return true;
                if (i > 0xFF) return false;
                return rom.u8(addr + 4) != 0;
            });
            for (uint i = 0; i < count; i++)
                result.Add((i, $"{U.ToHexString(i)} {NameResolver.GetClassName(i)}"));
            return result;
        }

        public static List<(uint id, string name)> MakeItemList()
        {
            var result = new List<(uint, string)>();
            var rom = CoreState.ROM;
            if (rom?.RomInfo == null) return result;
            uint baseAddr = ResolvePointer(rom, rom.RomInfo.item_pointer);
            uint dataSize = rom.RomInfo.item_datasize;
            if (baseAddr == 0 || dataSize == 0) return result;
            uint count = rom.getBlockDataCount(baseAddr, dataSize, (int i, uint addr) =>
            {
                if (i > 0xFF) return false;
                return U.isPointerOrNULL(rom.u32(addr + 12));
            });
            for (uint i = 0; i < count; i++)
                result.Add((i, $"{U.ToHexString(i)} {NameResolver.GetItemName(i)}"));
            return result;
        }

        public static List<(uint id, string name)> MakeSongList()
        {
            var result = new List<(uint, string)>();
            var rom = CoreState.ROM;
            if (rom?.RomInfo == null) return result;
            // Use a reasonable default count; song table details are in WinForms layer
            uint count = 0x80;
            for (uint i = 0; i < count; i++)
                result.Add((i, $"{U.ToHexString(i)} {NameResolver.GetSongName(i)}"));
            return result;
        }

        public static List<(uint id, string name)> MakeAffinityList()
        {
            // Affinities are fixed across FE versions
            string[] names = { "None", "Fire", "Thunder", "Wind", "Ice", "Dark", "Light", "Anima" };
            var result = new List<(uint, string)>();
            for (uint i = 0; i < (uint)names.Length; i++)
                result.Add((i, $"{U.ToHexString(i)} {names[i]}"));
            return result;
        }

        public static List<(uint id, string name)> MakeWeaponTypeList()
        {
            return new List<(uint id, string name)>
            {
                (0x00, "00 Sword"),
                (0x01, "01 Lance"),
                (0x02, "02 Axe"),
                (0x03, "03 Bow"),
                (0x04, "04 Staff"),
                (0x05, "05 Anima"),
                (0x06, "06 Light"),
                (0x07, "07 Dark"),
                (0x09, "09 Item"),
                (0x0B, "0B Dragonstone/Monster Weapon"),
                (0x0C, "0C Ring"),
                (0x11, "11 Dragonstone"),
                (0x12, "12 Dancer's Ring"),
            };
        }

        public static List<(uint id, string name)> MakeAdditionalDamageTypeList()
        {
            return new List<(uint id, string name)>
            {
                (0x00, "00 None"),
                (0x01, "01 Poison Effect"),
                (0x02, "02 Nosferatu Effect"),
                (0x03, "03 Eclipse Effect"),
                (0x04, "04 Devil Effect"),
                (0x05, "05 Inflicts Stone"),
                (0x06, "06 Sleep"),
                (0x07, "07 Berserk"),
                (0x08, "08 Silence"),
            };
        }
        public static List<(uint id, string name)> MakeWhenUsedList()
        {
            return new List<(uint id, string name)>
            {
                (0x00, "00 --"),
                (0x01, "01 Heal"),
                (0x02, "02 Mend"),
                (0x03, "03 Recover"),
                (0x04, "04 Physic"),
                (0x05, "05 Fortify"),
                (0x06, "06 Restore"),
                (0x07, "07 Silence"),
                (0x08, "08 Sleep"),
                (0x09, "09 Berserk"),
                (0x0A, "0A Warp"),
                (0x0B, "0B Rescue"),
                (0x0C, "0C Torch"),
                (0x0D, "0D Hammerne"),
                (0x0E, "0E Unlock"),
                (0x0F, "0F Barrier"),
                (0x10, "10 Angelic Robe"),
                (0x11, "11 Energy Ring"),
                (0x12, "12 Secret Book"),
                (0x13, "13 Speedwing"),
                (0x14, "14 Goddess Icon"),
                (0x15, "15 Dragonshield"),
                (0x16, "16 Talisman"),
                (0x17, "17 Swiftsole"),
                (0x18, "18 Body Ring"),
                (0x19, "19 Hero Crest"),
                (0x1A, "1A Knight Crest"),
                (0x1B, "1B Orion's Bolt"),
                (0x1C, "1C Elysian Whip"),
                (0x1D, "1D Guiding Ring"),
                (0x1E, "1E Chest Key"),
                (0x1F, "1F Door Key"),
                (0x20, "20 Lockpick"),
                (0x21, "21 Vulnerary"),
                (0x22, "22 Elixir"),
                (0x23, "23 Pure Water"),
                (0x24, "24 Antitoxin"),
                (0x25, "25 Torch"),
                (0x26, "26 Chest Key"),
                (0x27, "27 Mine"),
                (0x28, "28 Light rune"),
                (0x29, "29 Filla's Might"),
                (0x2A, "2A Ninis's Grace"),
                (0x2B, "2B Thor's Ire"),
                (0x2C, "2C Set's Litany"),
                (0x2D, "2D Master Seal"),
                (0x2E, "2E Metis's Tome"),
                (0x2F, "2F Dummy?"),
                (0x30, "30 Ocean Seal"),
                (0x31, "31 Lunar Brace"),
                (0x32, "32 Solar Brace"),
                (0x33, "33 Vulnerary?"),
                (0x34, "34 Vulnerary?"),
                (0x35, "35 Vulnerary?"),
                (0x36, "36 Juna Fruit"),
                (0x37, "37 Latona"),
                (0x38, "38 Skill Scroll"),
            };
        }
        
        /// <summary>
        /// Resolve a ROMFEINFO pointer address to a ROM offset.
        /// Mirrors StructExportCore.ResolvePointer.
        /// </summary>
        private static uint ResolvePointer(ROM rom, uint pointerAddr)
        {
            if (pointerAddr == 0 || pointerAddr == U.NOT_FOUND) return 0;
            uint offset = U.toOffset(pointerAddr);
            if (!U.isSafetyOffset(offset, rom)) return 0;
            return rom.p32(offset);
        }
    }
}
