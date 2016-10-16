function returnWrapper(data, hasError, errorType, additionalData) {
    this.data = data;
    this.hasError = hasError || false;
    this.error = errorType || null;
    this.additionalData = additionalData || null;
}

var ErrorTypes = {
    InvalidParameters: "Input parameters were incorrect",
    HttpError: "The http call failed."
}