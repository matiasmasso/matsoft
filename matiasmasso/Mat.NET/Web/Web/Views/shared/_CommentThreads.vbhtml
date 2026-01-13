@ModelType DTOPostComment.TreeModel

@For Each item In Model.Items
    @Html.Partial("_CommentThread", item)
Next
