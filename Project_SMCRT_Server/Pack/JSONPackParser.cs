using GHEngine.IO.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack;

public class JSONPackParser : IDataPackParser
{
    // Static fields.
    public const string DEFAULT_NAME = "No Name";
    public const string DEFAULT_DESCRIPTION = "No description";

    public const string FILE_META = "meta.json";
    public const string FILE_SETUP = "setup.json";
    public const string DIR_ENTITIES = "entities";

    public const string KEY_NAME = "name";
    public const string KEY_DESCRIPTION = "description";
    public const string KEY_VERSION = "version";

    public const string KEY_PLANET = "planet";


    // Private fields.
    private readonly JSONEntityDeconstructor _entityDeconstructor = new();


    // Private methods.
    private void AddToPack(IModifiableDataPack pack1, IDataPack pack2)
    {
        foreach (EntityDefinition Definition in pack2.EntityDefinitions)
        {
            pack1.AddEntityDefinition(Definition);
        }

        if (pack2.PlanetDefinition != null)
        {
            if (pack1.PlanetDefinition != null)
            {
                throw new PackContentException("Planet defined in multiple DataPacks, please only have one planet definition.");
            }
            pack1.SetPlanet(pack2.PlanetDefinition);
        }

        if (!pack1.TargetedGameVersion?.Equals(pack2.TargetedGameVersion) ?? false)
        {
            throw new PackContentException("Targeted game version mismatch in DataPacks, all packs must target the same version.");
        }
        pack1.SetVersion(pack2.TargetedGameVersion);
    }

    private void ParseMetaInfo(IModifiableDataPack pack, string rootPath)
    {
        string MetaPath = Path.Combine(rootPath, FILE_META);
        if (!File.Exists(MetaPath))
        {
            throw new PackContentException($"No pack meta info found for DataPack \"{rootPath}\"");
        }
        object? RootObject = new JSONDeserializer().Deserialize(File.ReadAllText(MetaPath));
        if (RootObject is not JSONCompound Compound)
        {
            throw new PackContentException($"Expected root of pack meta to be a compound for \"{MetaPath}\"");
        }

        pack.SetName(Compound.GetVerifiedOrDefault<string>(KEY_NAME, DEFAULT_NAME));
        pack.SetDescription(Compound.GetVerifiedOrDefault<string>(KEY_DESCRIPTION, DEFAULT_DESCRIPTION));

        if (Version.TryParse(Compound.GetVerified<string>(KEY_VERSION), out Version? TargetVersion))
        {
            pack.SetVersion(TargetVersion);
        }
        else
        {
            throw new PackContentException($"Invalid game version in meta \"{MetaPath}\"");
        }
    }

    private void ParseEntityDefinitions(IModifiableDataPack pack, string rootPath)
    {
        string EntitiesPath = Path.Combine(rootPath, DIR_ENTITIES);
        if (!Directory.Exists(EntitiesPath))
        {
            return;
        }

        foreach (string EntityPath in Directory.GetFiles(EntitiesPath, "*.json", SearchOption.AllDirectories))
        {
            object? RootObject = new JSONDeserializer().Deserialize(File.ReadAllText(EntityPath));
            if (RootObject is not JSONCompound Compound)
            {
                throw new PackContentException($"Expected root object of entity definition \"{rootPath}\" to be a compound.");
            }
            pack.AddEntityDefinition(_entityDeconstructor.GetDefinition(Compound));
        }
    }

    private void DeconstructSetup(IModifiableDataPack pack, JSONCompound compound)
    {
        if (compound.Get(KEY_PLANET, out string? PlanetKey))
        {
            try
            {
                pack.SetPlanet(new NamespacedKey(PlanetKey!));
            }
            catch (ArgumentException e)
            {
                throw new PackContentException($"Invalid setup planet key. {e.Message}");
            }
        }
    }

    private void ParseSetup(IModifiableDataPack pack, string rootPath)
    {
        string SetupPath = Path.Combine(rootPath, FILE_SETUP);
        if (!File.Exists(SetupPath))
        {
            return;
        }

        object? RootObject = new JSONDeserializer().Deserialize(File.ReadAllText(SetupPath));
        if (RootObject is not JSONCompound Compound)
        {
            throw new PackContentException("Root of setup must be a compound.");
        }
        DeconstructSetup(pack, Compound);
    }

    private IDataPack ParseSinglePack(string path)
    {
        IModifiableDataPack Pack = new DefaultDataPack();

        try
        {
            ParseMetaInfo(Pack, path);
            ParseSetup(Pack, path);
            ParseEntityDefinitions(Pack, path);
        }
        catch (PackContentException e)
        {
            throw new PackContentException($"Failed to parse pack \"{path}\": {e.Message}");
        }

        return Pack;
    }


    // Inherited methods.
    public IDataPack ParseCombinedPack(string packDirectory)
    {
        IModifiableDataPack ModifiablePack = new DefaultDataPack();

        try
        {
            if (!Directory.Exists(packDirectory))
            {
                return ModifiablePack;
            }

            foreach (string DataPackPath in Directory.GetDirectories(packDirectory))
            {
                AddToPack(ModifiablePack, ParseSinglePack(DataPackPath));
            }
        }
        catch (JSONEntryException e)
        {
            throw new PackContentException($"Invalid JSON entry for DataPack. {e.Message}");
        }
        catch (JSONDeserializeException e)
        {
            throw new PackContentException($"Invalid JSON formatting for DataPack. {e.Message}");
        }

        return ModifiablePack;
    }
}