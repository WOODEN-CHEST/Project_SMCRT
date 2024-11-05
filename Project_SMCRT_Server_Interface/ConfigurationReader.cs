using GHEngine.IO.JSON;
using Project_SMCRT_Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server_Interface;

public class ConfigurationReader
{
    // Static fields.
    public const string DEFAULT_IP_ADDRESS = "127.0.0.1";
    public const short DEFAULT_PORT = 25565;
    public const string DEFAULT_NAME = "Unnamed Server";
    public const string DEFAULT_DESCRIPTION = "No Description";
    public const short DEFAULT_MAX_PLAYERS = 10;

    public const string KEY_ADDRESS = "address";
    public const string KEY_PORT = "port";
    public const string KEY_NAME = "name";
    public const string KEY_DESCRIPTION = "description";
    public const string KEY_MAX_PLAYERS = "max_players";


    // Private methods.
    private SMCRTServerOptions GetDefaultConfiguration()
    {
        return new()
        {
            ServerAddress = IPAddress.Parse(DEFAULT_IP_ADDRESS),
            ServerPort = DEFAULT_PORT,
            ServerName = DEFAULT_NAME,
            Description = DEFAULT_DESCRIPTION,
            MaxPlayers = DEFAULT_MAX_PLAYERS
        };
    }

    private SMCRTServerOptions ParseCompound(JSONCompound compound)
    {
        IPAddress ServerAddress;
        try
        {
            ServerAddress = IPAddress.Parse(compound.GetVerifiedOrDefault(KEY_ADDRESS, DEFAULT_IP_ADDRESS));
        }
        catch (FormatException e)
        {
            throw new ConfigurationParseException("Invalid IP address");
        }

        short Port = (short)compound.GetVerifiedOrDefault<long>(KEY_PORT, DEFAULT_PORT);
        string Name = compound.GetVerifiedOrDefault(KEY_NAME, DEFAULT_NAME);
        string Description = compound.GetVerifiedOrDefault(KEY_DESCRIPTION, DEFAULT_DESCRIPTION);
        int MaxPlayers = (int)compound.GetVerifiedOrDefault<long>(KEY_MAX_PLAYERS, DEFAULT_MAX_PLAYERS);

        return new()
        {
            ServerAddress = ServerAddress,
            ServerPort = Port,
            ServerName = Name,
            Description = Description,
            MaxPlayers = MaxPlayers
        };
    }


    // Methods.
    public SMCRTServerOptions ReadConfig(string path)
    {
        if (!File.Exists(path))
        {
            return GetDefaultConfiguration();
        }

        try
        {
            object? ParsedData = new JSONDeserializer().Deserialize(File.ReadAllText(path));
            if (ParsedData is not JSONCompound Compound)
            {
                throw new ConfigurationParseException("Expected JSON compound as start of configuration.");
            }
            return ParseCompound(Compound);

        }
        catch (JSONDeserializeException e)
        {
            throw new ConfigurationParseException($"Malformed configuration file: {e}");
        }
        catch (JSONEntryException e)
        {
            throw new ConfigurationParseException($"Invalid entries for configuration file: {e}");
        }
    }
}