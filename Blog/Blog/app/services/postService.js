//  Should provide data to front end.
function postService($) {
    var self = this;

    self.base = "/api/post/";

    function getLatestPost() {
        $.get(self.base + "getLatest", function (data) {
            return new returnWrapper(data);
        })
        .fail(function (response) {
            console.log("Error on getLatestPost");
            return new returnWrapper(null, true, ErrorTypes.HttpError, response);
        })
    }

    function createPost(postModel) {
        if (postModel == null || postModel.isInvalid()) {
            return new returnWrapper(null, true, ErrorTypes.InvalidParameters);
        }

        $.post(
            self.base + "put",
            postModel,
        function (data) {
            return new returnWrapper(data);
        })
        .fail(function (response) {
            console.log("Error on createPost");
            return new returnWrapper(null, true, ErrorTypes.HttpError, response);
        });
    }

    function updatePost(postModel) {
        if (postModel == null || postModel.isInvalid()) {
            return new returnWrapper(null, true, ErrorTypes.InvalidParameters);
        }

        $.post(
            self.base + "post",
            postModel,
        function (data) {
            return new returnWrapper(data);
        })
        .fail(function (response) {
            console.log("Error on createPost");
            return new returnWrapper(null, true, ErrorTypes.HttpError, response);
        });
    }

    function getPosts() {
        $.get(
            self.base + "GetPosts",
            function (data) {
                return new returnWrapper(data);
            })
        .fail(function (response) {
            console.log("Error on createPost");
            return new returnWrapper(null, true, ErrorTypes.HttpError, response);
        });
    }

    return {
        createPost: createPost,
        getLatestPost: getLatestPost,
        getPosts: getPosts
    };
};