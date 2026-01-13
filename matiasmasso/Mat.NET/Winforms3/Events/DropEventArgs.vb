Public Class DropEventArgs
    Inherits EventArgs

    Public Property DocFiles As List(Of DTODocFile)

    Public Sub New(oDocFiles As List(Of DTODocFile))
        MyBase.New()
        _DocFiles = oDocFiles
    End Sub

    Public Sub New(oDocFile As DTODocFile)
        MyBase.New()
        _DocFiles = New List(Of DTODocFile)
        _DocFiles.Add(oDocFile)
    End Sub
End Class
