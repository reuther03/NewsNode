namespace NewsNode.Shared.Abstractions.Kernel.ValueObjects;

public enum PostActionType
{
    //TODO: czy dodac disliked i comment?
    Liked = 0,
    Disliked = 1,
    NotInterested = 2,
    Reposted = 4,
    Bookmarked = 5,
    Commented = 6
}