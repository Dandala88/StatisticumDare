using Core.HttpDynamo;
using StatisticumDare.Models;
using StatisticumDare.Services.Interfaces;

namespace StatisticumDare.Services
{
    public class LudumDareService: ILudumDareService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LudumDareService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        private async Task<UserProfile?> GetUserProfile(string username)
        {
            var userProfile = await HttpDynamo.GetRequestAsync<UserProfile>(_httpClientFactory, $"https://api.ldjam.com/vx/node2/walk/1/users/{username}");

            if (userProfile?.NodeId != null && userProfile.NodeId == 2)
                return null;
            return userProfile;
        }

        private async Task<GameFeed?> GetGameFeed(UserProfile userProfile)
        {
            var gameFeed = await HttpDynamo.GetRequestAsync<GameFeed>(_httpClientFactory, $"https://api.ldjam.com/vx/node/feed/{userProfile.NodeId}/authors/item/game");

            return gameFeed;
        }

        private async Task<GameData?> GetGameData(GameFeed gameFeed)
        {
            if (gameFeed?.Feed == null)
                return null;

            var gameIds = gameFeed.Feed.Select(x => x.Id).ToArray();
            var gameIdsString = string.Join("+", gameIds);
            var gameData = await HttpDynamo.GetRequestAsync<GameData>(_httpClientFactory, $"https://api.ldjam.com/vx/node2/get/{gameIdsString}");

            if (gameData?.Node == null)
                throw new Exception("No game data found for feed");

            foreach (Node node in gameData.Node)
            {
                if(node?.Meta?.Cover != null)
                    node.Meta.Cover = ConvertToStaticImageUrl(node.Meta.Cover, 480, 384);
                if (node?.Path != null)
                {
                    var edition = int.Parse(node.Path.Split('/')[3]);
                    var eventData = await GetEventData(edition);
                    if (eventData != null)
                        node.EventStats = await GetEventStats(eventData.NodeId);
                    node.Edition = edition;
                }
            }

            return gameData;
        }

        public async Task<LudumDareGameData?> GetGameDataByUsername(string username)
        {
            var userProfile = await GetUserProfile(username);

            if(userProfile == null)
                throw new Exception($"User {username} not found");

            var gameFeed = await GetGameFeed(userProfile);

            if(gameFeed == null)
                throw new Exception($"No games found for user: {username}");

            var gameData = await GetGameData(gameFeed);

            if (gameData?.Node != null)
            {
                var gameDataViewModel = new LudumDareGameData
                {
                    Games = new List<Game>()
                };

                foreach (var node in gameData.Node)
                {
                    int categoryCompetitors = 0;
                    if(node?.EventStats?.Stats != null && node?.Subsubtype != null)
                        categoryCompetitors = node.EventStats.Stats.GetCompetitors(node.Subsubtype);

                    if (node == null)
                        continue;

                    gameDataViewModel.Games.Add(new Game
                    {
                        Cover = node.Meta?.Cover,
                        Path = node.Path,
                        Name = node.Name,
                        Format = node.Subsubtype,
                        SubmittedDate = node.NodeTimestamp,
                        Overall = new Category
                        {
                            TotalScore = node.Grade?.Grade01,
                            Result = node.Magic?.Grade01Result,
                            AverageScore = node.Magic?.Grade01Average,
                            Percentile = Category.CalculatePercentile(categoryCompetitors, node.Magic?.Grade01Result)
                        },
                        Fun = new Category
                        {
                            TotalScore = node.Grade?.Grade02,
                            Result = node.Magic?.Grade02Result,
                            AverageScore = node.Magic?.Grade02Average,
                            Percentile = Category.CalculatePercentile(categoryCompetitors, node.Magic?.Grade02Result)
                        },
                        Innovation = new Category
                        {
                            TotalScore = node.Grade?.Grade03,
                            Result = node.Magic?.Grade03Result,
                            AverageScore = node.Magic?.Grade03Average,
                            Percentile = Category.CalculatePercentile(categoryCompetitors, node.Magic?.Grade03Result)
                        },
                        Theme = new Category
                        {
                            TotalScore = node.Grade?.Grade04,
                            Result = node.Magic?.Grade04Result,
                            AverageScore = node.Magic?.Grade04Average,
                            Percentile = Category.CalculatePercentile(categoryCompetitors, node.Magic?.Grade04Result)
                        },
                        Graphics = new Category
                        {
                            TotalScore = node.Grade?.Grade05,
                            Result = node.Magic?.Grade05Result,
                            AverageScore = node.Magic?.Grade05Average,
                            Percentile = Category.CalculatePercentile(categoryCompetitors, node.Magic?.Grade05Result)
                        },
                        Audio = new Category
                        {
                            TotalScore = node.Grade?.Grade06,
                            Result = node.Magic?.Grade06Result,
                            AverageScore = node.Magic?.Grade06Average,
                            Percentile = Category.CalculatePercentile(categoryCompetitors, node.Magic?.Grade06Result)
                        },
                        Humor = new Category
                        {
                            TotalScore = node.Grade?.Grade07,
                            Result = node.Magic?.Grade07Result,
                            AverageScore = node.Magic?.Grade07Average,
                            Percentile = Category.CalculatePercentile(categoryCompetitors, node.Magic?.Grade07Result)
                        },
                        Mood = new Category
                        {
                            TotalScore = node.Grade?.Grade08,
                            Result = node.Magic?.Grade08Result,
                            AverageScore = node.Magic?.Grade08Average,
                            Percentile = Category.CalculatePercentile(categoryCompetitors, node.Magic?.Grade08Result)
                        },
                        Edition = node.Edition,
                        CategoryCompetitors = categoryCompetitors
                    });
                }

                return gameDataViewModel;
            }

            return null;
        }

        private string? ConvertToStaticImageUrl(string imageUrl, int width, int height)
        {
            if (imageUrl == null) return null;
            var baseUrl = "https://static.jam.host/";
            imageUrl = imageUrl.Substring(3, imageUrl.Length - 3);
            var size = $"{width.ToString()}x{height.ToString()}";
            var endUrl = $".{size}.fit.jpg";

            return baseUrl + imageUrl + endUrl;
        }

        private async Task<EventData?> GetEventData(int edition)
        {
            var eventData = await HttpDynamo.GetRequestAsync<EventData>(_httpClientFactory, $"https://api.ldjam.com/vx/node2/walk/1/events/ludum-dare/{edition}/stats?node=&parent=&_superparent=&author=");

            return eventData;
        }

        public async Task<EventStats?> GetEventStats(int editionId)
        {
            var eventStats = await HttpDynamo.GetRequestAsync<EventStats>(_httpClientFactory, $"https://api.ldjam.com/vx/stats/{editionId}");

            return eventStats;
        }
    }
}
