Public Class MatEventArgs
    Inherits System.EventArgs

    Public Property Argument As Object

    Public Sub New(oArgument As Object)
        MyBase.New()
        _Argument = oArgument
    End Sub

    Public Sub New()
        MyBase.New
    End Sub

    Shared Shadows Function Empty() As MatEventArgs
        Dim retval As New MatEventArgs(Nothing)
        Return retval
    End Function

End Class
