using Xunit;
using FEBuilderGBA.Avalonia.Services;

namespace FEBuilderGBA.Core.Tests
{
    [Collection("SharedState")]
    public class ComboResourceHelperTests
    {
        [Fact]
        public void MakeAffinityList_ReturnsExpectedEntries()
        {
            var list = ComboResourceHelper.MakeAffinityList();
            Assert.Equal(8, list.Count);
            Assert.Contains("Fire", list[1].name);
            Assert.Equal(0u, list[0].id);
        }

        [Fact]
        public void MakeWeaponTypeList_ReturnsExpectedEntries()
        {
            var list = ComboResourceHelper.MakeWeaponTypeList();

            Assert.Equal(13, list.Count);

            Assert.Equal((uint)0x00, list[0].id);
            Assert.Equal((uint)0x01, list[1].id);
            Assert.Equal((uint)0x02, list[2].id);
            Assert.Equal((uint)0x03, list[3].id);
            Assert.Equal((uint)0x04, list[4].id);
            Assert.Equal((uint)0x05, list[5].id);
            Assert.Equal((uint)0x06, list[6].id);
            Assert.Equal((uint)0x07, list[7].id);
            Assert.Equal((uint)0x09, list[8].id);
            Assert.Equal((uint)0x0B, list[9].id);
            Assert.Equal((uint)0x0C, list[10].id);
            Assert.Equal((uint)0x11, list[11].id);
            Assert.Equal((uint)0x12, list[12].id);

        [Fact]
        public void MakeUnitList_NoRom_ReturnsEmpty()
        {
            ROM? oldRom = CoreState.ROM;
            try
            {
                CoreState.ROM = null!;
                var list = ComboResourceHelper.MakeUnitList();
                Assert.Empty(list);
            }
            finally
            {
                CoreState.ROM = oldRom!;
            }
        }

        [Fact]
        public void MakeClassList_NoRom_ReturnsEmpty()
        {
            ROM? oldRom = CoreState.ROM;
            try
            {
                CoreState.ROM = null!;
                var list = ComboResourceHelper.MakeClassList();
                Assert.Empty(list);
            }
            finally
            {
                CoreState.ROM = oldRom!;
            }
        }

        [Fact]
        public void MakeItemList_NoRom_ReturnsEmpty()
        {
            ROM? oldRom = CoreState.ROM;
            try
            {
                CoreState.ROM = null!;
                var list = ComboResourceHelper.MakeItemList();
                Assert.Empty(list);
            }
            finally
            {
                CoreState.ROM = oldRom!;
            }
        }

        [Fact]
        public void MakeSongList_NoRom_ReturnsEmpty()
        {
            ROM? oldRom = CoreState.ROM;
            try
            {
                CoreState.ROM = null!;
                var list = ComboResourceHelper.MakeSongList();
                Assert.Empty(list);
            }
            finally
            {
                CoreState.ROM = oldRom!;
            }
        }
    }
}
