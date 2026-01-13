@ModelType List(Of Exception)


<div>
    <img src="~/Media/Img/Ico/warn.gif" />&nbsp;Se ha producido un error
    @If Model IsNot Nothing Then
        For Each ex In Model
            @<div>
                @ex.Message
            </div>
        Next
    End If
</div>