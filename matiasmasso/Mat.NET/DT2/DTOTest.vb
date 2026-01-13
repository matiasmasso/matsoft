<DataContract>
Public Class DTOTest
    Private _Guid As Guid

    <DataMember> Property Nom As String

    <DataMember()> ReadOnly Property Guid As Guid
        Get
            Return _Guid
        End Get
    End Property

    Public Sub New()
        _Guid = Guid.NewGuid
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New()
        _Guid = oGuid
    End Sub

    Public Function FullNom() As String
        Dim retval As String = String.Format("{0} {1}", _Nom, _Guid.ToString)
        Return retval
    End Function

End Class
