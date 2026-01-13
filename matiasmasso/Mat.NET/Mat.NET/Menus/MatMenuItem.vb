Public Class MatMenuItem
    Inherits ToolStripMenuItem

    Public Property CustomObject As Object

    Public Sub New(sCaption As String, oCustomObject As Object) 'ByVal func As MenuItemClicked)
        MyBase.New(sCaption)
        _CustomObject = oCustomObject
    End Sub

End Class
