Public Class MatListItem
    Private mDisplayMember As Object
    Private mValueMember As Object

    Public Sub New(ByVal oValueMember As Object, ByVal oDisplayMember As Object)
        MyBase.new()
        mValueMember = oValueMember
        mDisplayMember = oDisplayMember
    End Sub

    Public Property Value() As Object
        Get
            Return mValueMember
        End Get
        Set(ByVal value As Object)
            mValueMember = value
        End Set
    End Property

    Public Property DisplayMember() As Object
        Get
            Return mDisplayMember
        End Get
        Set(ByVal value As Object)
            mDisplayMember = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return mDisplayMember.ToString()
    End Function
End Class
