function allPostsController($) {
    var self = this;

    this.postContainer = $("#postContainer");
    this.postService = new postService($);

    function getPosts() {
        var posts = self.postService.getPosts();
        console.log(posts);
    }
    getPosts();
}

var allPosts = allPostsController($);