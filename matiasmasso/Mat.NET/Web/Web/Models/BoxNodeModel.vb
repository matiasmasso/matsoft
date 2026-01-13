
Public Class BoxNodeModel
    Inherits BoxViewModel

    Property Children As List(Of BoxNodeModel)

    Public Sub New()
        MyBase.New()
        _Children = New List(Of BoxNodeModel)
    End Sub

    Shared Shadows Function Factory(title As String, Optional navigateTo As String = "", Optional tag As String = "") As BoxNodeModel
        Dim retval As New BoxNodeModel
        With retval
            .Title = title
            .NavigateTo = navigateTo
            .Tag = tag
        End With
        Return retval
    End Function

    Public Shadows Class Collection
        Inherits List(Of BoxNodeModel)
        Property Lang As DTOLang

        Shared Function Factory(oLang As DTOLang) As BoxNodeModel.Collection
            Dim retval As New BoxNodeModel.Collection
            retval.Lang = oLang
            Return retval
        End Function
    End Class

End Class
