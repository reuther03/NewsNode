namespace NewsNode.Shared.Abstractions.Kernel.ValueObjects;

public enum PostActionType
{
    //TODO: czy dodac disliked i comment?
    Liked = 0,
    Disliked = 1,
    Reposted = 2,
    Bookmarked = 3
}