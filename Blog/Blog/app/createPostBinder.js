//  Sits on page, binds page elements to post controller.
function createPostBinder($) {
    var self = this;

    this.postService = new postService($);

    this.title = $("#title");
    this.content = $("#content");
    this.createOrUpdateBtn = $("#createPost");

    this.createPost = function(){
        var createResponse = self.postService.createPost(self.createModel());

        //  Switch text on button to update.
        self.createOrUpdateBtn.html("Update");
        self.createOrUpdateBtn.click(updatePost);
    }

    this.updatePost = function(){
        console.log("UPDATE POST");
    }

    this.createModel = function(){
        var model = new postModel(self.title, self.content, [], "");
        return model;
    }


    this.createOrUpdateBtn.click(createPost);
}

var createPost = new createPostBinder($);