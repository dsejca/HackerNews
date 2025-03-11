

using Core.Models;

namespace Core.Interfaces
{
    public interface IHackerStories
    {
        Task<string> ListOfHackerStories(int numberOfStories);
        Task<HackNewsResponse> HackerStoriesById(long id);
    }
}
