public interface IChannelResolver
{
    ChannelResult ResolveChannels(IEnumerable<PlanetaryActivation> activations);
}