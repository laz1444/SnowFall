using fNbt;
using MiNET.Worlds;

namespace SnowFall
{
    public class customBiomeUtils : BiomeUtils
    {
        public static NbtCompound DefinitionList()
        {
            NbtCompound list = new NbtCompound("");
            foreach (Biome biome in Biomes)
            {
                if (string.IsNullOrEmpty(biome.DefinitionName))
                    continue;
                list.Add(
                    new NbtCompound(biome.DefinitionName)
                    {
                        new NbtFloat("downfall", biome.Downfall),
                        new NbtFloat("temperature", -1),
                        new NbtByte("rain", 1),
                    }
                );
            }
            return list;
        }
    }
}
