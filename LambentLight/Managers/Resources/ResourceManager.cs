using LambentLight.Config;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace LambentLight.Managers.Resources
{
    /// <summary>
    /// Class that manages the resources of the installer.
    /// </summary>
    public static class ResourceManager
    {
        /// <summary>
        /// The logger for our current class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// All of the resources that are available for installing.
        /// </summary>
        public static List<Resource> Resources = new List<Resource>();

        /// <summary>
        /// Collects all resources required for an install.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static Dictionary<Resource, Version> GetRequirements(Resource resource, Version version, int level = 0)
        {
            // Create a dictionary of resources and versions
            Dictionary<Resource, Version> TempList = new Dictionary<Resource, Version>();

            // If the level is equal or higher to 3, return
            if (level >= 3)
            {
                return TempList;
            }

            // Add our base resource and version
            TempList.Add(resource, version);

            // If we have dependencies
            if (resource.More.Requires != null)
            {
                // For every requirement
                foreach (string Requirement in resource.More.Requires)
                {
                    // Try to find a resource with that name
                    Resource Found = Resources.Where(res => res.Name == Requirement).FirstOrDefault();

                    // If the resource exists and is not on the list
                    if (Found != null && !TempList.ContainsKey(Found))
                    {
                        // Collect their requirements
                        Dictionary<Resource, Version> NewReqs = GetRequirements(Found, Found.More.Versions[0], level + 1);

                        // For every new requirement found
                        foreach (KeyValuePair<Resource, Version> NewReq in NewReqs)
                        {
                            // If is not on the list, add it
                            if (!TempList.ContainsKey(NewReq.Key))
                            {
                                TempList.Add(NewReq.Key, NewReq.Value);
                            }
                        }
                    }
                }
            }

            // Finally, return the list
            return TempList;
        }
        /// <summary>
        /// Adds the specified enumerator of resources into the list.
        /// </summary>
        /// <param name="resources"></param>
        public static void Add(ref List<Resource> tempResources, List<Resource> newResources, Compatibility compatibility, string repo)
        {
            // If one of the lists is null, return
            if (tempResources == null || newResources == null)
            {
                return;
            }

            // Iterate over the list of resources in the new one
            foreach (Resource resource in newResources)
            {
                // Get the first resource matching the name
                Resource found = tempResources.Where(x => x.Name == resource.Name).FirstOrDefault();

                // If the big list of resources contains this one, skip it
                if (found != null)
                {
                    Logger.Warn("Repository {0} already contains resource {1}, skipping...", found.Repo, found.Name);
                    continue;
                }

                // Save the repo and game
                resource.Repo = repo;
                resource.Compatibility = compatibility;
                // Otherwise, add it
                tempResources.Add(resource);
            }
        }
        /// <summary>
        /// Refreshes the list of resources.
        /// </summary>
        public static void Refresh()
        {
            // Create a new list of resources
            List<Resource> tempResources = new List<Resource>();
            // Get the readable name of the game
            string game = Program.Config.Game == Game.GrandTheftAutoV ? "gtav" : "rdr2";

            // For each resource repository
            foreach (string repo in Program.Config.Repos)
            {
                // Get the lists of resources
                List<Resource> outputGeneric = Downloader.DownloadJSON<List<Resource>>($"{repo}/resources/common.json");
                List<Resource> outputGame = Downloader.DownloadJSON<List<Resource>>($"{repo}/resources/{game}.json");

                // And add them
                Add(ref tempResources, outputGeneric, Compatibility.Common, repo);
                Add(ref tempResources, outputGame, (Compatibility)Program.Config.Game, repo);
            }

            // Store the resources in alphabetical order
            Resources = tempResources.OrderBy(x => x.Name).ToList();
            // Log what we have just done
            Logger.Debug("The list of resources has been updated");
        }
    }
}