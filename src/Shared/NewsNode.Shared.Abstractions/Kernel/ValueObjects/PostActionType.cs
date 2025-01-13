namespace NewsNode.Shared.Abstractions.Kernel.ValueObjects;

public enum PostActionType
{
    //TODO: czy dodac disliked i comment?
    Disliked = 0,
    ShowLess = 1,
    NotInterested = 2,
    Liked = 3,
    Reposted = 4,
    Bookmarked = 5,
    Commented = 6,
    Created = 7
}