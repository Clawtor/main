
function postModel(title, content, tags, category) {
    var self = this;

    this.title = title;
    this.content = content;
    this.tags = tags;
    this.category = category;

    self.isValid = function () {
        return this.title && this.content;
    }
}
