@ModelType List(Of Exception)
@Code
End Code

<div>
    Se ha producido un error
    @For each ex In Model
        @<div>
            @ex.Message
        </div>
    Next
</div>